IF SCHEMA_ID(N'Chesscom') IS NULL
    EXEC (N'CREATE SCHEMA [Chesscom];');
GO

DROP TABLE IF EXISTS Chesscom.PlayerRating;
GO

CREATE TABLE Chesscom.PlayerRating(
    RatingId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    PlayerId INT NOT NULL,
    Daily INT,
    [960Daily] INT,
    ChessRapid INT,
    ChessBullet INT,
    ChessBlitz INT,
    Tactics INT,
    Fide INT,
    CreatedOn DATETIMEOFFSET DEFAULT(SYSDATETIMEOFFSET()),
    UpdatedOn DATETIMEOFFSET DEFAULT(SYSDATETIMEOFFSET()),

    FOREIGN KEY (PlayerId) REFERENCES Chesscom.Player(PlayerId)
);
GO