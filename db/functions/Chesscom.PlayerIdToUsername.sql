CREATE OR ALTER FUNCTION Chesscom.PlayerIdToUsername(
    @PlayerId INT
)
RETURNS NVARCHAR(128)
AS
BEGIN
    RETURN 
	(
		SELECT TOP(1) P.Username
		FROM Chesscom.Player P
		WHERE P.PlayerId = @PlayerId
	)
END;
GO
