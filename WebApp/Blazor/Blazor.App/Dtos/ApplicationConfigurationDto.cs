using System.Collections.Generic;
using System.Text.Json;

namespace Blazor.App.Dtos
{
    public class ApplicationConfigurationDto
    {
        public AuthDto Auth { get; set; }

        public class AuthDto
        {
            public object Policies { get; set; }

            public object GrantedPolicies { get; set; }
        }

        public class CurrentUser
        {
            public bool IsAuthenticated { get; set; }
        }
    }


}
