USE [Locates]
GO
/****** Object:  Table [dbo].[tbInventorySubscriptionTypes]    Script Date: 04/07/2009 10:58:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbInventorySubscriptionTypes](
	[InventorySubscriptionTypeID] [int] IDENTITY(1,1) NOT NULL,
	[InventorySubscriptionTypeName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InventorySubscriptionTypeDescription] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_tbInventorySubsciptionTypes] PRIMARY KEY CLUSTERED 
(
	[InventorySubscriptionTypeID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF