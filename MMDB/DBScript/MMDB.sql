﻿CREATE TABLE Actor
(
ActorId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
ActorName VARCHAR(255) NOT NULL,
Gender BIT NOT NULL,
DateOfBirth DATE,
Bio VARCHAR(1000)
);

CREATE TABLE Producer
(
ProducerId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
ProducerName VARCHAR(255) NOT NULL,
Gender BIT NOT NULL,
DateOfBirth DATE,
Bio VARCHAR(1000)
);

CREATE TABLE Movie
(
MovieId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
MovieName VARCHAR(255) NOT NULL,
YearOfRelease INT NOT NULL,
Plot VARCHAR(1000),
Poster VARCHAR(255),
ProducerId INT REFERENCES Producer(ProducerId) NOT NULL,
);

CREATE TABLE Castings
(
MovieId INT REFERENCES Movie(MovieId) NOT NULL,
ActorId INT REFERENCES Actor(ActorId) NOT NULL,
PRIMARY KEY (MovieId, ActorId)
);