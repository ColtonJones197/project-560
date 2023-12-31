/*
Description: Show a player’s lifetime head-to-head win rate against all opponents, ranked from highest winrate to lowest.
Parameters: 
PlayerId: The unique Id of the desired player to analyze
Result Columns:
-	PlayerUsername
-	OpponentUsername
-	GamesWon
-	GamesDrawn
-	GamesLost
-	WinRate
*/

CREATE OR ALTER PROCEDURE Chesscom.PlayerHeadToHead
	@Username NVARCHAR(128),
	@FirstDate DATETIME,
	@LastDate DATETIME
AS
WITH GameCte AS(
	SELECT IIF(G.UserColor = N'W', G.BlackPlayer, G.WhitePlayer) AS OpponentUsername, G.*
	FROM Chesscom.AllGamesForPlayerByColor(@Username) G
)
SELECT G.OpponentUsername,
	SUM(IIF((G.Result = 1 AND G.UserColor = N'W') OR (G.Result = -1 AND G.UserColor = N'B'), 1, 0)) AS GamesWon,
	SUM(IIF(G.Result = 0, 1, 0)) AS GamesDrawn,
	SUM(IIF((G.Result = -1 AND G.UserColor = N'W') OR (G.Result = 1 AND G.UserColor = N'B'), 1, 0)) AS GamesLost,
	CAST(SUM(IIF((G.Result = 1 AND G.UserColor = N'W') OR (G.Result = -1 AND G.UserColor = N'B'), 1, 0)) AS DECIMAL) / COUNT(*) AS WinRate
FROM GameCte G
WHERE CONVERT(DATE, G.EndTime, 0) BETWEEN @FirstDate AND @LastDate
GROUP BY G.OpponentUsername
ORDER BY COUNT(*) DESC;
GO