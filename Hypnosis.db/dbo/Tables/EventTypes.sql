CREATE TABLE [dbo].[EventTypes] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Type_Name]     NVARCHAR (255) CONSTRAINT [DF_tblEventTypes_EventType_Name] DEFAULT ('') NOT NULL,
    [Type_Category] INT        CONSTRAINT [DF_tblEventTypes_EventType_Category] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_EventTypes] PRIMARY KEY CLUSTERED ([ID] ASC), 
    CONSTRAINT [FK_EventType_Categories] FOREIGN KEY ([Type_Category]) REFERENCES [dbo].[EventTypeCategories] ([ID]), 
    
);


