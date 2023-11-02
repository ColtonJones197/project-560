/* 
    I added this view for 2 reasons:
    1. I wanted to remember indexes and how they work, and needed a reason to make one
    2. This will improve performance as it is the primary way that the application will be accessing the rating table.
*/

IF OBJECT_ID(N'Chesscom.PlayerRatingCurrent', N'view') IS NOT NULL
    DROP VIEW Chesscom.PlayerRatingCurrent;
GO
CREATE VIEW Chesscom.PlayerRatingCurrent
WITH SCHEMABINDING
AS
    SELECT TOP(1) P.RatingId, 
        P.PlayerId, 
        P.Daily, 
        P.[960Daily], 
        P.ChessRapid, 
        P.ChessBlitz, 
        P.Tactics, 
        P.Fide, 
        P.CreatedOn, 
        P.UpdatedOn
    FROM Chesscom.PlayerRating P
    ORDER BY P.CreatedOn DESC;
GO

DROP INDEX IF EXISTS IX_PlayerId_CreatedOn;

CREATE UNIQUE CLUSTERED INDEX IX_PlayerId_CreatedOn
    ON Chesscom.PlayerRatingCurrent (PlayerId, CreatedOn);
GO
