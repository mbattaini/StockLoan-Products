USE [Sendero]
GO
/****** Object:  Table [dbo].[tbUsers]    Script Date: 08/28/2008 14:21:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbUsers](
	[UserId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ShortName] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Password] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Group] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Comment] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActUserId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActTime] [datetime] NOT NULL,
	[UsageCount] [bigint] NOT NULL CONSTRAINT [DF_tbUsers_AccessCount]  DEFAULT (0),
	[LastAccess] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_tbUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF