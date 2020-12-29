GO

CREATE TABLE Post(
    Id int NOT NULL IDENTITY,
    UserId int NOT NULL,
    Content text NOT NULL,
    Picture text NOT NULL,
    CreatedAt timestamp NOT NULL DEFAULT ((now())),
    CONSTRAINT PK_Post PRIMARY KEY (Id),
);
GO

CREATE TABLE Comment(
    Id int NOT NULL IDENTITY,
    UserId int NOT NULL,
    PostId int NOT NULL,
    CreatedAt timestamp NOT NULL DEFAULT ((now())),
    Content text NOT NULL,
    CONSTRAINT PK_Comment PRIMARY KEY (Id),
    CONSTRAINT Fk_Comment_Post FOREIGN KEY (PostId) REFERENCES Post (Id) ON DELETE CASCADE,
);