create procedure [dbo].[spCreateUser]
	@id nvarchar(100)
AS
	Begin
	set nocount off
	   Insert into [dbo].[User]
	   ([UserIdString]
	   ,[NumberOfWarnings]
	   ,[IsMuted]
	    )
		Values
		(@id
		,0
		,0
		)
		End
