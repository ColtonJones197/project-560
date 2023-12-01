CREATE OR ALTER PROCEDURE Chesscom.RemoveGame
    @GameUrl NVARCHAR(128)
AS
BEGIN
UPDATE Chesscom.Game G
SET G.IsRemoved = 1,
    G.UpdatedOn = SYSDATETIMEOFFSET()
WHERE G.GameUrl = @GameUrl 
END;
GO