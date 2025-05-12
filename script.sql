CREATE DATABASE JuanLogDB;
GO

USE [JuanLogDB]
GO

CREATE TABLE [dbo].Users(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar] (50),
	[Permission] [int],
	[HashedPassword] [varchar](44),
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

INSERT INTO [Users] VALUES ('Juan', 3, 'OyugfiY7LDdYSNFkQTJqwgCJ4ZmfkzL2jRZhk6i13QI=');

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
	[ExerciseName] [varchar] (100) NOT NULL,
	[CategoryId] [int] NOT NULL,
	CONSTRAINT [PK_Exercises] PRIMARY KEY CLUSTERED ([ExerciseName])
)
GO

INSERT INTO [Exercises] VALUES (1, 'lateral raises');
INSERT INTO [Exercises] VALUES (4, 'hanging knee ups');
INSERT INTO [Exercises] VALUES (3, 'platform pullups');
GO

CREATE TABLE [dbo].ExerciseEntries(
	[EntryId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ExerciseName] [varchar] (100) NOT NULL,
	[When] [Date] NOT NULL,
	[Weight] [int],
	CONSTRAINT [PK_ExerciseEntries] PRIMARY KEY CLUSTERED ([EntryId] ASC),
	FOREIGN KEY ([ExerciseName]) REFERENCES Exercises([ExerciseName]),
)
GO

INSERT INTO [ExerciseEntries] VALUES (1, 'lateral raises', '1900-01-01', 1);
INSERT INTO [ExerciseEntries] VALUES (1, 'hanging knee ups', CURRENT_TIMESTAMP, 2);
INSERT INTO [ExerciseEntries] VALUES (3, 'lateral raises', CURRENT_TIMESTAMP, 5);
GO

CREATE TABLE [dbo].SetTable(
	[SetId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[EntryId] [int] NOT NULL,
	[Repetitions] [int],
	FOREIGN KEY ([EntryId]) REFERENCES ExerciseEntries([EntryId]),
)