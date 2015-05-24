CREATE TABLE [gt].[Users]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    FirstName nvarchar(128) not null,
    LastName nvarchar (128) not null,
    UserName nvarchar(128) unique not null,
    [Password] nvarchar(24) not null
)
