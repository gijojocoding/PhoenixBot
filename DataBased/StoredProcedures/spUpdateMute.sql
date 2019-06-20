CREATE PROCEDURE [dbo].[spUpdateMute]
	@UserIdString nvarchar(100), @mute smallint
AS
begin
update [User]
set [IsMuted] = @mute
where [UserIdString] = @UserIdString
end
