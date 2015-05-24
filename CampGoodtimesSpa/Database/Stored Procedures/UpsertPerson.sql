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
