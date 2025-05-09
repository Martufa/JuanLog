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
	[EntryId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ExerciseId] [int] NOT NULL,
	[When] [Date] NOT NULL,
	[Weight] [int],
	CONSTRAINT [PK_ExerciseEntries] PRIMARY KEY CLUSTERED ([EntryId] ASC),
	FOREIGN KEY ([ExerciseId]) REFERENCES Exercises([ExerciseId]),
)
GO

INSERT INTO [ExerciseEntries] VALUES (1, 1, '1900-01-01');
INSERT INTO [ExerciseEntries] VALUES (1, 2, CURRENT_TIMESTAMP);
INSERT INTO [ExerciseEntries] VALUES (3, 3, CURRENT_TIMESTAMP);
GO

CREATE TABLE [dbo].SetTable(
	[SetId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[EntryId] [int] NOT NULL,
	[Repetitions] [int],
	FOREIGN KEY ([EntryId]) REFERENCES ExerciseEntries([EntryId]),
)