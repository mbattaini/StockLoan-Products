USE [Locates]
GO
/****** Object:  Table [dbo].[tbInventoryImportExecution]    Script Date: 04/24/2009 14:11:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInventoryImportExecution](
	[InventoryImportExecutionID] [bigint] IDENTITY(1,1) NOT NULL,
	[SubscriberID] [bigint] NOT NULL,
	[FileTime] [datetime] NULL,
	[ExecutionTime] [datetime] NOT NULL,
	[ExecutionStatus] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExecutionRecordsImported] [int] NULL CONSTRAINT [DF_tbInventoryImportExecution_ExecutionRecordsImported]  DEFAULT ((0)),
	[ExecutionHost] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_tbInventoryImportExecution] PRIMARY KEY CLUSTERED 
(
	[InventoryImportExecutionID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [Locates]
GO
ALTER TABLE [dbo].[tbInventoryImportExecution]  WITH CHECK ADD  CONSTRAINT [FK_tbInventoryImportExecution_tbInventorySubscriber] FOREIGN KEY([SubscriberID])
REFERENCES [dbo].[tbInventorySubscriber] ([InventorySubscriberID])