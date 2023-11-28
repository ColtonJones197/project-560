CREATE OR ALTER PROCEDURE Chesscom.RetrievePlayers

AS
SELECT P.PlayerId, P.[Username], P.ChesscomId, P.Avatar, P.Title, P.[Status], P.[Name]
FROM Chesscom.Player P
ORDER BY P.[Username] ASC
GO