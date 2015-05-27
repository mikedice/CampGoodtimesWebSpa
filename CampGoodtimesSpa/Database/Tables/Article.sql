CREATE TABLE [gt].[Article]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    ArticleType int not null,
    Author nvarchar(128),
    CreatedOn DateTime not null,
    CreatedBy int not null,
    ModifiedOn DateTime,
    ModifiedBy int,
    DeletedOn DateTime,
    DeletedBy int,
    Title nvarchar(128) NOT NULL,
    ShortDescription nvarchar(256),
    Content nvarchar(3900),
    Attendance nvarchar (128),
    DateString nvarchar(128),
    ImageSmall nvarchar(512),
    ImageLarge nvarchar(512),
    ShowOnWebsite bit,
    [Order] int, 
	MoreInformationLink nvarchar(1024),
    CONSTRAINT [FK_Article_ToArticleTypeEnum] FOREIGN KEY ([ArticleType]) REFERENCES [gt].[ArticleTypeEnum]([Id]),
    CONSTRAINT [FK_ArticleCreatedBy_Users] FOREIGN KEY (CreatedBy) REFERENCES [gt].[Users]([Id]),
    CONSTRAINT [FK_ArticleModifiedBy_Users] FOREIGN KEY (ModifiedBy) REFERENCES [gt].[Users]([Id]),
    CONSTRAINT [FK_ArticleDeletedBy_Users] FOREIGN KEY (DeletedBy) REFERENCES [gt].[Users]([Id])

)
