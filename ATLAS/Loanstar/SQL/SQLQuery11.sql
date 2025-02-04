USE [Sendero]
GO
/****** Object:  Table [dbo].[tbContracts]    Script Date: 08/28/2008 14:20:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbContracts](
	[BizDate] [datetime] NOT NULL,
	[BookGroup] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContractId] [varchar](16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContractType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Book] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SecId] [varchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Quantity] [bigint] NOT NULL,
	[QuantitySettled] [bigint] NOT NULL,
	[Amount] [money] NOT NULL,
	[AmountSettled] [money] NOT NULL,
	[CollateralCode] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ValueDate] [datetime] NULL,
	[SettleDate] [datetime] NULL,
	[TermDate] [datetime] NULL,
	[Rate] [decimal](8, 5) NOT NULL,
	[RateCode] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StatusFlag] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PoolCode] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DivRate] [decimal](6, 3) NOT NULL,
	[DivCallable] [bit] NOT NULL,
	[IncomeTracked] [bit] NOT NULL,
	[MarginCode] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Margin] [decimal](5, 2) NOT NULL,
	[CurrencyIso] [char](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SecurityDepot] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CashDepot] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OtherBook] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Comment] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_tbContracts] PRIMARY KEY CLUSTERED 
(
	[BizDate] ASC,
	[BookGroup] ASC,
	[ContractId] ASC,
	[ContractType] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF