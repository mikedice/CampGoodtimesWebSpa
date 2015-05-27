/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

-- setup the super user
declare @userId int
select @userId = id from [gt].[users] where [gt].[users].[UserName] = 'admin'
if @userId is null
begin
    insert into [gt].[users] (FirstName,LastName,UserName,[Password]) values ('admin','admin','admin','gt.123')
end

set @userId = null
select @userId = id from [gt].[users] where [gt].[users].[UserName] = 'mikedice'
if @userId is null
begin
    insert into [gt].[users] (FirstName,LastName,UserName,[Password]) values ('Mike','Dice','mikedice','gt.123')
end

set @userId = null
select @userId = id from [gt].[users] where [gt].[users].[UserName] = 'kkrum'
if @userId is null
begin
    insert into [gt].[users] (FirstName,LastName,UserName,[Password]) values ('Kyle','Krum','kkrum','gt.123')
end

-- setup person type enum
declare @type int
declare @name nvarchar(64)
declare @value int

set @type = 1
set @name = 'Employee'
set @value = null;
select @value = [Id] from [gt].PersonTypeEnum where [gt].[PersonTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[PersonTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 2
set @name = 'Staff'
set @value=null;
select @value = [Id] from [gt].PersonTypeEnum where [gt].[PersonTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[PersonTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 3
set @name = 'Volunteer'
set @value=null;
select @value = [Id] from [gt].PersonTypeEnum where [gt].[PersonTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[PersonTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 4
set @name = 'Board'
set @value=null;
select @value = [Id] from [gt].PersonTypeEnum where [gt].[PersonTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[PersonTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 5
set @name = 'Sponsor'
set @value=null;
select @value = [Id] from [gt].PersonTypeEnum where [gt].[PersonTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[PersonTypeEnum]([Id],[Name]) values (@type, @name)
end

--setup article type enum
set @type = 1
set @name = 'Camps'
set @value=null;
select @value = [Id] from [gt].[ArticleTypeEnum] where [gt].[ArticleTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[ArticleTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 2
set @name = 'NewsFromTheDirector'
set @value=null;
select @value = [Id] from [gt].[ArticleTypeEnum] where [gt].[ArticleTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[ArticleTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 3
set @name = 'Events'
set @value=null;
select @value = [Id] from [gt].[ArticleTypeEnum] where [gt].[ArticleTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[ArticleTypeEnum]([Id],[Name]) values (@type, @name)
end

set @type = 4
set @name = 'Scholarships'
set @value=null;
select @value = [Id] from [gt].[ArticleTypeEnum] where [gt].[ArticleTypeEnum].[Name] = @name;
if @value is null
begin
    insert into [gt].[ArticleTypeEnum]([Id],[Name]) values (@type, @name)
end








