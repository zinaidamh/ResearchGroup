﻿CREATE TABLE [dbo].[Agents](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Agent_Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Agents] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Agents] ADD  CONSTRAINT [DF_Agents_Agent_Name]  DEFAULT ('') FOR [Agent_Name]
GO

