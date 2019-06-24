﻿CREATE TABLE [dbo].[User]
(
	[UserIdString] NVARCHAR(100) NOT NULL PRIMARY KEY, 
    [NumberOfWarnings] INT NOT NULL DEFAULT 0, 
    [IsMuted] SMALLINT NOT NULL DEFAULT 0
)