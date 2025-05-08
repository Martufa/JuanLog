CREATE DATABASE JuanLogDB;
GO

USE [JuanLogDB]
GO

CREATE TABLE [dbo].Users(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar] (50),
	[Permission] [int],
	[HashedPassword] [int],
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

INSERT INTO [Users] VALUES ('Juan', 3);
INSERT INTO [Users] VALUES ('Martufka', 3);
INSERT INTO [Users] VALUES ('Eliška', 1);
GO

CREATE TABLE [dbo].ExerciseCategories(
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50),
	CONSTRAINT [PK_ExerciseCategories] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
)
GO


INSERT INTO [ExerciseCategories] VALUES ('Nohy');
INSERT INTO [ExerciseCategories] VALUES ('Ruce');
INSERT INTO [ExerciseCategories] VALUES ('Záda');
INSERT INTO [ExerciseCategories] VALUES ('Bøicho');
GO

CREATE TABLE [dbo].Exercises(
	[ExerciseId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [varchar] (100),
	CONSTRAINT [PK_Exercises] PRIMARY KEY CLUSTERED ([ExerciseId] ASC)
)
GO

INSERT INTO [Exercises] VALUES (1, 'lateral raises');
INSERT INTO [Exercises] VALUES (4, 'hanging knee ups');
INSERT INTO [Exercises] VALUES (3, 'platform pullups');
GO

CREATE TABLE [dbo].ExerciseEntries(
	[EntryId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UserId] [int] NOT NULL,
	[ExerciseId] [int] NOT NULL, 
	[When] [Date] NOT NULL,
)
GO

INSERT INTO [ExerciseEntries] VALUES (1, 1, '1900-01-01');
INSERT INTO [ExerciseEntries] VALUES (1, 2, CURRENT_TIMESTAMP);
INSERT INTO [ExerciseEntries] VALUES (3, 3, CURRENT_TIMESTAMP);
GO

CREATE TABLE [dbo].Reps(
	[RepId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[EntryId] [int] NOT NULL,
	FOREIGN KEY ([EntryId]) REFERENCES ExerciseEntries([EntryId]),
)