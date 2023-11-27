DROP TABLE IF EXISTS Chesscom.Tournament;
GO

CREATE TABLE Chesscom.Tournament(
	TournamentId INT IDENTITY(1,1) NOT NULL,
	Url NVARCHAR(128) NOT NULL,
	Name NVARCHAR(128) NOT NULL,
	CreatorUsername NVARCHAR(128) NOT NULL,
	Status NVARCHAR(128) NOT NULL,
	Type NVARCHAR(128) NOT NULL,
	Rules NVARCHAR(128) NOT NULL,
	TimeClass NVARCHAR(32) NOT NULL,
	TimeControl NVARCHAR(32) NOT NULL,
	FinishTime TIMESTAMP,
	IsRated BIT NOT NULL,
	IsOfficial BIT NOT NULL,
	RegisteredUserCount INT NOT NULL,
	TotalRounds INT NOT NULL

	CONSTRAINT [PK_Chesscom_Tournament_TournamentId] PRIMARY KEY CLUSTERED
      (
         TournamentId ASC
      ),
	CONSTRAINT [UK_Chesscom_Tournament_Url] UNIQUE
	(
		[Url]
	)
);
GO