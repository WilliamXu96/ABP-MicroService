/*
 Navicat Premium Data Transfer

 Source Server         : .
 Source Server Type    : SQL Server
 Source Server Version : 15002000
 Source Host           : .:1433
 Source Catalog        : ABP
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000
 File Encoding         : 65001

 Date: 19/09/2022 13:35:48
*/


-- ----------------------------
-- 如果执行该sql，需要提前创建数据库
-- ----------------------------


-- ----------------------------
-- Table structure for AbpAuditLogActions
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpAuditLogActions]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpAuditLogActions]
GO

CREATE TABLE [dbo].[AbpAuditLogActions] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [AuditLogId] uniqueidentifier  NOT NULL,
  [ServiceName] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [MethodName] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NULL,
  [Parameters] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NULL,
  [ExecutionTime] datetime2(7)  NOT NULL,
  [ExecutionDuration] int  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpAuditLogActions] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpAuditLogs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpAuditLogs]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpAuditLogs]
GO

CREATE TABLE [dbo].[AbpAuditLogs] (
  [Id] uniqueidentifier  NOT NULL,
  [ApplicationName] nvarchar(96) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserId] uniqueidentifier  NULL,
  [UserName] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [TenantId] uniqueidentifier  NULL,
  [TenantName] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [ImpersonatorUserId] uniqueidentifier  NULL,
  [ImpersonatorUserName] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [ImpersonatorTenantId] uniqueidentifier  NULL,
  [ImpersonatorTenantName] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [ExecutionTime] datetime2(7)  NOT NULL,
  [ExecutionDuration] int  NOT NULL,
  [ClientIpAddress] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClientName] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClientId] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [CorrelationId] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [BrowserInfo] nvarchar(512) COLLATE Chinese_PRC_CI_AS  NULL,
  [HttpMethod] nvarchar(16) COLLATE Chinese_PRC_CI_AS  NULL,
  [Url] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [Exceptions] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [Comments] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [HttpStatusCode] int  NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpAuditLogs] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpClaimTypes
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpClaimTypes]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpClaimTypes]
GO

CREATE TABLE [dbo].[AbpClaimTypes] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Required] bit  NOT NULL,
  [IsStatic] bit  NOT NULL,
  [Regex] nvarchar(512) COLLATE Chinese_PRC_CI_AS  NULL,
  [RegexDescription] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [ValueType] int  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpClaimTypes] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpEntityChanges
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpEntityChanges]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpEntityChanges]
GO

CREATE TABLE [dbo].[AbpEntityChanges] (
  [Id] uniqueidentifier  NOT NULL,
  [AuditLogId] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [ChangeTime] datetime2(7)  NOT NULL,
  [ChangeType] tinyint  NOT NULL,
  [EntityTenantId] uniqueidentifier  NULL,
  [EntityId] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [EntityTypeFullName] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpEntityChanges] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpEntityPropertyChanges
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpEntityPropertyChanges]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpEntityPropertyChanges]
GO

CREATE TABLE [dbo].[AbpEntityPropertyChanges] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [EntityChangeId] uniqueidentifier  NOT NULL,
  [NewValue] nvarchar(512) COLLATE Chinese_PRC_CI_AS  NULL,
  [OriginalValue] nvarchar(512) COLLATE Chinese_PRC_CI_AS  NULL,
  [PropertyName] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [PropertyTypeFullName] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[AbpEntityPropertyChanges] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpLinkUsers
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpLinkUsers]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpLinkUsers]
GO

CREATE TABLE [dbo].[AbpLinkUsers] (
  [Id] uniqueidentifier  NOT NULL,
  [SourceUserId] uniqueidentifier  NOT NULL,
  [SourceTenantId] uniqueidentifier  NULL,
  [TargetUserId] uniqueidentifier  NOT NULL,
  [TargetTenantId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[AbpLinkUsers] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpOrganizationUnitRoles
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpOrganizationUnitRoles]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpOrganizationUnitRoles]
GO

CREATE TABLE [dbo].[AbpOrganizationUnitRoles] (
  [RoleId] uniqueidentifier  NOT NULL,
  [OrganizationUnitId] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[AbpOrganizationUnitRoles] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpOrganizationUnits
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpOrganizationUnits]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpOrganizationUnits]
GO

CREATE TABLE [dbo].[AbpOrganizationUnits] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [ParentId] uniqueidentifier  NULL,
  [Code] nvarchar(95) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [DisplayName] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [DeleterId] uniqueidentifier  NULL,
  [DeletionTime] datetime2(7)  NULL
)
GO

ALTER TABLE [dbo].[AbpOrganizationUnits] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpPermissionGrants
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpPermissionGrants]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpPermissionGrants]
GO

CREATE TABLE [dbo].[AbpPermissionGrants] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [Name] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ProviderName] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ProviderKey] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[AbpPermissionGrants] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpRoleClaims
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpRoleClaims]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpRoleClaims]
GO

CREATE TABLE [dbo].[AbpRoleClaims] (
  [Id] uniqueidentifier  NOT NULL,
  [RoleId] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [ClaimType] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ClaimValue] nvarchar(1024) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpRoleClaims] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpRoles
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpRoles]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpRoles]
GO

CREATE TABLE [dbo].[AbpRoles] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [Name] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [NormalizedName] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [IsDefault] bit  NOT NULL,
  [IsStatic] bit  NOT NULL,
  [IsPublic] bit  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpRoles] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpSecurityLogs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpSecurityLogs]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpSecurityLogs]
GO

CREATE TABLE [dbo].[AbpSecurityLogs] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [ApplicationName] nvarchar(96) COLLATE Chinese_PRC_CI_AS  NULL,
  [Identity] nvarchar(96) COLLATE Chinese_PRC_CI_AS  NULL,
  [Action] nvarchar(96) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserId] uniqueidentifier  NULL,
  [UserName] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [TenantName] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClientId] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [CorrelationId] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClientIpAddress] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [BrowserInfo] nvarchar(512) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpSecurityLogs] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpSettings
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpSettings]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpSettings]
GO

CREATE TABLE [dbo].[AbpSettings] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(2048) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ProviderName] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [ProviderKey] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpSettings] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpTenantConnectionStrings
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpTenantConnectionStrings]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpTenantConnectionStrings]
GO

CREATE TABLE [dbo].[AbpTenantConnectionStrings] (
  [TenantId] uniqueidentifier  NOT NULL,
  [Name] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(1024) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[AbpTenantConnectionStrings] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpTenants
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpTenants]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpTenants]
GO

CREATE TABLE [dbo].[AbpTenants] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [DeleterId] uniqueidentifier  NULL,
  [DeletionTime] datetime2(7)  NULL
)
GO

ALTER TABLE [dbo].[AbpTenants] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpUserClaims
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpUserClaims]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpUserClaims]
GO

CREATE TABLE [dbo].[AbpUserClaims] (
  [Id] uniqueidentifier  NOT NULL,
  [UserId] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [ClaimType] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ClaimValue] nvarchar(1024) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpUserClaims] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpUserLogins
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpUserLogins]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpUserLogins]
GO

CREATE TABLE [dbo].[AbpUserLogins] (
  [UserId] uniqueidentifier  NOT NULL,
  [LoginProvider] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [ProviderKey] nvarchar(196) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ProviderDisplayName] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpUserLogins] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpUserOrganizationUnits
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpUserOrganizationUnits]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpUserOrganizationUnits]
GO

CREATE TABLE [dbo].[AbpUserOrganizationUnits] (
  [UserId] uniqueidentifier  NOT NULL,
  [OrganizationUnitId] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[AbpUserOrganizationUnits] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpUserRoles
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpUserRoles]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpUserRoles]
GO

CREATE TABLE [dbo].[AbpUserRoles] (
  [UserId] uniqueidentifier  NOT NULL,
  [RoleId] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[AbpUserRoles] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpUsers
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpUsers]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpUsers]
GO

CREATE TABLE [dbo].[AbpUsers] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [UserName] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [NormalizedUserName] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Name] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [Surname] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NULL,
  [Email] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [NormalizedEmail] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [EmailConfirmed] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [PasswordHash] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [SecurityStamp] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [IsExternal] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [PhoneNumber] nvarchar(16) COLLATE Chinese_PRC_CI_AS  NULL,
  [PhoneNumberConfirmed] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [IsActive] bit  NOT NULL,
  [TwoFactorEnabled] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [LockoutEnd] datetimeoffset(7)  NULL,
  [LockoutEnabled] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [AccessFailedCount] int DEFAULT ((0)) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [DeleterId] uniqueidentifier  NULL,
  [DeletionTime] datetime2(7)  NULL
)
GO

ALTER TABLE [dbo].[AbpUsers] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for AbpUserTokens
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AbpUserTokens]') AND type IN ('U'))
	DROP TABLE [dbo].[AbpUserTokens]
GO

CREATE TABLE [dbo].[AbpUserTokens] (
  [UserId] uniqueidentifier  NOT NULL,
  [LoginProvider] nvarchar(64) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Name] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [Value] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AbpUserTokens] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_dict
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_dict]') AND type IN ('U'))
	DROP TABLE [dbo].[base_dict]
GO

CREATE TABLE [dbo].[base_dict] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [Name] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Description] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_dict] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_dict_details
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_dict_details]') AND type IN ('U'))
	DROP TABLE [dbo].[base_dict_details]
GO

CREATE TABLE [dbo].[base_dict_details] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [Pid] uniqueidentifier  NOT NULL,
  [Label] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Sort] smallint  NOT NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_dict_details] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_jobs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_jobs]') AND type IN ('U'))
	DROP TABLE [dbo].[base_jobs]
GO

CREATE TABLE [dbo].[base_jobs] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [Name] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Enabled] bit  NOT NULL,
  [Sort] int  NOT NULL,
  [Description] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_jobs] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_menu
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_menu]') AND type IN ('U'))
	DROP TABLE [dbo].[base_menu]
GO

CREATE TABLE [dbo].[base_menu] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [FormId] uniqueidentifier  NULL,
  [Pid] uniqueidentifier  NULL,
  [CategoryId] int  NOT NULL,
  [Name] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Sort] int  NOT NULL,
  [Path] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [Permission] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [Icon] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL,
  [Component] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [AlwaysShow] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [Hidden] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [Label] nvarchar(128) COLLATE Chinese_PRC_CI_AS DEFAULT (N'') NOT NULL
)
GO

ALTER TABLE [dbo].[base_menu] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_orgs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_orgs]') AND type IN ('U'))
	DROP TABLE [dbo].[base_orgs]
GO

CREATE TABLE [dbo].[base_orgs] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [CategoryId] smallint  NOT NULL,
  [Pid] uniqueidentifier  NULL,
  [Name] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FullName] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Sort] int  NOT NULL,
  [Leaf] bit  NOT NULL,
  [CascadeId] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [Enabled] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_orgs] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_role_menu
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_role_menu]') AND type IN ('U'))
	DROP TABLE [dbo].[base_role_menu]
GO

CREATE TABLE [dbo].[base_role_menu] (
  [RoleId] uniqueidentifier  NOT NULL,
  [MenuId] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_role_menu] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_user_jobs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_user_jobs]') AND type IN ('U'))
	DROP TABLE [dbo].[base_user_jobs]
GO

CREATE TABLE [dbo].[base_user_jobs] (
  [UserId] uniqueidentifier  NOT NULL,
  [JobId] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_user_jobs] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_user_orgs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_user_orgs]') AND type IN ('U'))
	DROP TABLE [dbo].[base_user_orgs]
GO

CREATE TABLE [dbo].[base_user_orgs] (
  [UserId] uniqueidentifier  NOT NULL,
  [OrganizationId] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_user_orgs] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerApiResourceClaims
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerApiResourceClaims]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerApiResourceClaims]
GO

CREATE TABLE [dbo].[IdentityServerApiResourceClaims] (
  [Type] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ApiResourceId] uniqueidentifier  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerApiResourceClaims] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerApiResourceProperties
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerApiResourceProperties]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerApiResourceProperties]
GO

CREATE TABLE [dbo].[IdentityServerApiResourceProperties] (
  [ApiResourceId] uniqueidentifier  NOT NULL,
  [Key] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerApiResourceProperties] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerApiResources
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerApiResources]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerApiResources]
GO

CREATE TABLE [dbo].[IdentityServerApiResources] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [DisplayName] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(1000) COLLATE Chinese_PRC_CI_AS  NULL,
  [Enabled] bit  NOT NULL,
  [AllowedAccessTokenSigningAlgorithms] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ShowInDiscoveryDocument] bit  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [DeleterId] uniqueidentifier  NULL,
  [DeletionTime] datetime2(7)  NULL
)
GO

ALTER TABLE [dbo].[IdentityServerApiResources] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerApiResourceScopes
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerApiResourceScopes]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerApiResourceScopes]
GO

CREATE TABLE [dbo].[IdentityServerApiResourceScopes] (
  [ApiResourceId] uniqueidentifier  NOT NULL,
  [Scope] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerApiResourceScopes] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerApiResourceSecrets
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerApiResourceSecrets]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerApiResourceSecrets]
GO

CREATE TABLE [dbo].[IdentityServerApiResourceSecrets] (
  [Type] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(4000) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ApiResourceId] uniqueidentifier  NOT NULL,
  [Description] nvarchar(1000) COLLATE Chinese_PRC_CI_AS  NULL,
  [Expiration] datetime2(7)  NULL
)
GO

ALTER TABLE [dbo].[IdentityServerApiResourceSecrets] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerApiScopeClaims
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerApiScopeClaims]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerApiScopeClaims]
GO

CREATE TABLE [dbo].[IdentityServerApiScopeClaims] (
  [Type] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ApiScopeId] uniqueidentifier  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerApiScopeClaims] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerApiScopeProperties
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerApiScopeProperties]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerApiScopeProperties]
GO

CREATE TABLE [dbo].[IdentityServerApiScopeProperties] (
  [ApiScopeId] uniqueidentifier  NOT NULL,
  [Key] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerApiScopeProperties] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerApiScopes
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerApiScopes]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerApiScopes]
GO

CREATE TABLE [dbo].[IdentityServerApiScopes] (
  [Id] uniqueidentifier  NOT NULL,
  [Enabled] bit  NOT NULL,
  [Name] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [DisplayName] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(1000) COLLATE Chinese_PRC_CI_AS  NULL,
  [Required] bit  NOT NULL,
  [Emphasize] bit  NOT NULL,
  [ShowInDiscoveryDocument] bit  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [DeleterId] uniqueidentifier  NULL,
  [DeletionTime] datetime2(7)  NULL
)
GO

ALTER TABLE [dbo].[IdentityServerApiScopes] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerClientClaims
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerClientClaims]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerClientClaims]
GO

CREATE TABLE [dbo].[IdentityServerClientClaims] (
  [ClientId] uniqueidentifier  NOT NULL,
  [Type] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerClientClaims] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerClientCorsOrigins
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerClientCorsOrigins]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerClientCorsOrigins]
GO

CREATE TABLE [dbo].[IdentityServerClientCorsOrigins] (
  [ClientId] uniqueidentifier  NOT NULL,
  [Origin] nvarchar(150) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerClientCorsOrigins] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerClientGrantTypes
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerClientGrantTypes]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerClientGrantTypes]
GO

CREATE TABLE [dbo].[IdentityServerClientGrantTypes] (
  [ClientId] uniqueidentifier  NOT NULL,
  [GrantType] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerClientGrantTypes] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerClientIdPRestrictions
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerClientIdPRestrictions]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerClientIdPRestrictions]
GO

CREATE TABLE [dbo].[IdentityServerClientIdPRestrictions] (
  [ClientId] uniqueidentifier  NOT NULL,
  [Provider] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerClientIdPRestrictions] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerClientPostLogoutRedirectUris
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerClientPostLogoutRedirectUris]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerClientPostLogoutRedirectUris]
GO

CREATE TABLE [dbo].[IdentityServerClientPostLogoutRedirectUris] (
  [ClientId] uniqueidentifier  NOT NULL,
  [PostLogoutRedirectUri] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerClientPostLogoutRedirectUris] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerClientProperties
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerClientProperties]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerClientProperties]
GO

CREATE TABLE [dbo].[IdentityServerClientProperties] (
  [ClientId] uniqueidentifier  NOT NULL,
  [Key] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerClientProperties] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerClientRedirectUris
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerClientRedirectUris]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerClientRedirectUris]
GO

CREATE TABLE [dbo].[IdentityServerClientRedirectUris] (
  [ClientId] uniqueidentifier  NOT NULL,
  [RedirectUri] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerClientRedirectUris] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerClients
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerClients]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerClients]
GO

CREATE TABLE [dbo].[IdentityServerClients] (
  [Id] uniqueidentifier  NOT NULL,
  [ClientId] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ClientName] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(1000) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClientUri] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NULL,
  [LogoUri] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NULL,
  [Enabled] bit  NOT NULL,
  [ProtocolType] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [RequireClientSecret] bit  NOT NULL,
  [RequireConsent] bit  NOT NULL,
  [AllowRememberConsent] bit  NOT NULL,
  [AlwaysIncludeUserClaimsInIdToken] bit  NOT NULL,
  [RequirePkce] bit  NOT NULL,
  [AllowPlainTextPkce] bit  NOT NULL,
  [RequireRequestObject] bit  NOT NULL,
  [AllowAccessTokensViaBrowser] bit  NOT NULL,
  [FrontChannelLogoutUri] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NULL,
  [FrontChannelLogoutSessionRequired] bit  NOT NULL,
  [BackChannelLogoutUri] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NULL,
  [BackChannelLogoutSessionRequired] bit  NOT NULL,
  [AllowOfflineAccess] bit  NOT NULL,
  [IdentityTokenLifetime] int  NOT NULL,
  [AllowedIdentityTokenSigningAlgorithms] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [AccessTokenLifetime] int  NOT NULL,
  [AuthorizationCodeLifetime] int  NOT NULL,
  [ConsentLifetime] int  NULL,
  [AbsoluteRefreshTokenLifetime] int  NOT NULL,
  [SlidingRefreshTokenLifetime] int  NOT NULL,
  [RefreshTokenUsage] int  NOT NULL,
  [UpdateAccessTokenClaimsOnRefresh] bit  NOT NULL,
  [RefreshTokenExpiration] int  NOT NULL,
  [AccessTokenType] int  NOT NULL,
  [EnableLocalLogin] bit  NOT NULL,
  [IncludeJwtId] bit  NOT NULL,
  [AlwaysSendClientClaims] bit  NOT NULL,
  [ClientClaimsPrefix] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [PairWiseSubjectSalt] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserSsoLifetime] int  NULL,
  [UserCodeType] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [DeviceCodeLifetime] int  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [DeleterId] uniqueidentifier  NULL,
  [DeletionTime] datetime2(7)  NULL
)
GO

ALTER TABLE [dbo].[IdentityServerClients] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerClientScopes
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerClientScopes]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerClientScopes]
GO

CREATE TABLE [dbo].[IdentityServerClientScopes] (
  [ClientId] uniqueidentifier  NOT NULL,
  [Scope] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerClientScopes] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerClientSecrets
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerClientSecrets]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerClientSecrets]
GO

CREATE TABLE [dbo].[IdentityServerClientSecrets] (
  [Type] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(4000) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ClientId] uniqueidentifier  NOT NULL,
  [Description] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NULL,
  [Expiration] datetime2(7)  NULL
)
GO

ALTER TABLE [dbo].[IdentityServerClientSecrets] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerDeviceFlowCodes
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerDeviceFlowCodes]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerDeviceFlowCodes]
GO

CREATE TABLE [dbo].[IdentityServerDeviceFlowCodes] (
  [Id] uniqueidentifier  NOT NULL,
  [DeviceCode] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [UserCode] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [SubjectId] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [SessionId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClientId] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Description] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Expiration] datetime2(7)  NOT NULL,
  [Data] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[IdentityServerDeviceFlowCodes] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerIdentityResourceClaims
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerIdentityResourceClaims]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerIdentityResourceClaims]
GO

CREATE TABLE [dbo].[IdentityServerIdentityResourceClaims] (
  [Type] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [IdentityResourceId] uniqueidentifier  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerIdentityResourceClaims] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerIdentityResourceProperties
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerIdentityResourceProperties]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerIdentityResourceProperties]
GO

CREATE TABLE [dbo].[IdentityServerIdentityResourceProperties] (
  [IdentityResourceId] uniqueidentifier  NOT NULL,
  [Key] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(2000) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[IdentityServerIdentityResourceProperties] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerIdentityResources
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerIdentityResources]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerIdentityResources]
GO

CREATE TABLE [dbo].[IdentityServerIdentityResources] (
  [Id] uniqueidentifier  NOT NULL,
  [Name] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [DisplayName] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(1000) COLLATE Chinese_PRC_CI_AS  NULL,
  [Enabled] bit  NOT NULL,
  [Required] bit  NOT NULL,
  [Emphasize] bit  NOT NULL,
  [ShowInDiscoveryDocument] bit  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [DeleterId] uniqueidentifier  NULL,
  [DeletionTime] datetime2(7)  NULL
)
GO

ALTER TABLE [dbo].[IdentityServerIdentityResources] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for IdentityServerPersistedGrants
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityServerPersistedGrants]') AND type IN ('U'))
	DROP TABLE [dbo].[IdentityServerPersistedGrants]
GO

CREATE TABLE [dbo].[IdentityServerPersistedGrants] (
  [Key] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Type] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [SubjectId] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [SessionId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClientId] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Description] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [Expiration] datetime2(7)  NULL,
  [ConsumedTime] datetime2(7)  NULL,
  [Data] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Id] uniqueidentifier  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[IdentityServerPersistedGrants] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Primary Key structure for table __EFMigrationsHistory
-- ----------------------------
ALTER TABLE [dbo].[__EFMigrationsHistory] ADD CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpAuditLogActions
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpAuditLogActions_AuditLogId]
ON [dbo].[AbpAuditLogActions] (
  [AuditLogId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_ExecutionTime]
ON [dbo].[AbpAuditLogActions] (
  [TenantId] ASC,
  [ServiceName] ASC,
  [MethodName] ASC,
  [ExecutionTime] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpAuditLogActions
-- ----------------------------
ALTER TABLE [dbo].[AbpAuditLogActions] ADD CONSTRAINT [PK_AbpAuditLogActions] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpAuditLogs
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpAuditLogs_TenantId_ExecutionTime]
ON [dbo].[AbpAuditLogs] (
  [TenantId] ASC,
  [ExecutionTime] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_AbpAuditLogs_TenantId_UserId_ExecutionTime]
ON [dbo].[AbpAuditLogs] (
  [TenantId] ASC,
  [UserId] ASC,
  [ExecutionTime] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpAuditLogs
-- ----------------------------
ALTER TABLE [dbo].[AbpAuditLogs] ADD CONSTRAINT [PK_AbpAuditLogs] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table AbpClaimTypes
-- ----------------------------
ALTER TABLE [dbo].[AbpClaimTypes] ADD CONSTRAINT [PK_AbpClaimTypes] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpEntityChanges
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpEntityChanges_AuditLogId]
ON [dbo].[AbpEntityChanges] (
  [AuditLogId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId]
ON [dbo].[AbpEntityChanges] (
  [TenantId] ASC,
  [EntityTypeFullName] ASC,
  [EntityId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpEntityChanges
-- ----------------------------
ALTER TABLE [dbo].[AbpEntityChanges] ADD CONSTRAINT [PK_AbpEntityChanges] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpEntityPropertyChanges
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpEntityPropertyChanges_EntityChangeId]
ON [dbo].[AbpEntityPropertyChanges] (
  [EntityChangeId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpEntityPropertyChanges
-- ----------------------------
ALTER TABLE [dbo].[AbpEntityPropertyChanges] ADD CONSTRAINT [PK_AbpEntityPropertyChanges] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpLinkUsers
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_TargetTenantId]
ON [dbo].[AbpLinkUsers] (
  [SourceUserId] ASC,
  [SourceTenantId] ASC,
  [TargetUserId] ASC,
  [TargetTenantId] ASC
)
WHERE ([SourceTenantId] IS NOT NULL AND [TargetTenantId] IS NOT NULL)
GO


-- ----------------------------
-- Primary Key structure for table AbpLinkUsers
-- ----------------------------
ALTER TABLE [dbo].[AbpLinkUsers] ADD CONSTRAINT [PK_AbpLinkUsers] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpOrganizationUnitRoles
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId]
ON [dbo].[AbpOrganizationUnitRoles] (
  [RoleId] ASC,
  [OrganizationUnitId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpOrganizationUnitRoles
-- ----------------------------
ALTER TABLE [dbo].[AbpOrganizationUnitRoles] ADD CONSTRAINT [PK_AbpOrganizationUnitRoles] PRIMARY KEY CLUSTERED ([OrganizationUnitId], [RoleId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpOrganizationUnits
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpOrganizationUnits_Code]
ON [dbo].[AbpOrganizationUnits] (
  [Code] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_AbpOrganizationUnits_ParentId]
ON [dbo].[AbpOrganizationUnits] (
  [ParentId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpOrganizationUnits
-- ----------------------------
ALTER TABLE [dbo].[AbpOrganizationUnits] ADD CONSTRAINT [PK_AbpOrganizationUnits] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpPermissionGrants
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey]
ON [dbo].[AbpPermissionGrants] (
  [TenantId] ASC,
  [Name] ASC,
  [ProviderName] ASC,
  [ProviderKey] ASC
)
WHERE ([TenantId] IS NOT NULL)
GO


-- ----------------------------
-- Primary Key structure for table AbpPermissionGrants
-- ----------------------------
ALTER TABLE [dbo].[AbpPermissionGrants] ADD CONSTRAINT [PK_AbpPermissionGrants] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpRoleClaims
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpRoleClaims_RoleId]
ON [dbo].[AbpRoleClaims] (
  [RoleId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpRoleClaims
-- ----------------------------
ALTER TABLE [dbo].[AbpRoleClaims] ADD CONSTRAINT [PK_AbpRoleClaims] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpRoles
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpRoles_NormalizedName]
ON [dbo].[AbpRoles] (
  [NormalizedName] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpRoles
-- ----------------------------
ALTER TABLE [dbo].[AbpRoles] ADD CONSTRAINT [PK_AbpRoles] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpSecurityLogs
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpSecurityLogs_TenantId_Action]
ON [dbo].[AbpSecurityLogs] (
  [TenantId] ASC,
  [Action] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_AbpSecurityLogs_TenantId_ApplicationName]
ON [dbo].[AbpSecurityLogs] (
  [TenantId] ASC,
  [ApplicationName] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_AbpSecurityLogs_TenantId_Identity]
ON [dbo].[AbpSecurityLogs] (
  [TenantId] ASC,
  [Identity] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_AbpSecurityLogs_TenantId_UserId]
ON [dbo].[AbpSecurityLogs] (
  [TenantId] ASC,
  [UserId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpSecurityLogs
-- ----------------------------
ALTER TABLE [dbo].[AbpSecurityLogs] ADD CONSTRAINT [PK_AbpSecurityLogs] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpSettings
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [IX_AbpSettings_Name_ProviderName_ProviderKey]
ON [dbo].[AbpSettings] (
  [Name] ASC,
  [ProviderName] ASC,
  [ProviderKey] ASC
)
WHERE ([ProviderName] IS NOT NULL AND [ProviderKey] IS NOT NULL)
GO


-- ----------------------------
-- Primary Key structure for table AbpSettings
-- ----------------------------
ALTER TABLE [dbo].[AbpSettings] ADD CONSTRAINT [PK_AbpSettings] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table AbpTenantConnectionStrings
-- ----------------------------
ALTER TABLE [dbo].[AbpTenantConnectionStrings] ADD CONSTRAINT [PK_AbpTenantConnectionStrings] PRIMARY KEY CLUSTERED ([TenantId], [Name])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpTenants
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpTenants_Name]
ON [dbo].[AbpTenants] (
  [Name] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpTenants
-- ----------------------------
ALTER TABLE [dbo].[AbpTenants] ADD CONSTRAINT [PK_AbpTenants] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpUserClaims
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpUserClaims_UserId]
ON [dbo].[AbpUserClaims] (
  [UserId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpUserClaims
-- ----------------------------
ALTER TABLE [dbo].[AbpUserClaims] ADD CONSTRAINT [PK_AbpUserClaims] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpUserLogins
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpUserLogins_LoginProvider_ProviderKey]
ON [dbo].[AbpUserLogins] (
  [LoginProvider] ASC,
  [ProviderKey] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpUserLogins
-- ----------------------------
ALTER TABLE [dbo].[AbpUserLogins] ADD CONSTRAINT [PK_AbpUserLogins] PRIMARY KEY CLUSTERED ([UserId], [LoginProvider])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpUserOrganizationUnits
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId]
ON [dbo].[AbpUserOrganizationUnits] (
  [UserId] ASC,
  [OrganizationUnitId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpUserOrganizationUnits
-- ----------------------------
ALTER TABLE [dbo].[AbpUserOrganizationUnits] ADD CONSTRAINT [PK_AbpUserOrganizationUnits] PRIMARY KEY CLUSTERED ([OrganizationUnitId], [UserId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpUserRoles
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpUserRoles_RoleId_UserId]
ON [dbo].[AbpUserRoles] (
  [RoleId] ASC,
  [UserId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpUserRoles
-- ----------------------------
ALTER TABLE [dbo].[AbpUserRoles] ADD CONSTRAINT [PK_AbpUserRoles] PRIMARY KEY CLUSTERED ([UserId], [RoleId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table AbpUsers
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_AbpUsers_Email]
ON [dbo].[AbpUsers] (
  [Email] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_AbpUsers_NormalizedEmail]
ON [dbo].[AbpUsers] (
  [NormalizedEmail] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_AbpUsers_NormalizedUserName]
ON [dbo].[AbpUsers] (
  [NormalizedUserName] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_AbpUsers_UserName]
ON [dbo].[AbpUsers] (
  [UserName] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table AbpUsers
-- ----------------------------
ALTER TABLE [dbo].[AbpUsers] ADD CONSTRAINT [PK_AbpUsers] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table AbpUserTokens
-- ----------------------------
ALTER TABLE [dbo].[AbpUserTokens] ADD CONSTRAINT [PK_AbpUserTokens] PRIMARY KEY CLUSTERED ([UserId], [LoginProvider], [Name])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table base_dict
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_base_dict_Name]
ON [dbo].[base_dict] (
  [Name] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table base_dict
-- ----------------------------
ALTER TABLE [dbo].[base_dict] ADD CONSTRAINT [PK_base_dict] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table base_dict_details
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_base_dict_details_Pid]
ON [dbo].[base_dict_details] (
  [Pid] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table base_dict_details
-- ----------------------------
ALTER TABLE [dbo].[base_dict_details] ADD CONSTRAINT [PK_base_dict_details] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_jobs
-- ----------------------------
ALTER TABLE [dbo].[base_jobs] ADD CONSTRAINT [PK_base_jobs] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_menu
-- ----------------------------
ALTER TABLE [dbo].[base_menu] ADD CONSTRAINT [PK_base_menu] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table base_orgs
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_base_orgs_Pid]
ON [dbo].[base_orgs] (
  [Pid] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table base_orgs
-- ----------------------------
ALTER TABLE [dbo].[base_orgs] ADD CONSTRAINT [PK_base_orgs] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_role_menu
-- ----------------------------
ALTER TABLE [dbo].[base_role_menu] ADD CONSTRAINT [PK_base_role_menu] PRIMARY KEY CLUSTERED ([RoleId], [MenuId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_user_jobs
-- ----------------------------
ALTER TABLE [dbo].[base_user_jobs] ADD CONSTRAINT [PK_base_user_jobs] PRIMARY KEY CLUSTERED ([UserId], [JobId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_user_orgs
-- ----------------------------
ALTER TABLE [dbo].[base_user_orgs] ADD CONSTRAINT [PK_base_user_orgs] PRIMARY KEY CLUSTERED ([UserId], [OrganizationId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerApiResourceClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiResourceClaims] ADD CONSTRAINT [PK_IdentityServerApiResourceClaims] PRIMARY KEY CLUSTERED ([ApiResourceId], [Type])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerApiResourceProperties
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiResourceProperties] ADD CONSTRAINT [PK_IdentityServerApiResourceProperties] PRIMARY KEY CLUSTERED ([ApiResourceId], [Key], [Value])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerApiResources
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiResources] ADD CONSTRAINT [PK_IdentityServerApiResources] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerApiResourceScopes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiResourceScopes] ADD CONSTRAINT [PK_IdentityServerApiResourceScopes] PRIMARY KEY CLUSTERED ([ApiResourceId], [Scope])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerApiResourceSecrets
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiResourceSecrets] ADD CONSTRAINT [PK_IdentityServerApiResourceSecrets] PRIMARY KEY CLUSTERED ([ApiResourceId], [Type], [Value])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerApiScopeClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiScopeClaims] ADD CONSTRAINT [PK_IdentityServerApiScopeClaims] PRIMARY KEY CLUSTERED ([ApiScopeId], [Type])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerApiScopeProperties
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiScopeProperties] ADD CONSTRAINT [PK_IdentityServerApiScopeProperties] PRIMARY KEY CLUSTERED ([ApiScopeId], [Key], [Value])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerApiScopes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiScopes] ADD CONSTRAINT [PK_IdentityServerApiScopes] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerClientClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientClaims] ADD CONSTRAINT [PK_IdentityServerClientClaims] PRIMARY KEY CLUSTERED ([ClientId], [Type], [Value])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerClientCorsOrigins
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientCorsOrigins] ADD CONSTRAINT [PK_IdentityServerClientCorsOrigins] PRIMARY KEY CLUSTERED ([ClientId], [Origin])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerClientGrantTypes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientGrantTypes] ADD CONSTRAINT [PK_IdentityServerClientGrantTypes] PRIMARY KEY CLUSTERED ([ClientId], [GrantType])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerClientIdPRestrictions
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientIdPRestrictions] ADD CONSTRAINT [PK_IdentityServerClientIdPRestrictions] PRIMARY KEY CLUSTERED ([ClientId], [Provider])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerClientPostLogoutRedirectUris
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientPostLogoutRedirectUris] ADD CONSTRAINT [PK_IdentityServerClientPostLogoutRedirectUris] PRIMARY KEY CLUSTERED ([ClientId], [PostLogoutRedirectUri])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerClientProperties
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientProperties] ADD CONSTRAINT [PK_IdentityServerClientProperties] PRIMARY KEY CLUSTERED ([ClientId], [Key], [Value])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerClientRedirectUris
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientRedirectUris] ADD CONSTRAINT [PK_IdentityServerClientRedirectUris] PRIMARY KEY CLUSTERED ([ClientId], [RedirectUri])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table IdentityServerClients
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_IdentityServerClients_ClientId]
ON [dbo].[IdentityServerClients] (
  [ClientId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerClients
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClients] ADD CONSTRAINT [PK_IdentityServerClients] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerClientScopes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientScopes] ADD CONSTRAINT [PK_IdentityServerClientScopes] PRIMARY KEY CLUSTERED ([ClientId], [Scope])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerClientSecrets
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientSecrets] ADD CONSTRAINT [PK_IdentityServerClientSecrets] PRIMARY KEY CLUSTERED ([ClientId], [Type], [Value])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table IdentityServerDeviceFlowCodes
-- ----------------------------
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdentityServerDeviceFlowCodes_DeviceCode]
ON [dbo].[IdentityServerDeviceFlowCodes] (
  [DeviceCode] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_IdentityServerDeviceFlowCodes_Expiration]
ON [dbo].[IdentityServerDeviceFlowCodes] (
  [Expiration] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_IdentityServerDeviceFlowCodes_UserCode]
ON [dbo].[IdentityServerDeviceFlowCodes] (
  [UserCode] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerDeviceFlowCodes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerDeviceFlowCodes] ADD CONSTRAINT [PK_IdentityServerDeviceFlowCodes] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerIdentityResourceClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerIdentityResourceClaims] ADD CONSTRAINT [PK_IdentityServerIdentityResourceClaims] PRIMARY KEY CLUSTERED ([IdentityResourceId], [Type])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerIdentityResourceProperties
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerIdentityResourceProperties] ADD CONSTRAINT [PK_IdentityServerIdentityResourceProperties] PRIMARY KEY CLUSTERED ([IdentityResourceId], [Key], [Value])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerIdentityResources
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerIdentityResources] ADD CONSTRAINT [PK_IdentityServerIdentityResources] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table IdentityServerPersistedGrants
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_IdentityServerPersistedGrants_Expiration]
ON [dbo].[IdentityServerPersistedGrants] (
  [Expiration] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_IdentityServerPersistedGrants_SubjectId_ClientId_Type]
ON [dbo].[IdentityServerPersistedGrants] (
  [SubjectId] ASC,
  [ClientId] ASC,
  [Type] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_IdentityServerPersistedGrants_SubjectId_SessionId_Type]
ON [dbo].[IdentityServerPersistedGrants] (
  [SubjectId] ASC,
  [SessionId] ASC,
  [Type] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table IdentityServerPersistedGrants
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerPersistedGrants] ADD CONSTRAINT [PK_IdentityServerPersistedGrants] PRIMARY KEY CLUSTERED ([Key])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table AbpAuditLogActions
-- ----------------------------
ALTER TABLE [dbo].[AbpAuditLogActions] ADD CONSTRAINT [FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId] FOREIGN KEY ([AuditLogId]) REFERENCES [dbo].[AbpAuditLogs] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpEntityChanges
-- ----------------------------
ALTER TABLE [dbo].[AbpEntityChanges] ADD CONSTRAINT [FK_AbpEntityChanges_AbpAuditLogs_AuditLogId] FOREIGN KEY ([AuditLogId]) REFERENCES [dbo].[AbpAuditLogs] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpEntityPropertyChanges
-- ----------------------------
ALTER TABLE [dbo].[AbpEntityPropertyChanges] ADD CONSTRAINT [FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId] FOREIGN KEY ([EntityChangeId]) REFERENCES [dbo].[AbpEntityChanges] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpOrganizationUnitRoles
-- ----------------------------
ALTER TABLE [dbo].[AbpOrganizationUnitRoles] ADD CONSTRAINT [FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationUnitId] FOREIGN KEY ([OrganizationUnitId]) REFERENCES [dbo].[AbpOrganizationUnits] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[AbpOrganizationUnitRoles] ADD CONSTRAINT [FK_AbpOrganizationUnitRoles_AbpRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AbpRoles] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpOrganizationUnits
-- ----------------------------
ALTER TABLE [dbo].[AbpOrganizationUnits] ADD CONSTRAINT [FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[AbpOrganizationUnits] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpRoleClaims
-- ----------------------------
ALTER TABLE [dbo].[AbpRoleClaims] ADD CONSTRAINT [FK_AbpRoleClaims_AbpRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AbpRoles] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpTenantConnectionStrings
-- ----------------------------
ALTER TABLE [dbo].[AbpTenantConnectionStrings] ADD CONSTRAINT [FK_AbpTenantConnectionStrings_AbpTenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AbpTenants] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpUserClaims
-- ----------------------------
ALTER TABLE [dbo].[AbpUserClaims] ADD CONSTRAINT [FK_AbpUserClaims_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpUserLogins
-- ----------------------------
ALTER TABLE [dbo].[AbpUserLogins] ADD CONSTRAINT [FK_AbpUserLogins_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpUserOrganizationUnits
-- ----------------------------
ALTER TABLE [dbo].[AbpUserOrganizationUnits] ADD CONSTRAINT [FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationUnitId] FOREIGN KEY ([OrganizationUnitId]) REFERENCES [dbo].[AbpOrganizationUnits] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[AbpUserOrganizationUnits] ADD CONSTRAINT [FK_AbpUserOrganizationUnits_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpUserRoles
-- ----------------------------
ALTER TABLE [dbo].[AbpUserRoles] ADD CONSTRAINT [FK_AbpUserRoles_AbpRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AbpRoles] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

ALTER TABLE [dbo].[AbpUserRoles] ADD CONSTRAINT [FK_AbpUserRoles_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table AbpUserTokens
-- ----------------------------
ALTER TABLE [dbo].[AbpUserTokens] ADD CONSTRAINT [FK_AbpUserTokens_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerApiResourceClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiResourceClaims] ADD CONSTRAINT [FK_IdentityServerApiResourceClaims_IdentityServerApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[IdentityServerApiResources] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerApiResourceProperties
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiResourceProperties] ADD CONSTRAINT [FK_IdentityServerApiResourceProperties_IdentityServerApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[IdentityServerApiResources] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerApiResourceScopes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiResourceScopes] ADD CONSTRAINT [FK_IdentityServerApiResourceScopes_IdentityServerApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[IdentityServerApiResources] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerApiResourceSecrets
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiResourceSecrets] ADD CONSTRAINT [FK_IdentityServerApiResourceSecrets_IdentityServerApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[IdentityServerApiResources] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerApiScopeClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiScopeClaims] ADD CONSTRAINT [FK_IdentityServerApiScopeClaims_IdentityServerApiScopes_ApiScopeId] FOREIGN KEY ([ApiScopeId]) REFERENCES [dbo].[IdentityServerApiScopes] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerApiScopeProperties
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiScopeProperties] ADD CONSTRAINT [FK_IdentityServerApiScopeProperties_IdentityServerApiScopes_ApiScopeId] FOREIGN KEY ([ApiScopeId]) REFERENCES [dbo].[IdentityServerApiScopes] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerClientClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientClaims] ADD CONSTRAINT [FK_IdentityServerClientClaims_IdentityServerClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerClientCorsOrigins
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientCorsOrigins] ADD CONSTRAINT [FK_IdentityServerClientCorsOrigins_IdentityServerClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerClientGrantTypes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientGrantTypes] ADD CONSTRAINT [FK_IdentityServerClientGrantTypes_IdentityServerClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerClientIdPRestrictions
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientIdPRestrictions] ADD CONSTRAINT [FK_IdentityServerClientIdPRestrictions_IdentityServerClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerClientPostLogoutRedirectUris
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientPostLogoutRedirectUris] ADD CONSTRAINT [FK_IdentityServerClientPostLogoutRedirectUris_IdentityServerClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerClientProperties
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientProperties] ADD CONSTRAINT [FK_IdentityServerClientProperties_IdentityServerClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerClientRedirectUris
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientRedirectUris] ADD CONSTRAINT [FK_IdentityServerClientRedirectUris_IdentityServerClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerClientScopes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientScopes] ADD CONSTRAINT [FK_IdentityServerClientScopes_IdentityServerClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerClientSecrets
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientSecrets] ADD CONSTRAINT [FK_IdentityServerClientSecrets_IdentityServerClients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerIdentityResourceClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerIdentityResourceClaims] ADD CONSTRAINT [FK_IdentityServerIdentityResourceClaims_IdentityServerIdentityResources_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [dbo].[IdentityServerIdentityResources] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table IdentityServerIdentityResourceProperties
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerIdentityResourceProperties] ADD CONSTRAINT [FK_IdentityServerIdentityResourceProperties_IdentityServerIdentityResources_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [dbo].[IdentityServerIdentityResources] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

