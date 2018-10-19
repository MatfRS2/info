Kreiranje korisnika (izvrsava se u master database)

CREATE LOGIN student2 WITH PASSWORD = 'Student_2018';  

Ctrl + Shift + E


----------------------------------------------------------------------------------------------

EXEC sp_helpsrvrolemember 'dbcreator'; 
ALTER SERVER ROLE  dbcreator ADD MEMBER student2; 


----------------------------------------------------------------------------------------------


-- Create a new database called 'Horor_filmovi'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'Horor_filmovi'
)
CREATE DATABASE Horor_filmovi
GO

SELECT Name FROM sys.Databases -- vide se baze

USE MyHorrorDB
CREATE TABLE Movies (MovieId INT, Name NVARCHAR(255), Year INT)
INSERT INTO Movies VALUES (1, 'Halloween', 1978);
INSERT INTO Movies VALUES (2, 'Psycho', 1960);
INSERT INTO Movies VALUES (3, 'The Texas Chainsaw Massacre', 1974);
INSERT INTO Movies VALUES (4, 'The Exorcist', 1973);
INSERT INTO Movies VALUES (5, 'Night of the Living Dead', 1968);
GO

F1 -- sql -- use Database HororFilmovi

SELECT * FROM Movies


----------------------------------------------------------------------------------------------

Tipovi podataka u bazi:
http://www-db.deis.unibo.it/courses/TW/DOCS/w3schools/sql/sql_datatypes_general.asp.html

----------------------------------------------------------------------------------------------
Osnovne operacije nad bazom
https://medium.freecodecamp.org/how-to-perform-crud-operations-with-asp-net-core-using-vs-code-and-ado-net-b12404aef708
