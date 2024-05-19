IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Store')
BEGIN
   CREATE DATABASE [Store]
END
GO

USE [Store]
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Item' and xtype='U')
BEGIN
    CREATE TABLE [Item]
	(
        [Id] INT PRIMARY KEY IDENTITY (1, 1),
        [Name] VARCHAR (100) NOT NULL,
		[Price] DECIMAL(10,2) NOT NULL,
		[DateAdded] DATETIME2 NOT NULL
    ) 
END