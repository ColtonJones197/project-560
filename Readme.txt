# Chess History Viewer
### Created by Tushar Lanka and Colton Jones

## HERE ARE THE FOLDERS OF THE PROJECT THAT MATTER:
 - [db](/db/) contains the TSQL scripts that are to be ran within the MSSQL Server 2022 database. Anything database related is in there.
 - [netapi](/netapi/) contains the rest api that we'll be using to communicate between the database and client. This is how we connect data to the database. If you want to use this you will need to change the connection strings, build and run the project, and visit the address the api is being hosted at in order to work with the api. You can also just use CURL or an api testing tool like insomnia but I wouldn't recommend that as swagger is much easier to use.
 - [view] this contains the razor application that we use as a view

SETTING UP THE DB: Like the demo john gave in class, I have set up a powershell script that allows you to run all the sql for the project and set up a database if you have permission to do that. It's located within the /db/ folder.

TABLES: Table declarations can be found within the /db/tables folder.

PROCEDURES/SQL OPERATIONS: Can be found in the /db/procedures/ folder. Be sure to run the function and table scripts before running the procedures.

DATA: In order to pull data from the web, you can use the project located within the /netapi/ folder or start with a clean database and populate it using the application

OTHER OBJECTS: Other SQL objects can be found within the /db/ folder. The sql for this project is mostly tables and procedures.

APPLICATION CODE: can be found within the /view/ folder