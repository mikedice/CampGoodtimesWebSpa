CREATE PROCEDURE [gt].[DeleteDonor]
    @donorId int,
    @userName nvarchar(128)
AS
    if @donorId is null return;

    declare @userId int
    select @userId = [Id] from [gt].[Users] where [gt].[Users].[UserName]=@userName;
    if @userId is not null
    begin
        Update Donors set DeletedOn=GetDate(), DeletedBy=@userId where [Id]=@donorId;
    end
RETURN 0
