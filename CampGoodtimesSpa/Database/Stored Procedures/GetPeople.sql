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
