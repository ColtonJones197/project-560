IF NOT EXISTS
   (
      SELECT *
      FROM sys.schemas s
      WHERE s.[name] = N'Chesscom'
   )
BEGIN
   EXEC(N'CREATE SCHEMA [Chesscom] AUTHORIZATION [dbo]');
END;
