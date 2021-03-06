USE [Sendero]
GO
/****** Object:  Table [dbo].[tbHolidays]    Script Date: 08/28/2008 14:21:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[tbHolidays](
	[HolidayDate] [datetime] NOT NULL,
	[CountryCode] [char](2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsBankHoliday] [bit] NOT NULL,
	[IsExchangeHoliday] [bit] NOT NULL,
	[IsHoliday] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT (1),
 CONSTRAINT [PK_tbHolidays] PRIMARY KEY CLUSTERED 
(
	[HolidayDate] ASC,
	[CountryCode] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF