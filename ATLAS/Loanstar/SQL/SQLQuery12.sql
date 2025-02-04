USE [Sendero]
GO
/****** Object:  Table [dbo].[tbCountries]    Script Date: 08/28/2008 14:20:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbCountries](
	[CountryCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SettleDays] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
