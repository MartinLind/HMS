﻿
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/04/2017 01:54:15
-- Generated from EDMX file: C:\Users\MING\Source\Repos\HMS\HMS\HMS\Models\DB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HMSDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PatientLocalCase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ObjectSet_LocalCase] DROP CONSTRAINT [FK_PatientLocalCase];
GO
IF OBJECT_ID(N'[dbo].[FK_LocalCaseUser_LocalCase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocalCaseUser] DROP CONSTRAINT [FK_LocalCaseUser_LocalCase];
GO
IF OBJECT_ID(N'[dbo].[FK_LocalCaseUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LocalCaseUser] DROP CONSTRAINT [FK_LocalCaseUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_LocalCaseRoom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ObjectSet_Room] DROP CONSTRAINT [FK_LocalCaseRoom];
GO
IF OBJECT_ID(N'[dbo].[FK_Person_inherits_Object]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ObjectSet_Person] DROP CONSTRAINT [FK_Person_inherits_Object];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ObjectSet_Patient] DROP CONSTRAINT [FK_Patient_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_LocalCase_inherits_Object]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ObjectSet_LocalCase] DROP CONSTRAINT [FK_LocalCase_inherits_Object];
GO
IF OBJECT_ID(N'[dbo].[FK_User_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ObjectSet_User] DROP CONSTRAINT [FK_User_inherits_Person];
GO
IF OBJECT_ID(N'[dbo].[FK_Room_inherits_Object]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ObjectSet_Room] DROP CONSTRAINT [FK_Room_inherits_Object];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ObjectSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ObjectSet];
GO
IF OBJECT_ID(N'[dbo].[ObjectSet_Person]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ObjectSet_Person];
GO
IF OBJECT_ID(N'[dbo].[ObjectSet_Patient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ObjectSet_Patient];
GO
IF OBJECT_ID(N'[dbo].[ObjectSet_LocalCase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ObjectSet_LocalCase];
GO
IF OBJECT_ID(N'[dbo].[ObjectSet_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ObjectSet_User];
GO
IF OBJECT_ID(N'[dbo].[ObjectSet_Room]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ObjectSet_Room];
GO
IF OBJECT_ID(N'[dbo].[LocalCaseUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LocalCaseUser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ObjectSet'
CREATE TABLE [dbo].[ObjectSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [timecreate] datetime  NOT NULL,
    [timemodify] datetime  NOT NULL,
    [isactive] bit  NOT NULL
);
GO

-- Creating table 'ObjectSet_Person'
CREATE TABLE [dbo].[ObjectSet_Person] (
    [prename] nvarchar(max)  NOT NULL,
    [surname] nvarchar(max)  NOT NULL,
    [phone] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [gender] nvarchar(max)  NOT NULL,
    [street] nvarchar(max)  NOT NULL,
    [city] nvarchar(max)  NOT NULL,
    [zip] nvarchar(max)  NOT NULL,
    [dateofbirth] datetime  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'ObjectSet_Patient'
CREATE TABLE [dbo].[ObjectSet_Patient] (
    [hcID] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'ObjectSet_LocalCase'
CREATE TABLE [dbo].[ObjectSet_LocalCase] (
    [timeopen] datetime  NOT NULL,
    [timeclosed] datetime  NOT NULL,
    [casenr] nvarchar(max)  NOT NULL,
    [personincharge] nvarchar(max)  NOT NULL,
    [diagnosis] nvarchar(max)  NOT NULL,
    [medication] nvarchar(max)  NOT NULL,
    [therapy] nvarchar(max)  NOT NULL,
    [expectedtime] nvarchar(max)  NOT NULL,
    [Property1] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [Patient_Id] int  NOT NULL
);
GO

-- Creating table 'ObjectSet_User'
CREATE TABLE [dbo].[ObjectSet_User] (
    [password] nvarchar(max)  NOT NULL,
    [rolename] nvarchar(max)  NOT NULL,
    [accessright1] bit  NOT NULL,
    [accessright2] bit  NOT NULL,
    [accessright3] bit  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'ObjectSet_Room'
CREATE TABLE [dbo].[ObjectSet_Room] (
    [number] nvarchar(max)  NOT NULL,
    [space] nvarchar(max)  NOT NULL,
    [vacancy] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL,
    [LocalCase_Id] int  NOT NULL
);
GO

-- Creating table 'LocalCaseUser'
CREATE TABLE [dbo].[LocalCaseUser] (
    [LocalCase_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ObjectSet'
ALTER TABLE [dbo].[ObjectSet]
ADD CONSTRAINT [PK_ObjectSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ObjectSet_Person'
ALTER TABLE [dbo].[ObjectSet_Person]
ADD CONSTRAINT [PK_ObjectSet_Person]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ObjectSet_Patient'
ALTER TABLE [dbo].[ObjectSet_Patient]
ADD CONSTRAINT [PK_ObjectSet_Patient]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ObjectSet_LocalCase'
ALTER TABLE [dbo].[ObjectSet_LocalCase]
ADD CONSTRAINT [PK_ObjectSet_LocalCase]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ObjectSet_User'
ALTER TABLE [dbo].[ObjectSet_User]
ADD CONSTRAINT [PK_ObjectSet_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ObjectSet_Room'
ALTER TABLE [dbo].[ObjectSet_Room]
ADD CONSTRAINT [PK_ObjectSet_Room]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LocalCase_Id], [User_Id] in table 'LocalCaseUser'
ALTER TABLE [dbo].[LocalCaseUser]
ADD CONSTRAINT [PK_LocalCaseUser]
    PRIMARY KEY CLUSTERED ([LocalCase_Id], [User_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Patient_Id] in table 'ObjectSet_LocalCase'
ALTER TABLE [dbo].[ObjectSet_LocalCase]
ADD CONSTRAINT [FK_PatientLocalCase]
    FOREIGN KEY ([Patient_Id])
    REFERENCES [dbo].[ObjectSet_Patient]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientLocalCase'
CREATE INDEX [IX_FK_PatientLocalCase]
ON [dbo].[ObjectSet_LocalCase]
    ([Patient_Id]);
GO

-- Creating foreign key on [LocalCase_Id] in table 'LocalCaseUser'
ALTER TABLE [dbo].[LocalCaseUser]
ADD CONSTRAINT [FK_LocalCaseUser_LocalCase]
    FOREIGN KEY ([LocalCase_Id])
    REFERENCES [dbo].[ObjectSet_LocalCase]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [User_Id] in table 'LocalCaseUser'
ALTER TABLE [dbo].[LocalCaseUser]
ADD CONSTRAINT [FK_LocalCaseUser_User]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[ObjectSet_User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocalCaseUser_User'
CREATE INDEX [IX_FK_LocalCaseUser_User]
ON [dbo].[LocalCaseUser]
    ([User_Id]);
GO

-- Creating foreign key on [LocalCase_Id] in table 'ObjectSet_Room'
ALTER TABLE [dbo].[ObjectSet_Room]
ADD CONSTRAINT [FK_LocalCaseRoom]
    FOREIGN KEY ([LocalCase_Id])
    REFERENCES [dbo].[ObjectSet_LocalCase]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocalCaseRoom'
CREATE INDEX [IX_FK_LocalCaseRoom]
ON [dbo].[ObjectSet_Room]
    ([LocalCase_Id]);
GO

-- Creating foreign key on [Id] in table 'ObjectSet_Person'
ALTER TABLE [dbo].[ObjectSet_Person]
ADD CONSTRAINT [FK_Person_inherits_Object]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ObjectSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'ObjectSet_Patient'
ALTER TABLE [dbo].[ObjectSet_Patient]
ADD CONSTRAINT [FK_Patient_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ObjectSet_Person]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'ObjectSet_LocalCase'
ALTER TABLE [dbo].[ObjectSet_LocalCase]
ADD CONSTRAINT [FK_LocalCase_inherits_Object]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ObjectSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'ObjectSet_User'
ALTER TABLE [dbo].[ObjectSet_User]
ADD CONSTRAINT [FK_User_inherits_Person]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ObjectSet_Person]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'ObjectSet_Room'
ALTER TABLE [dbo].[ObjectSet_Room]
ADD CONSTRAINT [FK_Room_inherits_Object]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ObjectSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------