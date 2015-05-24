CREATE PROCEDURE [gt].[GetPeople]
    @personType int
AS
    SELECT  p.[Id],
            pt.Name as PersonType,
            p.CreatedOn,
            ucb.UserName as CreatedBy,
            p.ModifiedOn,
            umb.UserName as ModifiedBy,
            p.DeletedOn,
            udb.UserName as DeletedBy,
            p.[Name],
            p.VisibleOnWebsite,
            p.ImageSmall,
            p.ImageLarge,
            p.Title,
            p.[Order]
     FROM [gt].[Person] p 
        join gt.PersonTypeEnum pt on p.PersonType = pt.Id
        join gt.Users ucb on p.CreatedBy = ucb.Id
        join gt.Users umb on p.ModifiedBy = umb.Id
        join gt.Users udb on p.ModifiedBy = udb.Id
        where p.[PersonType]=@personType  and p.DeletedOn is null
RETURN 0
