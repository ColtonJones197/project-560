DROP PROCEDURE IF EXISTS Chesscom.CreatePlayerRating
GO

CREATE OR ALTER PROCEDURE Chesscom.CreatePlayerRating
@PlayerId INT,
@Daily INT = NULL,
@Daily960 INT = NULL,
@ChessRapid INT = NULL,
@ChessBullet INT = NULL,
@ChessBlitz INT = NULL,
@Tactics INT = NULL,
@Fide INT = NULL,
@RatingId INT OUTPUT

AS
INSERT Chesscom.PlayerRating(PlayerId, Daily, [960Daily], ChessRapid, ChessBullet, ChessBlitz, Tactics, Fide)
VALUES(@PlayerId, @Daily, @Daily960, @ChessRapid, @ChessBullet, @ChessBlitz, @Tactics, @Fide); 

SET @RatingId = SCOPE_IDENTITY();
GO