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
