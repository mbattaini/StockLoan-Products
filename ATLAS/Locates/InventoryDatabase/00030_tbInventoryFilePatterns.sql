USE [Locates]
GO
/****** Object:  Table [dbo].[tbInventoryFilePatterns]    Script Date: 04/24/2009 14:27:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbInventoryFilePatterns](
	[InventoryFilePatternID] [bigint] IDENTITY(1,1) NOT NULL,
	[Desk] [varchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[HeaderRegEx] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DataRegEx] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrailerRegEx] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccountRegEx] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateRegEx] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RowCountRegEx] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_tbInventoryFilePatterns] PRIMARY KEY CLUSTERED 
(
	[InventoryFilePatternID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [Locates]
GO
ALTER TABLE [dbo].[tbInventoryFilePatterns]  WITH CHECK ADD  CONSTRAINT [FK_tbInventoryFileDataMask_tbDesks] FOREIGN KEY([Desk])
REFERENCES [dbo].[tbDesks] ([Desk])
ON UPDATE CASCADE