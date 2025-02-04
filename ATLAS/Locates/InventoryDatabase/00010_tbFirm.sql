USE [Locates]
GO
/****** Object:  Table [dbo].[tbFirms]    Script Date: 04/24/2009 14:15:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbFirms](
	[FirmCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Firm] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
 CONSTRAINT [PK_tbFirms] PRIMARY KEY CLUSTERED 
(
	[FirmCode] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF