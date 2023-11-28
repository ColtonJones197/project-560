CREATE OR ALTER PROCEDURE Chesscom.GetCurrentRatingByUsername
	@Username NVARCHAR(128)
AS

SELECT TOP(1)
	PR.RatingId, 
	PR.PlayerId, 
	PR.Daily, 
	PR.[960Daily], 
	PR.ChessBlitz, 
	PR.ChessRapid, 
	PR.ChessBullet, 
	PR.Tactics, 
	PR.Fide,
	PR.CreatedOn
FROM Chesscom.Player P
	INNER JOIN Chesscom.PlayerRating PR ON PR.PlayerId = P.PlayerId
WHERE P.Username = @Username
ORDER BY PR.CreatedOn ASC, PR.RatingId DESC;
GO