USE [Sendero]
GO
/****** Object:  Table [dbo].[tbBooks]    Script Date: 08/28/2008 14:20:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbBooks](
	[BookGroup] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Book] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BookParent] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BookName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AddressLine1] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AddressLine2] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AddressLine3] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FaxNumber] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MarginBorrow] [float] NULL,
	[MarginLoan] [float] NULL,
	[MarkRoundHouse] [char](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MarkRoundInstitution] [char](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RateStockBorrow] [decimal](8, 5) NULL,
	[RateStockLoan] [decimal](8, 5) NULL,
	[RateBondBorrow] [decimal](8, 5) NULL,
	[RateBondLoan] [decimal](8, 5) NULL,
	[CountryCode] [char](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActUserId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActTime] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_tbBooks] PRIMARY KEY CLUSTERED 
(
	[BookGroup] ASC,
	[Book] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF