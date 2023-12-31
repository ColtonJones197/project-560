IF SCHEMA_ID(N'Chesscom') IS NULL
    EXEC (N'CREATE SCHEMA [Chesscom];');
GO

DROP TABLE IF EXISTS Chesscom.Country;
GO

CREATE TABLE Chesscom.Country(
    CountryId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(128) NOT NULL UNIQUE,
    CountryCode INT NOT NULL UNIQUE,
    CreatedOn DATETIMEOFFSET NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
    UpdatedOn DATETIMEOFFSET NOT NULL DEFAULT(SYSDATETIMEOFFSET())
);
GO