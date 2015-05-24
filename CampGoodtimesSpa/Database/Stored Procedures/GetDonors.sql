CREATE PROCEDURE [gt].[GetDonors]
AS
    SELECT d.Id,
        d.CreatedOn,
        dcb.UserName as CreatedBy,
        d.ModifiedOn,
        dmb.UserName as ModifiedBy,
        d.DeletedOn,
        dcb.UserName as DeletedBy,
        d.DonationDate,
        d.Giver,
        d.InHonorOf
    FROM gt.Donors d
        join [gt].[Users] dcb on d.CreatedBy = dcb.Id
        join [gt].[Users] dmb on d.ModifiedBy = dmb.Id
        join [gt].[Users] ddb on d.DeletedBy = ddb.Id
        where d.DeletedOn is null
RETURN 0
