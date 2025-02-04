USE [Sendero]
GO
/****** Object:  Table [dbo].[tbBookContacts]    Script Date: 08/28/2008 14:20:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbBookContacts](
	[BookGroup] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Book] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FirstName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LastName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Function] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FaxNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comment] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActUserId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActTime] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_tbBookContacts] PRIMARY KEY CLUSTERED 
(
	[BookGroup] ASC,
	[Book] ASC,
	[FirstName] ASC,
	[LastName] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF