DROP PROCEDURE IF EXISTS Chesscom.CreateGame
GO

CREATE OR ALTER PROCEDURE Chesscom.CreateGame
@Url NVARCHAR(128),
@WhitePlayerUsername NVARCHAR(128),
@BlackPlayerUsername NVARCHAR(128),
@Result INT,
@PGN TEXT,
@EcoOpening NVARCHAR(12) = NULL,
@StartTime DATETIMEOFFSET,
@EndTime DATETIMEOFFSET,
@TimeControl NVARCHAR(32),
@Rules NVARCHAR(32),
@TournamentUrl NVARCHAR(32) = NULL,
@GameId INT OUTPUT

AS
INSERT Chesscom.Game(Url, WhitePlayerId, BlackPlayerId, Result, PGN, EcoOpening, StartTime, EndTime, TimeControl, Rules, TournamentId)
VALUES(
	@Url,
	(SELECT P.PlayerId FROM Chesscom.Player P WHERE P.Username = @WhitePlayerUsername),
	(SELECT P.PlayerId FROM Chesscom.Player P WHERE P.Username = @BlackPlayerUsername),
	@Result,
	@PGN,
	@EcoOpening,
	@StartTime,
	@EndTime,
	@TimeControl,
	@Rules,
	@TournamentUrl
)


SET @GameId = SCOPE_IDENTITY();
GO