
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/03/2014 01:42:20
-- Generated from EDMX file: C:\Box\Development\sandbox\dotnet\LogEventSystem\LogEventSystem.DataStorage\LogEventSystemModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LogEventSystem_tests];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[LogEvents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogEvents];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LogEvents'
CREATE TABLE [dbo].[LogEvents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(300)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [EventType] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LogEvents'
ALTER TABLE [dbo].[LogEvents]
ADD CONSTRAINT [PK_LogEvents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------