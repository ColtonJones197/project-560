CREATE OR ALTER PROCEDURE Chesscom.GetGamesByUser
	@Username NVARCHAR(128)
AS

SELECT 
	G.GameId,
	G.Url,
	G.WhitePlayerId,
	ChessCom.PlayerIdToUsername(G.WhitePlayerId) AS WhiteUsername,
	G.BlackPlayerId,
	ChessCom.PlayerIdToUsername(G.BlackPlayerId) AS BlackUsername,
	G.Result,
	G.PGN,
	G.EcoOpening,
	G.StartTime,
	G.EndTime,
	G.TimeControl,
	G.Rules,
	G.TournamentId
FROM Chesscom.Game G
	INNER JOIN Chesscom.Player P ON P.PlayerId = G.WhitePlayerId OR P.PlayerId = G.BlackPlayerId
WHERE P.Username = @Username
	AND G.IsRemoved = 0
ORDER BY G.StartTime DESC, G.GameId;
GO