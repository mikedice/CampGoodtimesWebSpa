CREATE TABLE [gt].[Donors]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    CreatedOn DateTime not null,
    CreatedBy int not null,
    ModifiedOn DateTime,
    ModifiedBy int,
    DeletedOn DateTime,
    DeletedBy int,
    DonationDate DateTime not null,
    Giver nvarchar(512) not null,
    InHonorOf nvarchar(512)
    CONSTRAINT [FK_DonorCreatedBy_Users] FOREIGN KEY (CreatedBy) REFERENCES [gt].[Users]([Id]),
    CONSTRAINT [FK_DonorModifiedBy_Users] FOREIGN KEY (ModifiedBy) REFERENCES [gt].[Users]([Id]),
    CONSTRAINT [FK_DonorDeletedBy_Users] FOREIGN KEY (DeletedBy) REFERENCES [gt].[Users]([Id])
)
