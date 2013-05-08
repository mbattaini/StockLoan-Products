USE [Sendero]
GO
/****** Object:  Table [dbo].[tbKeyValues]    Script Date: 08/28/2008 14:21:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[tbKeyValues](
	[KeyId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[KeyValue] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_tbKeyValues] PRIMARY KEY CLUSTERED 
(
	[KeyId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF