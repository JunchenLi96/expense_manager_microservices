CREATE TABLE [dbo].[Transactions]
   (
      [ID] INT  IDENTITY (1, 1) NOT NULL,
      [Description]   NVARCHAR (50)  NOT NULL,
      [CreatedAt]     NVARCHAR (50)  NOT NULL,
      [UpdatedAt]     NVARCHAR (50),
      [Type]          INT  NOT NULL,
      [Value]         DECIMAL(5,2)   NOT NULL, 
      PRIMARY KEY CLUSTERED ([ID] ASC), 
      [UserID]        INT 
      FOREIGN KEY     REFERENCES dbo.Users (ID)
        ON DELETE CASCADE
        ON UPDATE CASCADE
   )
;

GO