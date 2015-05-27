
GO
PRINT N'Creating [gt]...';


GO
CREATE SCHEMA [gt]


GO
PRINT N'Creating [gt].[PersonTypeEnum]...';


GO
CREATE TABLE [gt].[PersonTypeEnum] (
    [Id]   INT           NOT NULL,
    [Name] NVARCHAR (64) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [gt].[ArticleTypeEnum]...';


GO
CREATE TABLE [gt].[ArticleTypeEnum] (
    [Id]   INT           NOT NULL,
    [Name] NVARCHAR (64) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [gt].[Article]...';


GO
CREATE TABLE [gt].[Article] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [ArticleType]      INT             NOT NULL,
    [Author]           NVARCHAR (128)  NULL,
    [CreatedOn]        DATETIME        NOT NULL,
    [CreatedBy]        INT             NOT NULL,
    [ModifiedOn]       DATETIME        NULL,
    [ModifiedBy]       INT             NULL,
    [DeletedOn]        DATETIME        NULL,
    [DeletedBy]        INT             NULL,
    [Title]            NVARCHAR (128)  NOT NULL,
    [ShortDescription] NVARCHAR (256)  NULL,
    [Content]          NVARCHAR (3900) NULL,
    [Attendance]       NVARCHAR (128)  NULL,
    [DateString]       NVARCHAR (128)  NULL,
    [ImageSmall]       NVARCHAR (512)  NULL,
    [ImageLarge]       NVARCHAR (512)  NULL,
    [ShowOnWebsite]    BIT             NULL,
    [Order]            INT             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [gt].[Person]...';


GO
CREATE TABLE [gt].[Person] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [PersonType]       INT            NOT NULL,
    [CreatedOn]        DATETIME       NOT NULL,
    [CreatedBy]        INT            NOT NULL,
    [ModifiedOn]       DATETIME       NULL,
    [ModifiedBy]       INT            NULL,
    [DeletedOn]        DATETIME       NULL,
    [DeletedBy]        INT            NULL,
    [Name]             NVARCHAR (128) NOT NULL,
    [VisibleOnWebsite] BIT            NULL,
    [ImageSmall]       NVARCHAR (512) NULL,
    [ImageLarge]       NVARCHAR (512) NULL,
    [Title]            NVARCHAR (128) NULL,
    [Order]            INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [gt].[Users]...';


GO
CREATE TABLE [gt].[Users] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (128) NOT NULL,
    [LastName]  NVARCHAR (128) NOT NULL,
    [UserName]  NVARCHAR (128) NOT NULL,
    [Password]  NVARCHAR (24)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);


GO
PRINT N'Creating [gt].[Donors]...';


GO
CREATE TABLE [gt].[Donors] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CreatedOn]    DATETIME       NOT NULL,
    [CreatedBy]    INT            NOT NULL,
    [ModifiedOn]   DATETIME       NULL,
    [ModifiedBy]   INT            NULL,
    [DeletedOn]    DATETIME       NULL,
    [DeletedBy]    INT            NULL,
    [DonationDate] DATETIME       NOT NULL,
    [Giver]        NVARCHAR (512) NOT NULL,
    [InHonorOf]    NVARCHAR (512) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [gt].[FK_Article_ToArticleTypeEnum]...';


GO
ALTER TABLE [gt].[Article]
    ADD CONSTRAINT [FK_Article_ToArticleTypeEnum] FOREIGN KEY ([ArticleType]) REFERENCES [gt].[ArticleTypeEnum] ([Id]);


GO
PRINT N'Creating [gt].[FK_ArticleCreatedBy_Users]...';


GO
ALTER TABLE [gt].[Article]
    ADD CONSTRAINT [FK_ArticleCreatedBy_Users] FOREIGN KEY ([CreatedBy]) REFERENCES [gt].[Users] ([Id]);


GO
PRINT N'Creating [gt].[FK_ArticleModifiedBy_Users]...';


GO
ALTER TABLE [gt].[Article]
    ADD CONSTRAINT [FK_ArticleModifiedBy_Users] FOREIGN KEY ([ModifiedBy]) REFERENCES [gt].[Users] ([Id]);


GO
PRINT N'Creating [gt].[FK_ArticleDeletedBy_Users]...';


GO
ALTER TABLE [gt].[Article]
    ADD CONSTRAINT [FK_ArticleDeletedBy_Users] FOREIGN KEY ([DeletedBy]) REFERENCES [gt].[Users] ([Id]);


GO
PRINT N'Creating [gt].[FK_Person_PersonTypeEnum]...';


GO
ALTER TABLE [gt].[Person]
    ADD CONSTRAINT [FK_Person_PersonTypeEnum] FOREIGN KEY ([PersonType]) REFERENCES [gt].[PersonTypeEnum] ([Id]);


GO
PRINT N'Creating [gt].[FK_PersonCreatedBy_Users]...';


GO
ALTER TABLE [gt].[Person]
    ADD CONSTRAINT [FK_PersonCreatedBy_Users] FOREIGN KEY ([CreatedBy]) REFERENCES [gt].[Users] ([Id]);


GO
PRINT N'Creating [gt].[FK_PersonModifiedBy_Users]...';


GO
ALTER TABLE [gt].[Person]
    ADD CONSTRAINT [FK_PersonModifiedBy_Users] FOREIGN KEY ([ModifiedBy]) REFERENCES [gt].[Users] ([Id]);


GO
PRINT N'Creating [gt].[FK_PersonDeletedBy_Users]...';


GO
ALTER TABLE [gt].[Person]
    ADD CONSTRAINT [FK_PersonDeletedBy_Users] FOREIGN KEY ([DeletedBy]) REFERENCES [gt].[Users] ([Id]);


GO
PRINT N'Creating [gt].[FK_DonorCreatedBy_Users]...';


GO
ALTER TABLE [gt].[Donors]
    ADD CONSTRAINT [FK_DonorCreatedBy_Users] FOREIGN KEY ([CreatedBy]) REFERENCES [gt].[Users] ([Id]);


GO
PRINT N'Creating [gt].[FK_DonorModifiedBy_Users]...';


GO
ALTER TABLE [gt].[Donors]
    ADD CONSTRAINT [FK_DonorModifiedBy_Users] FOREIGN KEY ([ModifiedBy]) REFERENCES [gt].[Users] ([Id]);


GO
PRINT N'Creating [gt].[FK_DonorDeletedBy_Users]...';


GO
ALTER TABLE [gt].[Donors]
    ADD CONSTRAINT [FK_DonorDeletedBy_Users] FOREIGN KEY ([DeletedBy]) REFERENCES [gt].[Users] ([Id]);


GO
PRINT N'Creating [gt].[GetDonor]...';


GO
CREATE PROCEDURE [gt].[GetDonor]
    @id int
AS
    SELECT d.Id,
        d.CreatedOn,
        (select gt.users.UserName from gt.Users where gt.Users.Id = d.CreatedBy) as 'CreatedBy',
        d.ModifiedOn,
        (select gt.users.UserName from gt.Users where gt.Users.Id = d.ModifiedBy) as 'ModifedBy',
        d.DeletedOn,
        (select gt.users.UserName from gt.Users where gt.Users.Id = d.DeletedBy) as 'DeletedBy',
        d.DonationDate,
        d.Giver,
        d.InHonorOf
    FROM gt.Donors d
        where d.Id=@id and d.DeletedOn is null
RETURN 0
GO
PRINT N'Creating [gt].[GetPerson]...';


GO
CREATE PROCEDURE [gt].[GetPerson]
    @id int
AS
    SELECT  p.[Id],
            pt.Name as PersonType,
            p.CreatedOn,
            (select gt.users.UserName from gt.Users where gt.Users.Id = p.CreatedBy) as 'CreatedBy',
            p.ModifiedOn,
            (select gt.users.UserName from gt.Users where gt.Users.Id = p.ModifiedBy) as 'ModifedBy',
            p.DeletedOn,
            (select gt.users.UserName from gt.Users where gt.Users.Id = p.DeletedBy) as 'DeletedBy',
            p.[Name],
            p.VisibleOnWebsite,
            p.ImageSmall,
            p.ImageLarge,
            p.Title,
            p.[Order]
     FROM [gt].[Person] p 
        join gt.PersonTypeEnum pt on p.PersonType = pt.Id
        where p.[Id]=@id  and p.DeletedOn is null
RETURN 0
GO
PRINT N'Creating [gt].[GetArticle]...';


GO
CREATE PROCEDURE [gt].[GetArticle]
    @id int
AS
    SELECT a.Id,
        at.Name as ArticleType,
        a.Author,
        a.CreatedOn,
        (select gt.users.UserName from gt.Users where gt.Users.Id = a.CreatedBy) as 'CreatedBy',
        a.ModifiedOn,
		(select gt.users.UserName from gt.Users where gt.Users.Id = a.ModifiedBy) as 'ModifedBy',
        a.DeletedOn,
		(select gt.users.UserName from gt.Users where gt.Users.Id = a.DeletedBy) as 'DeletedBy',
        a.Title,
        a.ShortDescription,
        a.Content,
        a.Attendance,
        a.DateString,
        a.ImageSmall,
        a.ImageLarge,
        a.ShowOnWebsite,
        a.[Order]
 from [gt].[Article] a
    join [gt].[ArticleTypeEnum] at on a.ArticleType = at.Id
    where a.Id=@id and a.DeletedBy is null
RETURN 0
GO
PRINT N'Creating [gt].[DeleteArticle]...';


GO
CREATE PROCEDURE [gt].[DeleteArticle]
    @articleId int,
    @userName nvarchar(128)
AS
    declare @user int
    select @user = gt.Users.Id from gt.Users where gt.Users.UserName = @userName;
    if @user is null return;
    if @articleId is null return;

    update gt.Article set
        gt.Article.DeletedBy = @user,
        gt.Article.DeletedOn = GETDATE()
    WHERE gt.Article.Id = @articleId;

RETURN 0
GO
PRINT N'Creating [gt].[UpsertArticle]...';


GO
CREATE PROCEDURE [gt].[UpsertArticle]
    @userName nvarchar(128),
    @author nvarchar(128),
    @articleId int = null,
    @articleType int = null,
    @title nvarchar(128) = null,
    @shortDescription nvarchar(256) = null,
    @content nvarchar(3900) = null,
    @attendance nvarchar(128) = null,
    @dateString nvarchar(128) = null,
    @imageSmall nvarchar(512) = null,
    @imageLarge nvarchar(512) = null,
    @showOnWebsite bit = null,
    @order int = null
AS
    declare @user int
    declare @instance int
    select @user = gt.Users.Id from gt.Users where gt.Users.UserName = @userName;
    if @user is null return;

    if @articleId is not null
    begin
        select @instance = gt.Article.Id from gt.Article where gt.Article.Id = @articleId;
        if @instance is null return;
        update gt.Article
            set
                gt.Article.ModifiedBy = @user,
                gt.Article.ModifiedOn = GETDATE(),
                gt.Article.Author = @author,
                gt.Article.ArticleType = @articleType,
                gt.Article.Title = @title,
                gt.Article.ShortDescription = @shortDescription,
                gt.Article.Content = @content,
                gt.Article.Attendance = @attendance,
                gt.Article.DateString = @dateString,
                gt.Article.ImageLarge = @imageLarge,
                gt.Article.ImageSmall = @imageSmall,
                gt.Article.ShowOnWebsite = @showOnWebsite,
                gt.Article.[Order] = @order
            where gt.Article.Id = @instance
    end

    if @articleId is null
    begin
        insert into gt.Article (
            Author,
            CreatedOn,
            CreatedBy,
            ArticleType,
            Title,
            ShortDescription,
            Content,
            Attendance,
            DateString,
            ImageSmall,
            ImageLarge,
            ShowOnWebsite,
            [Order])
        VALUES (
        @author,
        GETDATE(),
        @user,
        @articleType,
        @title,
        @shortDescription,
        @content,
        @attendance,
        @dateString,
        @imageSmall,
        @imageLarge,
        @showOnWebsite,
        @order);
    end
RETURN 0
GO
PRINT N'Creating [gt].[UpsertPerson]...';


GO
CREATE PROCEDURE [gt].[UpsertPerson]
    @userName nvarchar(128),
    @personId int = null,
    @personType int = null,
    @name nvarchar(128) = null,
    @visibleOnWebsite bit = null,
    @imageSmall nvarchar(512) = null,
    @imageLarge nvarchar(512) = null,
    @title nvarchar(128) = null,
    @order int = null
AS
    declare @instance int
    declare @user int
    
    select @user = gt.Users.Id from gt.Users where gt.Users.UserName = @userName;
    if @user is null return;

    if @personId is not null
    begin
        select @instance = gt.Person.Id from gt.Person where gt.Person.Id = @personId;
        if @instance is null return;
        update gt.Person set
            gt.Person.ModifiedBy = @user,
            gt.Person.ModifiedOn = GETDATE(),
            gt.Person.PersonType = @personType,
            gt.Person.Name = @name,
            gt.Person.VisibleOnWebsite = @visibleOnWebsite,
            gt.Person.ImageSmall = @imageSmall,
            gt.Person.ImageLarge = @imageLarge,
            gt.Person.Title = @title,
            gt.Person.[Order] = @order
        where gt.Person.Id = @instance;
    end

    if @personId is null
    begin
        insert into gt.Person (CreatedBy, 
        CreatedOn, PersonType, Name, VisibleOnWebsite, 
        ImageSmall, ImageLarge, Title, [Order])
        values(
            @user,
            GETDATE(),
            @personType,
            @name,
            @visibleOnWebsite,
            @imageSmall,
            @imageLarge,
            @title,
            @order
        )
    end


RETURN 0
GO
PRINT N'Creating [gt].[DeletePerson]...';


GO
CREATE PROCEDURE [gt].[DeletePerson]
    @userName nvarchar(128),
    @personId int
AS
    if @personId is null return;

    declare @user int
    select @user = gt.Users.Id from gt.Users where gt.Users.UserName = @userName;
    if @user is not null
    begin
        Update gt.Person set DeletedOn = GETDATE(), DeletedBy = @user where gt.Person.Id = @personId
    end
RETURN 0
GO
PRINT N'Creating [gt].[UpsertDonor]...';


GO
CREATE PROCEDURE [gt].[UpsertDonor]
    @userName nvarchar(128),
    @id int = null,
    @donationDate datetime = null,
    @giver nvarchar(512) = null,
    @inHonorOf nvarchar(512) = null
AS
    declare @instance int
    declare @user int

    if @id is not null
    begin
        select @instance = Id from gt.Donors where gt.Donors.Id = @id;
        if @instance is not null
        begin
            select @user = Id from gt.Users where gt.Users.UserName = @userName;
            if @user is not null
            begin
                update gt.donors set 
                    gt.Donors.ModifiedBy = @user,
                    gt.Donors.ModifiedOn = GETDATE(),
                    gt.Donors.DonationDate = @donationDate,
                    gt.Donors.Giver = @giver,
                    gt.Donors.InHonorOf = @inHonorOf
                    WHERE gt.donors.Id = @instance;
            end
        end    
    end

    if @id is null
    begin
        select @user = Id from gt.Users where gt.Users.UserName = @userName;
        if @user is not null
        begin
            insert into gt.Donors(CreatedBy, CreatedOn, DonationDate, Giver, InHonorOf)
            values (@user, GETDATE(), @donationDate, @giver, @inHonorOf)
        end
    end

RETURN 0
GO
PRINT N'Creating [gt].[DeleteDonor]...';


GO
CREATE PROCEDURE [gt].[DeleteDonor]
    @donorId int,
    @userName nvarchar(128)
AS
    if @donorId is null return;

    declare @userId int
    select @userId = [Id] from [gt].[Users] where [gt].[Users].[UserName]=@userName;
    if @userId is not null
    begin
        Update Donors set DeletedOn=GetDate(), DeletedBy=@userId where [Id]=@donorId;
    end
RETURN 0
GO
PRINT N'Creating [gt].[GetDonors]...';


GO
CREATE PROCEDURE [gt].[GetDonors]
AS
    SELECT d.Id,
        d.CreatedOn,
        (select gt.users.UserName from gt.Users where gt.Users.Id = d.CreatedBy) as 'CreatedBy',
        d.ModifiedOn,
        (select gt.users.UserName from gt.Users where gt.Users.Id = d.ModifiedBy) as 'ModifedBy',
        d.DeletedOn,
        (select gt.users.UserName from gt.Users where gt.Users.Id = d.DeletedBy) as 'DeletedBy',
        d.DonationDate,
        d.Giver,
        d.InHonorOf
    FROM gt.Donors d
        where d.DeletedOn is null
RETURN 0
GO
PRINT N'Creating [gt].[GetArticles]...';


GO
CREATE PROCEDURE [gt].[GetArticles]
    @articleType int
AS
    SELECT a.Id,
        at.Name as ArticleType,
        a.Author,
        a.CreatedOn,
        (select gt.users.UserName from gt.Users where gt.Users.Id = a.CreatedBy) as 'CreatedBy',
        a.ModifiedOn,
		(select gt.users.UserName from gt.Users where gt.Users.Id = a.ModifiedBy) as 'ModifedBy',
        a.DeletedOn,
		(select gt.users.UserName from gt.Users where gt.Users.Id = a.DeletedBy) as 'DeletedBy',
        a.Title,
        a.ShortDescription,
        a.Content,
        a.Attendance,
        a.DateString,
        a.ImageSmall,
        a.ImageLarge,
        a.ShowOnWebsite,
        a.[Order]
 from [gt].[Article] a
    join [gt].[ArticleTypeEnum] at on a.ArticleType = at.Id
    where a.ArticleType = @articleType and a.DeletedBy is null

RETURN 0
GO
PRINT N'Creating [gt].[GetPeople]...';


GO
CREATE PROCEDURE [gt].[GetPeople]
    @personType int
AS
    SELECT  p.[Id],
            pt.Name as PersonType,
            p.CreatedOn,
            (select gt.users.UserName from gt.Users where gt.Users.Id = p.CreatedBy) as 'CreatedBy',
            p.ModifiedOn,
            (select gt.users.UserName from gt.Users where gt.Users.Id = p.ModifiedBy) as 'ModifedBy',
            p.DeletedOn,
            (select gt.users.UserName from gt.Users where gt.Users.Id = p.DeletedBy) as 'DeletedBy',
            p.[Name],
            p.VisibleOnWebsite,
            p.ImageSmall,
            p.ImageLarge,
            p.Title,
            p.[Order]
     FROM [gt].[Person] p 
        join gt.PersonTypeEnum pt on p.PersonType = pt.Id
        where p.[PersonType]=@personType  and p.DeletedOn is null
RETURN 0
GO
/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

-- setup the super user
declare @userId int
select @userId = id from [gt].[users] where [gt].[users].[UserName] = 'admin'
if @userId is null
begin
    insert into [gt].[users] (FirstName,LastName,UserName,[Password]) values ('admin','admin','admin','gt.123')
end

set @userId = null
select @userId = id from [gt].[users] where [gt].[users].[UserName] = 'mikedice'
if @userId is null
begin
    insert into [gt].[users] (FirstName,LastName,UserName,[Password]) values ('Mike','Dice','mikedice','gt.123')
end

set @userId = null
select @userId = id from [gt].[users] where [gt].[users].[UserName] = 'kkrum'
if @userId is null
begin
    insert into [gt].[users] (FirstName,LastName,UserName,[Password]) values ('Kyle','Krum','kkrum','gt.123')
end

-- setup person type enum
declare @type int
declare @name nvarchar(64)
declare @value int

set @type = 1
set @name = 'Employee'
set @value = null;
select @value = [Id] from [gt].PersonTypeEnum where [gt].[PersonTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[PersonTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 2
set @name = 'Staff'
set @value=null;
select @value = [Id] from [gt].PersonTypeEnum where [gt].[PersonTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[PersonTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 3
set @name = 'Volunteer'
set @value=null;
select @value = [Id] from [gt].PersonTypeEnum where [gt].[PersonTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[PersonTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 4
set @name = 'Board'
set @value=null;
select @value = [Id] from [gt].PersonTypeEnum where [gt].[PersonTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[PersonTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 5
set @name = 'Sponsor'
set @value=null;
select @value = [Id] from [gt].PersonTypeEnum where [gt].[PersonTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[PersonTypeEnum]([Id],[Name]) values (@type, @name)
end

--setup article type enum
set @type = 1
set @name = 'Camps'
set @value=null;
select @value = [Id] from [gt].[ArticleTypeEnum] where [gt].[ArticleTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[ArticleTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 2
set @name = 'NewsFromTheDirector'
set @value=null;
select @value = [Id] from [gt].[ArticleTypeEnum] where [gt].[ArticleTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[ArticleTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 3
set @name = 'Events'
set @value=null;
select @value = [Id] from [gt].[ArticleTypeEnum] where [gt].[ArticleTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[ArticleTypeEnum]([Id],[Name]) values (@type, @name)
end
