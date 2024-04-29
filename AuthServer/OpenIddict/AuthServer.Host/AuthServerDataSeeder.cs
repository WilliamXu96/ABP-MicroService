using Microsoft.Extensions.Configuration;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace AuthServer
{
    public class AuthServerDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IConfiguration _configuration;
        private readonly IAbpApplicationManager _applicationManager;
        private readonly IOpenIddictScopeManager _scopeManager;
        private readonly IPermissionDataSeeder _permissionDataSeeder;

        public AuthServerDataSeeder(
            IConfiguration configuration,
            IAbpApplicationManager applicationManager,
            IOpenIddictScopeManager scopeManager,
            IPermissionDataSeeder permissionDataSeeder)
        {
            _configuration = configuration;
            _applicationManager = applicationManager;
            _scopeManager = scopeManager;
            _permissionDataSeeder = permissionDataSeeder;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await CreateApiScopesAsync();
            await CreateApplicationsAsync();
        }

        private async Task CreateApiScopesAsync()
        {
            await CreateApiScopeAsync("BaseService");
            await CreateApiScopeAsync("InternalGateway");
            await CreateApiScopeAsync("WebAppGateway");
            await CreateApiScopeAsync("BusinessService");
        }

        private async Task CreateApiScopeAsync(string name)
        {
            if (await _scopeManager.FindByNameAsync(name) == null)
            {
                await _scopeManager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    Name = name,
                    DisplayName = name + " API",
                    Resources =
                {
                    name
                }
                });
            }
        }

        private async Task CreateApplicationsAsync()
        {
            var commonScopes = new List<string>
            {
                OpenIddictConstants.Permissions.Scopes.Address,
                OpenIddictConstants.Permissions.Scopes.Email,
                OpenIddictConstants.Permissions.Scopes.Phone,
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Scopes.Roles
            };

            var configurationSection = _configuration.GetSection("OpenIddict:Applications");

            //Vue Client
            var vueClientId = configurationSection["Web_App:ClientId"];
            if (!vueClientId.IsNullOrWhiteSpace())
            {
                var VueClientRootUrl = configurationSection["Web_App:RootUrl"]?.TrimEnd('/');
                var WebAppScopes = new List<string>()
                {
                    "BaseService",
                    "WebAppGateway",
                    "BusinessService"
                };
                WebAppScopes.AddRange(commonScopes);
                await CreateApplicationAsync(
                    name: vueClientId,
                    type: OpenIddictConstants.ClientTypes.Public,
                    consentType: OpenIddictConstants.ConsentTypes.Implicit,
                    displayName: "Vue Application",
                    secret: null,
                    grantTypes: new List<string>
                    {
                        OpenIddictConstants.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.GrantTypes.Password,
                        OpenIddictConstants.GrantTypes.ClientCredentials,
                        OpenIddictConstants.GrantTypes.RefreshToken
                    },
                    scopes: WebAppScopes,
                    redirectUri: VueClientRootUrl,
                    clientUri: VueClientRootUrl,
                    postLogoutRedirectUri: VueClientRootUrl
                );
            }

            // Blazor Client
            var blazorClientId = configurationSection["Blazor:ClientId"];
            if (!blazorClientId.IsNullOrWhiteSpace())
            {
                var blazorRootUrl = configurationSection["Blazor:RootUrl"].TrimEnd('/');
                var BlazorScopes = new List<string>()
                {
                    "BaseService",
                    "WebAppGateway",
                    "BusinessService"
                };
                BlazorScopes.AddRange(commonScopes);
                await CreateApplicationAsync(
                    name: blazorClientId,
                    type: OpenIddictConstants.ClientTypes.Public,
                    consentType: OpenIddictConstants.ConsentTypes.Implicit,
                    displayName: "Blazor Application",
                    secret: null,
                    grantTypes: new List<string>
                    {
                        OpenIddictConstants.GrantTypes.AuthorizationCode,
                    },
                    scopes: BlazorScopes,
                    redirectUri: $"{blazorRootUrl}/authentication/login-callback",
                    clientUri: blazorRootUrl,
                    postLogoutRedirectUri: $"{blazorRootUrl}/authentication/logout-callback"
                );
            }

            // Business Client
            var businessClientId = configurationSection["Business:ClientId"];
            if (!businessClientId.IsNullOrWhiteSpace())
            {
                var BusinessScopes = new List<string>()
                {
                    "BaseService",
                    "InternalGateway"
                };
                await CreateApplicationAsync(
                    name: businessClientId,
                    type: OpenIddictConstants.ClientTypes.Confidential,
                    consentType: OpenIddictConstants.ConsentTypes.Implicit,
                    displayName: "Business Application",
                    secret: "1q2w3e*",
                    grantTypes: new List<string>
                    {
                        OpenIddictConstants.GrantTypes.ClientCredentials,
                    },
                    scopes: BusinessScopes
                );
            }
        }

        private async Task CreateApplicationAsync(
        [NotNull] string name,
        [NotNull] string type,
        [NotNull] string consentType,
        string displayName,
        string secret,
        List<string> grantTypes,
        List<string> scopes,
        string clientUri = null,
        string redirectUri = null,
        string postLogoutRedirectUri = null,
        List<string> permissions = null)
        {
            if (!string.IsNullOrEmpty(secret) && string.Equals(type, OpenIddictConstants.ClientTypes.Public, StringComparison.OrdinalIgnoreCase))
            {
                throw new BusinessException("NoClientSecretCanBeSetForPublicApplications");
            }

            if (string.IsNullOrEmpty(secret) && string.Equals(type, OpenIddictConstants.ClientTypes.Confidential, StringComparison.OrdinalIgnoreCase))
            {
                throw new BusinessException("TheClientSecretIsRequiredForConfidentialApplications");
            }
            var client = await _applicationManager.FindByClientIdAsync(name);
            if (!string.IsNullOrEmpty(name) && await _applicationManager.FindByClientIdAsync(name) != null)
            {
                return;
            }

            //var client = await _applicationManager.FindByClientIdAsync(name);
            if (client == null)
            {
                var application = new AbpApplicationDescriptor
                {
                    ClientId = name,
                    ClientType = type,
                    ClientSecret = secret,
                    ConsentType = consentType,
                    DisplayName = displayName,
                    ClientUri = clientUri,
                };

                Check.NotNullOrEmpty(grantTypes, nameof(grantTypes));
                Check.NotNullOrEmpty(scopes, nameof(scopes));

                if (new[] { OpenIddictConstants.GrantTypes.AuthorizationCode, OpenIddictConstants.GrantTypes.Implicit }.All(grantTypes.Contains))
                {
                    application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.CodeIdToken);

                    if (string.Equals(type, OpenIddictConstants.ClientTypes.Public, StringComparison.OrdinalIgnoreCase))
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.CodeIdTokenToken);
                        application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.CodeToken);
                    }
                }

                if (!redirectUri.IsNullOrWhiteSpace() || !postLogoutRedirectUri.IsNullOrWhiteSpace())
                {
                    application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Logout);
                }

                foreach (var grantType in grantTypes)
                {
                    if (grantType == OpenIddictConstants.GrantTypes.AuthorizationCode)
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode);
                        application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Code);
                    }

                    if (grantType == OpenIddictConstants.GrantTypes.AuthorizationCode || grantType == OpenIddictConstants.GrantTypes.Implicit)
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Authorization);
                    }

                    if (grantType == OpenIddictConstants.GrantTypes.AuthorizationCode ||
                        grantType == OpenIddictConstants.GrantTypes.ClientCredentials ||
                        grantType == OpenIddictConstants.GrantTypes.Password ||
                        grantType == OpenIddictConstants.GrantTypes.RefreshToken ||
                        grantType == OpenIddictConstants.GrantTypes.DeviceCode)
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
                        application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Revocation);
                        application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Introspection);
                    }

                    if (grantType == OpenIddictConstants.GrantTypes.ClientCredentials)
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.ClientCredentials);
                    }

                    if (grantType == OpenIddictConstants.GrantTypes.Implicit)
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.Implicit);
                    }

                    if (grantType == OpenIddictConstants.GrantTypes.Password)
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.Password);
                    }

                    if (grantType == OpenIddictConstants.GrantTypes.RefreshToken)
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.RefreshToken);
                    }

                    if (grantType == OpenIddictConstants.GrantTypes.DeviceCode)
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.GrantTypes.DeviceCode);
                        application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Device);
                    }

                    if (grantType == OpenIddictConstants.GrantTypes.Implicit)
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.IdToken);
                        if (string.Equals(type, OpenIddictConstants.ClientTypes.Public, StringComparison.OrdinalIgnoreCase))
                        {
                            application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.IdTokenToken);
                            application.Permissions.Add(OpenIddictConstants.Permissions.ResponseTypes.Token);
                        }
                    }
                }

                var buildInScopes = new[]
                {
                OpenIddictConstants.Permissions.Scopes.Address,
                OpenIddictConstants.Permissions.Scopes.Email,
                OpenIddictConstants.Permissions.Scopes.Phone,
                OpenIddictConstants.Permissions.Scopes.Profile,
                OpenIddictConstants.Permissions.Scopes.Roles
            };

                foreach (var scope in scopes)
                {
                    if (buildInScopes.Contains(scope))
                    {
                        application.Permissions.Add(scope);
                    }
                    else
                    {
                        application.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.Scope + scope);
                    }
                }

                if (redirectUri != null)
                {
                    if (!redirectUri.IsNullOrEmpty())
                    {
                        if (!Uri.TryCreate(redirectUri, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
                        {
                            throw new BusinessException("InvalidRedirectUri", redirectUri);
                        }

                        if (application.RedirectUris.All(x => x != uri))
                        {
                            application.RedirectUris.Add(uri);
                        }
                    }
                }

                if (postLogoutRedirectUri != null)
                {
                    if (!postLogoutRedirectUri.IsNullOrEmpty())
                    {
                        if (!Uri.TryCreate(postLogoutRedirectUri, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
                        {
                            throw new BusinessException("InvalidPostLogoutRedirectUri", postLogoutRedirectUri);
                        }

                        if (application.PostLogoutRedirectUris.All(x => x != uri))
                        {
                            application.PostLogoutRedirectUris.Add(uri);
                        }
                    }
                }

                if (permissions != null)
                {
                    await _permissionDataSeeder.SeedAsync(
                        ClientPermissionValueProvider.ProviderName,
                        name,
                        permissions,
                        null
                    );
                }

                await _applicationManager.CreateAsync(application);
            }
        }
    }
}
