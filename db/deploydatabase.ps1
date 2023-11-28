Param(
   [string] $Server = "(localdb)\MSSQLLocalDb",
   [string] $Database = "chessdb"
)

# This script requires the SQL Server module for PowerShell.
# The below commands may be required.

# To check whether the module is installed.
# Get-Module -ListAvailable -Name SqlServer;

# Install the SQL Server Module
# Install-Module -Name SqlServer -Scope CurrentUser

$CurrentDrive = (Get-Location).Drive.Name + ":"

Write-Host ""
Write-Host "Rebuilding database $Database on $Server..."

<#
   If on your local machine, you can drop and re-create the database.
#>
& ".\Scripts\DropDatabase.ps1" -Database $Database
& ".\Scripts\CreateDatabase.ps1" -Database $Database

<#
   If on the department's server, you don't have permissions to drop or create databases.
   In this case, maintain a script to drop all tables.
#>
#Write-Host "Dropping tables..."
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Tables\DropTables.sql"

Write-Host "Creating schema..."
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "schemas\Chesscom.sql"

Write-Host "Creating tables..."
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "tables\Chesscom.Tournament.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "tables\Chesscom.Player.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "tables\Chesscom.PlayerRating.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "tables\Chesscom.Game.sql"

Write-Host "Functions..."
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "functions\Chesscom.PlayerIdToUsername.sql"

Write-Host "Stored procedures..."
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "procedures\Chesscom.CreateGame.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "procedures\Chesscom.CreatePlayer.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "procedures\Chesscom.CreatePlayerRating.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "procedures\Chesscom.GetCurrentRatingByUsername.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "procedures\Chesscom.GetGamesByUser.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "procedures\Chesscom.GetRatingsByUsername.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "procedures\Chesscom.RetrievePlayers.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "procedures\Chesscom.WinRateByPlayer.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "procedures\Chesscom.PlayerWinRatesByOpening.sql"

Write-Host "Inserting data..."
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Data\Person.AddressType.sql"

Write-Host "Rebuild completed."
Write-Host ""

Set-Location $CurrentDrive
