USE [Sendero]
GO
/****** Object:  Table [dbo].[tbFunctions]    Script Date: 08/28/2008 14:20:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbFunctions](
	[FunctionPath] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MayView] [bit] NOT NULL,
	[MayEdit] [bit] NULL,
	[BookGroupList] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_tbFunctions] PRIMARY KEY CLUSTERED 
(
	[FunctionPath] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF