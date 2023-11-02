CREATE OR ALTER PROCEDURE Chesscom.GetRatingsByUsername
	@Username NVARCHAR(128)
AS

SELECT 
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
WHERE P.Username = @Username;
GO