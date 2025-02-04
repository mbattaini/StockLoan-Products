USE [Sendero]
GO
/****** Object:  Table [dbo].[tbDeals]    Script Date: 08/28/2008 14:20:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbDeals](
	[DealId] [char](16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BookGroup] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DealType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Book] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BookContact] [varchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ContractId] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecId] [varchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Quantity] [bigint] NULL,
	[Amount] [money] NULL,
	[CollateralCode] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValueDate] [datetime] NULL,
	[SettleDate] [datetime] NULL,
	[TermDate] [datetime] NULL,
	[Rate] [decimal](8, 5) NULL,
	[RateCode] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PoolCode] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DivRate] [decimal](6, 3) NULL,
	[DivCallable] [bit] NULL,
	[IncomeTracked] [bit] NULL,
	[MarginCode] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Margin] [decimal](5, 2) NULL,
	[CurrencyIso] [char](3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityDepot] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CashDepot] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comment] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DealStatus] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_tbDeals_DealStatus]  DEFAULT ('D'),
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tbDeals_IsActive]  DEFAULT (1),
	[ActUserId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_tbDeals_ActUserId]  DEFAULT ('ADMIN'),
	[ActTime] [datetime] NOT NULL CONSTRAINT [DF_tbDeals_ActTime]  DEFAULT (getutcdate()),
 CONSTRAINT [PK_tbDeals] PRIMARY KEY CLUSTERED 
(
	[DealId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF