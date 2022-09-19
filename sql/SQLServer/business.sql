/*
 Navicat Premium Data Transfer

 Source Server         : .
 Source Server Type    : SQL Server
 Source Server Version : 15002000
 Source Host           : .:1433
 Source Catalog        : Business
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000
 File Encoding         : 65001

 Date: 19/09/2022 13:38:23
*/


-- ----------------------------
-- 如果执行该sql，需要提前创建数据库
-- ----------------------------


-- ----------------------------
-- Table structure for base_flow
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_flow]') AND type IN ('U'))
	DROP TABLE [dbo].[base_flow]
GO

CREATE TABLE [dbo].[base_flow] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [FormId] uniqueidentifier  NOT NULL,
  [Title] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Code] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [UseDate] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Level] int  NOT NULL,
  [Remark] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Status] int DEFAULT ((0)) NOT NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_flow] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_flow_line
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_flow_line]') AND type IN ('U'))
	DROP TABLE [dbo].[base_flow_line]
GO

CREATE TABLE [dbo].[base_flow_line] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [BaseFlowId] uniqueidentifier  NOT NULL,
  [Label] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [From] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [To] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Remark] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[base_flow_line] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_flow_line_form
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_flow_line_form]') AND type IN ('U'))
	DROP TABLE [dbo].[base_flow_line_form]
GO

CREATE TABLE [dbo].[base_flow_line_form] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [BaseFlowId] uniqueidentifier  NOT NULL,
  [FlowLineId] uniqueidentifier  NOT NULL,
  [FieldId] uniqueidentifier  NOT NULL,
  [FieldName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [FieldType] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Condition] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [IntContent] int  NOT NULL,
  [Content] nvarchar(1000) COLLATE Chinese_PRC_CI_AS  NULL,
  [Remark] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[base_flow_line_form] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_flow_node
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_flow_node]') AND type IN ('U'))
	DROP TABLE [dbo].[base_flow_node]
GO

CREATE TABLE [dbo].[base_flow_node] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [BaseFlowId] uniqueidentifier  NOT NULL,
  [NodeId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Name] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Type] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Left] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Top] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Ico] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [State] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Executor] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Users] nvarchar(1000) COLLATE Chinese_PRC_CI_AS  NULL,
  [Roles] nvarchar(1000) COLLATE Chinese_PRC_CI_AS  NULL,
  [Remark] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[base_flow_node] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_form
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_form]') AND type IN ('U'))
	DROP TABLE [dbo].[base_form]
GO

CREATE TABLE [dbo].[base_form] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [Api] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FormName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [DisplayName] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Description] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Disabled] bit  NOT NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [Namespace] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [EntityName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TableName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Remark] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_form] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_form_datas
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_form_datas]') AND type IN ('U'))
	DROP TABLE [dbo].[base_form_datas]
GO

CREATE TABLE [dbo].[base_form_datas] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [FormId] uniqueidentifier  NOT NULL,
  [Data] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_form_datas] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_form_fields
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_form_fields]') AND type IN ('U'))
	DROP TABLE [dbo].[base_form_fields]
GO

CREATE TABLE [dbo].[base_form_fields] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [FormId] uniqueidentifier  NOT NULL,
  [FieldType] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [DataType] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FieldName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Label] nvarchar(128) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Placeholder] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [DefaultValue] nvarchar(256) COLLATE Chinese_PRC_CI_AS  NULL,
  [FieldOrder] int  NOT NULL,
  [Icon] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Maxlength] int  NULL,
  [IsReadonly] bit  NOT NULL,
  [IsRequired] bit  NOT NULL,
  [IsIndex] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [IsSort] bit  NOT NULL,
  [Disabled] bit  NOT NULL,
  [Regx] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [Span] int DEFAULT ((24)) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[base_form_fields] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_form_fields_opts
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_form_fields_opts]') AND type IN ('U'))
	DROP TABLE [dbo].[base_form_fields_opts]
GO

CREATE TABLE [dbo].[base_form_fields_opts] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [FormId] uniqueidentifier  NOT NULL,
  [FormFieldId] uniqueidentifier  NOT NULL,
  [Label] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Value] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[base_form_fields_opts] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for base_form_workflow
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[base_form_workflow]') AND type IN ('U'))
	DROP TABLE [dbo].[base_form_workflow]
GO

CREATE TABLE [dbo].[base_form_workflow] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [FormId] uniqueidentifier  NOT NULL,
  [BaseFlowId] uniqueidentifier  NOT NULL,
  [EntityId] uniqueidentifier  NOT NULL,
  [Status] int DEFAULT ((0)) NOT NULL,
  [NodeId] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[base_form_workflow] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for Book
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Book]') AND type IN ('U'))
	DROP TABLE [dbo].[Book]
GO

CREATE TABLE [dbo].[Book] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [Name] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Description] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [Price] int  NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[Book] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for car
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[car]') AND type IN ('U'))
	DROP TABLE [dbo].[car]
GO

CREATE TABLE [dbo].[car] (
  [Id] uniqueidentifier  NOT NULL,
  [TenantId] uniqueidentifier  NULL,
  [Name] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Price] int  NOT NULL,
  [Type] int  NOT NULL,
  [IsDeleted] bit DEFAULT (CONVERT([bit],(0))) NOT NULL,
  [ExtraProperties] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConcurrencyStamp] nvarchar(40) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreationTime] datetime2(7)  NOT NULL,
  [CreatorId] uniqueidentifier  NULL,
  [LastModificationTime] datetime2(7)  NULL,
  [LastModifierId] uniqueidentifier  NULL
)
GO

ALTER TABLE [dbo].[car] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Primary Key structure for table __EFMigrationsHistory
-- ----------------------------
ALTER TABLE [dbo].[__EFMigrationsHistory] ADD CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table base_flow
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_base_flow_FormId]
ON [dbo].[base_flow] (
  [FormId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table base_flow
-- ----------------------------
ALTER TABLE [dbo].[base_flow] ADD CONSTRAINT [PK_base_flow] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_flow_line
-- ----------------------------
ALTER TABLE [dbo].[base_flow_line] ADD CONSTRAINT [PK_base_flow_line] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_flow_line_form
-- ----------------------------
ALTER TABLE [dbo].[base_flow_line_form] ADD CONSTRAINT [PK_base_flow_line_form] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_flow_node
-- ----------------------------
ALTER TABLE [dbo].[base_flow_node] ADD CONSTRAINT [PK_base_flow_node] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_form
-- ----------------------------
ALTER TABLE [dbo].[base_form] ADD CONSTRAINT [PK_base_form] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_form_datas
-- ----------------------------
ALTER TABLE [dbo].[base_form_datas] ADD CONSTRAINT [PK_base_form_datas] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_form_fields
-- ----------------------------
ALTER TABLE [dbo].[base_form_fields] ADD CONSTRAINT [PK_base_form_fields] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table base_form_fields_opts
-- ----------------------------
ALTER TABLE [dbo].[base_form_fields_opts] ADD CONSTRAINT [PK_base_form_fields_opts] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table base_form_workflow
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_base_form_workflow_EntityId]
ON [dbo].[base_form_workflow] (
  [EntityId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_base_form_workflow_NodeId]
ON [dbo].[base_form_workflow] (
  [NodeId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table base_form_workflow
-- ----------------------------
ALTER TABLE [dbo].[base_form_workflow] ADD CONSTRAINT [PK_base_form_workflow] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Book
-- ----------------------------
ALTER TABLE [dbo].[Book] ADD CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table car
-- ----------------------------
ALTER TABLE [dbo].[car] ADD CONSTRAINT [PK_car] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

