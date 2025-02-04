USE [Locates]
GO
/****** Object:  Table [dbo].[tbInventorySubscriber]    Script Date: 04/24/2009 14:27:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbInventorySubscriber](
	[InventorySubscriberID] [bigint] IDENTITY(1,1) NOT NULL,
	[InventoryFilePatternID] [bigint] NULL,
	[Desk] [varchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SubscriptionTypeID] [int] NOT NULL CONSTRAINT [DF_tbInventorySubscriber_SubscriptionType]  DEFAULT ((1)),
	[FilePathName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileHost] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileUserName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FilePassword] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FileCheckTime] [datetime] NULL,
	[FileTime] [datetime] NULL,
	[FileStatus] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UsePgp] [bit] NULL,
	[LoadExtensionPgp] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MailAddress] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MailSubject] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BizDate] [datetime] NULL,
	[LoadTime] [datetime] NULL,
	[LoadCount] [int] NULL,
	[LoadStatus] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comment] [char](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActUserId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActTime] [datetime] NOT NULL,
	[IsBizDatePrior] [bit] NOT NULL CONSTRAINT [DF__tbInvento__IsBiz__634EBE90]  DEFAULT ((0)),
	[IsEnabled] [bit] NOT NULL,
	[IsRunning] [bit] NOT NULL CONSTRAINT [DF_tbInventorySubscriber_IsRunning]  DEFAULT ((0)),
 CONSTRAINT [PK_tbInventorySubscriber] PRIMARY KEY CLUSTERED 
(
	[InventorySubscriberID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [Locates]
GO
ALTER TABLE [dbo].[tbInventorySubscriber]  WITH CHECK ADD  CONSTRAINT [FK_tbInventorySubscriber_tbInventoryFilePatterns] FOREIGN KEY([InventoryFilePatternID])
REFERENCES [dbo].[tbInventoryFilePatterns] ([InventoryFilePatternID])
GO
ALTER TABLE [dbo].[tbInventorySubscriber]  WITH CHECK ADD  CONSTRAINT [FK_tbInventorySubscriber_tbInventorySubsciptionTypes] FOREIGN KEY([SubscriptionTypeID])
REFERENCES [dbo].[tbInventorySubscriptionTypes] ([InventorySubscriptionTypeID])
GO
ALTER TABLE [dbo].[tbInventorySubscriber]  WITH CHECK ADD  CONSTRAINT [FK_tbInventorySubscriberList_tbDesks] FOREIGN KEY([Desk])
REFERENCES [dbo].[tbDesks] ([Desk])
ON UPDATE CASCADE