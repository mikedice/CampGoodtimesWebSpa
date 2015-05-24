CREATE PROCEDURE [gt].[DeletePerson]
    @userName nvarchar(128),
    @personId int
AS
    if @personId is null return;

    declare @user int
    select @user = gt.Users.Id from gt.Users where gt.Users.UserName = @userName;
    if @user is not null
    begin
        Update gt.Person set DeletedOn = GETDATE(), DeletedBy = @user where gt.Person.Id = @personId
    end
RETURN 0
