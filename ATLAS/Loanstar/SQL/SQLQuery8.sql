USE [Sendero]
GO
/****** Object:  Table [dbo].[tbBookGroups]    Script Date: 08/28/2008 14:20:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbBookGroups](
	[BizDate] [datetime] NOT NULL,
	[BookGroup] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BookGroupName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FundingRate] [decimal](5, 3) NOT NULL,
	[DayCount] [smallint] NOT NULL,
 CONSTRAINT [PK_tbBookGroups] PRIMARY KEY CLUSTERED 
(
	[BizDate] ASC,
	[BookGroup] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF