CREATE TABLE [dbo].[Institutes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[MainPhone] [nvarchar](20) NULL,
	[Phones] [nvarchar](255) NULL,
	[City] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](10) NULL,
	[Address] [nvarchar](255) NULL,
	[InMailingList] [bit] NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Comments] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[Address_Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_Institutes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Institutes] ADD  CONSTRAINT [DF_Institutes_Name]  DEFAULT ('') FOR [Name]
GO

ALTER TABLE [dbo].[Institutes] ADD  CONSTRAINT [DF_Table_1_Institute_InMailingList]  DEFAULT ((0)) FOR [InMailingList]
GO

ALTER TABLE [dbo].[Institutes] ADD  CONSTRAINT [DF_Institutes_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO

