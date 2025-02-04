USE [Locates]
GO
/****** Object:  Table [dbo].[tbDesks]    Script Date: 04/07/2009 10:54:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbDesks](
	[Desk] [varchar](12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FirmCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CountryCode] [char](2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DeskTypeCode] [varchar](3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_tbDesks] PRIMARY KEY CLUSTERED 
(
	[Desk] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [Locates]
GO
ALTER TABLE [dbo].[tbDesks]  WITH CHECK ADD  CONSTRAINT [FK_tbDesks_tbCountries] FOREIGN KEY([CountryCode])
REFERENCES [dbo].[tbCountries] ([CountryCode])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbDesks]  WITH CHECK ADD  CONSTRAINT [FK_tbDesks_tbDeskTypes] FOREIGN KEY([DeskTypeCode])
REFERENCES [dbo].[tbDeskTypes] ([DeskTypeCode])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tbDesks]  WITH CHECK ADD  CONSTRAINT [FK_tbDesks_tbFirms] FOREIGN KEY([FirmCode])
REFERENCES [dbo].[tbFirms] ([FirmCode])
ON UPDATE CASCADE