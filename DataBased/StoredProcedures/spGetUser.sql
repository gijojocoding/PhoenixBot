CREATE PROCEDURE [dbo].[spGetUser]
	@id nvarchar(100)
AS
begin
	set nocount off
	SELECT *
	from [User]
	where UserIdString = @id

end