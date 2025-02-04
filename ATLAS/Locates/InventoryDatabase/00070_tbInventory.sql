USE [Locates]
GO
/****** Object:  Table [dbo].[tbInventory]    Script Date: 04/24/2009 14:48:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbInventory](
	[BizDate] [datetime] NOT NULL,
	[Desk] [varchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SecId] [varchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Account] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModeCode] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Quantity] [bigint] NOT NULL,
	[ImportExecutionID] [bigint] NULL,
 CONSTRAINT [PK_tbInventory_1] PRIMARY KEY CLUSTERED 
(
	[BizDate] ASC,
	[Desk] ASC,
	[SecId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF