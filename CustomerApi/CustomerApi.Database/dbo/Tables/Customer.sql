CREATE TABLE [dbo].[Customer] (
    [Id] UNIQUEIDENTIFIER DEFAULT (NEWID()) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Birthday] DATETIME2 NULL, 
    [Age] INT NULL, 
    CONSTRAINT [PK_Customer] PRIMARY KEY ([Id]) ,

);

