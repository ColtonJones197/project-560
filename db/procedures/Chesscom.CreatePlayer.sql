DROP PROCEDURE IF EXISTS Chesscom.CreatePlayer
GO

CREATE OR ALTER PROCEDURE Chesscom.CreatePlayer
@Username NVARCHAR(128),
@ChesscomId INT,
@Avatar NVARCHAR(128) = NULL,
@Title NVARCHAR(8) = NULL,
@Status NVARCHAR(32) = NULL,
@Name NVARCHAR(128) = NULL,
@PlayerId INT OUTPUT

AS
INSERT Chesscom.Player(Username, ChesscomId, Avatar, Title, [Status], [Name])
VALUES(@Username, @ChesscomId, @Avatar, @Title, @Status, @Name); 

SET @PlayerId = SCOPE_IDENTITY();
GO