/*
	Description: Show the monthly Win Loss Draw rate for a player.
	Parameters: 
	PlayerId: Db key of the player, 
	FirstDate: the first date of the desired range to include in the result, 
	LastDate: the last date of the desired range to include in the result
	Result Columns:
	-	PlayerId
	-	GamesWon
	-	GamesDrawn
	-	GamesLost
	-	WinRate
	-	DrawRate
	-	LossRate
*/

CREATE OR ALTER PROCEDURE Chesscom.WinRateByPlayer
	@Username NVARCHAR(128),
	@FirstDate DATETIME,
	@LastDate DATETIME
AS

SELECT P.Username, 
	SUM(IIF(WG.Result = 1, 1, 0) + IIF(BG.Result = -1, 1, 0)) AS GamesWon,
	SUM(IIF(WG.Result = 0, 1, 0) + IIF(BG.Result = 0, 1, 0)) AS GamesDrawn,
	SUM(IIF(WG.Result = 1, 1, 0) + IIF(BG.Result = -1, 1, 0)) AS GamesLost,
	SUM(IIF(WG.Result = 1, 1, 0) + IIF(BG.Result = -1, 1, 0)) / (COUNT(WG.GameId) + COUNT(BG.GameId)) AS WinRate,
	SUM(IIF(WG.Result = 0, 1, 0) + IIF(BG.Result = 0, 1, 0)) / (COUNT(WG.GameId) + COUNT(BG.GameId)) AS DrawRate,
	SUM(IIF(WG.Result = 1, 1, 0) + IIF(BG.Result = -1, 1, 0)) / (COUNT(WG.GameId) + COUNT(BG.GameId)) AS LossRate
FROM Chesscom.Player P
	INNER JOIN Chesscom.Game WG ON WG.WhitePlayerId = P.PlayerId
	INNER JOIN Chesscom.Game BG ON BG.BlackPlayerId = P.PlayerId
WHERE P.Username = @Username
	AND WG.EndTime BETWEEN @FirstDate AND @LastDate
	AND BG.EndTime BETWEEN @FirstDate AND @LastDate
GROUP BY P.Username;