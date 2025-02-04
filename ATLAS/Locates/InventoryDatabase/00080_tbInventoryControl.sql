USE [Locates]
GO
/****** Object:  Table [dbo].[tbInventoryControl]    Script Date: 04/24/2009 14:50:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbInventoryControl](
	[SecId] [varchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Desk] [varchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Account] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BizDate] [datetime] NOT NULL,
	[ScribeTime] [datetime] NOT NULL,
 CONSTRAINT [PK_tbInventoryControl] PRIMARY KEY CLUSTERED 
(
	[SecId] ASC,
	[Desk] ASC,
	[Account] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF