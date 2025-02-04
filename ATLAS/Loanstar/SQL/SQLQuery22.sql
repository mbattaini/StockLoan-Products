USE [Sendero]
GO
/****** Object:  Table [dbo].[tbUserRoles]    Script Date: 08/28/2008 14:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbUserRoles](
	[UserId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Comment] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActUserId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActTime] [datetime] NOT NULL,
 CONSTRAINT [PK_tbUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleCode] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [Sendero]
GO
ALTER TABLE [dbo].[tbUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_tbUserRoles_tbRoles] FOREIGN KEY([RoleCode])
REFERENCES [dbo].[tbRoles] ([RoleCode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbUserRoles]  WITH NOCHECK ADD  CONSTRAINT [FK_tbUserRoles_tbUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[tbUsers] ([UserId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbUserRoles] CHECK CONSTRAINT [FK_tbUserRoles_tbUsers]