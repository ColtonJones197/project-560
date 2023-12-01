CREATE OR ALTER PROCEDURE Chesscom.RemoveGame
    @Username NVARCHAR(128)
AS
BEGIN
UPDATE Chesscom.Player P
SET P.IsRemoved = 1,
    P.UpdatedOn = SYSDATETIMEOFFSET()
WHERE P.Username = @Username
END;
GO