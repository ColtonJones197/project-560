/*
Description: Show a player’s lifetime games by time control, grouping those games into rapid, blitz, and bullet categories.
Parameters: 
PlayerId: Db key of the player
Result Columns:
-	TimeControl (Rapid, Blitz, Bullet)
-	TotalGames
-	GamesWon
-	GamesDrawn
-	GamesLost
-	WinRate
*/

CREATE OR ALTER PROCEDURE Chesscom.PlayerWinRateByTimeControl
	@Username NVARCHAR(128),
	@FirstDate DATE,
	@LastDate DATE
AS

SELECT G.TimeControl,
	SUM(IIF((G.Result = 1 AND G.UserColor = N'W') OR (G.Result = -1 AND G.UserColor = N'B'), 1, 0)) AS GamesWon,
	SUM(IIF(G.Result = 0, 1, 0)) AS GamesDrawn,
	SUM(IIF((G.Result = -1 AND G.UserColor = N'W') OR (G.Result = 1 AND G.UserColor = N'B'), 1, 0)) AS GamesLost,
	CAST(SUM(IIF((G.Result = 1 AND G.UserColor = N'W') OR (G.Result = -1 AND G.UserColor = N'B'), 1, 0)) AS DECIMAL) / COUNT(*) AS WinRate
FROM Chesscom.AllGamesForPlayerByColor(@Username) G
WHERE CONVERT(DATE, G.EndTime, 0) BETWEEN @FirstDate AND @LastDate
GROUP BY G.TimeControl
ORDER BY COUNT(*) DESC;
GO