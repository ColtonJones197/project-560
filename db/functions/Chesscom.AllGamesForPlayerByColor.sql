CREATE OR ALTER FUNCTION Chesscom.AllGamesForPlayerByColor(@Username NVARCHAR(128))
RETURNS TABLE
AS
RETURN(
SELECT IIF(PW.Username = @Username, N'W', N'B') AS UserColor, PW.Username AS WhitePlayer, PB.Username AS BlackPlayer, G.*
FROM Chesscom.Game G
	INNER JOIN Chesscom.Player PW ON PW.PlayerId = G.WhitePlayerId
	INNER JOIN Chesscom.Player PB ON PB.PlayerId = G.BlackPlayerId
WHERE PW.Username = @Username OR PB.Username = @Username
	AND G.IsRemoved = 0
);
GO