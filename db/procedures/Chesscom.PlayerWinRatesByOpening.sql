/*
	Description: Show all opening lines winrates for a player, ranking them from highest winrate to lowest, within a given time frame.
	Parameters: 
	PlayerId: Db key of the player, 
	FirstDate: the first date of the desired range to include in the result, 
	LastDate: the last date of the desired range to include in the result
	Result Columns:
	-	Username
	-	OpeningLine
	-	GamesWon
	-	GamesDrawn
	-	GamesLost
*/

CREATE OR ALTER PROCEDURE Chesscom.PlayerWinRatesByOpening
	@Username NVARCHAR(128),
	@FirstDate DATETIME,
	@LastDate DATETIME
AS

SELECT G.EcoOpening AS Opening,
	SUM(IIF((G.Result = 1 AND G.UserColor = N'W') OR (G.Result = -1 AND G.UserColor = N'B'), 1, 0)) AS GamesWon,
	SUM(IIF(G.Result = 0, 1, 0)) AS GamesDrawn,
	SUM(IIF((G.Result = -1 AND G.UserColor = N'W') OR (G.Result = 1 AND G.UserColor = N'B'), 1, 0)) AS GamesLost,
	CAST(SUM(IIF((G.Result = 1 AND G.UserColor = N'W') OR (G.Result = -1 AND G.UserColor = N'B'), 1, 0)) AS DECIMAL) / COUNT(*) AS WinRate
FROM Chesscom.AllGamesForPlayerByColor(@Username) G
WHERE CONVERT(DATE, G.EndTime, 0) BETWEEN @FirstDate AND @LastDate
GROUP BY G.EcoOpening
ORDER BY COUNT(*) DESC;
GO