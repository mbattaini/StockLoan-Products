USE [Sendero]
GO
/****** Object:  Table [dbo].[tbRoleFunctions]    Script Date: 08/28/2008 14:21:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbRoleFunctions](
	[RoleCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FunctionPath] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MayView] [bit] NOT NULL,
	[MayEdit] [bit] NULL,
	[BookGroupList] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Comment] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ActUserId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActTime] [datetime] NOT NULL,
 CONSTRAINT [PK_tbRoleFunctions] PRIMARY KEY CLUSTERED 
(
	[RoleCode] ASC,
	[FunctionPath] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [Sendero]
GO
ALTER TABLE [dbo].[tbRoleFunctions]  WITH CHECK ADD  CONSTRAINT [FK_tbRoleFunctions_tbFunctions] FOREIGN KEY([FunctionPath])
REFERENCES [dbo].[tbFunctions] ([FunctionPath])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tbRoleFunctions]  WITH CHECK ADD  CONSTRAINT [FK_tbRoleFunctions_tbRoles] FOREIGN KEY([RoleCode])
REFERENCES [dbo].[tbRoles] ([RoleCode])
ON UPDATE CASCADE
ON DELETE CASCADE