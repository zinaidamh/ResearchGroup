CREATE TABLE [dbo].[Events] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [SubType_ID]   INT            CONSTRAINT [DF_tblEvents_EventSubType_ID] DEFAULT ((0)) NOT NULL,
    [FirstDate]    DATE           NULL,
    [Person_ID]    INT            NULL,
    [Institute_ID] INT            NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [AlertDate]    DATE           NULL,
    [AlertDone]    BIT            CONSTRAINT [DF_tblEvents_Event_AlertDone] DEFAULT ((0)) NOT NULL,
    [CreatedAt]    DATETIME       CONSTRAINT [DF_tblEvents_Event_CreatedDate] DEFAULT (getdate()) NOT NULL, 
	[Agent_ID] [int] NULL,
	[ExpirationDate] [date] NULL,
	[FileName] [nvarchar](50) NULL,
	[FileHref] [nvarchar](max) NULL,
	[SiteName] [nvarchar](50) NULL,
    CONSTRAINT [FK_Events_ToPerson] FOREIGN KEY ([Person_ID]) REFERENCES [Persons]([ID]), 
    CONSTRAINT [FK_Events_ToSubTypes] FOREIGN KEY ([SubType_ID]) REFERENCES [EventSubTypes]([ID]), 
    CONSTRAINT [FK_Events_ToInstitute] FOREIGN KEY ([Institute_ID]) REFERENCES [Institutes]([ID]), 
    CONSTRAINT [FK_Events_ToAgents] FOREIGN KEY ([Agent_ID]) REFERENCES [Agents]([ID]), 
    CONSTRAINT [PK_Events] PRIMARY KEY ([ID])
);

