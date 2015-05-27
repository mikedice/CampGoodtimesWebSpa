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
        a.[Order],
		a.MoreInformationLink
 from [gt].[Article] a
    join [gt].[ArticleTypeEnum] at on a.ArticleType = at.Id
    where a.ArticleType = @articleType and a.DeletedBy is null

RETURN 0
