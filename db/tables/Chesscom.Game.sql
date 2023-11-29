DROP TABLE IF EXISTS Chesscom.Game;
GO

CREATE TABLE Chesscom.Game(
	GameId INT IDENTITY (1,1) NOT NULL,
	Url NVARCHAR(128) NOT NULL,
	WhitePlayerId INT NOT NULL,
	BlackPlayerId INT NOT NULL,
	Result INT NOT NULL,
	PGN TEXT NOT NULL,
	EcoOpening NVARCHAR(12),
	StartTime DATETIME NOT NULL,
	EndTime DATETIME,
	TimeControl NVARCHAR(32) NOT NULL,
	Rules NVARCHAR(32) NOT NULL,
	TournamentId INT,
	IsRemoved BIT NOT NULL DEFAULT(0),
	CreatedOn DATETIMEOFFSET NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
	UpdatedOn DATETIMEOFFSET NOT NULL DEFAULT(SYSDATETIMEOFFSET()),


	CONSTRAINT [PK_Chesscom_Game_GameId] PRIMARY KEY CLUSTERED
      (
         GameId ASC
      ),
	CONSTRAINT [UK_Chesscom_Game_Url] UNIQUE
	(
		[Url]
	)
);
GO



/****************************
 * Check Constraints
 ****************************/

IF NOT EXISTS
   (
      SELECT *
      FROM sys.check_constraints cc
      WHERE cc.parent_object_id = OBJECT_ID(N'Chesscom.Game')
         AND cc.[name] = N'CK_Chesscom_Game_Result'
   )
BEGIN
   ALTER TABLE Chesscom.Game
   ADD CONSTRAINT [CK_Chesscom_Game_Result] CHECK
   (
      Result BETWEEN -1 AND 1
   )
END;