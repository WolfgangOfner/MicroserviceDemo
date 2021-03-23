CREATE TABLE [dbo].[Order] (
    [Id] UNIQUEIDENTIFIER DEFAULT (NEWID()) NOT NULL, 
    [OrderState] INT NOT NULL, 
    [CustomerGuid] UNIQUEIDENTIFIER NOT NULL, 
    [CustomerFullName] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id]) ,
);

