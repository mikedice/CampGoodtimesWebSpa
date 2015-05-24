CREATE PROCEDURE [gt].[GetArticles]
    @articleType int
AS
    SELECT a.Id,
        at.Name as ArticleType,
        a.CreatedOn,
        ucb.UserName as CreatedBy,
        a.ModifiedOn,
        umb.UserName as ModifiedBy,
        a.DeletedOn,
        udb.UserName as DeletedBy,
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
    join [gt].[Users] ucb on a.CreatedBy = ucb.Id
    join [gt].[Users] umb on a.ModifiedBy = umb.Id
    join [gt].[Users] udb on a.DeletedBy = udb.Id
    where a.ArticleType = @articleType and a.DeletedBy is null

RETURN 0
