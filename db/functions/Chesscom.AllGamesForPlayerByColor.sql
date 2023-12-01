CREATE FUNCTION Chesscom.AllGamesForPlayerByColor(@Username NVARCHAR(128))
RETURNS TABLE
AS
RETURN(
SELECT IIF(PW.Username = @Username, N'W', N'B') AS UserColor, G.*
FROM Chesscom.Game G
	INNER JOIN Chesscom.Player PW ON PW.PlayerId = G.WhitePlayerId
	INNER JOIN Chesscom.Player PB ON PB.PlayerId = G.BlackPlayerId
WHERE PW.Username = @Username OR PB.Username = @Username
);
GO