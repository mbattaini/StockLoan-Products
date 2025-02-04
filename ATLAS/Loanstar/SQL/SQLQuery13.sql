USE [Sendero]
GO
/****** Object:  Table [dbo].[tbCurrencies]    Script Date: 08/28/2008 14:20:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbCurrencies](
	[CurrencyCode] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Currency] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_tbCurrencies] PRIMARY KEY CLUSTERED 
(
	[CurrencyCode] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF