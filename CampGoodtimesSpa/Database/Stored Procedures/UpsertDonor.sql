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
