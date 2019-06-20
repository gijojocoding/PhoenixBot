CREATE PROCEDURE [dbo].[spUpdateWarning]
	@UserIdString nvarchar(100), @warning int
AS
begin
update [User]
set [NumberOfWarnings] = [NumberOfWarnings] + @warning
where [UserIdString] = @UserIdString
end
