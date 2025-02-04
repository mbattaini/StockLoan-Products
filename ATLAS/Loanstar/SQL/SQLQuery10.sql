USE [Sendero]
GO
/****** Object:  Table [dbo].[tbBooksClearingInstructions]    Script Date: 08/28/2008 14:20:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbBooksClearingInstructions](
	[BookGroup] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Book] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CountryCode] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CurrencyCode] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DivRate] [decimal](12, 5) NOT NULL,
	[CashInstructions] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityInstructions] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActUserId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActTime] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_tbBooksClearingInstructions] PRIMARY KEY CLUSTERED 
(
	[BookGroup] ASC,
	[Book] ASC,
	[CountryCode] ASC,
	[CurrencyCode] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF