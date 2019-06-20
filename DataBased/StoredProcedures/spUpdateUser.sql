CREATE PROCEDURE [dbo].[spUpdateUser]
	@UserIdString nvarchar(100), @warning int, @mute smallint
AS
begin
update [User]
set [NumberOfWarnings] = @warning, [IsMuted] = @mute
where [UserIdString] = @UserIdString
end
