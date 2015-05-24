CREATE TABLE [gt].[Person]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    PersonType int not null,
    CreatedOn DateTime not null,
    CreatedBy int not null,
    ModifiedOn DateTime,
    ModifiedBy int,
    DeletedOn DateTime,
    DeletedBy int,
    Name nvarchar(128) NOT NULL,
    VisibleOnWebsite bit,
    ImageSmall nvarchar(512),
    ImageLarge nvarchar(512),
    Title nvarchar(128),
    [Order] int, 
    CONSTRAINT [FK_Person_PersonTypeEnum] FOREIGN KEY ([PersonType]) REFERENCES [gt].[PersonTypeEnum]([Id]), 
    CONSTRAINT [FK_PersonCreatedBy_Users] FOREIGN KEY (CreatedBy) REFERENCES [gt].[Users]([Id]),
    CONSTRAINT [FK_PersonModifiedBy_Users] FOREIGN KEY (ModifiedBy) REFERENCES [gt].[Users]([Id]),
    CONSTRAINT [FK_PersonDeletedBy_Users] FOREIGN KEY (DeletedBy) REFERENCES [gt].[Users]([Id])
)
