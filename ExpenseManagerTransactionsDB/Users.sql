CREATE TABLE [dbo].[Users] (
    [ID]           INT  IDENTITY (1, 1) NOT NULL,
    [UserName]     NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO