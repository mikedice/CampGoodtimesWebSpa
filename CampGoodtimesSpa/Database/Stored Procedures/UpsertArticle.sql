CREATE PROCEDURE [gt].[UpsertArticle]
    @userName nvarchar(128),
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
