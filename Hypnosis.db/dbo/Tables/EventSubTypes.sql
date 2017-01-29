CREATE TABLE [dbo].[EventSubTypes] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Type_ID]      INT            CONSTRAINT [DF_tblEventSubTypes_EventType_ID] DEFAULT ((0)) NOT NULL,
    [SubType_Name] NVARCHAR (255) CONSTRAINT [DF_tblEventSubTypes_EventSubType_Name] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_EventSubTypes] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_EventSubTypes_ToTypes] FOREIGN KEY ([Type_ID]) REFERENCES [EventTypes]([ID])
);

