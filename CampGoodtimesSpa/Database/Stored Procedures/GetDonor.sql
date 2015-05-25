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
