Use [Sendero]
Go

if not exists (select * from dbo.sysusers where name = N'roleClient')
	EXEC sp_addrole N'roleClient'
GO

GRANT  EXECUTE  ON [dbo].[spKeyValueSet]  TO [roleClient]
GO

GRANT  EXECUTE  ON [dbo].[spKeyValueGet]  TO [roleClient]
GO


if not exists (select * from syscolumns where name = 'BookGroupList' And id = (select id from sysobjects where name = 'tbFunctions'))
	Alter Table tbFunctions Add BookGroupList varchar (50) Null
GO

Update	tbFunctions
Set	BookGroupList = '[NONE]'	
Where	FunctionPath In
		(
		'AdminBooks',
		'PositionBoxSummary',
		'PositionContractBlotter',
		'PositionDealBlotter',
		'PositionOpenContracts',
		'PositionRecalls'
		)

Select	*
Into	#RoleFunctions
From	tbRoleFunctions

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tgFunctionsInsert]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[tgFunctionsInsert]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tgFunctionInsert]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[tgFunctionInsert]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tgRolesInsert]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[tgRolesInsert]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tgRoleInsert]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[tgRoleInsert]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbRoleFunctions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbRoleFunctions]
GO

CREATE TABLE [dbo].[tbRoleFunctions] (
	[RoleCode] [varchar] (5) NOT NULL ,
	[FunctionPath] [varchar] (50) NOT NULL ,
	[MayView] [bit] NOT NULL ,
	[MayEdit] [bit] NULL ,
	[BookGroupList] [varchar] (100) NULL ,
	[Comment] [varchar] (50) NULL ,
	[ActUserId] [varchar] (50) NOT NULL ,
	[ActTime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbRoleFunctions] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbRoleFunctions] PRIMARY KEY  CLUSTERED 
	(
		[RoleCode],
		[FunctionPath]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tbRoleFunctions] ADD 
	CONSTRAINT [FK_tbRoleFunctions_tbFunctions] FOREIGN KEY 
	(
		[FunctionPath]
	) REFERENCES [dbo].[tbFunctions] (
		[FunctionPath]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_tbRoleFunctions_tbRoles] FOREIGN KEY 
	(
		[RoleCode]
	) REFERENCES [dbo].[tbRoles] (
		[RoleCode]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE TRIGGER [tgFunctionInsert] ON [dbo].[tbFunctions] 
For Insert

As
Begin

Insert	tbRoleFunctions
	Select	RoleCode, FunctionPath, MayView, MayEdit, BookGroupList, Null, ActUserId, ActTime 
	From	inserted,
		tbRoles

Update	tbRoleFunctions
Set	MayView = 1,
	MayEdit = Case When MayEdit Is Null Then Null Else 1 End,
	BookGroupList = Case When BookGroupList Is Null Then Null Else '[ALL]' End
Where	RoleCode = 'ADMIN'

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE TRIGGER [tgRoleInsert] ON dbo.tbRoles 
For Insert
As

Begin

Insert	tbRoleFunctions
	Select	RoleCode, FunctionPath, MayView, MayEdit, BookGroupList, Null, ActUserId, ActTime
	From	inserted,
		tbFunctions

Update	tbRoleFunctions
Set	MayView = 1,
	MayEdit = Case When MayEdit Is Null Then Null Else 1 End,
	BookGroupList = Case When BookGroupList Is Null Then Null Else '[ALL]' End
Where	RoleCode = 'ADMIN'

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

Insert	tbRoleFunctions
Select	RoleCode,
	FunctionPath,
	MayView,
	MayEdit,
	Null,
	Comment,
	ActUserId,
	ActTime 
From	#RoleFunctions

Drop Table	#RoleFunctions

Update	tbRoleFunctions
Set	BookGroupList = '[NONE]'	
Where	FunctionPath In
		(
		'AdminBooks',
		'PositionBoxSummary',
		'PositionContractBlotter',
		'PositionDealBlotter',
		'PositionOpenContracts',
		'PositionRecalls'
		)
And	BookGroupList Is Null
And	RoleCode != 'ADMIN'

Update	tbRoleFunctions
Set	BookGroupList = '[ALL]'	
Where	FunctionPath In
		(
		'AdminBooks',
		'PositionBoxSummary',
		'PositionContractBlotter',
		'PositionDealBlotter',
		'PositionOpenContracts',
		'PositionRecalls'
		)
And	BookGroupList Is Null
And	RoleCode = 'ADMIN'

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spUserViewEdit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spUserViewEdit]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spUserViewEdit
	@UserId varchar(50),
	@FunctionPath varchar(50),
	@MayView bit = 0 Output,
	@MayEdit bit = 0 Output
As
Begin

Set RowCount 1

Select		@MayView = IsNull(RF.MayView, 0)
From		tbUsers U,
		tbUserRoles UR,
		tbRoleFunctions RF
Where           U.UserId = @UserId
	And	U.IsActive = 1
	And	U.UserId = UR.UserId
	And	UR.RoleCode = RF.RoleCode
	And	RF.FunctionPath = @FunctionPath
Order By	1 Desc

If (@@RowCount = 0)
	Select	@MayView = 0

Select		@MayEdit = IsNull(RF.MayEdit, 0)
From		tbUsers U,
		tbUserRoles UR,
		tbRoleFunctions RF
Where		U.UserId = @UserId
	And	U.IsActive = 1
	And	U.UserId = UR.UserId
	And	UR.RoleCode = RF.RoleCode
	And	RF.FunctionPath = @FunctionPath
Order By	1 Desc

If @@RowCount = 0
	Select	@MayEdit = 0

Set RowCount 0

If ((@MayView = 1) Or (@MayEdit = 1))
	Update	tbUsers
	Set	UsageCount = UsageCount + 1,
		LastAccess = GetUtcDate()
	Where	UserId = @UserId

End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spUserViewEdit]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spUserViewEditBookGroup]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spUserViewEditBookGroup]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spUserViewEditBookGroup
	@UserId varchar(50),
	@FunctionPath varchar(50),
	@BookGroup varchar(10),
	@MayView bit = 0 Output,
	@MayEdit bit = 0 Output
As
Begin

Set RowCount 1

Select		@MayView = IsNull(RF.MayView, 0)
From		tbUsers U,
		tbUserRoles UR,
		tbRoleFunctions RF
Where           U.UserId = @UserId
	And	U.IsActive = 1
	And	U.UserId = UR.UserId
	And	UR.RoleCode = RF.RoleCode
	And	RF.FunctionPath = @FunctionPath
	And	((CharIndex(@BookGroup + ';', BookGroupList) > 0) Or (Upper(BookGroupList) = '[ALL]'))
Order By	1 Desc

If (@@RowCount = 0)
	Select	@MayView = 0

Select		@MayEdit = IsNull(RF.MayEdit, 0)
From		tbUsers U,
		tbUserRoles UR,
		tbRoleFunctions RF
Where		U.UserId = @UserId
	And	U.IsActive = 1
	And	U.UserId = UR.UserId
	And	UR.RoleCode = RF.RoleCode
	And	RF.FunctionPath = @FunctionPath
	And	((CharIndex(@BookGroup + ';', BookGroupList) > 0) Or (Upper(BookGroupList) = '[ALL]'))
Order By	1 Desc

If @@RowCount = 0
	Select	@MayEdit = 0

Set RowCount 0

End
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spUserViewEditBookGroup]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRoleFunctionSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRoleFunctionSet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spRoleFunctionSet
	@RoleCode varchar(5),
	@FunctionPath varchar(50),
	@MayView bit = Null,
	@MayEdit bit = Null,
	@BookGroupList varchar(100) = Null,
	@Comment varchar(50) = Null,
	@ActUserId varchar(50)
As
Begin

Update	tbRoleFunctions
Set	MayView = IsNull(@MayView, MayView),
	MayEdit = Case When MayEdit Is Not Null Then IsNull(@MayEdit, MayEdit) Else Null End,
	BookGroupList = Case When BookGroupList Is Not Null Then IsNull(@BookGroupList, BookGroupList) Else Null End,
	Comment = IsNull(@Comment, Comment),
	ActUserId = @ActUserId,
	ActTime = GetUtcDate()
Where	RoleCode = @RoleCode And
	FunctionPath = @FunctionPath

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spRoleFunctionSet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRoleFunctionsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRoleFunctionsGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spRoleFunctionsGet
		@RoleCode varchar(5) = Null,
		@FunctionPath varchar(50) = Null,
		@UtcOffset smallint = 0
As
Begin

Select		RF.RoleCode,
		RF.FunctionPath,
		RF.MayView,
		RF.MayEdit,
		RF.BookGroupList,
		RF.Comment,
		U.ShortName As ActUserShortName,
		DateAdd(n, @UtcOffset, RF.ActTime) As ActTime
From		tbRoleFunctions RF,
		tbUsers U
Where		RF.ActUserId = U.UserId
	And	RF.RoleCode = IsNull(@RoleCode, RF.RoleCode)
	And	RF.FunctionPath = IsNull(@FunctionPath, RF.FunctionPath)
	And	@RoleCode != 'ADMIN'
Order By	1, 2

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spRoleFunctionsGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spUserRoleSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spUserRoleSet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spUserRoleSet
	@UserId varchar(50),
	@RoleCode varchar(5),
	@Comment varchar(50) = Null,
	@ActUserId varchar(50),
	@Delete bit = 0
As
Begin

If (@Delete = 1)
Begin
	Delete
	From	tbUserRoles
	Where	UserId = @UserId
	And	RoleCode = @RoleCode
	And	@UserId != 'ADMIN'

	Return
End

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update	tbUserRoles
Set	Comment = IsNull(@Comment, Comment),
	ActUserId = @ActUserId,
	ActTime = GetUtcDate()
Where	UserId = @UserId And
	RoleCode = @RoleCode
And	@UserId != 'ADMIN'

If (@@RowCount = 0 And @UserId != 'ADMIN')
	Insert 	tbUserRoles
	Values	(
		@UserId,
		@RoleCode,
		@Comment,
		@ActUserId,
		GetUTCDate()
		)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spUserRoleSet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRoleSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRoleSet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spRoleSet
	@RoleCode varchar(5),
	@Role varchar(50) = Null,
	@Comment varchar(50) = Null,
	@ActUserId varchar(50),
	@Delete bit = 0
As
Begin

If (@Delete = 1)
Begin
	Delete
	From	tbRoles
	Where	RoleCode = @RoleCode

	Return
End

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update	tbRoles
Set	Role = IsNull(@Role, Role),
	Comment = IsNull(@Comment, Comment),
	ActUserId = @ActUserId,
	ActTime = GetUtcDate()
Where	RoleCode = @RoleCode

If (@@RowCount = 0)
	Insert 	tbRoles
	Values	(
		@RoleCode,
		@Role,
		@Comment,
		@ActUserId,
		GetUTCDate()
		)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spRoleSet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBookGroupGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBookGroupGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBookGroupGet 
	@BizDate datetime = Null,
	@UserId	varchar(50) = Null,
	@FunctionPath varchar(50) = Null

As
Begin

If (@BizDate Is Null)
	Select	@BizDate = KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate'

If ((@UserId Is Null) And (@FunctionPath Is Null)) 
	Select	Convert(char(10), BG.BizDate, 120) As BizDate,
		BG.BookGroup,
		BG.FundingRate,
		BG.DayCount,
		IsNull(B.BookName, '[Unknown]') As BookName
	From 	tbBookGroups BG,
		tbBooks B
	Where	BizDate  = @BizDate
	And	BG.BookGroup *= B.BookGroup
	And	BG.BookGroup *= B.Book
Else
Begin
	Declare	@BookGroup varchar(10),
		@MayView bit,
		@MayEdit bit
		
	Select	Convert(char(10), BG.BizDate, 120) As BizDate,
		BG.BookGroup,
		BG.FundingRate,
		BG.DayCount,
		IsNull(B.BookName, '[Unknown]') As BookName,
		Convert(bit, Null) As MayView,
		Convert(bit, Null) As MayEdit,
		Convert(bit, 0) As HasBeenSet
	Into	#BookGroups	
	From 	tbBookGroups BG,
		tbBooks B
	Where	BizDate  = @BizDate
	And	BG.BookGroup *= B.BookGroup
	And	BG.BookGroup *= B.Book

	Set RowCount 1

	While (1 = 1)
	Begin
		Select	@BookGroup = IsNull(BookGroup, ''),
			@MayView = 0,
			@MayEdit = 0
		From	#BookGroups
		Where	HasBeenSet = 0

		If (@@RowCount = 0)
			Break

		Exec	spUserViewEditBookGroup @UserId, @FunctionPath, @BookGroup, @MayView Output, @MayEdit Output

		Update	#BookGroups
		Set	MayView = @MayView,
			MayEdit = @MayEdit,
			HasBeenSet = 1
		Where	BookGroup = @BookGroup		
	End

	Set RowCount 0

	Select	BizDate,
		BookGroup,
		FundingRate,
		DayCount,
		BookName,
		MayView,
		MayEdit
	From	#BookGroups

	Drop Table #BookGroups
End

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBookGroupGet]  TO [roleMedalist]
GO





























--| SecMaster modifications.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tbSecIdLinks_tbSecMaster]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
	alter table [dbo].[tbSecIdLinks] drop constraint FK_tbSecIdLinks_tbSecMaster
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbSecIdLinks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	truncate table tbSecIdLinks
Go

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tgSecMasterSecIdListSet]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
	drop trigger [dbo].[tgSecMasterSecIdListSet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tgSecIdLinkSet]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
	drop trigger [dbo].[tgSecIdLinkSet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbSecMaster]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbSecMaster]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSecTypeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spSecTypeGet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbSecTypes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbSecTypes]
GO

CREATE TABLE [dbo].[tbSecMaster] (
	[SecId] [varchar] (12) NOT NULL ,
	[SecIdTypeIndex] [tinyint] NOT NULL ,
	[Description] [varchar] (100) NULL ,
	[BaseType] [char] (1) NOT NULL ,
	[ClassGroup] [varchar] (25) NULL ,
	[CountryCode] [char] (2) NOT NULL ,
	[CurrencyCode] [char] (3) NOT NULL ,
	[IsDtcEligible] [bit] NULL ,
	[LastPrice] [float] NULL ,
	[LastPriceDate] [datetime] NULL ,
	[AccruedInterest] [float] NULL ,
	[RecordDateCash] [datetime] NULL ,
	[DividendRate] [float] NULL ,
	[Sp] [varchar] (5) NULL ,
	[Moody] [varchar] (5) NULL ,
	[IsEasy] [bit] NOT NULL ,
	[IsHard] [bit] NOT NULL ,
	[IsThreshold] [bit] NOT NULL ,
	[IsNoLend] [bit] NOT NULL ,
	[ThresholdDayCount] [smallint] NOT NULL ,
	[SecIdList] [varchar] (100) NULL ,
	[IsActive] [bit] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbSecMaster] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbSecMaster] PRIMARY KEY  CLUSTERED 
	(
		[SecId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tbSecMaster] ADD 
	CONSTRAINT [DF_tbSecMaster_IsActive] DEFAULT (1) FOR [IsActive]
GO

ALTER TABLE [dbo].[tbSecMaster] ADD 
	CONSTRAINT [FK_tbSecMaster_tbCountries] FOREIGN KEY 
	(
		[CountryCode]
	) REFERENCES [dbo].[tbCountries] (
		[CountryCode]
	) ON UPDATE CASCADE ,
	CONSTRAINT [FK_tbSecMaster_tbCurrencies] FOREIGN KEY 
	(
		[CurrencyCode]
	) REFERENCES [dbo].[tbCurrencies] (
		[CurrencyCode]
	) ON UPDATE CASCADE  ,
	CONSTRAINT [FK_tbSecMaster_tbSecIdTypes] FOREIGN KEY 
	(
		[SecIdTypeIndex]
	) REFERENCES [dbo].[tbSecIdTypes] (
		[SecIdTypeIndex]
	) ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[tbSecIdLinks] ADD 
	CONSTRAINT [FK_tbSecIdLinks_tbSecMaster] FOREIGN KEY 
	(
		[SecId]
	) REFERENCES [dbo].[tbSecMaster] (
		[SecId]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Trigger tgSecIdLinkSet On dbo.tbSecMaster 
After Insert 

As
Begin

/*
Anticipate row inserts one at a time.
*/

Set NoCount On

Declare	@SecId		varchar(12),
	@SecIdTypeIndex	tinyint

-- Get the sec id and type of sec id that was inserted.
Select	@SecId = SecId,
	@SecIdTypeIndex = SecIdTypeIndex
From	inserted

-- Create the first link.
Exec	spSecIdLinkSet @SecId, @SecIdTypeIndex, @SecId

Set NoCount Off

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Trigger tgSecMasterSecIdListSet On [dbo].[tbSecIdLinks] 
For Insert, Update, Delete

As
Begin

/*
Maintains the SecIdList field in tbSecMaster current.
*/

Set NoCount On

If (Select Count(*) From deleted) > 1 -- Bulk delete from tbSecMaster so do nothing. 
	Return 

Declare	@SecId varchar(12),
	@SecIdTypeCount	tinyint,
	@SecIdTypeIndex	tinyint,
	@SecIdList varchar(100)

-- Resolve SecId if this is an insert or update.	
Select	@SecId = SecId
From	inserted

-- Resolve SecId if this is a delete.	
If @SecId Is Null
	Select	@SecId = SecId
	From	deleted

-- Initialize other local variables.
Select	@SecIdTypeIndex = 0,
	@SecIdList = ''	
From	tbSecIdTypes

-- Create list of potential links.
Select	T.SecIdTypeIndex,
	L.SecIdLink
Into	#Links
From	tbSecIdTypes T,
	tbSecIdLinks L
Where	L.SecId = @SecId And
	T.SecIdTypeIndex *= L.SecIdTypeIndex  

-- Iterate through potential links to create a delimited, ordered list of links.
While (@@RowCount > 0) 
	Select	@SecIdList = @SecIdList + IsNull(SecIdLink, '') + '|',
		@SecIdTypeIndex = @SecIdTypeIndex + 1
	From	#Links
	Where	SecIdTypeIndex = @SecIdTypeIndex

-- Update the security master.
Update	tbSecMaster
Set	SecIdList = @SecIdList
Where	SecId = @SecId

Set NoCount Off

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSecIdLinkSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spSecIdLinkSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spSecIdLinkSet
	@SecId varchar(12),
	@SecIdTypeIndex tinyint,
	@SecIdLink varchar(12) = Null,
	@Delete bit = 0
As
Begin

Set NoCount On

If (@Delete = 1)
Begin
	Delete
	From	tbSecIdLinks
	Where	SecId = @SecId
	And	SecIdTypeIndex = @SecIdTypeIndex
	
	Return
End

Begin Transaction

If (Not Exists (Select SecId From tbSecIdLinks Where SecId = @SecId And SecIdTypeIndex = @SecIdTypeIndex))
	Insert 	tbSecIdLinks
	Values	(
		@SecId,
		@SecIdTypeIndex,
		@SecIdLink
		)
Else
	Update	tbSecIdLinks
	Set	SecIdLink = @SecIdLink
	Where	SecId = @SecId
	And	SecIdTypeIndex = @SecIdTypeIndex
	And	SecIdLink != @SecIdLink

Delete
From	tbSecIdLinks
Where	SecId != @SecId
And	SecIdTypeIndex = @SecIdTypeIndex
And	SecIdLink = @SecIdLink

Commit Transaction

Set NoCount Off

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spSecIdLinkSet]  TO [roleMedalist]
GO

GRANT  EXECUTE  ON [dbo].[spSecIdLinkSet]  TO [roleCourier]
GO

GRANT  EXECUTE  ON [dbo].[spSecIdLinkSet]  TO [roleClient]
GO


if not exists (select * from syscolumns where name = 'IsActive' And id = (select id from sysobjects where name = 'tbCountries'))
	Alter Table tbCountries Add IsActive bit Not Null DEFAULT (1)
GO


if not exists (select * from syscolumns where name = 'IsActive' And id = (select id from sysobjects where name = 'tbCurrencies'))
	Alter Table tbCurrencies Add IsActive bit Not Null DEFAULT (1)
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSecMasterItemSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spSecMasterItemSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spSecMasterItemSet
	@SecId varchar(12),
	@SecIdTypeIndex tinyint = Null,
	@Custom varchar(12) = Null,
	@CUSIP varchar(12) = Null,
	@Symbol varchar(12) = Null,
	@SEDOL varchar(12) = Null,
	@ISIN varchar(12) = Null,
	@QUICK varchar(12) = Null,
	@Description varchar(100) = Null,
	@BaseType char(1) = Null,
	@ClassGroup varchar(25) = Null,
	@CountryCode char(2) = Null,
	@CurrencyCode char(3) = Null,
	@IsDtcEligible bit = Null,
	@LastPrice float = Null,
	@LastPriceDate datetime = Null,
	@AccruedInterest float = Null,
	@RecordDateCash datetime = Null,
	@DividendRate float = Null,
	@Sp varchar(5) = Null,
	@Moody varchar(5) = Null,
	@IsEasy bit = Null,
	@IsHard bit = Null,
	@IsThreshold bit = Null,
	@IsNoLend bit = Null,
	@ThresholdDayCount smallint = Null,
	@IsActive bit = Null,
	@ReturnRecord bit = 0
As
Begin

Set NoCount On

If (@CountryCode Is Not Null)
Begin
	Select @CountryCode = LTrim(RTrim(@CountryCode))

	If (Len(@CountryCode) != 2)
		Select @CountryCode = '**'

	If Not Exists (Select CountryCode From tbCountries Where CountryCode = @CountryCode)
		Insert
		tbCountries
		Values (@CountryCode, Null, Null, 1)
End

If (@CurrencyCode Is Not Null)
Begin
	Select @CurrencyCode = LTrim(RTrim(@CurrencyCode))

	If (Len(@CurrencyCode) != 3)
		Select @CurrencyCode = '***'

	If Not Exists (Select CurrencyCode From tbCurrencies Where CurrencyCode = @CurrencyCode)
		Insert
		tbCurrencies
		Values (@CurrencyCode, Null, 1)
End

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

	Update	tbSecMaster
	Set	Description = IsNull(@Description, Description),
		BaseType = IsNull(@BaseType, BaseType),
		ClassGroup = IsNull(@ClassGroup, ClassGroup),
		CountryCode = IsNull(@CountryCode, CountryCode),
		CurrencyCode = IsNull(@CurrencyCode, CurrencyCode),
		IsDtcEligible = IsNull(@IsDtcEligible, IsDtcEligible),
		LastPrice = IsNull(@LastPrice, LastPrice),
		LastPriceDate = IsNull(@LastPriceDate, LastPriceDate),
		AccruedInterest = IsNull(@AccruedInterest, AccruedInterest),
		RecordDateCash = IsNull(@RecordDateCash, RecordDateCash),
		DividendRate = IsNull(@DividendRate, DividendRate),
		Sp = IsNull(@Sp, Sp),
		Moody = IsNull(@Moody, Moody),
		IsEasy = IsNull(@IsEasy, IsEasy),
		IsHard = IsNull(@IsHard, IsHard),
		IsThreshold = IsNull(@IsThreshold, IsThreshold),
		IsNoLend = IsNull(@IsNoLend, IsNoLend),
		ThresholdDayCount = IsNull(@ThresholdDayCount, ThresholdDayCount),
		IsActive = IsNull(@IsActive, IsActive)
	Where	SecId = @SecId

	If (@@RowCount = 0)
		Insert 	tbSecMaster
		Values	(
			@SecId,
			IsNull(@SecIdTypeIndex, 0),
			@Description,
			IsNull(@BaseType, 'U'),
			IsNull(@ClassGroup, 'UNKNOWN'),
			IsNull(@CountryCode, '**'),
			IsNull(@CurrencyCode, '***'),
			@IsDtcEligible,
			@LastPrice,
			@LastPriceDate,
			@AccruedInterest,
			@RecordDateCash,
			@DividendRate,
			@Sp,
			@Moody,
			IsNull(@IsEasy, 0),
			IsNull(@IsHard, 0),
			IsNull(@IsThreshold, 0),
			IsNull(@IsNoLend, 0),
			IsNull(@ThresholdDayCount, 0),
			Null,
			1
			)
	
	If (@Custom Is Not Null)
		If (@Custom = '') -- Remove existing link if it exists.
			Exec spSecIdLinkSet @SecId, 0, @Custom,	1 
		Else -- Add this link.
			Exec spSecIdLinkSet @SecId, 0, @Custom,	0
	
	If (@CUSIP Is Not Null)
		If (@CUSIP = '') -- Remove existing link if it exists.
			Exec spSecIdLinkSet @SecId, 1, @CUSIP,	1 
		Else -- Add this link.
			Exec spSecIdLinkSet @SecId, 1, @CUSIP,	0
	
	If (@Symbol Is Not Null)
		If (@Symbol = '') -- Remove existing link if it exists.
			Exec spSecIdLinkSet @SecId, 2, @Symbol,	1 
		Else -- Add this link.
			Exec spSecIdLinkSet @SecId, 2, @Symbol,	0
	
	If (@SEDOL Is Not Null)
		If (@SEDOL = '') -- Remove existing link if it exists.
			Exec spSecIdLinkSet @SecId, 3, @SEDOL,	1 
		Else -- Add this link.
			Exec spSecIdLinkSet @SecId, 3, @SEDOL,	0
	
	If (@ISIN Is Not Null)
		If (@ISIN = '') -- Remove existing link if it exists.
			Exec spSecIdLinkSet @SecId, 4, @ISIN,	1 
		Else -- Add this link.
			Exec spSecIdLinkSet @SecId, 4, @ISIN,	0
	
	If (@QUICK Is Not Null)
		If (@QUICK = '') -- Remove existing link if it exists.
			Exec spSecIdLinkSet @SecId, 5, @QUICK,	1 
		Else -- Add this link.
			Exec spSecIdLinkSet @SecId, 5, @QUICK,	0
	
Commit Transaction

If (@ReturnRecord = 1)
	Select	*
	From	tbSecMaster
	Where	SecId = @SecId

Set NoCount Off

End
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spSecMasterItemSet]  TO [roleMedalist]
GO

GRANT  EXECUTE  ON [dbo].[spSecMasterItemSet]  TO [roleCourier]
GO

GRANT  EXECUTE  ON [dbo].[spSecMasterItemSet]  TO [roleClient]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBorrowEasySet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBorrowEasySet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spBorrowEasySet
	@TradeDate datetime,
	@WaitFiles bit = 1
As
Begin

Declare	@BizDate datetime,
	@BizDateCount int,
	@BizDateFailCount int,
	@QuantityHaircut varchar(10),
	@QuantityMinimum varchar(10),
	@SourceList varchar(250),
	@SecIdList varchar(250)
	
Select	@BizDateFailCount = 3
Exec	spKeyValueGet 'EasyBorrowBizDateFailCount', @BizDateFailCount Output

Select	@QuantityHaircut = '1.0'
Exec	spKeyValueGet 'EasyBorrowQuantityHaircut', @QuantityHaircut Output

Select	@QuantityMinimum = '350000'
Exec	spKeyValueGet 'EasyBorrowQuantityMinimum', @QuantityMinimum Output

Select	@SourceList = 'Desk list delimited by [;]'
Exec	spKeyValueGet 'EasyBorrowSourceList', @SourceList Output

Select	@SecIdList = 'Sec ID list delimited by [;]'
Exec	spKeyValueGet 'EasyBorrowSecIdList', @SecIdList Output

If (@WaitFiles = 1) -- Must have file feeds for trade date.
Begin
	Select Distinct BizDate
	Into		#BizDates
	From		tbInventorySubscriber
	Where		CharIndex(Desk + ';', @SourceList) > 0
	
	Select	@BizDateCount = @@RowCount
	
	If (@BizDateCount = 0)
	Begin
		RAISERROR('Inventory file feed[s] are not in for any desk on the source list.', 16, 1)
		Return
	End
	
	If (@BizDateCount > 1)
	Begin
		RAISERROR('Inventory file feed[s] are not all in for today.', 16, 1)
		Return
	End
	
	If @TradeDate != (Select BizDate From #BizDates)
	Begin
		RAISERROR('Inventory file feed[s] are not any in for today.', 16, 1)
		Return
	End
End

-- Purge existing records case this is a re-run.
Delete
From	tbBorrowEasy
Where	TradeDate = @TradeDate

-- Reset security master.
Update	tbSecMaster
Set	IsEasy = 0
Where	IsEasy = 1

-- Insert all potential easy stocks.
Insert		tbBorrowEasy
Select		@TradeDate,
		I.SecId,
		Convert(bigint, Sum(I.Quantity * Convert(float, @QuantityHaircut))) As Quantity
From		tbInventory I,
		tbInventoryControl IC,
		tbSecMaster SM
Where		I.BizDate = IC.BizDate
	And	I.Desk = IC.Desk
	And	I.SecId = IC.SecId
	And	I.Quantity >= Convert(bigint, @QuantityMinimum)
	And	((CharIndex(I.Desk + ';', @SourceList) > 0) And (I.ModeCode = 'F'))
	And	IC.BizDate = @TradeDate
	And	I.SecId = SM.SecId
	And	SM.BaseType = 'E'
Group By	I.SecId

-- Remove hard stocks.
Delete
From	tbBorrowEasy
Where	TradeDate = @TradeDate
	And	SecId In (Select SecId From tbBorrowHard Where EndTime Is Null)

-- Remove no stocks.
Delete
From	tbBorrowEasy
Where	TradeDate = @TradeDate
	And	SecId In (Select SecId From tbBorrowNo Where EndTime Is Null)

-- Insert stocks known to be easy but that may not be in reported inventory.
Insert		tbBorrowEasy
Select		@TradeDate,
		SecId,
		Convert(bigint, @QuantityMinimum) As Quantity
From		tbSecMaster
Where		(CharIndex(SecId + ';', @SecIdList) > 0)
	And	SecId Not In (Select SecId From tbBorrowEasy Where TradeDate = @TradeDate)

-- Remove threshold stocks.
Delete
From	tbBorrowEasy
Where	TradeDate = @TradeDate
	And	SecId In (	Select	SecId
				From	tbThreshold T,
					tbThresholdControl TC
				Where	T.BizDate = TC.BizDate
					And	TC.Exchange = T.Exchange	)

-- Remove fails.
Delete
From	tbBorrowEasy
Where	TradeDate = @TradeDate
	And	SecId In (	Select	SecId
				From	tbBoxPosition
				Where	BizDate = (Select KeyValue from tbKeyValues Where KeyId = 'BizDatePrior')
				And	((ClearingFailOutSettled >= Convert(int, @BizDateFailCount))
					Or (BrokerFailOutSettled >= Convert(int, @BizDateFailCount))
					Or (DvpFailOutSettled >= Convert(int, @BizDateFailCount)))		)

-- Set security master.
Update	tbSecMaster
Set	IsEasy = 1
Where	SecId In (Select SecId From tbBorrowEasy Where TradeDate = @TradeDate)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBorrowEasySet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBorrowNoSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBorrowNoSet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spBorrowNoSet
	@SecId varchar(12),
	@ActUserId varchar(50),
	@Delete bit = 0
As
Begin

Exec spSecIdSymbolLookup @SecId, @SecId Output

If (@Delete = 1)
Begin
	Update	tbBorrowNo
	Set	EndTime = GetUtcDate(),
		ActUserId = @ActUserId
	Where	SecId = @SecId And
		EndTime Is Null

	Update	tbSecMaster
	Set	IsNoLend = 0
	Where	SecId = @SecId

	Return
End

If (@SecId In (Select SecId From tbBorrowNo Where EndTime Is Null))
	Return

Insert 	tbBorrowNo
Values	(
	@SecId,
	GetUTCDate(),
	Null,
	@ActUserId
	)

	Update	tbSecMaster
	Set	IsNoLend = 1
	Where	SecId = @SecId

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBorrowNoSet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSecMasterItemGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spSecMasterItemGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spSecMasterItemGet
	@SecId varchar(12)
As
Begin

Set RowCount 1

Select		*
From		tbSecMaster SM,
		tbSecIdLinks SIL
Where		SM.SecId = SIL.SecId
	And	SIL.SecIdLink = @SecID
Order By	BaseType Desc

Set RowCount 0

End
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spSecMasterItemGet]  TO [roleMedalist]
GO

GRANT  EXECUTE  ON [dbo].[spSecMasterItemGet]  TO [roleCourier]
GO

GRANT  EXECUTE  ON [dbo].[spSecMasterItemGet]  TO [roleClient]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spSecIdSymbolLookup]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spSecIdSymbolLookup]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure spSecIdSymbolLookup
	@SecIdLink  varchar(12),
	@SecId varchar(12) = Null Output,
	@Symbol varchar(8) = Null Output
As
Begin

Set RowCount 1

Select		@SecId = SM.SecId
From		tbSecMaster SM,
		tbSecIdLinks SIL
Where		SM.SecId = SIL.SecId
	And	SIL.SecIdLink = @SecIdLink
Order By	SM.BaseType Desc -- If link is ambiguous, bias for equity.

If (@SecId Is Null)
		Select @SecId = @SecIdLink

Select		@Symbol = SecIdLink
From		tbSecIdLinks
Where		((SecId = @SecId) Or (SecId = @SecIdLink))
	And	SecIdTypeIndex = 2

Set RowCount 0

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spSecIdSymbolLookup]  TO [roleMedalist]
GO

GRANT  EXECUTE  ON [dbo].[spSecIdSymbolLookup]  TO [roleCourier]
GO

GRANT  EXECUTE  ON [dbo].[spSecIdSymbolLookup]  TO [roleClient]
GO


-- 01


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spDeskQuipGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spDeskQuipGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spDeskQuipGet
	@SecId varchar(12) = Null,
	@AgeDays smallint = 365,
	@UtcOffset smallint = 0,
	@Since datetime = Null
As
Begin

If (@Since Is Null)
	Select	@Since = Convert(char(10), DateAdd(d, - @AgeDays, GetUtcDate()), 120)
Else
	Select	@Since = DateAdd(n, -@UtcOffset, @Since)

Select		DQ.SecId,
		SIL.SecIdLink As Symbol,
		DQ.DeskQuip,
		U.ShortName As ActUserShortName,
		Convert(char(23), DateAdd(n, @UtcOffset, DQ.ActTime), 121) As ActTime
From		tbDeskQuips DQ,
		tbSecIdLinks SIL,
		tbUsers U
Where		DQ.ActUserId = U.UserId
	And	DQ.SecId = IsNull(@SecId, DQ.SecId)
	And	DQ.ActTime > @Since
	And	DQ.SecId *= SIL.SecId
	And	SIL.SecIdTypeIndex = 2
Order By	ActTime Desc

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spDeskQuipGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spDeskQuipSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spDeskQuipSet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spDeskQuipSet
	@SecId varchar(12),
	@DeskQuip varchar(50),
	@ActUserId varchar(50),
	@ActTime datetime = Null Output,
	@Symbol varchar(12) = Null Output,
	@ActUserShortName varchar(15) = Null Output
As
Begin

Declare	@SecIdLink varchar(12)
Select	@SecIdLink = @SecId

Exec	spSecIdSymbolLookup @SecIdLink, @SecId Output, @Symbol Output

Select	@ActTime = GetUtcDate()

Insert 	tbDeskQuips
Values	(
	@SecId,
	@DeskQuip,
	@ActUserId,
	@ActTime
	)

Select	@ActUserShortName = IsNull(ShortName, @ActUserId)
From	tbUsers
Where	UserId = @ActUserId

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spDeskQuipSet]  TO [roleMedalist]
GO

--------------------------------------------------------------------------------
--| End SecMaster modifications.


--| Contract modifications.
--------------------------------------------------------------------------------

If Not Exists (Select * From tbFunctions Where FunctionPath = 'AdminBooks')
	Insert Into tbFunctions Values ('AdminBooks','0','0', '[NONE]')
GO

If Not Exists (Select * From tbFunctions Where FunctionPath = 'PositionOpenContracts')
	Insert Into tbFunctions Values ('PositionOpenContracts','0','0', '[NONE]')
GO

if not exists (select * from dbo.sysusers where name = N'roleLoanet')
	exec sp_addrole N'roleLoanet'
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbBooks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbBooks]
GO

CREATE TABLE [dbo].[tbBooks] (
	[BookGroup] [varchar] (10) NOT NULL ,
	[Book] [varchar] (10) NOT NULL ,
	[BookParent] [varchar] (10) NOT NULL ,
	[BookName] [varchar] (50) NULL ,
	[AddressLine1] [varchar] (50) NULL ,
	[AddressLine2] [varchar] (50) NULL ,
	[AddressLine3] [varchar] (50) NULL ,
	[Phone] [varchar] (25) NULL ,
	[DtcDeliver] [char] (4) NULL ,
	[DtcMark] [char] (4) NULL ,
	[AmountMinimum] [int] NULL ,
	[PriceMinimum] [int] NULL ,
	[MarginBorrow] [float] NULL ,
	[MarginLoan] [float] NULL ,
	[MarkRoundHouse] [char] (3) NULL ,
	[MarkRoundInstitution] [char] (3) NULL ,
	[AmountLimitBorrow] [bigint] NULL ,
	[AmountLimitLoan] [bigint] NULL ,
	[RateStockBorrow] [decimal](8, 5) NULL ,
	[RateStockLoan] [decimal](8, 5) NULL ,
	[RateBondBorrow] [decimal](8, 5) NULL ,
	[RateBondLoan] [decimal](8, 5) NULL ,
	[TaxId] [varchar] (25) NULL ,
	[FaxNumber] [varchar] (25) NULL ,
	[Firm] [varchar] (5) NULL ,
	[Country] [char] (2) NULL ,
	[DeskType] [varchar] (3) NULL ,
	[Comment] [varchar] (50) NULL ,
	[ActUserId] [varchar] (50) NOT NULL ,
	[ActTime] [datetime] NOT NULL ,
	[IsActive] [bit] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbBooks] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbBooks] PRIMARY KEY  CLUSTERED 
	(
		[BookGroup],
		[Book]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBooksGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBooksGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBooksGet 
	@IsActive bit = Null,
	@UtcOffset smallint = 0
As
Begin

Declare	@BizDate datetime
Select	@BizDate = Convert(datetime, (Select KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate'))

Select 		B.BookGroup,
		B.BookParent,
		Sum	(Case	When C.AmountSettled > C.Amount
				Then C.AmountSettled * CharIndex('B', C.ContractType)
				Else C.Amount * CharIndex('B', C.ContractType) End) As BorrowAmount,
		Sum	(Case	When C.AmountSettled > C.Amount
				Then C.AmountSettled * CharIndex('L', C.ContractType)
 				Else C.Amount * CharIndex('L', C.ContractType) End) As LoanAmount
Into 		#Amounts
From 		tbBooks B,
		tbContracts C
Where		C.BizDate = @BizDate
	And	C.BookGroup = B.BookGroup
	And	C.Book = B.Book
Group By	B.BookGroup, B.BookParent

Select		B.BookGroup,
		B.Book,
		B.BookParent,
		B.BookName,
		B.DtcDeliver,
		B.DtcMark,
		B.AmountMinimum,
		B.PriceMinimum,
		B.MarginBorrow,
		B.MarginLoan,
		B.MarkRoundHouse,
		B.MarkRoundInstitution,
		B.AmountLimitBorrow,
		B.AmountLimitLoan,
		B.RateStockBorrow,
		B.RateStockLoan,
		B.RateBondBorrow,
		B.RateBondLoan,
		B.TaxId,
		B.FaxNumber,
		B.Firm,
		B.Country,
		B.DeskType,
		B.Comment,	
		U.ShortName As ActUserShortName,
		DateAdd(n, @UtcOffset, B.ActTime) As ActTime,
		B.IsActive,
		Convert(bigint, IsNull(A.BorrowAmount, 0)) As BorrowAmount,
		Convert(bigint, IsNull(A.LoanAmount, 0)) As LoanAmount
From 		tbBooks B,
		tbUsers U,
		#Amounts A
Where		B.ActUserId = U.UserId
	And	B.BookGroup *= A.BookGroup
	And	B.BookParent *= A.BookParent
	And	B.IsActive = IsNull(@IsActive, B.IsActive)
Order By	1, 2

Drop Table	#Amounts

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBooksGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBookParentGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBookParentGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBookParentGet
	@BizDate datetime = Null,
	@BizDatePrior datetime = Null
As
Begin

If @BizDate Is Null
	Select	@BizDate = Convert(datetime, (Select KeyValue From tbKeyValues Where KeyId = 'BizDate'))

If @BizDatePrior Is Null
	Select	@BizDatePrior = Convert(datetime, (Select KeyValue From tbKeyValues Where KeyId = 'BizDatePrior'))

Select 		C.BookGroup,
		B.BookParent,
		Sum	(Case	When	(C.Amount > C.AmountSettled)
				Then	C.Amount * CharIndex(C.ContractType, 'B')
				Else	C.AmountSettled * CharIndex(C.ContractType, 'B') End) As BorrowAmount,
		Sum	(Case 	When 	(C.Amount > C.AmountSettled)
				Then	C.Amount * CharIndex(C.ContractType, 'L')
				Else	C.AmountSettled * CharIndex(C.ContractType, 'L') End) As LoanAmount
Into 		#AmountsPrior
From 		tbBooks B,
		tbContracts C
Where		C.BizDate = @BizDatePrior
	And	C.Book = B.Book
Group By	C.BookGroup,
		B.BookParent

Select 		C.BookGroup,
		B.BookParent,
		Sum	(Case	When	(C.Amount > C.AmountSettled)
				Then	C.Amount * CharIndex(C.ContractType, 'B')
				Else	C.AmountSettled * CharIndex(C.ContractType, 'B') End) As BorrowAmount,
		Sum	(Case 	When 	(C.Amount > C.AmountSettled)
				Then	C.Amount * CharIndex(C.ContractType, 'L')
				Else	C.AmountSettled * CharIndex(C.ContractType, 'L') End) As LoanAmount
Into 		#AmountsNow
From 		tbBooks B,
		tbContracts C
Where		C.BizDate = @BizDate
	And	C.Book = B.Book
Group By	C.BookGroup,
		B.BookParent

Select		B.BookGroup,
		B.BookParent,
		B.BookName,
		B.AmountLimitBorrow,
		B.AmountLimitLoan,
		B.AmountLimitBorrow + B.AmountLimitLoan As AmountLimitTotal,
		IsNull(AP.BorrowAmount, 0) As AmountBorrowPriorDay,
		IsNull(AP.LoanAmount, 0) As AmountLoanPriorDay,
		IsNull(AP.BorrowAmount, 0) + IsNull(AP.LoanAmount, 0) As AmountTotalPriorDay,
		IsNull(AN.BorrowAmount, 0) As AmountBorrowToday,
		IsNull(AN.LoanAmount, 0) As AmountLoanToday,
		IsNull(AN.BorrowAmount, 0) + IsNull(AN.LoanAmount, 0) As AmountTotalToday,
		B.Comment,
		U.ShortName As ActUserShortName,
		B.ActTime
From		tbBooks B,
		#AmountsPrior AP,
		#AmountsNow AN,
		tbUsers U
Where		B.BookParent = B.Book
	And	B.BookGroup *= AP.BookGroup
	And	B.BookParent *= AP.BookParent
	And	B.BookGroup *= AN.BookGroup
	And	B.BookParent *= AN.BookParent
	And	B.ActUserId *= U.UserId
Order By	1, 2

Drop Table	#AmountsPrior
Drop Table	#AmountsNow

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBookParentGet]  TO [medalistService]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBookGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBookGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBookGet
	@BookGroup varchar(10) = Null,
	@Book varchar(10) = Null,
	@IsActive bit = Null,
	@UtcOffset smallint = 0
As
Begin

Select		B.BookGroup,
		B.Book,
		B.BookParent,
		B.BookName,
		B.AddressLine1,
		B.AddressLine2,
		B.AddressLine3,
		B.Phone,
		B.DtcDeliver,
		B.DtcMark,
		B.AmountMinimum,
		B.PriceMinimum,
		B.MarginBorrow,
		B.MarginLoan,
		B.MarkRoundHouse,
		B.MarkRoundInstitution,
		B.AmountLimitBorrow,
		B.AmountLimitLoan,
		B.RateStockBorrow,
		B.RateStockLoan,
		B.RateBondBorrow,
		B.RateBondLoan,
		B.TaxId,
		B.FaxNumber,
		B.Firm,
		B.Country,
		B.DeskType,
		B.Comment,
		U.ShortName As ActUserShortName,
		B.ActTime,
		B.IsActive
From		tbBooks B,
		tbUsers U
Where		B.BookGroup = IsNull(@BookGroup, B.BookGroup)
	And	B.Book = IsNull(@Book, Book)
	And	B.IsActive = IsNull(@IsActive, B.IsActive)
	And	B.ActUserId *= U.UserId
Order By	1, 2

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBookGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBookSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBookSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBookSet
	@BookGroup varchar(10), 
	@Book varchar(10),
	@AmountLimitBorrow bigint = Null,
	@AmountLimitLoan bigint = Null,
	@FaxNumber varchar(25) = Null,
	@Firm varchar(5) = Null,
	@Country varchar(2) = Null,
	@DeskType varchar(3) = Null,
	@Comment varchar(50) = Null,
	@ActUserId varchar(50) = Null
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update		tbBooks
Set		AmountLimitBorrow = IsNull(@AmountLimitBorrow, AmountLimitBorrow),
		AmountLimitLoan	= IsNull(@AmountLimitLoan, AmountLimitLoan),
		FaxNumber = IsNull(@FaxNumber, FaxNumber),
		Firm = IsNull(@Firm, Firm),
		Country = IsNull(@Country, Country),
		DeskType = IsNull(@DeskType, DeskType),
		Comment = IsNull(@Comment, Comment),
		ActUserId = IsNull(@ActUserId, 'ADMIN'),
		ActTime	= GetUTCDate()
Where		BookGroup = @BookGroup
	And	Book = @Book

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBookSet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbBookGroups]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbBookGroups]
GO

CREATE TABLE [dbo].[tbBookGroups] (
	[BizDate] [datetime] NOT NULL ,
	[BookGroup] [varchar] (10) NOT NULL ,
	[FundingRate] [decimal] (5,3) NOT NULL ,
	[DayCount] [smallint] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbBookGroups] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbBookGroups] PRIMARY KEY  CLUSTERED 
	(
		[BizDate],
		[BookGroup]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBookGroupsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBookGroupsGet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBookGroupGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBookGroupGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBookGroupGet 
	@BizDate datetime = Null,
	@UserId	varchar(50) = Null,
	@FunctionPath varchar(50) = Null
As
Begin

If (@BizDate Is Null)
	Select	@BizDate = KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate'

If ((@UserId Is Null) And (@FunctionPath Is Null)) 
	Select	Convert(char(10), BG.BizDate, 120) As BizDate,
		BG.BookGroup,
		BG.FundingRate,
		BG.DayCount,
		IsNull(B.BookName, '[Unknown]') As BookName
	From 	tbBookGroups BG,
		tbBooks B
	Where	BizDate  = @BizDate
	And	BG.BookGroup *= B.BookGroup
	And	BG.BookGroup *= B.Book
Else
Begin
	Declare	@BookGroup varchar(10),
		@MayView bit,
		@MayEdit bit
		
	Select	Convert(char(10), BG.BizDate, 120) As BizDate,
		BG.BookGroup,
		BG.FundingRate,
		BG.DayCount,
		IsNull(B.BookName, '[Unknown]') As BookName,
		Convert(bit, Null) As MayView,
		Convert(bit, Null) As MayEdit,
		Convert(bit, 0) As HasBeenSet
	Into	#BookGroups	
	From 	tbBookGroups BG,
		tbBooks B
	Where	BizDate  = @BizDate
	And	BG.BookGroup *= B.BookGroup
	And	BG.BookGroup *= B.Book

	Set RowCount 1

	While (1 = 1)
	Begin
		Select	@BookGroup = IsNull(BookGroup, ''),
			@MayView = 0,
			@MayEdit = 0
		From	#BookGroups
		Where	HasBeenSet = 0

		If (@@RowCount = 0)
			Break

		Exec	spUserViewEditBookGroup @UserId, @FunctionPath, @BookGroup, @MayView Output, @MayEdit Output

		Update	#BookGroups
		Set	MayView = @MayView,
			MayEdit = @MayEdit,
			HasBeenSet = 1
		Where	BookGroup = @BookGroup		
	End

	Set RowCount 0

	Select	BizDate,
		BookGroup,
		FundingRate,
		DayCount,
		BookName,
		MayView,
		MayEdit
	From	#BookGroups

	Drop Table #BookGroups
End

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBookGroupGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbContracts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbContracts]
GO

CREATE TABLE [dbo].[tbContracts] (
	[BizDate] [datetime] NOT NULL ,
	[BookGroup] [varchar] (10) NOT NULL ,
	[ContractId] [varchar] (15) NOT NULL ,
	[ContractType] [char] (1) NOT NULL ,
	[Book] [varchar] (10) NOT NULL ,
	[SecId] [varchar] (12) NOT NULL ,
	[Quantity] [bigint] NOT NULL ,
	[QuantitySettled] [bigint] NOT NULL ,
	[Amount] [money] NOT NULL ,
	[AmountSettled] [money] NOT NULL ,
	[CollateralCode] [varchar] (1) NOT NULL ,
	[ValueDate] [datetime] NULL ,
	[SettleDate] [datetime] NULL ,
	[TermDate] [datetime] NULL ,
	[Rate] [decimal] (8, 5) NOT NULL ,
	[RateCode] [varchar] (1) NOT NULL ,
	[StatusFlag] [varchar] (1) NOT NULL ,
	[PoolCode] [varchar] (1) NOT NULL ,
	[DivRate] [decimal] (6,3) NOT NULL ,
	[DivCallable] [bit] NOT NULL ,
	[IncomeTracked] [bit] NOT NULL ,
	[MarginCode] [varchar] (1) NOT NULL ,
	[Margin] [decimal] (3,1) NOT NULL ,
	[CurrencyIso] [char] (3) NOT NULL ,
	[SecurityDepot] [varchar] (2) NOT NULL ,
	[CashDepot] [varchar] (2) NOT NULL ,
	[OtherBook] [varchar] (10) NOT NULL ,
	[Comment] [varchar] (20) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbContracts] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbContracts] PRIMARY KEY  CLUSTERED 
	(
		[BizDate],
		[BookGroup],
		[ContractId],
		[ContractType]
	)  ON [PRIMARY] 
GO

CREATE  INDEX [IX_tbContracts] ON [dbo].[tbContracts]([BizDate], [SecId]) ON [PRIMARY]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContractGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spContractGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spContractGet
	@BizDate datetime = Null,
	@BookGroup varchar(10) = Null,
	@ContractId varchar(15) = Null,
	@ContractType char(1) = Null
As
Begin

If (@BizDate Is Null)
	Select	@BizDate = Convert(datetime, (Select KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate'))

Select	Convert(char(10), C.BizDate, 120) As BizDate,
	C.BookGroup,
	IsNull(B.BookParent, C.Book) As BookParent,
	C.ContractId,
	C.ContractType,
	C.Book,
	C.SecId,
	IsNull(SIL.SecIdLink, '') As Symbol,
	IsNull(SM.BaseType, 'U') As BaseType,
	IsNull(SM.ClassGroup, '') As ClassGroup,
	C.Quantity,
	C.QuantitySettled,
	Case	When C.Quantity = C.QuantitySettled Then Convert(bit, 1)
		Else Convert(bit, 0) End As IsSettledQuantity,
	Convert(bigint, 0) As QuantityRecalled,
	Convert(bit, 0) As HasRecall,
	C.Amount,
	C.AmountSettled,
	Case	When C.Amount = C.AmountSettled Then Convert(bit, 1)
		Else Convert(bit, 0) End As IsSettledAmount,
	C.CollateralCode,
	C.ValueDate,
	C.SettleDate,
	C.TermDate,
	Case	When C.CollateralCode = 'C' Then C.Rate
		Else BG.FundingRate - C.Rate End As RebateRate, 
	Case	When C.CollateralCode = 'C' Then BG.FundingRate - C.Rate
		Else C.Rate End As FeeRate,
	C.Rate,
	C.RateCode,
	C.StatusFlag,
	C.PoolCode,
	C.DivRate,
	C.DivCallable,
	C.IncomeTracked,
	C.MarginCode,
	C.Margin,
	C.CurrencyIso,
	C.SecurityDepot,
	C.CashDepot,
	C.OtherBook,
	C.Comment,
	IsNull(SM.IsEasy, 0) As IsEasy,	
	IsNull(SM.IsHard, 0) As IsHard,	
	IsNull(SM.IsNoLend, 0) As IsNoLend,	
	IsNull(SM.IsThreshold, 0) As IsThreshold,
	Case	When SM.LastPrice Is Null Then
			Convert(decimal, AmountSettled / Margin)
		Else
			Convert(decimal, QuantitySettled * SM.LastPrice * (1.00 - (CharIndex('B', BaseType) * 0.99)))
		End As Value,
	Case	When SM.LastPrice Is Null Then
			Convert(bit, 1)
		Else
			Convert(bit, 0)
		End As ValueIsEstimate,
	Convert(decimal(18,3), 1.0) As ValueRatio,	
	Convert(decimal(18,2), 0.0) As Income
Into	#Contracts
From	tbContracts C,
	tbBooks B,
	tbSecIdLinks SIL,
	tbSecMaster SM,
	tbBookGroups BG
Where	C.BizDate = @BizDate
And	C.BookGroup = IsNull(@BookGroup, C.BookGroup)
And	C.ContractId = IsNull(@ContractId, C.ContractId)
And	C.ContractType = IsNull(@ContractType, C.ContractType)
And	B.BookGroup =* C.BookGroup
And	B.Book =* C.Book
And	SIL.SecId =* C.SecId
And	SIL.SecIdTypeIndex = 2
And	SM.SecId =* C.SecId
And	C.BookGroup = BG.BookGroup
And	BG.BizDate = @BizDate

If (@BizDate != (Select Convert(datetime, (Select KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate')))) -- Is for prior date.
Begin
	Update	#Contracts
	Set	Income = Case	When ContractType = 'L' Then AmountSettled * FeeRate / 36000.0
				Else -1.0 * AmountSettled * FeeRate / 36000.0 End,
		IsEasy = 0,
		IsHard = 0,
		IsNoLend = 0,
		IsThreshold = 0,
		ValueRatio = Case When Value > 0 Then AmountSettled / Value Else ValueRatio End

	Update	#Contracts
	Set	IsEasy = 1
	From	#Contracts C,
		tbBorrowEasy BE
	Where	BE.TradeDate = @BizDate
	And	BE.SecId = C.SecId	

	Update	#Contracts
	Set	IsHard = 1
	From	#Contracts C,
		tbBorrowHard BH
	Where	BH.SecId = C.SecId	
	And	BH.StartTime < @BizDate + 1
	And	IsNull(BH.EndTime, @BizDate + 1) > @BizDate

	Update	#Contracts
	Set	IsNoLend = 1
	From	#Contracts C,
		tbBorrowNo BN
	Where	BN.SecId = C.SecId	
	And	BN.StartTime < @BizDate + 1
	And	IsNull(BN.EndTime, @BizDate + 1) > @BizDate

	-- Generate list of threshold stocks for the business date presented.
	Select		Max(BizDate) As BizDate,
			Exchange
	Into		#ThresholdControl
	From		tbThreshold
	Where		BizDate < @BizDate
	Group By	Exchange

	Select		T.SecId,
			T.Symbol 
	Into		#Threshold
	From 		tbThreshold T,
			#ThresholdControl TC
	Where 		T.BizDate = TC.BizDate
	And		TC.Exchange = T.Exchange

	Drop Table	#ThresholdControl

	Update	#Contracts
	Set	IsThreshold = 1
	From	#Contracts C,
		#Threshold T
	Where	T.SecId = C.SecId
	Or	T.Symbol = C.Symbol	

	Drop Table	#Threshold
End
Else -- Is for current date.
Begin
	Update	#Contracts
	Set	Income = Case	When ContractType = 'L' Then AmountSettled * FeeRate / 36000.0
				Else -1.0 * AmountSettled * FeeRate / 36000.0 End,
		ValueRatio = Case When Value > 0 Then AmountSettled / Value Else ValueRatio End
End

Select		C.BookGroup,
		C.ContractId,
		C.ContractType,
		Sum(R.Quantity) As QuantityRecalled,
		R.Status
Into		#Recalls
From		#Contracts C,
		tbRecalls R
Where		C.BookGroup = R.BookGroup
And		C.ContractId = R.ContractId
And		C.ContractType = R.ContractType
Group By	C.BookGroup, C.ContractId, C.ContractType, R.Status
Having		Sum(R.Quantity) > 0

Update	#Contracts
Set	QuantityRecalled = R.QuantityRecalled,
	HasRecall = 1
From	#Contracts C,
	#Recalls R
Where	C.BookGroup = R.BookGroup
And	C.ContractId = R.ContractId
And	C.ContractType = R.ContractType
And	R.Status = 'O'
	
Drop Table #Recalls

Select	*
From	#Contracts

Drop Table #Contracts

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spContractGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContractSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spContractSet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spContractSet
	@BizDate datetime,
	@BookGroup varchar(10),
	@ContractId varchar(15),
	@ContractType char(1),
	@Book varchar(10) = Null,
	@SecId varchar(12) = Null,
	@Quantity bigint = Null,
	@QuantitySettled bigint = Null,
	@Amount money = Null,
	@AmountSettled money = Null,
	@CollateralCode varchar(1) = Null,
	@ValueDate datetime = Null,
	@SettleDate datetime = Null,
	@TermDate datetime = Null,
	@Rate decimal(8,5) = Null,
	@RateCode varchar(1) = Null,
	@StatusFlag varchar(1) = Null,
	@PoolCode varchar(1) = Null,
	@DivRate decimal(6,3) = Null,
	@DivCallable bit = Null,
	@IncomeTracked bit = Null,
	@MarginCode varchar(1) = Null,
	@Margin decimal(5, 2) = Null,
	@CurrencyIso char(3) = Null,
	@SecurityDepot varchar(2) = Null,
	@CashDepot varchar(2) = Null,
	@OtherBook varchar(10) = Null,
	@Comment varchar(20) = Null,
	@ReturnData bit = 0,
	@IsIncremental bit = 0
As
Begin

Set RowCount 1

If Exists (	Select BizDate
		From tbContracts
		Where	BizDate  	= @BizDate
		And	BookGroup  	= @BookGroup
		And	ContractId  	= @ContractId
		And	ContractType  	= @ContractType	)

	Update	tbContracts
	Set	Book 		= IsNull(@Book, Book),
		SecId 		= IsNull(@SecId, SecId),
		Quantity 	= Case	When @IsIncremental = 1
					Then IsNull(@Quantity, 0) + (Quantity)
					Else IsNull(@Quantity, Quantity) End,
		QuantitySettled	= Case	When @IsIncremental = 1
					Then IsNull(@QuantitySettled, 0) + (QuantitySettled)
					Else IsNull(@QuantitySettled, QuantitySettled) End,
		Amount 		= Case	When @IsIncremental = 1
					Then IsNull(@Amount, 0) + (Amount)
					Else IsNull(@Amount, Amount) End,
		AmountSettled	= Case	When @IsIncremental = 1
					Then IsNull(@AmountSettled, 0) + (AmountSettled)
					Else IsNull(@AmountSettled, AmountSettled) End,
		CollateralCode 	= IsNull(@CollateralCode, CollateralCode),
		ValueDate 	= IsNull(@ValueDate, ValueDate),
		SettleDate 	= IsNull(@SettleDate, SettleDate),
		TermDate 	= IsNull(@TermDate, TermDate),
		Rate 		= IsNull(@Rate, Rate),
		RateCode 	= IsNull(@RateCode, RateCode),
		StatusFlag 	= IsNull(@StatusFlag, StatusFlag),
		PoolCode 	= IsNull(@PoolCode, PoolCode),
		DivRate  	= IsNull(@DivRate, DivRate),
		DivCallable 	= IsNull(@DivCallable, DivCallable),
		IncomeTracked 	= IsNull(@IncomeTracked, IncomeTracked),
		MarginCode 	= IsNull(@MarginCode, MarginCode),	
		Margin 		= IsNull(@Margin,Margin),
		CurrencyIso 	= IsNull(@CurrencyIso, CurrencyIso),
		SecurityDepot 	= IsNull(@SecurityDepot, SecurityDepot),
		CashDepot 	= IsNull(@CashDepot, CashDepot),
		OtherBook 	= IsNull(@OtherBook, OtherBook),
		Comment 	= IsNull(@Comment, Comment)
	Where	BizDate  	= @BizDate
	And	BookGroup  	= @BookGroup
	And	ContractId  	= @ContractId
	And	ContractType  	= @ContractType	
Else
Begin
	Declare	@BizDatePrior datetime

	Select	@BizDatePrior 	= BizDate
	From	tbContracts
	Where	BizDate 	< @BizDate
	And	BizDate 	> DateAdd(d, -30, @BizDate)
	And	BookGroup  	= @BookGroup
	And	ContractId  	= @ContractId
	And	ContractType  	= @ContractType	
	Order By BizDate Desc		

	If (@BizDatePrior Is Not Null)
	Begin
		Insert	tbContracts
		Select	@BizDate,        
			BookGroup,
			ContractId,
			ContractType,
			Book,
			SecId,
			Quantity,
			QuantitySettled,
			Amount,
			AmountSettled,
			CollateralCode,
			ValueDate,
			SettleDate,
			TermDate,
			Rate,
			RateCode,
			StatusFlag,
			PoolCode,
			DivRate,
			DivCallable,
			IncomeTracked,
			MarginCode,
			Margin,
			CurrencyIso,
			SecurityDepot,
			CashDepot,
			OtherBook,
			Comment
		From	tbContracts
		Where	BizDate 	= @BizDatePrior
		And	BookGroup  	= @BookGroup
		And	ContractId  	= @ContractId
		And	ContractType  	= @ContractType	

		Update	tbContracts
		Set	Book 		= IsNull(@Book, Book),
			SecId 		= IsNull(@SecId, SecId),
			Quantity 	= Case	When @IsIncremental = 1
						Then IsNull(@Quantity, 0) + (Quantity)
						Else IsNull(@Quantity, Quantity) End,
			QuantitySettled	= Case	When @IsIncremental = 1
						Then IsNull(@QuantitySettled, 0) + (QuantitySettled)
						Else IsNull(@QuantitySettled, QuantitySettled) End,
			Amount 		= Case	When @IsIncremental = 1
						Then IsNull(@Amount, 0) + (Amount)
						Else IsNull(@Amount, Amount) End,
			AmountSettled	= Case	When @IsIncremental = 1
						Then IsNull(@AmountSettled, 0) + (AmountSettled)
						Else IsNull(@AmountSettled, AmountSettled) End,
			CollateralCode 	= IsNull(@CollateralCode, CollateralCode),
			ValueDate 	= IsNull(@ValueDate, ValueDate),
			SettleDate 	= IsNull(@SettleDate, SettleDate),
			TermDate 	= IsNull(@TermDate, TermDate),
			Rate 		= IsNull(@Rate, Rate),
			RateCode 	= IsNull(@RateCode, RateCode),
			StatusFlag 	= IsNull(@StatusFlag, StatusFlag),
			PoolCode 	= IsNull(@PoolCode, PoolCode),
			DivRate  	= IsNull(@DivRate, DivRate),
			DivCallable 	= IsNull(@DivCallable, DivCallable),
			IncomeTracked 	= IsNull(@IncomeTracked, IncomeTracked),
			MarginCode 	= IsNull(@MarginCode, MarginCode),	
			Margin 		= IsNull(@Margin,Margin),
			CurrencyIso 	= IsNull(@CurrencyIso, CurrencyIso),
			SecurityDepot 	= IsNull(@SecurityDepot, SecurityDepot),
			CashDepot 	= IsNull(@CashDepot, CashDepot),
			OtherBook 	= IsNull(@OtherBook, OtherBook),
			Comment 	= IsNull(@Comment, Comment)
		Where	BizDate  	= @BizDate
		And	BookGroup  	= @BookGroup
		And	ContractId  	= @ContractId
		And	ContractType  	= @ContractType	
	End
	Else
		Insert 	tbContracts
		Values	(
			@BizDate,
			@BookGroup,
			@ContractId,
			@ContractType,
			@Book,
			@SecId,
			Abs(IsNull(@Quantity, 0)),
			Abs(IsNull(@QuantitySettled, 0)),
			Abs(IsNull(@Amount, 0)),
			Abs(IsNull(@AmountSettled, 0)),
			@CollateralCode,
			@ValueDate,
			@SettleDate,
			@TermDate,
			@Rate,
			@RateCode,
			@StatusFlag,
			@PoolCode,
			@DivRate,
			@DivCallable,
			@IncomeTracked,
			@MarginCode,
			@Margin,
			@CurrencyIso,
			@SecurityDepot,
			@CashDepot,
			@OtherBook,
			@Comment
			)
End

Set RowCount 0

If (@ReturnData = 1)
	Exec	spContractGet @BizDate, @BookGroup, @ContractId, @ContractType

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spContractSet]  TO [roleMedalist]
GO

GRANT  EXECUTE  ON [dbo].[spContractSet]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContractBizDateList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spContractBizDateList]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spContractBizDateList
	@ListItemCountMax int = 50 
As
Begin

Set RowCount @ListItemCountMax

Select Distinct Convert(char(10), BizDate, 120) As BizDate 
From		tbBookGroups
Order By	1 Desc

Set RowCount 0

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spContractBizDateList]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spContractBizDateRoll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spContractBizDateRoll]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spContractBizDateRoll
            @BizDatePrior datetime,
            @BizDate datetime,
            @DayCount  smallint = 1,
            @RecordCount int = 0 Output
As
Begin 

Begin Transaction

Delete
From	tbBookGroups
Where	BizDate = @BizDate

Insert	tbBookGroups
Select	@BizDate,
	BookGroup,
	FundingRate,
	@DayCount
From	tbBookGroups
Where	BizDate = @BizDatePrior           

Delete
From	tbContracts
Where	BizDate = @BizDate

Insert	tbContracts
Select	@BizDate,        
	BookGroup,
	ContractId,
	ContractType,
	Book,
	SecId,
	Quantity,
	QuantitySettled,
	Amount,
	AmountSettled,
	CollateralCode,
	ValueDate,
	SettleDate,
	TermDate,
	Rate,
	RateCode,
	StatusFlag,
	PoolCode,
	DivRate,
	DivCallable,
	IncomeTracked,
	MarginCode,
	Margin,
	CurrencyIso,
	SecurityDepot,
	CashDepot,
	OtherBook,
	Comment
From	tbContracts
Where	BizDate = @BizDatePrior

Select	@RecordCount = @@RowCount

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spContractBizDateRoll]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbMarks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbMarks]
GO

CREATE TABLE [dbo].[tbMarks] (
	[BizDate] [datetime] NOT NULL ,
	[BookGroup] [varchar] (10) NOT NULL ,
	[ContractId] [varchar] (15) NOT NULL ,
	[ContractType] [char] (1) NOT NULL ,
	[Amount] [money] NOT NULL ,
	[State] [char] (1) NOT NULL ,
	[Comment] [varchar] (50) NOT NULL ,
	[ActUserId] [varchar] (50) NOT NULL ,
	[ActTime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE  INDEX [IX_tbMarks] ON [dbo].[tbMarks]([BizDate], [BookGroup], [ContractId], [ContractType]) ON [PRIMARY]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMarkSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spMarkSet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spMarkSet
	@BizDate datetime,
	@BookGroup varchar(10),
	@ContractId varchar(15),
	@ContractType char(1),
	@Amount money = Null,
	@State char(1) = Null,
	@Comment varchar(50) = Null,
	@ActUserId varchar(50) = Null
AS
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update	tbMarks
Set	Amount = IsNull(@Amount, Amount),
	State = IsNull(@State, State),
	Comment = IsNull(@Comment, Comment),
	ActUserId = IsNull(@ActUserId, ActUserId),
	ActTime   = GetUtcDate()
Where	BizDate = @BizDate
And	BookGroup = @BookGroup
And	ContractId = @ContractId
And	ContractType = @ContractType

If (@@RowCount = 0)
	Insert 	tbMarks
	Values	(
		@BizDate,
		@BookGroup,
		@ContractId,
		@ContractType,
		IsNull(@Amount, 0),
		IsNull(@State, 'U'),
		IsNull(@Comment, ''),
		IsNull(@ActUserId, 'ADMIN'),
		GetUtcDate()
		)

Update	tbContracts
Set	Amount = Amount + IsNull(@Amount, 0)
Where	BizDate = @BizDate
And	BookGroup = @BookGroup
And	ContractId = @ContractId
And	ContractType = @ContractType

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spMarkSet]  TO [roleMedalist]
GO

--------------------------------------------------------------------------------
--| End Contract modifications.


--| Deal modifications.
--------------------------------------------------------------------------------

If Not Exists (Select * From tbFunctions Where FunctionPath = 'PositionDealBlotter')
	Insert Into tbFunctions Values ('PositionDealBlotter', '0', '0', '[NONE]')
GO

if exists (select * From dbo.sysobjects Where id = object_id(N'[dbo].[tbDeals]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbDeals]
GO

CREATE TABLE [dbo].[tbDeals] (
	[DealId] [char] (16) NOT NULL ,
	[BookGroup] [varchar] (10) NOT NULL ,
	[DealType] [char] (1) NULL ,
	[Book] [varchar] (10) NULL ,
	[BookContact] [varchar] (15) NULL ,
	[SecId] [varchar] (12) NOT NULL ,
	[Quantity] [bigint] NULL ,
	[Amount] [money] NULL ,
	[CollateralCode] [varchar] (1) NULL ,
	[ValueDate] [datetime] NULL ,
	[SettleDate] [datetime] NULL ,
	[TermDate] [datetime] NULL ,
	[Rate] [decimal](8, 5) NULL ,
	[RateCode] [varchar] (1) NULL ,
	[PoolCode] [varchar] (1) NULL ,
	[DivRate] [decimal](6, 3) NULL ,
	[DivCallable] [bit] NULL ,
	[IncomeTracked] [bit] NULL ,
	[MarginCode] [varchar] (1) NULL ,
	[Margin] [decimal](5, 2) NULL ,
	[CurrencyIso] [char] (3) NULL ,
	[SecurityDepot] [varchar] (2) NULL ,
	[CashDepot] [varchar] (2) NULL ,
	[Comment] [varchar] (20) NULL ,
	[DealStatus] [varchar] (1) NOT NULL ,
	[IsActive] [bit] NOT NULL ,
	[ActUserId] [varchar] (50) NOT NULL ,
	[ActTime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbDeals] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbDeals] PRIMARY KEY  CLUSTERED 
	(
		[DealId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tbDeals] ADD 
	CONSTRAINT [DF_tbDeals_DealStatus] DEFAULT ('D') FOR [DealStatus],
	CONSTRAINT [DF_tbDeals_IsActive] DEFAULT (1) FOR [IsActive],
	CONSTRAINT [DF_tbDeals_ActUserId] DEFAULT ('ADMIN') FOR [ActUserId],
	CONSTRAINT [DF_tbDeals_ActTime] DEFAULT (GetUtcDate()) FOR [ActTime]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spDealGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spDealGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spDealGet 
	@DealId char(16) = Null,
	@DealIdPrefix char(1) = Null,
	@IsActive bit = 1,
	@UtcOffset smallint = 0
As
Begin

Select 		D.DealId,
		D.BookGroup,	
		D.DealType,
		D.Book,
		D.BookContact,
		D.SecId,
		SIL.SecIdLink As Symbol,
		D.Quantity,
		D.Amount,
		D.CollateralCode,
		D.ValueDate,
		D.SettleDate,
		D.TermDate,
		D.Rate,
		D.RateCode,
		D.PoolCode,
		D.DivRate,
		D.DivCallable,
		D.IncomeTracked,
		D.Margin,
		D.MarginCode,
		D.CurrencyIso,
		D.SecurityDepot,
		D.CashDepot,
		D.Comment,
		D.DealStatus,
		U.ShortName As ActUserShortName,
		DateAdd(n, @UtcOffset, D.ActTime) As ActTime,
		D.IsActive	
From 		tbDeals D,
		tbUsers U,
		tbSecIdLinks SIL
Where  		D.IsActive = IsNull(@IsActive, D.IsActive)
	And	D.DealId = IsNull(@DealId, D.DealId)
	And	Substring(D.DealId, 1, 1) = IsNull(@DealIdPrefix, Substring(D.DealId, 1, 1))	
	And	D.ActUserId *=U.UserId
	And	D.SecId *= SIL.SecId
	And	SIL.SecIdTypeIndex = 2
Order By 	1

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spDealGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spDealSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spDealSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spDealSet
	@DealId char(16),
	@BookGroup varchar(10) = Null,
	@DealType char(1) = Null,
	@Book varchar(10)= Null,
	@BookContact varchar(15) = Null,
	@SecId varchar(12) = Null,
	@Quantity bigint = Null,
	@Amount money = Null,
	@CollateralCode varchar(1) = Null,
	@ValueDate datetime = Null,
	@SettleDate datetime = Null,
	@TermDate datetime = Null,	
	@Rate decimal(8, 5) = Null,
	@RateCode varchar(1) = Null,
	@PoolCode varchar(1) = Null,
	@DivRate decimal(6, 3) = Null,
	@DivCallable bit = Null,
	@IncomeTracked bit = Null,
	@MarginCode varchar(1) = Null,
	@Margin decimal(5, 2) = Null,
	@CurrencyIso char(3) = Null,
	@SecurityDepot varchar(2) = Null,
	@CashDepot varchar(2) = Null,
	@Comment varchar(20) = Null,
	@DealStatus char(1) = Null,
	@IsActive bit= Null,
	@ActUserId varchar(50)=Null,
	@ReturnData bit = 0
As
Begin

Exec spSecIdSymbolLookup @SecId, @SecId Output

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update	tbDeals
Set	BookGroup 	= IsNull(@BookGroup, BookGroup),
	DealType 	= IsNull(@DealType, DealType),
	Book 		= IsNull(@Book, Book),
	BookContact 	= IsNull(@BookContact,BookContact),
	SecId 		= IsNull(@SecId, SecId),
	Quantity 	= IsNull(@Quantity, Quantity),
	Amount 		= IsNull(@Amount, Amount),
	CollateralCode 	= IsNull(@CollateralCode, CollateralCode),
	ValueDate 	= IsNull(@ValueDate, ValueDate),
	SettleDate 	= IsNull(@SettleDate, SettleDate),
	TermDate 	= IsNull(@TermDate, TermDate),
	Rate 		= IsNull(@Rate, Rate),
	RateCode 	= IsNull(@RateCode, RateCode),
	PoolCode 	= IsNull(@PoolCode, PoolCode),
	DivRate  	= IsNull(@DivRate, DivRate),
	DivCallable 	= IsNull(@DivCallable, DivCallable),
	IncomeTracked 	= IsNull(@IncomeTracked, IncomeTracked),
	MarginCode 	= IsNull(@MarginCode, MarginCode),	
	Margin 		= IsNull(@Margin,Margin),
	CurrencyIso 	= IsNull(@CurrencyIso, CurrencyIso),
	SecurityDepot 	= IsNull(@SecurityDepot, SecurityDepot),
	CashDepot 	= IsNull(@CashDepot, CashDepot),
	Comment 	= IsNull(@Comment, Comment),
	DealStatus 	= IsNull(@DealStatus, DealStatus),
	IsActive   	= IsNull(@IsActive, IsActive),
	ActUserId 	= IsNull(@ActUserId, ActUserId),
	ActTime   	= GetUtcDate()
Where	DealId  	= @DealId
	
If (@@RowCount = 0)
Begin
	Insert 	Into tbDeals
	Values	(
		@DealId,
		@BookGroup,		
		@DealType,
		@Book,
		@BookContact,
		@SecId,
		@Quantity,
		@Amount,
		IsNull(@CollateralCode, 'C'),
		@ValueDate,
		@SettleDate,
		@TermDate,	
		@Rate,
		@RateCode,
		IsNull(@PoolCode, ''),
		IsNull(@DivRate, 100.000),
		@DivCallable,
		IsNull(@IncomeTracked, 1),
		IsNull(@MarginCode, '%'),
		@Margin,
		IsNull(@CurrencyIso, 'USD'),
		IsNull(@SecurityDepot, '  '),
		IsNull(@CashDepot, '  '),
		IsNull(@Comment, ''),
		IsNull(@DealStatus, 'D'),
		IsNull(@IsActive, 1),
		IsNull(@ActUserId, 'ADMIN'),
		GetUtcDate()
		)
End

Commit Transaction

If (@ReturnData = 1)
	Exec spDealGet @DealId = @DealId, @IsActive = Null

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spDealSet]  TO [roleMedalist]
GO

--------------------------------------------------------------------------------
--| End Deal modifications.


--| Loanet modifications.
--------------------------------------------------------------------------------

if not exists (select * from dbo.sysusers where name = N'roleLoanet')
	EXEC sp_addrole N'roleLoanet'
GO

GRANT  EXECUTE  ON [dbo].[spKeyValueSet]  TO [roleLoanet]
GO

GRANT  EXECUTE  ON [dbo].[spKeyValueGet]  TO [roleLoanet]
GO

GRANT  EXECUTE  ON [dbo].[spHolidayGet]  TO [roleLoanet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbLoanetClients]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbLoanetClients]
GO

CREATE TABLE [dbo].[tbLoanetClients] (
	[BizDate] [datetime] NOT NULL ,
	[ClientId] [char] (4) NOT NULL ,
	[ContraClientId] [char] (4) NOT NULL ,
	[ContraClientDtc] [char] (4) NULL ,
	[MinMarkAmount] [char] (4) NULL ,
	[MinMarkPrice] [char] (1) NULL ,
	[MarkRoundHouse] [char] (3) NULL ,
	[MarkRoundInstitution] [char] (3) NULL ,
	[AccountName] [varchar] (30) NULL ,
	[AddressLine2] [varchar] (30) NULL ,
	[ParamsApply] [char] (1) NULL ,
	[BorrowMarkCode] [char] (1) NULL ,
	[BorrowCollateralCode] [char] (3) NULL ,
	[LoanMarkCode] [char] (1) NULL ,
	[LoanCollateralCode] [char] (3) NULL ,
	[AddressLine3] [varchar] (30) NULL ,
	[AddressLine4] [varchar] (30) NULL ,
	[RelatedAccount] [varchar] (4) NULL ,
	[BorrowLimit] [char] (10) NULL ,
	[BorrowDateChange] [char] (6) NULL ,
	[LoanLimit] [char] (10) NULL ,
	[LoanDateChange] [char] (6) NULL ,
	[BorrowSecLimit] [char] (10) NULL ,
	[BorrowSecDateChange] [char] (6) NULL ,
	[LoanSecLimit] [char] (10) NULL ,
	[LoanSecDateChange] [char] (6) NULL ,
	[TaxId] [varchar] (9) NULL ,
	[StockBorrowRate] [char] (5) NULL ,
	[StockLoanRate] [char] (5) NULL ,
	[BondBorrowRate] [char] (5) NULL ,
	[BondLoanRate] [char] (5) NULL ,
	[DtcMarkNumber] [char] (4) NULL ,
	[CreditLimitAccount] [varchar] (4) NULL ,
	[CallbackAccount] [varchar] (4) NULL ,
	[ThirdPartyInstructions] [varchar] (17) NULL ,
	[AdditionalAddress] [varchar] (25) NULL ,
	[OccAccountFlag] [varchar] (1) NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbLoanetClients] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbLoanetClients] PRIMARY KEY  CLUSTERED 
	(
		[BizDate],
		[ClientId],
		[ContraClientId]
	)  ON [PRIMARY] 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetClientInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetClientInsert]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetClientInsert
	@BizDate datetime,
	@ClientId char(4),
	@ContraClientId char(4),
	@ContraClientDtc char(4) = null,
	@MinMarkAmount char(4) = null,
	@MinMarkPrice char(1) = null,
	@MarkRoundHouse char(3) = null,
	@MarkRoundInstitution char(3) = null,
	@AccountName varchar(30) = null,
	@AddressLine2 varchar(30) = null,
	@ParamsApply char(1) = null,
	@BorrowMarkCode char(1) = null,
	@BorrowCollateralCode char(3) = null,
	@LoanMarkCode char(1) = null,
	@LoanCollateralCode char(3) = null,
	@AddressLine3 varchar(30) = null,
	@AddressLine4 varchar(30) = null,
	@RelatedAccount varchar(4) = null,
	@BorrowLimit char(10) = null,
	@BorrowDateChange char(6) = null,
	@LoanLimit char(10) = null,
	@LoanDateChange char(6) = null,
	@BorrowSecLimit char(10) = null,
	@BorrowSecDateChange char(6) = null,
	@LoanSecLimit char(10) = null,
	@LoanSecDateChange char(6) = null,
	@TaxId char(9) = null,
	@StockBorrowRate char(5) = null,
	@StockLoanRate char(5) = null,
	@BondBorrowRate char(5) = null,
	@BondLoanRate char(5) = null,
	@DtcMarkNumber char(4) = null,
	@CreditLimitAccount varchar(4) = null,
	@CallbackAccount varchar(4) = null,
	@ThirdPartyInstructions varchar(17) = null,
	@AdditionalAddress varchar(25) = null,
	@OccAccountFlag varchar(1) = null
As
Begin

Insert  tbLoanetClients
Values (
	@BizDate,
	@ClientId,
	@ContraClientId,
	@ContraClientDtc,
	@MinMarkAmount,
	@MinMarkPrice,
	@MarkRoundHouse,
	@MarkRoundInstitution,
	@AccountName,
	@AddressLine2,
	@ParamsApply,
	@BorrowMarkCode,
	@BorrowCollateralCode,
	@LoanMarkCode,
	@LoanCollateralCode,
	@AddressLine3,
	@AddressLine4,
	@RelatedAccount,
	@BorrowLimit,
	@BorrowDateChange,
	@LoanLimit,
	@LoanDateChange,
	@BorrowSecLimit,
	@BorrowSecDateChange,
	@LoanSecLimit,
	@LoanSecDateChange,
	@TaxId,
	@StockBorrowRate,
	@StockLoanRate,
	@BondBorrowRate,
	@BondLoanRate,
	@DtcMarkNumber,
	@CreditLimitAccount,
	@CallbackAccount,
	@ThirdPartyInstructions,
	@AdditionalAddress,
	@OccAccountFlag
	)
End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetClientInsert]  TO [roleLoanet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetClientPurge]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetClientPurge]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetClientPurge
	@BizDate datetime,
	@ClientId char(4)
As
Begin

Delete
From	tbLoanetClients
Where	BizDate = @BizDate
And	ClientId = @ClientId

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetClientPurge]  TO [roleLoanet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbLoanetClientsLong]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbLoanetClientsLong]
GO

CREATE TABLE [dbo].[tbLoanetClientsLong] (
	[BizDate] [datetime] NOT NULL ,
	[ClientId] [char] (4) NOT NULL ,
	[ContraClientId] [char] (4) NOT NULL ,
	[AccountName] [varchar] (30) NULL ,
	[AddressLine1] [varchar] (30) NULL ,
	[AddressLine2] [varchar] (30) NULL ,
	[AddressLine3] [varchar] (30) NULL ,
	[AddressLine4] [varchar] (30) NULL ,
	[AddressLine5] [varchar] (30) NULL ,
	[Phone] [char] (14) NULL ,
	[TaxId] [char] (10) NULL ,
	[ContraClientDtc] [char] (4) NULL ,
	[ThirdPartyInstructions] [varchar] (17) NULL ,
	[DeliveryInstructions] [varchar] (30) NULL ,
	[MarkDtc] [char] (4) NULL ,
	[MarkInstructions] [varchar] (30) NULL ,
	[RecallDtc] [char] (5) NULL ,
	[CdxCuId] [char] (4) NULL ,
	[OccDelivery] [char] (1) NULL ,
	[ParentAccount] [char] (4) NULL ,
	[AssociatedAccount] [char] (4) NULL ,
	[CreditLimitAccount] [char] (4) NULL ,
	[AssociatedCbAccount] [char] (4) NULL ,
	[BorrowLimit] [char] (14) NULL ,
	[BorrowDateChange] [char] (8) NULL ,
	[BorrowSecLimit] [char] (14) NULL ,
	[BorrowSecDateChange] [char] (8) NULL ,
	[LoanLimit] [char] (14) NULL ,
	[LoanDateChange] [char] (8) NULL ,
	[LoanSecLimit] [char] (14) NULL ,
	[LoanSecDateChange] [char] (8) NULL ,
	[MinMarkAmount] [char] (4) NULL ,
	[MinMarkPrice] [char] (1) NULL ,
	[MarkRoundHouse] [char] (3) NULL ,
	[MarkRoundInstitution] [char] (3) NULL ,
	[MarkValueHouse] [char] (4) NULL ,
	[MarkValueInstitution] [char] (4) NULL ,
	[BorrowMarkCode] [char] (1) NULL ,
	[BorrowCollateral] [char] (3) NULL ,
	[LoanMarkCode] [char] (1) NULL ,
	[LoanCollateral] [char] (3) NULL ,
	[IncludeAccrued] [char] (1) NULL ,
	[StandardMark] [char] (1) NULL ,
	[OmnibusMark] [char] (1) NULL ,
	[StockBorrowRate] [char] (5) NULL ,
	[StockLoanRate] [char] (5) NULL ,
	[BondBorrowRate] [char] (5) NULL ,
	[BondLoanRate] [char] (5) NULL ,
	[BusinessIndex] [char] (2) NULL ,
	[BusinessAmount] [char] (18) NULL ,
	[InstitutionalCashPool] [char] (1) NULL ,
	[InstitutionalFeeType] [char] (1) NULL ,
	[InstitutionalFeeRate] [char] (5) NULL ,
	[LoanEquity] [char] (1) NULL ,
	[LoanDebt] [char] (1) NULL ,
	[ReturnEquity] [char] (1) NULL ,
	[ReturnDebt] [char] (1) NULL ,
	[IncomeEquity] [char] (1) NULL ,
	[IncomeDebt] [char] (1) NULL ,
	[DayBasis] [char] (1) NULL ,
	[AccountClass] [char] (3) NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbLoanetClientsLong] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbLoanetClientsLong] PRIMARY KEY  CLUSTERED 
	(
		[BizDate],
		[ClientId],
		[ContraClientId]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetClientLongInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetClientLongInsert]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetClientLongInsert
	@BizDate [datetime],
	@ClientId [char] (4),
	@ContraClientId [char] (4),
	@AccountName [varchar] (30),
	@AddressLine1 [varchar] (30),
	@AddressLine2 [varchar] (30),
	@AddressLine3 [varchar] (30),
	@AddressLine4 [varchar] (30),
	@AddressLine5 [varchar] (30),
	@Phone [char] (14),
	@TaxId [char] (9),
	@ContraClientDtc [char] (4),
	@ThirdPartyInstructions [varchar] (17),
	@DeliveryInstructions [varchar] (30),
	@MarkDtc [char] (4),
	@MarkInstructions [varchar] (30),
	@RecallDtc [char] (5),
	@CdxCuId [char] (4),
	@OccDelivery [char] (1),
	@ParentAccount [char] (4),
	@AssociatedAccount [char] (4),
	@CreditLimitAccount [char] (4),
	@AssociatedCbAccount [char] (4),
	@BorrowLimit [char] (14),
	@BorrowDateChange [char] (8),
	@BorrowSecLimit [char] (14),
	@BorrowSecDateChange [char] (8),
	@LoanLimit [char] (14),
	@LoanDateChange [char] (8),
	@LoanSecLimit [char] (14),
	@LoanSecDateChange [char] (8),
	@MinMarkAmount [char] (4),
	@MinMarkPrice [char] (1),
	@MarkRoundHouse [char] (3),
	@MarkRoundInstitution [char] (3),
	@MarkValueHouse [char] (4),
	@MarkValueInstitution [char] (4),
	@BorrowMarkCode [char] (1),
	@BorrowCollateral [char] (3),
	@LoanMarkCode [char] (1),
	@LoanCollateral [char] (3),
	@IncludeAccrued [char] (1),
	@StandardMark [char] (1),
	@OmnibusMark [char] (1),
	@StockBorrowRate [char] (5),
	@StockLoanRate [char] (5),
	@BondBorrowRate [char] (5),
	@BondLoanRate [char] (5),
	@BusinessIndex [char] (2),
	@BusinessAmount [char] (18),
	@InstitutionalCashPool [char] (1),
	@InstitutionalFeeType [char] (1),
	@InstitutionalFeeRate [char] (5),
	@LoanEquity [char] (1),
	@LoanDebt [char] (1),
	@ReturnEquity [char] (1),
	@ReturnDebt [char] (1),
	@IncomeEquity [char] (1),
	@IncomeDebt [char] (1),
	@DayBasis [char] (1),
	@AccountClass [char] (3)
As
Begin

Insert  tbLoanetClientsLong
Values (
	@BizDate,
	@ClientId,
	@ContraClientId,
	@AccountName,
	@AddressLine1,
	@AddressLine2,
	@AddressLine3,
	@AddressLine4,
	@AddressLine5,
	@Phone,
	@TaxId,
	@ContraClientDtc,
	@ThirdPartyInstructions,
	@DeliveryInstructions,
	@MarkDtc,
	@MarkInstructions,
	@RecallDtc,
	@CdxCuId,
	@OccDelivery,
	@ParentAccount,
	@AssociatedAccount,
	@CreditLimitAccount,
	@AssociatedCbAccount,
	@BorrowLimit,
	@BorrowDateChange,
	@BorrowSecLimit,
	@BorrowSecDateChange,
	@LoanLimit,
	@LoanDateChange,
	@LoanSecLimit,
	@LoanSecDateChange,
	@MinMarkAmount,
	@MinMarkPrice,
	@MarkRoundHouse,
	@MarkRoundInstitution,
	@MarkValueHouse,
	@MarkValueInstitution,
	@BorrowMarkCode,
	@BorrowCollateral,
	@LoanMarkCode,
	@LoanCollateral,
	@IncludeAccrued,
	@StandardMark,
	@OmnibusMark,
	@StockBorrowRate,
	@StockLoanRate,
	@BondBorrowRate,
	@BondLoanRate,
	@BusinessIndex,
	@BusinessAmount,
	@InstitutionalCashPool,
	@InstitutionalFeeType,
	@InstitutionalFeeRate,
	@LoanEquity,
	@LoanDebt,
	@ReturnEquity,
	@ReturnDebt,
	@IncomeEquity,
	@IncomeDebt,
	@DayBasis,
	@AccountClass
	)
End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetClientLongInsert]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetClientLongPurge]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetClientLongPurge]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetClientLongPurge
	@BizDate datetime,
	@ClientId char(4)
As
Begin

Delete
From	tbLoanetClientsLong
Where	BizDate = @BizDate
And	ClientId = @ClientId

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetClientLongPurge]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbLoanetCollateral]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbLoanetCollateral]
GO

CREATE TABLE [dbo].[tbLoanetCollateral] (
	[BizDate] [datetime] NOT NULL ,
	[ClientId] [char] (4) NOT NULL ,
	[ContraClientId] [char] (4) NOT NULL ,
	[ContractType] [char] (1) NOT NULL ,
	[CollateralType] [char] (1) NOT NULL ,
	[SecId] [char] (9) NULL ,
	[Quantity] [bigint] NULL ,
	[Amount] [money] NULL ,
	[CurrencyIso] [char] (3) NULL ,
	[ContractId] [char] (9) NULL ,
	[ExpiryDate] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE  INDEX [IX_tbLoanetCollateral] ON [dbo].[tbLoanetCollateral]([BizDate], [ClientId], [ContraClientId]) ON [PRIMARY]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetCollateralInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetCollateralInsert]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spLoanetCollateralInsert
	@BizDate datetime,
	@ClientId char(4),
	@ContraClientId char(4),
	@ContractType char(1),
	@CollateralType char(1),
	@SecId varchar(12),
	@Quantity bigint,
	@Amount money,
	@CurrencyIso char(3),
	@ContractId char(9),
	@ExpiryDate datetime
As
Begin
	
Insert	tbLoanetCollateral
Values	(
	@BizDate,
	@ClientId,
	@ContraClientId,
	@ContractType,
	@CollateralType,
	@SecId,
	@Quantity,
	@Amount,
	@CurrencyIso,
	@ContractId,
	@ExpiryDate
	)	 

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetCollateralInsert]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetCollateralPurge]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetCollateralPurge]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spLoanetCollateralPurge
	@BizDate datetime,
	@ClientId char(4)
As
Begin
	
Delete
From		tbCollateral
Where		BizDate = @BizDate
	And	ClientId = @ClientId	
	And	ContraClientId = ContraClientId	

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetCollateralPurge]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbLoanetContractControl]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbLoanetContractControl]
GO

CREATE TABLE [dbo].[tbLoanetContractControl] (
	[ClientId] [char] (4) NOT NULL ,
	[BizDate] [datetime] NULL ,
	[RecordCount] [int] NULL ,
	[FundingRate] [decimal] (8,5) NULL ,
	[ScribeTime] [datetime] NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbLoanetContractControl] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbLoanetContractControl] PRIMARY KEY  CLUSTERED 
	(
		[ClientId]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetContractControlSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetContractControlSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spLoanetContractControlSet
	@ClientId char(4),
	@BizDate datetime,
	@RecordCount int,
	@FundingRate float
	
	
	-- ToDo: Add DayCount.
	
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update	tbLoanetContractControl
Set	BizDate = @BizDate,
	RecordCount = @RecordCount,
	FundingRate = @FundingRate,
	ScribeTime = GetUtcDate()
Where	ClientId = @ClientId

If (@@RowCount = 0)		
	Insert	tbLoanetContractControl
	Values	(@ClientId, @BizDate, @RecordCount, @FundingRate, GetUtcDate())

Update	tbBookGroups
Set	FundingRate = @FundingRate
Where	BookGroup = @ClientId
And	BizDate = @BizDate

If (@@RowCount = 0)		
	Insert	tbBookGroups
	Values	(@BizDate, @ClientId, @FundingRate, 1)

Update	tbBooks
Set	RateStockBorrow = @FundingRate,
	RateStockLoan = @FundingRate
Where	BookGroup = @ClientId
And	Book = @ClientId

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetContractControlSet]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbLoanetContracts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbLoanetContracts]
GO

CREATE TABLE [dbo].[tbLoanetContracts] (
	[BizDate] [datetime] NOT NULL ,
	[ClientId] [char] (4) NOT NULL ,
	[ContractId] [char] (9) NOT NULL ,
	[ContractType] [char] (1) NOT NULL ,
	[ContraClientId] [char] (4) NOT NULL ,
	[SecId] [char] (9) NOT NULL ,
	[Quantity] [bigint] NOT NULL ,
	[Amount] [money] NOT NULL ,
	[CollateralCode] [varchar] (1) NOT NULL ,
	[ValueDate] [datetime] NULL ,
	[SettleDate] [datetime] NULL ,
	[TermDate] [datetime] NULL ,
	[Rate] [decimal] (8,5) NOT NULL ,
	[RateCode] [varchar] (1) NOT NULL ,
	[StatusFlag] [varchar] (1) NOT NULL ,
	[PoolCode] [varchar] (1) NOT NULL ,
	[DivRate] [decimal] (6,3) NOT NULL ,
	[DivCallable] [bit] NOT NULL ,
	[IncomeTracked] [bit] NOT NULL ,
	[MarginCode] [varchar] (1) NOT NULL ,
	[Margin] [decimal] (5, 2) NOT NULL ,
	[CurrencyIso] [char] (3) NOT NULL ,
	[SecurityDepot] [varchar] (2) NOT NULL ,
	[CashDepot] [varchar] (2) NOT NULL ,
	[OtherClientId] [varchar] (4) NOT NULL ,
	[Comment] [varchar] (20) NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbLoanetContracts] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbLoanetContracts] PRIMARY KEY  CLUSTERED 
	(
		[BizDate],
		[ClientId],
		[ContractId],
		[ContractType]
	)  ON [PRIMARY] 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetContractInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetContractInsert]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spLoanetContractInsert
	@BizDate datetime,
	@ClientId char(4),
	@ContractId char(9),
	@ContractType char(1),
	@ContraClientId char(4),
	@SecId varchar(12),
	@Quantity bigint,
	@Amount money,
	@CollateralCode varchar(1),
	@ValueDate datetime,
	@SettleDate datetime,
	@TermDate datetime,
	@Rate decimal(8,5),
	@RateCode varchar(1),
	@StatusFlag varchar(1),
	@PoolCode varchar(1),
	@DivRate decimal(6,3),
	@DivCallable bit,
	@IncomeTracked bit,
	@MarginCode varchar(1),
	@Margin decimal(5, 2),
	@CurrencyIso char(3),
	@SecurityDepot varchar(2),
	@CashDepot varchar(2),
	@OtherClientId varchar(4),
	@Comment varchar(20)
As
Begin

Insert	tbLoanetContracts
Values	(
	@BizDate,
	@ClientId,
	@ContractId,
	@ContractType,
	@ContraClientId,
	@SecId,
	@Quantity,
	@Amount,
	@CollateralCode,
	@ValueDate,
	@SettleDate,
	@TermDate,
	@Rate,
	@RateCode,
	@StatusFlag,
	@PoolCode,
	@DivRate,
	@DivCallable,
	@IncomeTracked,
	@MarginCode,
	@Margin,
	@CurrencyIso,
	@SecurityDepot,
	@CashDepot,
	@OtherClientId,
	@Comment
	)	 
End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetContractInsert]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetContractPurge]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetContractPurge]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spLoanetContractPurge
	@BizDate datetime,
	@ClientId char(4)
As
Begin

Delete
From		tbLoanetContracts
Where		BizDate = @BizDate
	And	ClientId = @ClientId

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetContractPurge]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbLoanetMarks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbLoanetMarks]
GO

CREATE TABLE [dbo].[tbLoanetMarks] (
	[BizDate] [datetime] NOT NULL ,
	[ClientId] [char] (4) NOT NULL ,
	[ContractId] [char] (9) NOT NULL ,
	[ContractType] [char] (1) NOT NULL ,
	[Amount] [money] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbLoanetMarks] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbLoanetMarks] PRIMARY KEY  CLUSTERED 
	(
		[BizDate],
		[ClientId],
		[ContractId],
		[ContractType]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetMarkInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetMarkInsert]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetMarkInsert
	@BizDate datetime,
	@ClientId char(4),
	@ContractId char(9),
	@ContractType char(1),
	@Amount money
AS
Begin

Insert 	tbLoanetMarks
Values	(
	@BizDate,
	@ClientId,
	@ContractId,
	@ContractType,
	@Amount
	)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetMarkInsert]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetMarkPurge]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetMarkPurge]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spLoanetMarkPurge
	@BizDate datetime,
	@ClientId char(4) = Null
As
Begin
	
Delete
From		tbLoanetMarks
Where		BizDate = @BizDate
	And	ClientId = IsNull(@ClientId, ClientId)	

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetMarkPurge]  TO [roleLoanet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbLoanetRecalls]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbLoanetRecalls]
GO

CREATE TABLE [dbo].[tbLoanetRecalls] (
	[BizDate] [datetime] NOT NULL ,
	[RecallId] [char] (16) NOT NULL ,
	[ClientId] [char] (4)  NOT NULL ,
	[ContractId] [char] (9)  NOT NULL ,
	[ContractType] [char] (1)  NOT NULL ,
	[ContraClientId] [char] (4)  NOT NULL ,
	[SecId] [char] (9)  NOT NULL ,
	[Quantity] [bigint] NOT NULL ,
	[RecallDate] [datetime] NULL ,
	[BuyInDate] [datetime] NULL ,
	[Status] [char] (1)  NOT NULL ,
	[ReasonCode] [char] (2)  NOT NULL ,
	[SequenceNumber] [smallint] NOT NULL ,
	[Comment] [varchar] (11)  NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbLoanetRecalls] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbLoanetRecalls] PRIMARY KEY  CLUSTERED 
	(
		[BizDate],
		[RecallId]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetRecallInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetRecallInsert]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spLoanetRecallInsert
	@BizDate datetime,
	@RecallId char(16),
	@ClientId char(4),
	@ContractId char(9),
	@ContractType char(1),
	@ContraClientId char(4),
	@SecId varchar(12),
	@Quantity bigint,
	@RecallDate datetime,
	@BuyInDate datetime,
	@Status char(1),
	@ReasonCode char(2),
	@SequenceNumber smallint,
	@Comment varchar(11)
As
Begin
	
Insert	tbLoanetRecalls
Values	(
	@BizDate,
	@RecallId,
	@ClientId,
	@ContractId,
	@ContractType,
	@ContraClientId,
	@SecId,
	@Quantity,
	@RecallDate,
	@BuyInDate,
	@Status,
	@ReasonCode,
	@SequenceNumber,
	@Comment
	)	 

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetRecallInsert]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetRecallPurge]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetRecallPurge]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spLoanetRecallPurge
	@BizDate datetime,
	@ClientId char(4)
As
Begin
	
Delete
From	tbLoanetRecalls
Where	BizDate = @BizDate
And	ClientId = @ClientId

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetRecallPurge]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetBookUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetBookUpdate]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetBookUpdate
	@BizDate datetime,
	@ClientId char(4)
As
Begin

Select	Distinct
	LC1.ContraClientId As Book,
	Case	When	LC1.CreditLimitAccount = '' Then LC1.ContraClientId
		Else	LC1.CreditLimitAccount End As BookParent,
	LC1.AccountName As BookName,
	LC1.AddressLine2 As AddressLine1,
	LC1.AddressLine3 As AddressLine2,
	LC1.AddressLine4 As AddressLine3,
	LC1.ContraClientDtc As DtcDeliver,
	LC1.DtcMarkNumber As DtcMark,
	Convert(int, LC1.MinMarkAmount) As AmountMinimum,
	Convert(int, LC1.MinMarkPrice) As PriceMinimum,
	Convert(decimal(5, 2), Convert(decimal(3), LC1.BorrowCollateralCode) / 100) As MarginBorrow,
	Convert(decimal(5, 2), Convert(decimal(3), LC1.LoanCollateralCode) / 100) As MarginLoan,
	LC1.MarkRoundHouse,
	LC1.MarkRoundInstitution,
	Convert(bigint, LC1.BorrowLimit) As AmountLimitBorrow,
	Convert(bigint, LC1.LoanLimit) As AmountLimitLoan,
	Case	When	LC1.StockLoanRate = '00000' Then Null
		Else	Convert(decimal(8,5), Convert(decimal(5), LC1.StockLoanRate) / 1000) End As RateStockBorrow,
	Case	When	LC1.StockBorrowRate = '00000' Then Null
		Else	Convert(decimal(8,5), Convert(decimal(5), LC1.StockBorrowRate) / 1000) End As RateStockLoan,
	Case	When	LC1.BondLoanRate = '00000' Then Null
		Else	Convert(decimal(8,5), Convert(decimal(5), LC1.BondLoanRate) / 1000) End As RatebondBorrow,
	Case	When	LC1.BondBorrowRate = '00000' Then Null
		Else	Convert(decimal(8,5), Convert(decimal(5), LC1.BondBorrowRate) / 1000) End As RateBondLoan,
	Case	When	LC1.TaxId = '000000000' Then Null
		Else	LC1.TaxId End As TaxId
Into	#Clients
From	tbLoanetClients LC1,
	tbLoanetContracts LC
Where	LC1.BizDate = @BizDate
And	LC1.ClientId = @ClientId
And	LC1.ContraClientId = LC.ContraClientId
And	LC.BizDate > DateAdd(dd, -90, @BizDate)
And	LC.ClientId = @ClientId

While (@@RowCount != 0)
	Insert	#Clients
	Select	LC1.ContraClientId As Book,
		Case	When	LC1.CreditLimitAccount = '' Then LC1.ContraClientId
			Else	LC1.CreditLimitAccount End As BookParent,
		LC1.AccountName As BookName,
		LC1.AddressLine2 As AddressLine1,
		LC1.AddressLine3 As AddressLine2,
		LC1.AddressLine4 As AddressLine3,
		LC1.ContraClientDtc As DtcDeliver,
		LC1.DtcMarkNumber As DtcMark,
		Convert(int, LC1.MinMarkAmount) As AmountMinimum,
		Convert(int, LC1.MinMarkPrice) As PriceMinimum,
		Convert(decimal(5, 2), Convert(decimal(3), LC1.BorrowCollateralCode) / 100) As MarginBorrow,
		Convert(decimal(5, 2), Convert(decimal(3), LC1.LoanCollateralCode) / 100) As MarginLoan,
		LC1.MarkRoundHouse,
		LC1.MarkRoundInstitution,
		Convert(bigint, LC1.BorrowLimit) As AmountLimitBorrow,
		Convert(bigint, LC1.LoanLimit) As AmountLimitLoan,
		Case	When	LC1.StockLoanRate = '00000' Then Null
			Else	Convert(decimal(8,5), Convert(decimal(5), LC1.StockLoanRate) / 1000) End As RateStockBorrow,
		Case	When	LC1.StockBorrowRate = '00000' Then Null
			Else	Convert(decimal(8,5), Convert(decimal(5), LC1.StockBorrowRate) / 1000) End As RateStockLoan,
		Case	When	LC1.BondLoanRate = '00000' Then Null
			Else	Convert(decimal(8,5), Convert(decimal(5), LC1.BondLoanRate) / 1000) End As RatebondBorrow,
		Case	When	LC1.BondBorrowRate = '00000' Then Null
			Else	Convert(decimal(8,5), Convert(decimal(5), LC1.BondBorrowRate) / 1000) End As RateBondLoan,
		Case	When	LC1.TaxId = '000000000' Then Null
			Else	LC1.TaxId End As TaxId
	From	tbLoanetClients LC1
	Where	LC1.BizDate = @BizDate
	And	LC1.ClientId = @ClientId
	And	LC1.ContraClientId Not In (
			Select	Book
			From	#Clients )
	And	LC1.ContraClientId In (
			Select	BookParent
			From	#Clients )

Update	tbBooks
Set	BookParent = C.BookParent,
	BookName = C.BookName,
	AddressLine1 = C.AddressLine1,
	AddressLine2 = C.AddressLine2,
	AddressLine3 = C.AddressLine3,
	DtcDeliver = C.DtcDeliver,
	DtcMark	= C.DtcMark,
	AmountMinimum = C.AmountMinimum,
	PriceMinimum = C.PriceMinimum,
	MarginBorrow = C.MarginBorrow,
	MarginLoan = C.MarginLoan,
	MarkRoundHouse = C.MarkRoundHouse,
	MarkRoundInstitution = C.MarkRoundInstitution,
	AmountLimitBorrow = C.AmountLimitBorrow,
	AmountLimitLoan = C.AmountLimitLoan,
	RateStockBorrow = IsNull(C.RateStockBorrow, B.RateStockBorrow),
	RateStockLoan = IsNull(C.RateStockLoan, B.RateStockLoan),
	RateBondBorrow = IsNull(C.RateBondBorrow, B.RateBondBorrow),
	RateBondLoan = IsNull(C.RateBondLoan, B.RateBondLoan),
	TaxId = IsNull(C.TaxId, B.TaxId),
	ActUserId = 'ADMIN',
	ActTime = GetUtcDate(),
	IsActive = 1
From	tbBooks B,
	#Clients C
Where	B.BookGroup = @ClientId
And	B.Book = C.Book 
And	C.Book != @ClientId

Insert	tbBooks
Select	@ClientId,
	Book,
	BookParent,
	BookName,
	AddressLine1,
	AddressLine2,
	AddressLine3,
	Null,
	DtcDeliver,
	DtcMark,
	AmountMinimum,
	PriceMinimum,
	MarginBorrow,
	MarginLoan,
	MarkRoundHouse,
	MarkRoundInstitution,
	AmountLimitBorrow,
	AmountLimitLoan,
	RateStockBorrow,
	RateStockLoan,
	RateBondBorrow,
	RateBondLoan,
	TaxId,
	Null,
	Null,
	Null,
	Null,
	Null,
	'ADMIN',
	GetUtcDate(),
	1
From	#Clients
Where	@ClientId + Book Not In (Select BookGroup + Book From tbBooks)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetBookUpdate]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetBookLongUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetBookLongUpdate]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetBookLongUpdate
	@BizDate datetime,
	@ClientId char(4)
As
Begin

Select	Distinct
	LC1.ContraClientId As Book,
	Case	When	LC1.CreditLimitAccount = '    ' Then LC1.ContraClientId
		Else	LC1.CreditLimitAccount End As BookParent,
	LC1.AccountName As BookName,
	LC1.AddressLine1,
	LC1.AddressLine2,
	LC1.AddressLine3,
	LC1.Phone,
	LC1.ContraClientDtc As DtcDeliver,
	LC1.MarkDtc As DtcMark,
	Convert(int, LC1.MinMarkAmount) As AmountMinimum,
	Convert(int, LC1.MinMarkPrice) As PriceMinimum,
	Convert(decimal(5, 2), Convert(decimal(3), LC1.BorrowCollateral) / 100) As MarginBorrow,
	Convert(decimal(5, 2), Convert(decimal(3), LC1.LoanCollateral) / 100) As MarginLoan,
	LC1.MarkRoundHouse,
	LC1.MarkRoundInstitution,
	Convert(bigint, LC1.BorrowLimit) As AmountLimitBorrow,
	Convert(bigint, LC1.LoanLimit) As AmountLimitLoan,
	Case	When	LC1.StockLoanRate = '00000' Then Null
		Else	Convert(decimal(8,5), Convert(decimal(5), LC1.StockLoanRate) / 1000) End As RateStockBorrow,
	Case	When	LC1.StockBorrowRate = '00000' Then Null
		Else	Convert(decimal(8,5), Convert(decimal(5), LC1.StockBorrowRate) / 1000) End As RateStockLoan,
	Case	When	LC1.BondLoanRate = '00000' Then Null
		Else	Convert(decimal(8,5), Convert(decimal(5), LC1.BondLoanRate) / 1000) End As RatebondBorrow,
	Case	When	LC1.BondBorrowRate = '00000' Then Null
		Else	Convert(decimal(8,5), Convert(decimal(5), LC1.BondBorrowRate) / 1000) End As RateBondLoan,
	Case	When	LC1.TaxId = '000000000' Then Null
		Else	LC1.TaxId End As TaxId
Into	#Clients
From	tbLoanetClientsLong LC1,
	tbLoanetContracts LC
Where	LC1.BizDate = @BizDate
And	LC1.ClientId = @ClientId
And	LC1.ContraClientId = LC.ContraClientId
And	LC.BizDate > DateAdd(dd, -90, @BizDate)
And	LC.ClientId = @ClientId

While (@@RowCount != 0)
	Insert	#Clients
	Select	LC1.ContraClientId As Book,
		Case	When	LC1.CreditLimitAccount = '    ' Then LC1.ContraClientId
			Else	LC1.CreditLimitAccount End As BookParent,
		LC1.AccountName As BookName,
		LC1.AddressLine1,
		LC1.AddressLine2,
		LC1.AddressLine3,
		LC1.Phone,
		LC1.ContraClientDtc As DtcDeliver,
		LC1.MarkDtc As DtcMark,
		Convert(int, LC1.MinMarkAmount) As AmountMinimum,
		Convert(int, LC1.MinMarkPrice) As PriceMinimum,
		Convert(decimal(5, 2), Convert(decimal(3), LC1.BorrowCollateral) / 100) As MarginBorrow,
		Convert(decimal(5, 2), Convert(decimal(3), LC1.LoanCollateral) / 100) As MarginLoan,
		LC1.MarkRoundHouse,
		LC1.MarkRoundInstitution,
		Convert(bigint, LC1.BorrowLimit) As AmountLimitBorrow,
		Convert(bigint, LC1.LoanLimit) As AmountLimitLoan,
		Case	When	LC1.StockLoanRate = '00000' Then Null
			Else	Convert(decimal(8,5), Convert(decimal(5), LC1.StockLoanRate) / 1000) End As RateStockBorrow,
		Case	When	LC1.StockBorrowRate = '00000' Then Null
			Else	Convert(decimal(8,5), Convert(decimal(5), LC1.StockBorrowRate) / 1000) End As RateStockLoan,
		Case	When	LC1.BondLoanRate = '00000' Then Null
			Else	Convert(decimal(8,5), Convert(decimal(5), LC1.BondLoanRate) / 1000) End As RatebondBorrow,
		Case	When	LC1.BondBorrowRate = '00000' Then Null
			Else	Convert(decimal(8,5), Convert(decimal(5), LC1.BondBorrowRate) / 1000) End As RateBondLoan,
		Case	When	LC1.TaxId = '000000000' Then Null
			Else	LC1.TaxId End As TaxId
	From	tbLoanetClientsLong LC1
	Where	LC1.BizDate = @BizDate
	And	LC1.ClientId = @ClientId
	And	LC1.ContraClientId Not In (
			Select	Book
			From	#Clients )
	And	LC1.ContraClientId In (
			Select	BookParent
			From	#Clients )

Update	tbBooks
Set	BookParent = C.BookParent,
	BookName = C.BookName,
	AddressLine1 = C.AddressLine1,
	AddressLine2 = C.AddressLine2,
	AddressLine3 = C.AddressLine3,
	Phone = C.Phone,
	DtcDeliver = C.DtcDeliver,
	DtcMark	= C.DtcMark,
	AmountMinimum = C.AmountMinimum,
	PriceMinimum = C.PriceMinimum,
	MarginBorrow = C.MarginBorrow,
	MarginLoan = C.MarginLoan,
	MarkRoundHouse = C.MarkRoundHouse,
	MarkRoundInstitution = C.MarkRoundInstitution,
	AmountLimitBorrow = C.AmountLimitBorrow,
	AmountLimitLoan = C.AmountLimitLoan,
	RateStockBorrow = IsNull(C.RateStockBorrow, B.RateStockBorrow),
	RateStockLoan = IsNull(C.RateStockLoan, B.RateStockLoan),
	RateBondBorrow = IsNull(C.RateBondBorrow, B.RateBondBorrow),
	RateBondLoan = IsNull(C.RateBondLoan, B.RateBondLoan),
	TaxId = IsNull(C.TaxId, B.TaxId),
	ActUserId = 'ADMIN',
	ActTime = GetUtcDate(),
	IsActive = 1
From	tbBooks B,
	#Clients C
Where	B.BookGroup = @ClientId
And	B.Book = C.Book 
And	C.Book != @ClientId

Insert	tbBooks
Select	@ClientId,
	Book,
	BookParent,
	BookName,
	AddressLine1,
	AddressLine2,
	AddressLine3,
	Phone,
	DtcDeliver,
	DtcMark,
	AmountMinimum,
	PriceMinimum,
	MarginBorrow,
	MarginLoan,
	MarkRoundHouse,
	MarkRoundInstitution,
	AmountLimitBorrow,
	AmountLimitLoan,
	RateStockBorrow,
	RateStockLoan,
	RateBondBorrow,
	RateBondLoan,
	TaxId,
	Null,
	Null,
	Null,
	Null,
	Null,
	'ADMIN',
	GetUtcDate(),
	1
From	#Clients
Where	@ClientId + Book Not In (Select BookGroup + Book From tbBooks)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetBookLongUpdate]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetContractUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetContractUpdate]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetContractUpdate
	@BizDate datetime,
	@ClientId char(4)
As
Begin

Select	ClientId As BookGroup,
	ContractId,
	ContractType,
	ContraClientId As Book,
	SecId,
	Quantity,
	Quantity As QuantitySettled,
	Amount,
	Amount As AmountSettled,
	CollateralCode,
	ValueDate,
	SettleDate,
	TermDate,
	Rate,
	RateCode,
	StatusFlag,
	PoolCode,
	DivRate,
	DivCallable,
	IncomeTracked,
	MarginCode,
	Margin,
	CurrencyIso,
	SecurityDepot,
	CashDepot,
	OtherClientId As OtherBook,
	Comment
Into	#Loanet
From	tbLoanetContracts
Where	BizDate = @BizDate
And	ClientId = @ClientId

Update	tbContracts
Set	Quantity = L.Quantity,
	QuantitySettled = L.QuantitySettled,
	Amount = L.Amount,
	AmountSettled = L.AmountSettled,
	CollateralCode = L.CollateralCode,
	ValueDate = L.ValueDate,
	SettleDate = L.SettleDate,
	TermDate = L.TermDate,
	Rate = L.Rate,
	RateCode = L.RateCode,
	StatusFlag = L.StatusFlag,
	PoolCode = L.PoolCode,
	DivRate = L.DivRate,
	DivCallable = L.DivCallable,
	IncomeTracked = L.IncomeTracked,
	MarginCode = L.MarginCode,
	Margin = L.Margin,
	CurrencyIso = L.CurrencyIso,
	SecurityDepot = L.SecurityDepot,
	CashDepot = L.CashDepot,
	OtherBook = L.OtherBook,
	Comment = L.Comment
From	tbContracts C,
	#Loanet L
Where	C.BizDate = @BizDate
And	C.BookGroup = @ClientId
And	C.ContractId = L.ContractId
And	C.ContractType = L.ContractType
And	(
	(C.Quantity != L.Quantity) Or
	(C.QuantitySettled != L.QuantitySettled) Or
	(C.Amount != L.Amount) Or
	(C.AmountSettled != L.AmountSettled) Or
	(C.CollateralCode != L.CollateralCode) Or
	(IsNull(C.ValueDate, '') != IsNull(L.ValueDate, '')) Or
	(IsNull(C.SettleDate, '') != IsNull(L.SettleDate, '')) Or
	(IsNull(C.TermDate, '') != IsNull(L.TermDate, '')) Or
	(C.Rate != L.Rate) Or
	(C.RateCode != L.RateCode) Or
	(C.StatusFlag != L.StatusFlag) Or
	(C.PoolCode != L.PoolCode) Or
	(C.DivRate != L.DivRate) Or
	(C.DivCallable != L.DivCallable) Or
	(C.IncomeTracked != L.IncomeTracked) Or
	(C.MarginCode != L.MarginCode) Or
	(C.Margin != L.Margin) Or
	(C.CurrencyIso != L.CurrencyIso) Or
	(C.SecurityDepot != L.SecurityDepot) Or
	(C.CashDepot != L.CashDepot) Or
	(C.OtherBook != L.OtherBook) Or
	(C.Comment != L.Comment)
	)

Insert	tbContracts
Select	@BizDate,	
	BookGroup,
	ContractId,
	ContractType,
	Book,
	SecId,
	Quantity,
	QuantitySettled,
	Amount,
	AmountSettled,
	CollateralCode,
	ValueDate,
	SettleDate,
	TermDate,
	Rate,
	RateCode,
	StatusFlag,
	PoolCode,
	DivRate,
	DivCallable,
	IncomeTracked,
	MarginCode,
	Margin,
	CurrencyIso,
	SecurityDepot,
	CashDepot,
	OtherBook,
	Comment
From	#Loanet
Where	ContractId + ContractType Not In (
		Select	ContractId + ContractType
		From	tbContracts
		Where	BizDate = @BizDate
		And	BookGroup = @ClientId )

Delete
From	tbContracts
Where	BizDate = @BizDate
And	BookGroup = @ClientId
And	ContractId + ContractType Not In (
		Select	ContractId + ContractType
		From	#Loanet )

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetContractUpdate]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetRecallUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetRecallUpdate]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetRecallUpdate
	@BizDate datetime,
	@ClientId char(4)
As
Begin

-- Create a disposable temporary table of Loanet recalls for COB @BizDate.
Select	*
Into	#RecallPosition
From	tbLoanetRecalls
Where	BizDate = @BizDate
And	ClientId = @ClientId

-- Update local recall Quantity and BizDate.
Update	tbRecalls
Set	BizDate = RP.BizDate,
	Quantity = RP.Quantity,
	Status = Case When R.Status != 'B' Then 'O' Else R.Status End
From	tbRecalls R,
	#RecallPosition RP
Where	R.RecallId = RP.RecallId

-- Close local recalls where open in local set but not in the Loanet set.
Update	tbRecalls
Set	Status = 'C'
Where	RecallId Not In (Select RecallId From #RecallPosition)
And	BookGroup = @ClientId

-- Remove recalls from the Loanet set known in the local set.
Delete
From	#RecallPosition
Where	RecallId In (Select RecallId From tbRecalls Where BizDate = @BizDate And BookGroup = @ClientId)

-- Insert recalls from the Loanet set not known in the local set.
Insert	tbRecalls
Select	RecallId,
	ClientId,
	ContractId,
	ContractType,
	ContraClientId,
	SecId,
	Quantity,
	BuyInDate,
	Null,
	RecallDate,
	ReasonCode,
	SequenceNumber,
	Comment,
	null,
	'ADMIN',
	GetUtcDate(),	
	@BizDate,
	'O',
	Null,
	Null,
	Null,
	Null,
	Null
From	#RecallPosition

Drop Table #RecallPosition

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetRecallUpdate]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetContractSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetContractSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetContractSet 
	@BookGroup char(4),
	@DealId char(16),
	@ContractId char(9),
	@ContractType char(1),
	@Status char(1)
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

If (@Status = 'F')
Begin
	Update  tbDeals
	Set	DealStatus = 'D'
	Where	DealId    = @DealId
	And	BookGroup = @BookGroup
	And	DealType  = @ContractType
End


If (@Status = 'A')
Begin
	Insert Into tbContracts
	Select 	(Select KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate'),
		D.BookGroup,
		@ContractId,
		D.DealType,
		D.Book,
		D.SecId,
		D.Quantity,
		0,
		D.Amount,
		0,
		D.CollateralCode,
		D.ValueDate,
		D.SettleDate,
		D.TermDate,
		D.Rate,
		D.RateCode,
		'N',
		D.PoolCode,
		D.DivRate,
		D.DivCallable,
		D.IncomeTracked,
		D.MarginCode,
		D.Margin,
		D.CurrencyIso,
		D.SecurityDepot,
		D.CashDepot,
		' ',
		D.Comment			
	From 	tbDeals D
	Where 	D.DealId    = @DealId
	And	D.BookGroup = @BookGroup
	And	D.DealType  = @ContractType
	
	Update  tbDeals
	Set	DealStatus = 'C'
	Where	DealId 	  = @DealId
	And	BookGroup = @BookGroup
	And	DealType  = @ContractType
End

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetContractSet]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetRecallSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetRecallSet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetRecallSet 
	@TemporaryRecallId char(16) = null,
	@LoanetRecallId char(16) = null,
	@RecallSequence smallint = null,
	@Status char(1)
As
Begin

If (@Status = 'A')
Begin
	If  (@TemporaryRecallId Is Not Null)
		Begin
			Update 	tbRecalls
			Set	RecallId       = @LoanetRecallId,
				SequenceNumber = @RecallSequence,
				Status = 'O'
			Where 	RecallId = @TemporaryRecallId

			Update 	tbRecallActivity
			Set	RecallId = @LoanetRecallId
			Where	RecallId = @TemporaryRecallId
		End
	Else		
		Begin
			Update	tbRecalls
			Set	Status = 'C'
			Where	RecallId = @LoanetRecallId					
		End	
End

If (@Status = 'F')
Begin
	If  (@TemporaryRecallId Is Not Null)
		Begin
			Update 	tbRecalls
			Set	Status = @Status
			Where 	RecallId = @TemporaryRecallId
		End
End

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetRecallSet]  TO [roleLoanet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetMarkUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetMarkUpdate]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetMarkUpdate
	@BizDate datetime,
	@ClientId char(4) = Null
As
Begin

Update	tbContracts
Set	Amount = C.Amount + LM.Amount,
	AmountSettled = C.AmountSettled + LM.Amount
From	tbContracts C,
	tbLoanetMarks LM
Where	C.BizDate = LM.BizDate
And	LM.BizDate = @BizDate
And	C.BookGroup = LM.ClientId
And	LM.ClientId = IsNull(@ClientId, LM.ClientId)
And	C.ContractId = LM.ContractId
And	C.ContractType = LM.ContractType

Return	@@RowCount

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetMarkUpdate]  TO [roleLoanet]
GO

--------------------------------------------------------------------------------
--| End Loanet modifications.


--| Loanet CoOp modifications.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbLoanetCoOpMessages]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbLoanetCoOpMessages]
GO

CREATE TABLE [dbo].[tbLoanetCoOpMessages] (
	[PostDateTime] [datetime] NOT NULL ,
	[ProcessId] [char] (16) NOT NULL,
	[Content] [varchar] (500) NOT NULL 
) ON [PRIMARY]
GO

CREATE  CLUSTERED  INDEX [IX_tbLoanetCoOpMessages] ON [dbo].[tbLoanetCoOpMessages]([PostDateTime]) ON [PRIMARY]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetCoOpMessageInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetCoOpMessageInsert]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetCoOpMessageInsert
	@ProcessId char(16),
	@Content varchar(500)
As
Begin

Insert 	Into tbLoanetCoOpMessages
Values	(
	GetUtcDate(),
	@ProcessId,		
	@Content
	)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetCoOpMessageInsert]  TO [roleLoanet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetCoOpMessageGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetCoOpMessageGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetCoOpMessageGet
	@ProcessId char(16),
	@Version int = 1
As
Begin

Set 	RowCount @Version

Select	* Into #Results
From 	tbLoanetCoOpMessages
Where 	ProcessId = IsNull(@ProcessId, ProcessId)
Order by PostDateTime

Set 	RowCount 1
Select  Content
From	#Results

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetCoOpMessageGet]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetDatagramTransactionUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLoanetDatagramTransactionUpdate]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spLoanetDatagramTransactionUpdate
	@SystemTime datetime = Null,	
	@ClientId char(4) = Null,	
	@ContractId char(9) = Null,
	@MatchingContractId char(9) = Null,
	@ContractType char(1) = Null,
	@ContraClientId char(4) = Null,
	@SecId varchar(12) = Null,	
	@InputTerminal varchar(2) = Null,
	@InputTimestamp varchar(8) = Null,
	@TransDescription varchar(11) = Null,	
	@DebitCreditFlag char(1) = Null,
	@BatchCode char(1) = Null,
	@DeliverViaCode char(1) = Null,
	@Quantity bigint = Null,
	@Amount money = Null,
	@OriginalQuantity bigint = Null,
	@OriginalAmount money = Null,
	@CollateralCode varchar(1) = Null,
	@ValueDate datetime = Null,
	@SettleDate datetime = Null,
	@TermDate datetime = Null,
	@ReturnDate datetime = Null,
	@InterestFromDate datetime = Null,
	@InterestToDate datetime = Null,
	@Rate decimal(8,5) = Null,
	@RateCode varchar(1) = Null,
	@PoolCode varchar(1) = Null,
	@DivRate decimal(6,3) = Null,
	@DivCallable bit = Null,
	@IncomeTracked bit = Null,
	@MarginCode varchar(1) = Null,
	@Margin decimal (5, 2) = Null,
	@CurrencyIso char(3) = Null,
	@SecurityDepot varchar(2) = Null,
	@CashDepot varchar(2) = Null,
	@OtherClientId varchar(4) = Null,
	@Comment varchar(20) = Null,
	@Filler varchar(18) = Null,
	@Act varchar(255) output
As
Begin

Declare @BizDate datetime
Select  @BizDate = (Select KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate')

If (@DebitCreditFlag != Null)
Begin
    If (@ContractType = 'B' And @DebitCreditFlag = 'D') Or (@ContractType = 'L' And @DebitCreditFlag = 'C')
	Begin	        
		Select @Quantity = @Quantity  * 1
		Select @Amount = @Amount * 1
	End          
    Else If (@ContractType = 'B' And @DebitCreditFlag = 'C') Or (@ContractType = 'L' And @DebitCreditFlag = 'D')
	Begin        
	 	Select @Quantity = @Quantity  * (-1)
		Select @Amount  = @Amount * (-1)
	End
End

If (@MarginCode = '') Or (@MarginCode = 'I')
Begin
	Select	@Margin = (MarginBorrow * CharIndex(@ContractType, 'B')) + (MarginLoan * CharIndex(@ContractType, 'L'))  
	From	tbBooks
	Where	BookGroup = @ClientId
	And	Book = @ContraClientId	
	
	If (@Margin Is Null)
	Begin
		Select @Margin = 1.00
		Select @MarginCode = '%'
	End
End
Else If (@MarginCode = '%')
Begin
	Select @Margin = @Margin / 100
End

If (@RateCode = 'N')
Begin
	Select @Rate = @Rate * (-1)
End

If ((@TransDescription = 'Opened') Or (@TransDescription = 'Closed'))
Begin	
	If (@ContractType = 'B')
	Begin
		If (@Quantity < 0) Or (@Amount < 0)
		Begin
			Select @Act = 'Borrow of ' + Convert(varchar(15), Abs(@Quantity)) + ' has been closed'

			Exec spContractSet	@BizDate = @BizDate,
						@BookGroup = @ClientId,
						@ContractId = @ContractId,
						@ContractType = @ContractType,
						@Book = @ContraClientId,
						@SecId = @SecId,
						@Quantity = @Quantity,
						@QuantitySettled = @Quantity,
						@Amount = @Amount,
						@AmountSettled = @Amount,
						@CollateralCode = @CollateralCode,
						@ValueDate = @ValueDate,
						@SettleDate = @SettleDate,
						@TermDate = @TermDate,
						@SecurityDepot = @SecurityDepot,
						@CashDepot = @CashDepot,
						@Comment = @Comment,
						@ReturnData = 0,
						@IsIncremental = 1
		End
		Else	
		Begin
			Select @Act = 'New borrow of ' + Convert(varchar(15), @Quantity)
 
			Exec spContractSet	@BizDate = @BizDate,
						@BookGroup = @ClientId,
						@ContractId = @ContractId,
						@ContractType = @ContractType,
						@Book = @ContraClientId,
						@SecId = @SecId,
						@Quantity = @Quantity,
						@QuantitySettled = 0,
						@Amount = @Amount,
						@AmountSettled = 0,
						@CollateralCode = @CollateralCode,
						@ValueDate = @ValueDate,
						@SettleDate = @SettleDate,
						@TermDate = @TermDate,
						@Rate = @Rate,
						@RateCode = @RateCode,
						@StatusFlag = '',
						@PoolCode = @PoolCode,
						@DivRate = @DivRate,
						@DivCallable = @DivCallable,
						@IncomeTracked = @IncomeTracked,
						@MarginCode = @MarginCode,
						@Margin = @Margin,
						@CurrencyIso = @CurrencyIso,
						@SecurityDepot = @SecurityDepot,
						@CashDepot = @CashDepot,
						@OtherBook = @OtherClientId,
						@Comment = @Comment,
						@ReturnData = 0,
						@IsIncremental = 1
		End
	End
	Else If (@ContractType = 'L')
	Begin
		If (@Quantity < 0) Or (@Amount < 0)
		Begin
			Select @Act = 'Loan of ' + Convert(varchar(15), Abs(@Quantity)) + ' has been closed'

			Exec spContractSet	@BizDate = @BizDate,
						@BookGroup = @ClientId,
						@ContractId = @ContractId,
						@ContractType = @ContractType,
						@Book = @ContraClientId,
						@SecId = @SecId,
						@Quantity = @Quantity,
						@QuantitySettled = @Quantity,
						@Amount = @Amount,
						@AmountSettled = @Amount,
						@CollateralCode = @CollateralCode,
						@ValueDate = @ValueDate,
						@SettleDate = @SettleDate,
						@TermDate = @TermDate,
						@SecurityDepot = @SecurityDepot,
						@CashDepot = @CashDepot,
						@Comment = @Comment,
						@ReturnData = 0,
						@IsIncremental = 1
		End
		Else	
		Begin
			Select @Act = 'New loan of ' + Convert(varchar(15), @Quantity)

			Exec spContractSet	@BizDate = @BizDate,
						@BookGroup = @ClientId,
						@ContractId = @ContractId,
						@ContractType = @ContractType,
						@Book = @ContraClientId,
						@SecId = @SecId,
						@Quantity = @Quantity,
						@QuantitySettled = 0,
						@Amount = @Amount,
						@AmountSettled = 0,
						@CollateralCode = @CollateralCode,
						@ValueDate = @ValueDate,
						@SettleDate = @SettleDate,
						@TermDate = @TermDate,
						@Rate = @Rate,
						@RateCode = @RateCode,
						@StatusFlag = '',
						@PoolCode = @PoolCode,
						@DivRate = @DivRate,
						@DivCallable = @DivCallable,
						@IncomeTracked = @IncomeTracked,
						@MarginCode = @MarginCode,
						@Margin = @Margin,
						@CurrencyIso = @CurrencyIso,
						@SecurityDepot = @SecurityDepot,
						@CashDepot = @CashDepot,
						@OtherBook = @OtherClientId,
						@Comment = @Comment,
						@ReturnData = 0,
						@IsIncremental = 1
		End
	End

	Return
End


If ((@TransDescription = 'Rev Partial') Or (@TransDescription = 'Partial'))
Begin

	If (@Quantity < 0)
	Begin
		Select @Act = 'Partial return of ' + Convert(varchar(15), Abs(@Quantity))
		Exec spContractSet @BizDate = @BizDate, 
			   	   @BookGroup = @ClientId, 
				   @ContractId = @ContractId, 
				   @ContractType = @ContractType, 
				   @Book = @ContraClientId, 
				   @SecId = @SecId, 
				   @Quantity = @Quantity,
				   @Amount = @Amount, 
				   @IsIncremental = 1
	End
	Else
	Begin
		Select @Act = 'Reverse partial return of ' + Convert(varchar(15), @Quantity)
		Exec spContractSet @BizDate = @BizDate, 
			   	   @BookGroup = @ClientId, 
				   @ContractId = @ContractId, 
				   @ContractType = @ContractType, 
				   @Book = @ContraClientId, 
				   @SecId = @SecId, 
				   @Quantity = @Quantity,
				   @Amount = @Amount, 
				   @IsIncremental = 1
	End

	Return
End


If ((@TransDescription = 'Mark to Mkt') Or (@TransDescription = 'Rev Mrk Mkt'))
Begin
	Exec spContractSet @BizDate 	  = @BizDate, 
			   @BookGroup 	  = @ClientId, 
			   @ContractId 	  = @ContractId, 
			   @ContractType  = @ContractType, 
			   @Book 	  = @ContraClientId, 
			   @SecId 	  = @SecId, 
			   @Amount 	  = @Amount, 
			   @AmountSettled = @Amount, 
			   @IsIncremental = 1

	Select @Act = 'Mark adjustment of ' + Convert(varchar(12), @Amount, 0)
	
	Return
End

If (@TransDescription = 'Base Rt Chg')
Begin
	If(@ClientId = @ContraClientId)
	Begin	
		Update 	tbBookGroups
		Set	FundingRate = @Rate
		Where	BizDate   = @BizDate
		And	BookGroup = @ClientId
	
		Select @Act = 'Cost of funds set to ' + Convert(varchar(15), @Rate)
	End

	Else If(@ClientId != @ContraClientId)
	Begin
		If (@Comment = 'CHG CURRENT  STK RT')
			If (@ContractType = 'L')	
			Begin
				Update 	tbBooks
				Set 	RateStockLoan = @Rate
				Where 	BookGroup = @ClientId
				And	Book	  = @ContraClientId

				Update	tbContracts
				Set	Rate = @Rate,
					StatusFlag = 'X'
				From	tbSecMaster SM,
					tbContracts C
				Where	C.BizDate = @BizDate
				And	C.BookGroup = @ClientId
				And	C.Book = @ContraClientId 			
				And	C.ContractType = 'L'
				And	C.RateCode = 'T'
				And	C.SecId *= SM.SecId
				And	IsNull(SM.BaseType, 'U')  != 'B'

				Select @Act = 'Box rate for stock loan set to ' + Convert(varchar(15), @Rate)			
			End
			Else
			Begin
				Update 	tbBooks
				Set 	RateStockBorrow = @Rate
				Where 	BookGroup = @ClientId
				And	Book	  = @ContraClientId
			
				Update	tbContracts 
				Set	Rate = @Rate,
					StatusFlag = 'X'
				From	tbSecMaster SM,
					tbContracts C
				Where	C.BizDate  = @BizDate
				And	C.BookGroup = @ClientId
				And	C.Book = @ContraClientId 			
				And	C.ContractType = 'B'
				And	C.RateCode = 'T'
				And	C.SecId *= SM.SecId
				And	IsNull(SM.BaseType, 'U')  != 'B'

				Select @Act = 'Box rate for stock borrow set to ' + Convert(varchar(15), @Rate)				
			End
		Else If (@Comment = 'CHG CURRENT  BND RT')	
			If (@ContractType = 'L')
			Begin
				Update 	tbBooks
				Set 	RateBondLoan = @Rate
				Where 	BookGroup = @ClientId
				And	Book	  = @ContraClientId
		
				Update	tbContracts
				Set	Rate = @Rate,
					StatusFlag = 'X'
				From	tbSecMaster SM,
					tbContracts C
				Where	C.BizDate  = @BizDate
				And	C.BookGroup = @ClientId
				And	C.Book = @ContraClientId 			
				And	C.ContractType = 'L'
				And	C.RateCode = 'T'
				And	C.SecId *= SM.SecId
				And	IsNull(SM.BaseType, 'U')  = 'B'

				Select @Act = 'Box rate for bond loan set to ' + Convert(varchar(15), @Rate)
			End
			Else
			Begin
				Update 	tbBooks
				Set 	RateBondBorrow = @Rate
				Where 	BookGroup = @ClientId
				And	Book	  = @ContraClientId

				Update	tbContracts 
				Set	Rate = @Rate,
					StatusFlag = 'X'
				From	tbSecMaster SM,
					tbContracts C
				Where	C.BizDate = @BizDate
				And	C.BookGroup = @ClientId
				And	C.Book = @ContraClientId 			
				And	C.ContractType = 'B'
				And	C.RateCode = 'T'
				And	C.SecId *= SM.SecId
				And	IsNull(SM.BaseType, 'U')  = 'B'

				Select @Act = 'Box rate for bond borrow set to ' + Convert(varchar(15), @Rate)
			End
	End

	Return
End

If ((@TransDescription = 'Int. Change')	Or (@TransDescription = 'Rev Int Chg'))
Begin
	Update tbContracts
	Set	Rate 	 = IsNull(@Rate, Rate),
		RateCode = IsNull(@RateCode, RateCode)
	Where	BookGroup    = @ClientId
	And	ContractType = @ContractType
	And	ContractId   = @ContractId
	And	BizDate >= @InterestFromDate 

	Select @Act = 'Rate change set to ' + Convert(varchar(15), @Rate) + ' from ' + Convert(char(6), @InterestFromDate, 13)
				 					    + ' to ' + Convert(char(6), @InterestToDate, 13)
	Return
End

If (@TransDescription = 'Update')	
Begin
	Exec spContractSet @BizDate 	   = @BizDate, 
			   @BookGroup 	   = @ClientId, 
			   @ContractId 	   = @ContractId, 
			   @ContractType   = @ContractType, 
			   @Book           = @ContraClientId, 
			   @SecId          = @SecId, 
			   @Quantity 	   = @Quantity,
			   @Amount 	   = @Amount,
  			   @Rate 	   = @Rate,
			   @RateCode 	   = @RateCode, 
			   @TermDate	   = @TermDate,
			   @SettleDate	   = @SettleDate,
			   @Margin         = @Margin,
		           @MarginCode 	   = @MarginCode,
			   @DivRate 	   = @DivRate,
			   @Comment	   = @Comment,
			   @IsIncremental  = 1

	Select @Act = 'Contract update'

	Return
End

If ((@TransDescription = 'Delete') Or (@TransDescription = 'Rev Close'))
Begin
	If (@Quantity < 0)
	Begin
		Select @Act = 'Delete contract of ' + Convert(varchar(15), Abs(@Quantity))
		Exec spContractSet @BizDate 	    = @BizDate, 
				   @BookGroup 	    = @ClientId, 
				   @ContractId 	    = @ContractId, 
				   @ContractType    = @ContractType, 
				   @Book 	    = @ContraClientId, 
				   @SecId 	    = @SecId, 
				   @Quantity	    = @Quantity,
				   @Amount 	    = @Amount, 		
				   @IsIncremental   = 1
	End
	Else
	Begin
		Select @Act = 'Reverse close of ' + Convert(varchar(15), @Quantity)
		Exec spContractSet @BizDate 	    = @BizDate, 
				   @BookGroup 	    = @ClientId, 
				   @ContractId 	    = @ContractId, 
				   @ContractType    = @ContractType, 
				   @Book 	    = @ContraClientId, 
				   @SecId 	    = @SecId, 
				   @Quantity	    = @Quantity,
				   @QuantitySettled = @Quantity,
				   @Amount 	    = @Amount, 		
				   @AmountSettled   = @Amount,	  
				   @IsIncremental   = 1

	End

	Return
End

If (@TransDescription = 'PC Adj')
Begin
	Update tbContracts
	Set	PoolCode = @PoolCode
	Where	BookGroup = @ClientId
	And	ContractType = @ContractType
	And	ContractId = @ContractId
	And	BizDate >= @InterestFromDate 
	
	Select @Act = 'PC change to [' + @PoolCode + ']'

	Return
End

If (@TransDescription = 'Blck Rt Chg')
Begin	
	Exec spContractSet @BizDate 	 = @BizDate, 
			   @BookGroup 	 = @ClientId, 
			   @ContractId 	 = @ContractId, 
			   @ContractType = @ContractType, 
			   @Book 	 = @ContraClientId, 
			   @Rate	 = @Rate,
			   @RateCode     = @RateCode

	Select @Act = 'Rate change set to ' + Convert(varchar(15), @Rate)

	Return
End

If (@TransDescription = 'Adjustment')
Begin		
	Exec spContractSet @BizDate 	 = @BizDate, 
			   @BookGroup 	 = @ClientId, 
			   @ContractId 	 = @ContractId, 
			   @ContractType = @ContractType, 
			   @Book 	 = @ContraClientId, 
			   @Quantity 	 = @Quantity,
			   @Amount 	 = @Amount,
			   @IsIncremental   = 1

	Select @Act = 'Adjustment of quantity: ' + Convert(varchar(15), Abs(@Quantity)) + ' and amount: ' + Convert(varchar(15), Abs(@Amount))

	Return
End

If (@TransDescription = 'Dec Sec Col')
Begin		
	Return
End

If ((@TransDescription = 'DK Mark') Or (@TransDescription = 'DK New'))
Begin	
	Return
End

RAISERROR('TransDescription not recongized: %s', 11, 100, @TransDescription)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetDatagramTransactionUpdate]  TO [roleLoanet]
GO


--------------------------------------------------------------------------------
--| End Loanet CoOp modifications.


--| Recall modifications.
--------------------------------------------------------------------------------

If Not Exists (Select * From tbFunctions Where FunctionPath = 'PositionRecalls')
	Insert Into tbFunctions Values ('PositionRecalls', '0', '0', '[NONE]')
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbRecalls]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbRecalls]
GO

CREATE TABLE [dbo].[tbRecalls] (
	[RecallId] [char] (16)   NOT NULL ,
	[BookGroup] [varchar] (10)   NOT NULL ,
	[ContractId] [varchar] (15)   NOT NULL ,
	[ContractType] [char] (1)   NOT NULL ,
	[Book] [varchar] (10)   NOT NULL ,
	[SecId] [varchar] (12)   NOT NULL ,
	[Quantity] [bigint] NOT NULL ,
	[BaseDueDate] [datetime] NULL ,
	[MoveToDate] [datetime] NULL ,
	[OpenDateTime] [datetime] NOT NULL ,
	[ReasonCode] [char] (2)   NOT NULL ,
	[SequenceNumber] [smallint] NOT NULL ,
	[Comment] [varchar] (20)   NULL ,
	[Action] [varchar] (100)   NULL ,
	[ActUserId] [varchar] (50)   NOT NULL ,
	[ActTime] [datetime] NOT NULL ,
	[BizDate] [datetime] NULL ,
	[Status] [char] (1)   NOT NULL ,
	[FaxId] [varchar] (50)   NULL ,
	[FaxStatus] [varchar] (50)   NULL ,
	[FaxStatusTime] [datetime] NULL,
	[DeliveredToday] [bigint] NULL,
	[WillNeed] [bigint] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbRecalls] ADD 
	CONSTRAINT [DF_tbRecalls_Status] DEFAULT ('O') FOR [Status],
	CONSTRAINT [PK_tbRecalls] PRIMARY KEY  CLUSTERED 
	(
		[RecallId]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbRecallActivity]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbRecallActivity]
GO

CREATE TABLE [dbo].[tbRecallActivity] (
	[RecallId] [char] (16) NOT NULL ,
	[SerialId] [bigint] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,
	[Activity] [char] (100) NULL ,
	[ActUserId] [varchar] (50) NOT NULL ,
	[ActTime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbRecallActivity] ADD 
	CONSTRAINT [PK_tbRecallActivity] PRIMARY KEY  NONCLUSTERED 
	(
		[RecallId],
		[SerialId]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbRecallReasons]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbRecallReasons]
GO

CREATE TABLE [dbo].[tbRecallReasons] (
	[ReasonId] [char] (2) NOT NULL ,
	[ReasonCode] [char] (2) NOT NULL ,
	[ReasonName] [varchar] (50) NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbRecallReasons] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbRecallReasons] PRIMARY KEY  CLUSTERED 
	(
		[ReasonId]
	)  ON [PRIMARY] 
GO


If Not Exists (Select * From tbRecallReasons Where ReasonId = '01')
	Insert into tbRecallReasons Values('01', 'SD', 'Seg Deficit')
Go

If Not Exists (Select * From tbRecallReasons Where ReasonId = '02')
	Insert tbRecallReasons Values('02', 'RT','Retransmittal')
Go

If Not Exists (Select * From tbRecallReasons Where ReasonId = '03')
	Insert tbRecallReasons Values('03', 'CA', 'Corporate Action')
Go

If Not Exists (Select * From tbRecallReasons Where ReasonId = '04')
	Insert tbRecallReasons Values('04', 'PS', 'Pending Sale')
Go

If Not Exists (Select * From tbRecallReasons Where ReasonId = '05')
	Insert tbRecallReasons Values('05', 'FF', 'Firm Fail')
Go

If Not Exists (Select * From tbRecallReasons Where ReasonId = '06')
	Insert tbRecallReasons Values('06', 'TM', 'Termination')
Go

If Not Exists (Select * From tbRecallReasons Where ReasonId = '07')
	Insert tbRecallReasons Values('07', 'MG', 'Marginability')
Go

If Not Exists (Select * From tbRecallReasons Where ReasonId = '08')
	Insert tbRecallReasons Values('08', 'OT', 'Other')
Go


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRecallActivityGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spRecallActivityGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spRecallActivityGet 
	@BizDate datetime = Null,
	@RecallId char(16) = Null,
	@SerialId bigint = Null,
	@UtcOffset smallint = 0
As
Begin

Select 		RA.RecallId,
		RA.SerialId,
		RA.Activity,
		U.ShortName As ActUserId,
		DateAdd(n, @UtcOffset, RA.ActTime) As ActTime
From		tbRecallActivity RA,
		tbRecalls R,		
		tbUsers	U
Where		R.RecallId = IsNull(@RecallId, R.RecallId)
	And	R.RecallId = RA.RecallId
	And	R.BizDate >= IsNull(@BizDate, R.BizDate)
	And	RA.SerialId >= IsNull(@SerialId, RA.SerialId)
	And	RA.ActUserId *= U.UserId
Order By 	1, 2 Desc

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spRecallActivityGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRecallActivityInsert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spRecallActivityInsert]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spRecallActivityInsert 
	@RecallId char(16),
	@Activity varchar(100),
	@ActUserId varchar(50)
As
Begin

Insert Into tbRecallActivity(
	RecallId,
	ActTime,
	Activity,
	ActUserId
	)
Values	(
	@RecallId,
	GetUtcDate(),
	@Activity,
	@ActUserId
	)

Return @@Identity

End
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spRecallActivityInsert]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRecallsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spRecallsGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE PROCEDURE dbo.spRecallsGet 
	@BizDate datetime = Null,
	@RecallId char(16) = Null,
	@UtcOffset smallint = 0
As
Begin

Declare @ContractsBizDate datetime
Select   @ContractsBizDate = KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate'

If @BizDate = Null
	Select @BizDate = KeyValue From tbKeyValues Where KeyId = 'BizDate'

Select 		R.RecallId,
		R.BookGroup,
		R.ContractId,
		R.ContractType,
		R.Book,
		R.SecId,
		SIL.SecIdLink As Symbol,
		IsNull(SM.IsEasy, 0) As IsEasy,
		IsNull(SM.IsHard, 0) As IsHard,
		IsNull(SM.IsThreshold, 0) As IsThreshold,
		IsNull(SM.IsNoLend, 0) As IsNoLend,
		SM.ThresholdDayCount As DayCount,
		R.Quantity,
		R.BaseDueDate,
		R.MoveToDate,
		R.OpenDateTime,
		IsNull(RR.ReasonCode, R.ReasonCode) As ReasonCode,
		R.SequenceNumber,
		R.Comment,
		R.[Action],
		R.FaxId,
		R.FaxStatus,
		DateAdd(n, @UtcOffset, R.FaxStatusTime) As FaxStatusTime,
		R.DeliveredToday,
		R.WillNeed,
		U.ShortName As ActUserShortName,
		DateAdd(n, @UtcOffset, R.ActTime) As ActTime,
		Case When @BizDate < @ContractsBizDate  Then 'H' Else R.Status End As Status
From		tbRecalls R,
		tbRecallReasons RR,
		tbUsers U,
		tbSecIdLinks SIL,		
		tbSecMaster SM
Where		R.ActUserId *= U.UserId
	And	R.SecId *= SIL.SecId
	And	SIL.SecIdTypeIndex = 2
	And	R.SecId *= SM.SecId
	And	R.ReasonCode *= RR.ReasonId
	And	(
		(R.RecallId = @RecallId)
		Or
		(
		(R.Status In ('O','M','P') Or (R.Status In ('F','C','B') And R.ActTime > @BizDate))
		And @RecallId Is Null))
Order By 	1

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spRecallsGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRecallSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRecallSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spRecallSet
	@RecallId char(16),
	@BookGroup varchar(10) = Null,
	@ContractId varchar(15) = Null,
	@ContractType char(1) = Null,
	@Book varchar(10) = Null,
	@SecId varchar(12) = Null,
	@Quantity bigint = Null,
	@BaseDueDate datetime = Null,
	@MoveToDate datetime = Null,
	@OpenDateTime datetime = Null,
	@ReasonCode char(2) = Null,
	@SequenceNumber smallint = Null,
	@Comment varchar(20) = Null,
	@Action varchar(100) = Null,
	@FaxId varchar(50) = Null,
	@FaxStatus varchar(50) = Null,
	@DeliveredToday bigint = Null,
	@WillNeed bigint = Null,
	@ActUserId varchar(50) = Null,
	@Status char(1) = Null,
	@ReturnData bit = 0
As
Begin

Begin Transaction

Declare @QuantityOld bigint,
	@MoveToDateOld datetime,
	@ActionOld varchar(100),
	@SequenceNumberOld smallint,
	@StatusOld char(1),
	@Activity varchar(100),
	@ReturnValue int,
	@ActivitySerialId int,
	@FaxIdOld varchar(50),
	@FaxStatusOld varchar(50),
	@BizDate datetime,
	@RecallReason varchar(2),
	@FaxStatusTime datetime,
	@DeliveredTodayOld bigint,
	@WillNeedOld bigint

If (@FaxStatus = Null)
	Select @FaxStatusTime = Null
Else
	Select @FaxStatusTime = GetUtcDate()	

Select 	@QuantityOld 		= Quantity,
	@MoveToDateOld 		= MoveToDate,
	@ActionOld		= [Action],
	@SequenceNumberOld	= SequenceNumber,
	@StatusOld		= Status,
	@FaxIdOld 		= FaxId,
	@FaxStatusOld 		= FaxStatus,
	@DeliveredTodayOld	= DeliveredToday,
	@WillNeedOld 		= WillNeed,
	@ActUserId		= IsNull(@ActUserId, ActUserId)	
From	tbRecalls
Where	RecallId = @RecallId

If (@@RowCount = 1)
Begin
	If ((@QuantityOld != @Quantity) And (@Quantity != Null))
	Begin
		Select @Activity = 'Quantity changed from ' + Convert(varchar(15), @QuantityOld) + ' to ' + Convert(varchar(15), @Quantity) + '.'
		Exec @ReturnValue = spRecallActivityInsert @RecallId, @Activity, @ActUserId

		If @ActivitySerialId Is Null
			Select @ActivitySerialId = @ReturnValue
	End
			
	If ((IsNull(@MoveToDateOld, '') != IsNull(@MoveToDate, '') And (@MoveToDate != Null)))
	Begin
		Select @Activity = 'Move To date changed from ' + IsNull(Convert(varchar(6), @MoveToDateOld, 0), 'Null') + ' to ' + IsNull(Convert(varchar(6), @MoveToDate, 0), 'Null') + '.'
		Exec @ReturnValue = spRecallActivityInsert @RecallId, @Activity, @ActUserId

		If @ActivitySerialId Is Null Select @ActivitySerialId = @ReturnValue
	End
	
	If ((@SequenceNumberOld != @SequenceNumber) And (@SequenceNumber != Null))
	Begin
		Select @Activity = 'Sequence Number changed from: ' + @SequenceNumberOld + ' to ' + @SequenceNumber + '.'
		Exec @ReturnValue = spRecallActivityInsert @RecallId, @Activity, @ActUserId

		If @ActivitySerialId Is Null Select @ActivitySerialId = @ReturnValue
	End

	If ((@ActionOld != @Action) And (@Quantity != Null))
	Begin
		Select @Activity = 'New commentary: ' + @Action + '.'
		Exec @ReturnValue = spRecallActivityInsert @RecallId, @Activity, @ActUserId

		If @ActivitySerialId Is Null Select @ActivitySerialId = @ReturnValue
	End

	If ((@FaxIdOld != @FaxId) And (@FaxId != Null))
	Begin
		Select @Activity = 'New fax id: ' + @FaxId + '.'
		Exec @ReturnValue = spRecallActivityInsert @RecallId, @Activity, @ActUserId

		If @ActivitySerialId Is Null Select @ActivitySerialId = @ReturnValue
	End

	If ((@FaxStatusOld != @FaxStatus) And (@FaxStatus != Null))
	Begin
		Select @Activity = 'New fax status: ' + @FaxStatus + '.'
		Exec @ReturnValue = spRecallActivityInsert @RecallId, @Activity, @ActUserId

		If @ActivitySerialId Is Null Select @ActivitySerialId = @ReturnValue
	End

	If ((@DeliveredTodayOld != @DeliveredToday) And (@DeliveredToday != Null))
	Begin
		Select @Activity = 'Delivery today: ' + Convert(varchar(15), @DeliveredToday) + '.'
		Exec @ReturnValue = spRecallActivityInsert @RecallId, @Activity, @ActUserId

		If @ActivitySerialId Is Null Select @ActivitySerialId = @ReturnValue

	End

	If ((@WillNeedOld != @WillNeed)	And (@WillNeed != Null))
	Begin
		Select @Activity = 'Will need changed to: ' + Convert(varchar(15), @WillNeed) + '.'		
		Exec @ReturnValue = spRecallActivityInsert @RecallId, @Activity, @ActUserId

		If @ActivitySerialId Is Null Select @ActivitySerialId = @ReturnValue
	End

	If ((@StatusOld != @Status) And (@Status != Null))
	Begin
		Select @Activity = 'Status changed to: ' + @Status + '.'
		Exec @ReturnValue = spRecallActivityInsert @RecallId, @Activity, @ActUserId

		If @ActivitySerialId Is Null Select @ActivitySerialId = @ReturnValue
	End

End

Update	tbRecalls
Set	Quantity  	  = IsNull(@Quantity, Quantity),
	[Action]  	  = IsNull(@Action, [Action]),
	MoveToDate	  = IsNull(@MoveToDate, MoveToDate),
	ActUserId  	  = IsNull(@ActUserId, ActUserId),
	ActTime 	  = GetUtcDate(),
	Status		  = IsNull(@Status, Status),				
	FaxId		  = IsNull(@FaxId, FaxId),
	FaxStatus	  = IsNull(@FaxStatus, FaxStatus),
	FaxStatusTime 	  = IsNull(@FaxStatusTime, FaxStatusTime),
	DeliveredToday	  = IsNull(DeliveredToday, '0') + IsNull(@DeliveredToday, '0'),
	WillNeed	  = IsNull(@WillNeed, WillNeed)
Where	RecallId 	  = @RecallId


If(@@RowCount = 0)
Begin
	Select  @BizDate = (Select KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate')
	Select  @RecallReason = (Select ReasonId From tbRecallReasons Where ReasonCode = @ReasonCode)

Insert Into tbRecalls
Values (
	@RecallId,
	@BookGroup,
	@ContractId,
	@ContractType,
	@Book,
	@SecId, 
	@Quantity,
	@BaseDueDate,
	@MoveToDate,
	@OpenDateTime,
	@RecallReason,
	@SequenceNumber,
	@Comment, 
	@Action,	
	@ActUserId,
	GetUtcDate(),
	@BizDate,
	@Status,
	@FaxId,
	@FaxStatus,
	@FaxStatusTime,
	@DeliveredToday,	
	@WillNeed	
	)
End

Commit Transaction

If (@ReturnData = 1)
Begin
	Exec spRecallsGet @BizDate, @RecallId
	Exec spRecallActivityGet @BizDate, @RecallId, @ActivitySerialId
	Exec spContractGet @BizDate, @BookGroup, @ContractId, @ContractType
End

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spRecallSet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRecallReasonsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spRecallReasonsGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spRecallReasonsGet 
	@ReasonId char(2) = Null,
	@ReasonCode char(2) = Null
As
Begin

Select 	ReasonId,
	ReasonCode,
	ReasonName
From 	tbRecallReasons
Where	ReasonId = IsNull(@ReasonId, ReasonId)
And	ReasonCode = IsNull(@ReasonCode, ReasonCode)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spRecallReasonsGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetContractRecallUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetContractRecallUpdate]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetContractRecallUpdate
	@BookGroup char(4),
	@ContractId char(9),
	@ContractType char(1),
	@Book char(4),
	@RecallId char(16) output
As
Begin

Set RowCount 1

Select 	@RecallId = RecallId
From 	tbRecalls
Where	BookGroup = @BookGroup
And	ContractId = @ContractId
And	ContractType = @ContractType
And	Book = @Book
And	Status <> 'C'

Update	tbRecalls
Set	DeliveredToday = Quantity
From 	tbRecalls
Where	BookGroup = @BookGroup
And	ContractId = @ContractId
And	ContractType = @ContractType
And	Book = @Book
And	RecallId = @RecallId


If (@RecallId <> Null)
	Begin
		exec spRecallSet @RecallId = @RecallId,
		 		  @Status = 'C'
	
		Select @RecallId
	End
End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetContractRecallUpdate]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbRecallIndicators]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbRecallIndicators]
GO

CREATE TABLE [dbo].[tbRecallIndicators] (
	[IndicatorCode] [char] (1)  NOT NULL ,
	[IndicatorName] [varchar] (25)  NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbRecallIndicators] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbRecallIndicators] PRIMARY KEY  CLUSTERED 
	(
		[IndicatorCode]
	)  ON [PRIMARY] 
GO


If Not Exists (Select * From tbRecallIndicators Where IndicatorCode = 'R')
	Insert into tbRecallIndicators Values('R', 'Recall')	
Go

If Not Exists (Select * From tbRecallIndicators Where IndicatorCode = 'T')
	Insert tbRecallIndicators Values('T', 'Termination')
Go


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRecallIndicatorsGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spRecallIndicatorsGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spRecallIndicatorsGet 

As
Begin

Select 	IndicatorCode,
	IndicatorName
From 	tbRecallIndicators

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spRecallIndicatorsGet]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spRecallBizDateSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spRecallBizDateSet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spRecallBizDateSet 
	@RecordCount int output
AS
Begin

-- Reset the recall status for todays bizdate

Declare @ContractsBizDate datetime
Select   @ContractsBizDate = KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate'

Update 	tbRecalls
Set	Status = 'P'
Where	Status Not In ('C','F')
And	IsNull(MoveToDate, BaseDueDate) <= @ContractsBizDate

Select @RecordCount = @@RowCount

-- Reset the delivered today and will need fields to null

Update 	tbRecalls
Set	DeliveredToday 	= Null,
	WillNeed	= Null
Where	DeliveredToday  Is Not Null
Or	WillNeed	Is Not Null

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spRecallBizDateSet]  TO [roleMedalist]
GO
--------------------------------------------------------------------------------
--| End Recall modifications.


--| ShortSale Locate modifications.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spShortSaleLocates]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spShortSaleLocates]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spShortSaleLocateGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spShortSaleLocateGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spShortSaleLocateGet
		@LocateId bigint = Null,
		@TradeDate datetime = Null,
		@GroupCode varchar(5) = Null,
		@UtcOffset smallint = 0
As
Begin

Select		SSL.LocateId,
		SSL.LocateId % 100000 As LocateIdTail,
		SSL.TradeDate,
		SSL.ClientId,
		SSL.GroupCode,
		SSL.SecId,
		SIL.SecIdLink As Symbol,
		SSL.ClientQuantity,
		SSL.ClientComment,
		DateAdd(n, @UtcOffset, SSL.OpenTime) As OpenTime,
		SSL.Quantity,
		SSL.FeeRate,
		SSL.Comment,
		SSL.PreBorrow,
		U.ShortName As ActUserShortName,
		DateAdd(n, @UtcOffset, SSL.ActTime) As ActTime,
		SSL.Source,
		SSL.Status,
		IsNull(TG.GroupName, '[UNKNOWN]') As GroupName
From		tbShortSaleLocates SSL,
		tbSecIdLinks SIL,
		tbTradingGroups TG,
		tbUsers U
Where		SSL.ActUserId *= U.UserId
	And	SSL.GroupCode *= TG.GroupCode
	And	SSL.SecId *= SIL.SecId
	And	SIL.SecIdTypeIndex = 2
	And	SSL.LocateId = IsNull(@LocateId, SSL.LocateId)
	And	SSL.TradeDate = IsNull(@TradeDate, SSL.TradeDate)
	And	SSL.GroupCode = IsNull(@GroupCode, SSL.GroupCode)
Order By	1 Desc

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spShortSaleLocateGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spShortSaleLocateDates]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spShortSaleLocateDates]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spShortSaleTradeDates]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spShortSaleTradeDates]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spShortSaleTradeDates
	@GroupCode varchar(5) = Null,
	@MaxRows int = 0
As
Begin

If (@MaxRows > 1)
	Select	@MaxRows = @MaxRows - 1
	Set	RowCount @MaxRows

--	Get current trade date.
Declare	@TradeDateToday char(10)
Exec	spKeyValueGet 'BizDateExchange', @TradeDateToday Output

--	Create the list of trade dates appropriate for this user.
Select	Distinct Convert(char (10), TradeDate, 120) As 'TradeDate'
Into	#TradeDates
From	tbShortSaleLocates
Where	TradeDate < @TradeDateToday
And	GroupCode = IsNull(@GroupCode, GroupCode)

Insert	#TradeDates
Values	(@TradeDateToday)

Select		*
From		#TradeDates
Order By	1 Desc

Set 		RowCount 0
Drop Table	#TradeDates

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spShortSaleTradeDates]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTradingGroupGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spTradingGroupGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spTradingGroupGet
	@GroupCode varchar(5) = Null,
	@IsActive bit = Null
As
Begin

Select		*
From		tbTradingGroups
Where		GroupCode = IsNull(@GroupCode, GroupCode)
	And	IsActive = IsNull(@IsActive, IsActive)
Order By 	1

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spTradingGroupGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInventoryList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spInventoryList]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInventoryGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spInventoryGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spInventoryGet
	@SecId varchar(12),
	@UtcOffset smallint = 0,
	@AgeDayCount smallint = 10
As
Begin

Exec spSecIdSymbolLookup @SecId, @SecId Output

Select		IC.SecId,
		DateAdd(n, @UtcOffset, IC.ScribeTime) As ScribeTime,
		I.BizDate,
		I.Desk,
		I.Account,
		I.ModeCode,
		I.Quantity
From		tbInventory I,
		tbInventoryControl IC
Where		IC.SecId = @SecId
	And	I.BizDate = IC.BizDate
	And	I.Desk = IC.Desk
	And	I.SecId = IC.SecId
	And	I.Account = IC.Account
	And	IC.ScribeTime > DateAdd(d, - @AgeDayCount, GetUtcDate())
Order By	IC.ScribeTime Desc

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spInventoryGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInventoryHistoryGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spInventoryHistoryGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) 2005  All rights reserved.

CREATE Procedure dbo.spInventoryHistoryGet
	@SecId varchar(12)
As
Begin

Set RowCount 10

Select Distinct	BizDate
Into		#Dates
From		tbBookGroups
Where		BizDate > (GetDate() - 20)
Order  By	1 Desc

Select Distinct	I.Desk,
		Sum(Quantity) As TotalQuantity
Into		#Desks
From		tbInventory I,
		#Dates Dt
Where		I.BizDate = Dt.BizDate
	And	I.Desk = I.Desk
	And	SecId = @SecId
Group By	I.Desk

Set RowCount 0

Select		Dsk.Desk,
		Dt.BizDate,
		Sum(I.Quantity) As Quantity
From		#Dates Dt,
		#Desks Dsk,
		tbInventory I
Where		Dt.BizDate *= I.BizDate
	And	Dsk.Desk *= I.Desk
	And	SecId = @SecId
Group By	Dsk.TotalQuantity,
		Dsk.Desk,
		Dt.BizDate
Order By	Dsk.TotalQuantity Desc,
		Dsk.Desk,
		Dt.BizDate
		
Drop Table #Dates
Drop Table #Desks

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spInventoryHistoryGet]  TO [medalistService]
GO



if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spShortSaleLocateLookup]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spShortSaleLocateLookup]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spShortSaleLocateLookup
	@SecId varchar(12),
	@Quantity bigint = Null Output,
	@Source varchar(50) = Null Output,
	@Status varchar(10) = Null Output,
	@ActUserId varchar(50) = Null Output,
	@ActTime datetime = Null Output
As
Begin

Declare	@SupplyQuantity bigint,
	@AutoYesMinimum varchar(15),
	@ScribeTimeThreshold varchar(23),
	@Counter smallint,
	@Desk varchar(12),
	@Account varchar(15)

Select	@AutoYesMinimum = '500'

Exec spKeyValueGet @KeyId = 'ShortSaleLocateAutoYesMinimum', @KeyValue = @AutoYesMinimum Output
Exec spKeyValueGet @KeyId = 'BizDatePrior', @KeyValue = @ScribeTimeThreshold Output

Select	@ScribeTimeThreshold = @ScribeTimeThreshold + ' ' + Convert(char(13), GetUtcDate(), 114)

Select		I.Desk, I.Account, I.Quantity
Into		#Supply
From		tbInventory I,
		tbInventoryControl IC
Where		IC.SecId = @SecId
	And	I.Quantity > 0
	And	I.BizDate = IC.BizDate
	And	I.Desk = IC.Desk
	And	I.SecId = IC.SecId
	And	I.Account = IC.Account
	And	IC.ScribeTime > @ScribeTimeThreshold
Order By	I.Quantity Desc
 

Select   @SupplyQuantity = Sum(Quantity)
From     #Supply
 
If (IsNull(@SupplyQuantity, 0) > (IsNull(@Quantity, 0)))
Begin
	If (@Quantity Is Null)
		Select	@Quantity = @SupplyQuantity 
	
	Select	@Source = '',
		@Counter = 2,
		@Status = 'FullOK',
		@ActUserId = 'ADMIN',
		@ActTime = GetUtcDate()

	Set	RowCount 1

	While	((@Counter > 0) And (@SupplyQuantity > 0))          
	Begin
		Select	@Source = @Source + Desk + '[' + Account + ']; ',
			@SupplyQuantity = @SupplyQuantity - Quantity,
        		@Desk = Desk,
			@Account = Account,
			@Counter = @Counter - 1
		From	#Supply

		Delete
		From	#Supply
                Where   Desk = @Desk
		And	Account = @Account
	End

      	Return
End
            
If (IsNull(@SupplyQuantity, 0) > Convert(bigint, @AutoYesMinimum))
Begin
	Select	@Quantity = @SupplyQuantity 
	
	Select	@Source = '',
		@Counter = 2,
		@Status = 'Partial',
		@ActUserId = 'ADMIN',
		@ActTime = GetUtcDate()

	Set	RowCount 1

	While	((@Counter > 0) And (@SupplyQuantity > 0))          
	Begin
		Select	@Source = @Source + Desk + '[' + Account + ']; ',
			@SupplyQuantity = @SupplyQuantity - Quantity,
        		@Desk = Desk,
			@Account = Account,
			@Counter = @Counter - 1
		From	#Supply

		Delete
		From	#Supply
                Where   Desk = @Desk
		And	Account = @Account
	End

      	Return
End

Select	@Source = Null,
	@Quantity = Null,
	@Status = 'Pending'
            
End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spShortSaleLocateLookup]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spShortSaleLocateRequest]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spShortSaleLocateRequest]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spShortSaleLocateRequest
	@LocateId bigint = Null Output,
	@TradeDate datetime,
	@ClientId varchar(25),
	@GroupCode varchar(12),
	@SecId varchar(12) Output,
	@Symbol varchar(8) = Null Output,
	@ClientQuantity bigint,
	@ClientComment varchar(50) = Null,
	@OpenTime char(23) = Null Output,
	@Quantity bigint = Null Output,
	@FeeRate float = Null Output,
	@Comment varchar(50) = Null Output,
	@ActUserId varchar(25) = Null Output,
	@ActTime char(23) = Null Output,
	@Source varchar(100) = Null Output,
	@Status varchar(15) = Null Output,
	@ReturnData bit = 0
As
Begin

-- Set the request open time.
Select	@OpenTime = Convert(char(23), GetUtcDate() , 121)

-- Make sure we have a trade date.
If (@TradeDate Is Null)
Begin
	Select	@LocateId = 0,
		@Comment = 'Value for TradeDate may not be Null or blank.',
		@Status = 'Rejected'	
	Return
End

-- Make sure we have at least some kind of SecId.
Select	@SecId = Upper(RTrim(LTrim(@SecId)))

If ((@SecId Is Null) Or (@SecId = ''))
Begin
	Select	@LocateId = 0,
		@Comment = 'Value for SecId may not be null or blank.',
		@Status = 'Rejected'	
	Return
End

-- Make sure we have a positive value for the request quantity.
If ((@ClientQuantity Is Null) Or (@ClientQuantity < 1))
Begin
	Select	@LocateId = 0,
		@Comment = 'Value for ClientQuantity must be greater than zero.',
		@Status = 'Rejected'	
	Return
End

-- Resolve the SecId.
Exec spSecIdSymbolLookup @SecId, @SecId Output, @Symbol Output

-- Check the price threshold.
Declare	@PriceThresholdString varchar(25)
Declare	@PriceThresholdFloat float

Select	@PriceThresholdString = '5.00'
Exec	spKeyValueGet 'ShortSaleLocatePriceThreshold', @PriceThresholdString Output
Select	@PriceThresholdFloat = Convert(float, @PriceThresholdString)

If Exists (	Select	SecId From tbSecMaster
		Where	SecId = @SecId
		And	IsNull(LastPrice, @PriceThresholdFloat) < @PriceThresholdFloat	)
Begin
	Select	@Status = 'Pending',	
		@Comment = 'Check price!'

	GoTo DoInsert
End

-- Check the threshold list.
If Exists (	Select	SecId
		From	tbThreshold T,
			tbThresholdControl TC
		Where	(T.SecId = @SecId Or T.Symbol = @SecId) 
		And	TC.Exchange = T.Exchange
		And	T.BizDate = TC.BizDate)
Begin
	Select	@Status = 'Pending',	
		@Comment = 'Threshold item.'

	GoTo DoInsert
End

-- Check the no-borrow list.
If Exists (Select SecId From tbBorrowNo Where (SecId = @SecId Or SecId = @Symbol) And (EndTime Is Null))
Begin
	Declare	@NoNoList varchar(255)

	Select	@NoNoList = 'List of trading group codes delimited by ; chararcter.'

	Exec spKeyValueGet @KeyId = 'ShortSaleLocateNoNoList', @KeyValue = @NoNoList Output

	If (CharIndex(@GroupCode + ';', @NoNoList) > 0)
	Begin
		Select	@Comment = 'Super-Premium (call Desk for rate).'

		GoTo DoLookup
	End

	Select	@Quantity = 0,
		@Comment = 'No supply.',
		@ActUserId = 'ADMIN',
		@ActTime = GetUtcDate(),
		@Status = 'None'	

	GoTo DoInsert
End

-- Check the hard-to-borrow list.
If Exists (Select SecId From tbBorrowHard Where (SecId = @SecId Or SecId = @Symbol) And EndTime Is Null)
Begin
	Select	@Comment = 'Premium (call Desk for rate).'

	GoTo DoLookup
End

-- Check the easy-to-borrow list.
If Exists (Select SecId From tbBorrowEasy Where (SecId = @SecId Or SecId = @Symbol) And TradeDate = @TradeDate)
Begin
	Select	@Quantity = @ClientQuantity,
		@ActUserId = 'ADMIN',
		@ActTime = GetUtcDate(),
		@Status = 'FullOK',
		@Source = 'EB'

	GoTo DoInsert
End

DoLookup:

	-- But first check to see if this group has already done a request in this security for today.
	If Exists (	Select	LocateId
			From	tbShortSaleLocates
			Where	TradeDate = @TradeDate 
			And	GroupCode = @GroupCode
			And	SecId = @SecId	)
	Begin
		Select	@Status = 'Pending',	
			@Comment = 'Has previous request.'
	
		GoTo DoInsert
	End

	Select	@Quantity = @ClientQuantity -- @Quantity will be reset to null if not available in lookup.

	Exec spShortSaleLocateLookup @SecId, @Quantity Output, @Source Output, @Status Output,
		@ActUserId Output, @ActTime Output

DoInsert:

	Insert 	tbShortSaleLocates
		(
		TradeDate,
		ClientId,
		GroupCode,
		SecId,
		ClientQuantity,
		ClientComment,
		OpenTime,
		Quantity,
		FeeRate,
		Comment,
		ActUserId,
		ActTime,
		Source,
		Status
		)
	Values	(
		@TradeDate,
		@ClientId,
		@GroupCode,
		@SecId,
		@ClientQuantity,
		@ClientComment,
		@OpenTime,
		@Quantity,
		@FeeRate,
		@Comment,
		@ActUserId,
		@ActTime,
		@Source,
		@Status
		)

	Select	@LocateId = @@Identity

If (@ReturnData = 1)
	Exec	spShortSaleLocateGet @LocateId

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spShortSaleLocateRequest]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spShortSaleLocateSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spShortSaleLocateSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spShortSaleLocateSet
	@ActUserId varchar(50),
	@LocateId bigint,
	@Quantity bigint = Null,
	@FeeRate float = Null,
	@Comment varchar(50) = Null,
	@Source varchar(50) = Null,
	@PreBorrow bit = Null,
	@Status varchar(15) = Null,
	@ReturnData bit = 0
As
Begin

Declare	@CallDesk int

Select	@CallDesk = CharIndex('calldesk', Lower(@Comment))

If (@CallDesk = 0)
	Select	@CallDesk = CharIndex('call desk', Lower(@Comment))

Update	tbShortSaleLocates
Set	Quantity = IsNull(Quantity, @Quantity), -- Backwards so as not to update if already set.
	FeeRate = IsNull(@FeeRate, FeeRate),
	Comment = IsNull(@Comment, Comment),
	PreBorrow = IsNull(@PreBorrow, PreBorrow),
	ActUserId = @ActUserId,
	ActTime = Case
		When Quantity Is Null Then GetUtcDate() Else ActTime End, -- ActTime is constant after quantity has been set.
	Status = Case
		When IsNull(Quantity, @Quantity) Is Not Null And IsNull(Quantity, @Quantity) >= ClientQuantity
			Then 'FullOK'
		When IsNull(Quantity, @Quantity) Is Not Null And IsNull(Quantity, @Quantity) > 0
			Then 'Partial'
		When IsNull(Quantity, @Quantity) Is Not Null And IsNull(Quantity, @Quantity) = 0
			Then 'None'
		When @CallDesk > 0
			Then 'CallDesk'
		Else	'Pending' End,
	Source = IsNull(@Source, Source)
Where	LocateId = @LocateId

If (@@RowCount = 0)
	RAISERROR('LocateId %s is not known as a ShortSaleLocate request.', 16, 1, @LocateId)

If (@ReturnData = 1)
	Exec	spShortSaleLocateGet @LocateId
	
End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spShortSaleLocateSet]  TO [roleMedalist]
GO

--------------------------------------------------------------------------------
--| ShortSale Locate modifications.


--| Threshold modifications.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbThreshold]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
	Select	*
	Into	tbThresholdTemp
	From	tbThreshold
	
	Drop Table [dbo].[tbThreshold]
End
GO

CREATE TABLE [dbo].[tbThreshold] (
	[BizDate] [datetime] NOT NULL ,
	[Exchange] [char] (4) NOT NULL ,
	[Symbol] [varchar] (12) NOT NULL ,
	[SecId] [varchar] (12) NOT NULL ,
	[Description] [varchar] (100) NOT NULL ,
	[DayCount] [smallint] NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbThreshold] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbThreshold] PRIMARY KEY  CLUSTERED 
	(
		[BizDate],
		[Exchange],
		[Symbol]
	)  ON [PRIMARY] 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[tbThresholdTemp]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
Begin
	Insert		tbThreshold
	Select Distinct	BizDate,
			Exchange,
			Symbol,
			SecId,
			Description,
			Null
	From		tbThresholdTemp

	Drop Table tbThresholdTemp
End
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbThresholdControl]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbThresholdControl]
GO

CREATE TABLE [dbo].[tbThresholdControl] (
	[Exchange] [char] (4) NOT NULL ,
	[BizDate] [datetime] NOT NULL ,
	[ItemCount] [int] NULL ,
	[ScribeTime] [datetime] NULL ,
	[FileTimeStamp] [datetime] NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbThresholdControl] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbThresholdControl] PRIMARY KEY  CLUSTERED 
	(
		[Exchange]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spThresholdControlSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spThresholdControlSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spThresholdControlSet
	@Exchange	char(4),
	@BizDate	datetime,
	@ItemCount	int,
	@FileTimeStamp	datetime
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update	tbThresholdControl
Set	BizDate = @BizDate,
	ItemCount = @ItemCount,
	ScribeTime = GetUtcDate(),
	FileTimeStamp = @FileTimeStamp
Where	Exchange = @Exchange

If (@@RowCount = 0)
	Insert 	tbThresholdControl
	Values	(
		@Exchange,
		@BizDate,
		@ItemCount,
		GetUtcDate(),
		@FileTimeStamp
		)

Update	tbSecMaster
Set	IsThreshold = 0,
	ThresholdDayCount = 0
Where	IsThreshold = 1

Update	tbSecMaster
Set	IsThreshold = 1,
	ThresholdDayCount = T.DayCount
From 	tbSecMaster SM,
	tbThreshold T,
	tbThresholdControl TC
Where 	SM.SecId = T.SecId
And	T.BizDate = TC.BizDate
And	TC.Exchange = T.Exchange

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spThresholdControlSet]  TO [roleCourier]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spThresholdItemSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spThresholdItemSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure spThresholdItemSet
	@BizDate datetime,
	@Exchange char(4),
	@Symbol varchar(12),
	@Description varchar(100)
As
Begin

Declare	@SecId varchar(12),
	@BizDatePrior datetime,
	@DayCount smallint

Exec spSecIdSymbolLookup @Symbol, @SecId = @SecId Output

If (@SecId = @Symbol) -- Security master likely does or did not know this item.
	Exec spSecMasterItemSet
		@SecId = @Symbol,
		@SecIdTypeIndex = 2,
		@Description = @Description

Select	@BizDatePrior = Max(BizDate) -- Gets the prior business date for this exchange.
From	tbThreshold
Where	BizDate < @BizDate
And	Exchange = @Exchange

If (@BizDatePrior Is Not Null) -- We have prior data so increment day count if appropriate.
	Select	@DayCount = IsNull(DayCount, 1) + 1
	From	tbThreshold
	Where	BizDate = @BizDatePrior
	And	Exchange = @Exchange
	And	Symbol = @Symbol

If (@DayCount Is Null) -- This security was not a threshold item on the prior business date.
	Select	@DayCount = 1

Insert	tbThreshold
Values	(
	@BizDate,
	@Exchange,
	@Symbol,
	@SecId,
	@Description,
	@DayCount
	)	
	
End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spThresholdItemSet]  TO [roleCourier]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spThresholdList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spThresholdList]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spThresholdList
	@BizDate datetime = Null
As
Begin

If (@BizDate Is Null)
	Select		Convert(char(10), T.BizDate, 120) As ListDate,
			T.Exchange,
			T.SecId,
			T.Symbol, 
			T.Description,
			T.DayCount
	From 		tbThreshold T,
			tbThresholdControl TC
	Where 		T.BizDate = TC.BizDate
	And		TC.Exchange = T.Exchange
	Order By	T.Symbol
Else
	Begin
		Select		Max(BizDate) As BizDate,
				Exchange
		Into		#ThresholdControl
		From		tbThreshold
		Where		BizDate < @BizDate
		Group By	Exchange
	
		Select		Convert(char(10), T.BizDate, 120) As ListDate,
				T.Exchange,
				T.SecId,
				T.Symbol, 
				T.Description,
				T.DayCount
		From 		tbThreshold T,
				#ThresholdControl TC
		Where 		T.BizDate = TC.BizDate
		And		TC.Exchange = T.Exchange
		Order By	T.Symbol
	End
End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spThresholdList]  TO [roleMedalist]
GO

GRANT  EXECUTE  ON [dbo].[spThresholdList]  TO [roleCourier]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spThresholdPurge]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spThresholdPurge]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spThresholdPurge
	@BizDate	datetime,
	@Exchange	char(4)
As
Begin

Declare	@BizDatePrior	datetime

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Delete
From	tbThreshold
Where	BizDate = @BizDate
And	Exchange = @Exchange

If (@@RowCount = 0 And Not Exists (	Select	Exchange
					From	tbThresholdControl
					Where	Exchange = @Exchange And BizDate = @BizDate))
Begin
	Commit Transaction
	Return
End

Select	@BizDatePrior = Max(BizDate)
From	tbThreshold
Where	BizDate < @BizDate
And	Exchange = @Exchange

If (@BizDatePrior Is Not Null)	
	Update	tbThresholdControl
	Set	BizDate = @BizDatePrior,
		ItemCount = Null,
		ScribeTime = Null,
		FileTimeStamp = Null
	Where	Exchange = @Exchange
Else
	Delete
	From	tbThresholdControl
	Where	Exchange = @Exchange

Update	tbSecMaster
Set	IsThreshold = 0
Where	IsThreshold = 1

Update	tbSecMaster
Set	IsThreshold = 1
Where	SecId In (	Select	T.SecId
			From 	tbThreshold T,
				tbThresholdControl TC
			Where 	T.BizDate = TC.BizDate
			And	TC.Exchange = T.Exchange	)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spThresholdPurge]  TO [roleCourier]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spThresholdSubset]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spThresholdSubset]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spThresholdSubset
	@BizDateSubset datetime = Null,
	@BizDate datetime = Null
As
Begin

If @BizDateSubset Is Null
	Select	@BizDateSubset = Max(BizDate)
	From	tbThreshold
	Where	BizDate < @BizDate

If @BizDate Is Null
	Select	@BizDate = Max(BizDate)
	From	tbThreshold
	Where	BizDate < @BizDateSubset

Select		SecId, Symbol, [Description], Exchange
From		tbThreshold
Where		BizDate = @BizDateSubset
	And	Symbol Not In (Select Symbol From tbThreshold Where BizDate = @BizDate)
Order By	Symbol

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spThresholdSubset]  TO [roleMedalist]
GO

GRANT  EXECUTE  ON [dbo].[spThresholdSubset]  TO [roleCourier]
GO

--------------------------------------------------------------------------------
--| End Threshold modifications.


--| Inventory Subscriber modifications.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbInventoryFileDataMasks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbInventoryFileDataMasks]
GO

CREATE TABLE [dbo].[tbInventoryFileDataMasks] (
	[Desk] [varchar] (12) NOT NULL ,
	[RecordLength] [smallint] NOT NULL ,
	[HeaderFlag] [char] (1) NOT NULL ,
	[DataFlag] [char] (1) NOT NULL ,
	[TrailerFlag] [char] (1) NOT NULL ,
	[AccountLocale] [smallint] NOT NULL ,
	[Delimiter] [char] (1) NOT NULL ,
	[AccountOrdinal] [smallint] NOT NULL ,
	[SecIdOrdinal] [smallint] NOT NULL ,
	[QuantityOrdinal] [smallint] NOT NULL ,
	[RecordCountOrdinal] [smallint] NOT NULL ,
	[AccountPosition] [smallint] NOT NULL ,
	[AccountLength] [smallint] NOT NULL ,
	[BizDateDD] [smallint] NOT NULL ,
	[BizDateMM] [smallint] NOT NULL ,
	[BizDateYY] [smallint] NOT NULL ,
	[SecIdPosition] [smallint] NOT NULL ,
	[SecIdLength] [smallint] NOT NULL ,
	[QuantityPosition] [smallint] NOT NULL ,
	[QuantityLength] [smallint] NOT NULL ,
	[RecordCountPosition] [smallint] NOT NULL ,
	[RecordCountLength] [smallint] NOT NULL ,
	[ActUserId] [varchar] (50) NOT NULL ,
	[ActTime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbInventoryFileDataMasks] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbInventoryFileMask] PRIMARY KEY  CLUSTERED 
	(
		[Desk]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tbInventoryFileDataMasks] ADD 
	CONSTRAINT [FK_tbInventoryFileDataMask_tbDesks] FOREIGN KEY 
	(
		[Desk]
	) REFERENCES [dbo].[tbDesks] (
		[Desk]
	) ON UPDATE CASCADE 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInventoryFileDataMaskGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spInventoryFileDataMaskGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spInventoryFileDataMaskGet
	@Desk varchar(12),
	@RecordLength smallint = Null Output,
	@HeaderFlag char(1) = Null Output,
	@DataFlag char(1) = Null Output,
	@TrailerFlag char(1) = Null Output,
	@AccountLocale smallint = Null Output,
	@Delimiter char(1) = Null Output,
	@AccountOrdinal smallint = Null Output,
	@SecIdOrdinal smallint = Null Output,
	@QuantityOrdinal smallint = Null Output,
	@RecordCountOrdinal smallint = Null Output,
	@AccountPosition smallint = Null Output,
	@AccountLength smallint = Null Output,
	@BizDateDD smallint = Null Output,
	@BizDateMM smallint = Null Output,
	@BizDateYY smallint = Null Output,
	@SecIdPosition smallint = Null Output,
	@SecIdLength smallint = Null Output,
	@QuantityPosition smallint = Null Output,
	@QuantityLength smallint = Null Output,
	@RecordCountPosition smallint = Null Output,
	@RecordCountLength smallint = Null Output
As
Begin

Select	@RecordLength = RecordLength,
	@HeaderFlag = HeaderFlag,
	@DataFlag = DataFlag,
	@TrailerFlag = TrailerFlag,
	@AccountLocale = AccountLocale,
	@Delimiter = Delimiter,
	@AccountOrdinal = AccountOrdinal,
	@SecIdOrdinal = SecIdOrdinal,
	@QuantityOrdinal = QuantityOrdinal,
	@RecordCountOrdinal = RecordCountOrdinal,
	@AccountPosition = AccountPosition,
	@AccountLength = AccountLength,
	@BizDateDD = BizDateDD,
	@BizDateMM = BizDateMM,
	@BizDateYY = BizDateYY,
	@SecIdPosition = SecIdPosition,
	@SecIdLength = SecIdLength,
	@QuantityPosition = QuantityPosition,
	@QuantityLength = QuantityLength,
	@RecordCountPosition = RecordCountPosition,
	@RecordCountLength = RecordCountLength
From	tbInventoryFileDataMasks
Where	Desk = 	@Desk

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spInventoryFileDataMaskGet]  TO [roleMedalist]
GO

GRANT  EXECUTE  ON [dbo].[spInventoryFileDataMaskGet]  TO [roleCourier]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInventoryFileDataMaskList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spInventoryFileDataMaskList]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spInventoryFileDataMaskList
	@Desk varchar(12) = Null,
	@UtcOffset smallint = 0
As
Begin

Select	IFDM.Desk,
	IFDM.RecordLength,
	IFDM.HeaderFlag,
	IFDM.DataFlag,
	IFDM.TrailerFlag,
	IFDM.AccountLocale,
	IFDM.Delimiter,
	IFDM.AccountOrdinal,
	IFDM.SecIdOrdinal,
	IFDM.QuantityOrdinal,
	IFDM.RecordCountOrdinal,
	IFDM.AccountPosition,
	IFDM.AccountLength,
	IFDM.BizDateDD,
	IFDM.BizDateMM,
	IFDM.BizDateYY,
	IFDM.SecIdPosition,
	IFDM.SecIdLength,
	IFDM.QuantityPosition,
	IFDM.QuantityLength,
	IFDM.RecordCountPosition,
	IFDM.RecordCountLength,
	DateAdd(n, @UtcOffset, IFDM.ActTime) As ActTime,
	U.ShortName As ActUserShortName
From	tbInventoryFileDataMasks IFDM,
	tbUsers U
Where	Desk = IsNull(@Desk, Desk)
And	IFDM.ActUserId = U.UserId
 
End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spInventoryFileDataMaskList]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInventoryFileDataMaskSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spInventoryFileDataMaskSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spInventoryFileDataMaskSet
	@Desk varchar(12),
	@RecordLength smallint = Null,
	@HeaderFlag char(1) = Null,
	@DataFlag char(1) = Null,
	@TrailerFlag char(1) = Null,
	@AccountLocale smallint = Null,
	@Delimiter char(1) = Null,
	@AccountOrdinal smallint = Null,
	@SecIdOrdinal smallint = Null,
	@QuantityOrdinal smallint = Null,
	@RecordCountOrdinal smallint = Null,
	@AccountPosition smallint = Null,
	@AccountLength smallint = Null,
	@BizDateDD smallint = Null,
	@BizDateMM smallint = Null,
	@BizDateYY smallint = Null,
	@SecIdPosition smallint = Null,
	@SecIdLength smallint = Null,
	@QuantityPosition smallint = Null,
	@QuantityLength smallint = Null,
	@RecordCountPosition smallint = Null,
	@RecordCountLength smallint = Null,
	@ActorLogonId varchar(50)
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update	tbInventoryFileDataMask
Set	RecordLength = IsNull(@RecordLength, -1),
	HeaderFlag = IsNull(@HeaderFlag, '='),
	DataFlag = IsNull(@DataFlag, '='),
	TrailerFlag = IsNull(@TrailerFlag, '='),
	AccountLocale = IsNull(@AccountLocale, -1),
	Delimiter = IsNull(@Delimiter, '0'),
	AccountOrdinal = IsNull(@AccountOrdinal, -1),
	SecIdOrdinal = IsNull(@SecIdOrdinal, -1),
	QuantityOrdinal = IsNull(@QuantityOrdinal, -1),
	RecordCountOrdinal = IsNull(@RecordCountOrdinal, -1),
	AccountPosition = IsNull(@AccountPosition, -1),
	AccountLength = IsNull(@AccountLength, -1),
	BizDateDD = IsNull(@BizDateDD, -1),
	BizDateMM = IsNull(@BizDateMM, -1),
	BizDateYY = IsNull(@BizDateYY, -1),
	SecIdPosition = IsNull(@SecIdPosition, -1),
	SecIdLength = IsNull(@SecIdLength, -1),
	QuantityPosition = IsNull(@QuantityPosition, -1),
	QuantityLength = IsNull(@QuantityLength, -1),
	RecordCountPosition = IsNull(@RecordCountPosition, -1),
	RecordCountLength = IsNull(@RecordCountLength, -1),
	ActorLogonId = @ActorLogonId,
	ActTimeUtc = GetUTCDate()
Where	Desk =  @Desk

If (@@RowCount = 0)     
	Insert	tbInventoryFileDataMask
	Values	(
		@Desk,
		IsNull(@RecordLength, -1),
		IsNull(@HeaderFlag, '='),
		IsNull(@DataFlag, '='),
		IsNull(@TrailerFlag, '='),
		IsNull(@AccountLocale, -1),
		IsNull(@Delimiter, '0'),
		IsNull(@AccountOrdinal, -1),
		IsNull(@SecIdOrdinal, -1),
		IsNull(@QuantityOrdinal, -1),
		IsNull(@RecordCountOrdinal, -1),
		IsNull(@AccountPosition, -1),
		IsNull(@AccountLength, -1),
		IsNull(@BizDateDD, -1),
		IsNull(@BizDateMM, -1),
		IsNull(@BizDateYY, -1),
		IsNull(@SecIdPosition, -1),
		IsNull(@SecIdLength, -1),
		IsNull(@QuantityPosition, -1),
		IsNull(@QuantityLength, -1),
		IsNull(@RecordCountPosition, -1),
		IsNull(@RecordCountLength, -1),
		@ActorLogonId,
		GetUTCDate()
		)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spInventoryFileDataMaskSet]  TO [roleMedalist]
GO

--------------------------------------------------------------------------------
--| End Inventory Subscriber modifications.


--| Box Position modifications.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbBoxPosition]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbBoxPosition]
GO

CREATE TABLE [dbo].[tbBoxPosition] (
	[BizDate] [datetime] NOT NULL ,
	[SecId] [varchar] (12) NOT NULL ,
	[BookGroup] [varchar] (10) NOT NULL ,
	[CustomerLongSettled] [bigint] NOT NULL ,
	[CustomerLongTraded] [bigint] NOT NULL ,
	[CustomerShortSettled] [bigint] NOT NULL ,
	[CustomerShortTraded] [bigint] NOT NULL ,
	[FirmLongSettled] [bigint] NOT NULL ,
	[FirmLongTraded] [bigint] NOT NULL ,
	[FirmShortSettled] [bigint] NOT NULL ,
	[FirmShortTraded] [bigint] NOT NULL ,
	[CustomerPledgeSettled] [bigint] NOT NULL ,
	[CustomerPledgeTraded] [bigint] NOT NULL ,
	[FirmPledgeSettled] [bigint] NOT NULL ,
	[FirmPledgeTraded] [bigint] NOT NULL ,
	[DvpFailInSettled] [bigint] NOT NULL ,
	[DvpFailInTraded] [bigint] NOT NULL ,
	[DvpFailOutSettled] [bigint] NOT NULL ,
	[DvpFailOutTraded] [bigint] NOT NULL ,
	[BrokerFailInSettled] [bigint] NOT NULL ,
	[BrokerFailInTraded] [bigint] NOT NULL ,
	[BrokerFailOutSettled] [bigint] NOT NULL ,
	[BrokerFailOutTraded] [bigint] NOT NULL ,
	[ClearingFailInSettled] [bigint] NOT NULL ,
	[ClearingFailInTraded] [bigint] NOT NULL ,
	[ClearingFailOutSettled] [bigint] NOT NULL ,
	[ClearingFailOutTraded] [bigint] NOT NULL ,
	[OtherFailInSettled] [bigint] NOT NULL ,
	[OtherFailInTraded] [bigint] NOT NULL ,
	[OtherFailOutSettled] [bigint] NOT NULL ,
	[OtherFailOutTraded] [bigint] NOT NULL ,
	[ExDeficitSettled] [bigint] NOT NULL ,
	[ExDeficitTraded] [bigint] NOT NULL ,
	[DvpFailInDayCount] [int] NOT NULL ,
	[DvpFailOutDayCount] [int] NOT NULL ,
	[BrokerFailInDayCount] [int] NOT NULL ,
	[BrokerFailOutDayCount] [int] NOT NULL ,
	[ClearingFailInDayCount] [int] NOT NULL ,
	[ClearingFailOutDayCount] [int] NOT NULL ,
	[OtherFailInDayCount] [int] NOT NULL ,
	[OtherFailOutDayCount] [int] NOT NULL ,
	[DeficitDayCount] [int] NOT NULL ,
	[State] [bit] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbBoxPosition] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbBoxPosition] PRIMARY KEY  CLUSTERED 
	(
		[BizDate],
		[SecId],
		[BookGroup]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxPositionGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBoxPositionGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure spBoxPositionGet
	@BizDate  datetime,
	@SecId varchar(12),
	@PoolCodeIgnore varchar(255) = Null
As
Begin

If (@PoolCodeIgnore Is Null)
	Select	@PoolCodeIgnore = ''
	Exec	spKeyValueGet 'BoxPositionPoolCodeIgnore', @PoolCodeIgnore Output

Select		BG.BookGroup,
		IsNull(BP.CustomerLongSettled, 0) As CustomerLongSettled,
		IsNull(BP.CustomerLongTraded, 0) As CustomerLongTraded,
		IsNull(BP.CustomerShortSettled, 0) As CustomerShortSettled,
		IsNull(BP.CustomerShortTraded, 0) As CustomerShortTraded,
		IsNull(BP.FirmLongSettled, 0) As FirmLongSettled,
		IsNull(BP.FirmLongTraded, 0) As FirmLongTraded,
		IsNull(BP.FirmShortSettled, 0) As FirmShortSettled,
		IsNull(BP.FirmShortTraded, 0) As FirmShortTraded,
		IsNull(BP.CustomerPledgeSettled, 0) As CustomerPledgeSettled,
		IsNull(BP.CustomerPledgeTraded, 0) As CustomerPledgeTraded,
		IsNull(BP.FirmPledgeSettled, 0) As FirmPledgeSettled,
		IsNull(BP.FirmPledgeTraded, 0) As FirmPledgeTraded,
		IsNull(BP.DvpFailInSettled, 0) As DvpFailInSettled,
		IsNull(BP.DvpFailInTraded, 0) As DvpFailInTraded,
		IsNull(BP.DvpFailOutSettled, 0) As DvpFailOutSettled,
		IsNull(BP.DvpFailOutTraded, 0) As DvpFailOutTraded,
		IsNull(BP.BrokerFailInSettled, 0) As BrokerFailInSettled,
		IsNull(BP.BrokerFailInTraded, 0) As BrokerFailInTraded,
		IsNull(BP.BrokerFailOutSettled, 0) As BrokerFailOutSettled,
		IsNull(BP.BrokerFailOutTraded, 0) As BrokerFailOutTraded,
		IsNull(BP.ClearingFailInSettled, 0) As ClearingFailInSettled,
		IsNull(BP.ClearingFailInTraded, 0) As ClearingFailInTraded,
		IsNull(BP.ClearingFailOutSettled, 0) As ClearingFailOutSettled,
		IsNull(BP.ClearingFailOutTraded, 0) As ClearingFailOutTraded,
		IsNull(BP.OtherFailInSettled, 0) As OtherFailInSettled,
		IsNull(BP.OtherFailInTraded, 0) As OtherFailInTraded,
		IsNull(BP.OtherFailOutSettled, 0) As OtherFailOutSettled,
		IsNull(BP.OtherFailOutTraded, 0) As OtherFailOutTraded,
		IsNull(BP.ExDeficitSettled, 0) As ExDeficitSettled,
		IsNull(BP.ExDeficitTraded, 0) As ExDeficitTraded,
		IsNull(BP.DvpFailInDayCount, 0) As DvpFailInDayCount,
		IsNull(BP.DvpFailOutDayCount, 0) As DvpFailOutDayCount,
		IsNull(BP.BrokerFailInDayCount, 0) As BrokerFailInDayCount,
		IsNull(BP.BrokerFailOutDayCount, 0) As BrokerFailOutDayCount,
		IsNull(BP.ClearingFailInDayCount, 0) As ClearingFailInDayCount,
		IsNull(BP.ClearingFailOutDayCount, 0) As ClearingFailOutDayCount,
		IsNull(BP.OtherFailInDayCount, 0) As OtherFailInDayCount,
		IsNull(BP.OtherFailOutDayCount, 0) As OtherFailOutDayCount,
		IsNull(BP.DeficitDayCount, 0) As DeficitDayCount,
		Convert(bigint, 0) As StockBorrowSettled,
		Convert(bigint, 0) As StockBorrowTraded,
		Convert(bigint, 0) As StockLoanSettled,
		Convert(bigint, 0) As StockLoanTraded
Into		#Position
From		tbBookGroups BG,
		tbBoxPosition BP
Where		BG.BizDate = @BizDate
And		BG.BookGroup *= BP.BookGroup
And		BP.BizDate = @BizDate
And		BP.SecId = @SecId
Order By	1

Select		BookGroup,
		IsNull(Sum(QuantitySettled * CharIndex(ContractType, 'B')), 0) As StockBorrowSettled,
		IsNull(Sum(Quantity * CharIndex(ContractType, 'B')), 0) As StockBorrowTraded,
		IsNull(Sum(QuantitySettled * CharIndex(ContractType, 'L')), 0) As StockLoanSettled,
		IsNull(Sum(Quantity * CharIndex(ContractType, 'L')), 0) As StockLoanTraded
Into		#BorrowLoan
From		tbContracts
Where		BizDate = @BizDate
	And	SecId = @SecId
	And	CharIndex(PoolCode, @PoolCodeIgnore) = 0
Group By	BookGroup

Update		#Position
Set		StockBorrowSettled = BL.StockBorrowTraded, -- Showing traded only.
		StockBorrowTraded = BL.StockBorrowTraded,
		StockLoanSettled = BL.StockLoanTraded, -- Showing traded only.
		StockLoanTraded = BL.StockLoanTraded
From		#Position P,
		#BorrowLoan BL
Where		P.BookGroup = BL.BookGroup

Drop Table	#BorrowLoan

Update		#Position
Set		ExDeficitSettled = (CustomerLongSettled - CustomerShortSettled + FirmLongSettled - FirmShortSettled
				+ StockBorrowTraded - StockLoanTraded - OtherFailInSettled + OtherFailOutSettled
				- DvpFailInSettled + DvpFailOutSettled - ClearingFailInSettled + ClearingFailOutSettled
				- BrokerFailInSettled + BrokerFailOutSettled - CustomerPledgeSettled - FirmPledgeSettled),
		ExDeficitTraded  = (CustomerLongTraded - CustomerShortTraded + FirmLongTraded - FirmShortTraded
				+ StockBorrowTraded - StockLoanTraded - OtherFailInTraded + OtherFailOutTraded 
				- DvpFailInTraded + DvpFailOutTraded - ClearingFailInTraded + ClearingFailOutTraded
				- BrokerFailInTraded + BrokerFailOutTraded - CustomerPledgeTraded - FirmPledgeTraded)
From		#Position

Select		*
From		#Position

Drop Table	#Position

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxPositionGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxPositionStateSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBoxPositionStateSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure spBoxPositionStateSet
	@BizDate  datetime,
	@BookGroup  varchar(10)
As
Begin

Update	tbBoxPosition
Set	State = 0
Where	BizDate = @BizDate
And	SecId = SecId
And	BookGroup = @BookGroup

Return	@@RowCount

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxPositionStateSet]  TO [roleClient]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxPositionPurge]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBoxPositionPurge]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure spBoxPositionPurge
	@BizDate  datetime,
	@BookGroup  varchar(10)
As
Begin

Delete
From	tbBoxPosition
Where	BizDate = @BizDate
And	SecId = SecId
And	BookGroup = @BookGroup
And	State = 0

Return	@@RowCount

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxPositionPurge]  TO [roleClient]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxPositionItemSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBoxPositionItemSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spBoxPositionItemSet
		@BizDatePrior datetime = Null,
		@BizDate datetime,
		@SecId varchar(12),
		@BookGroup varchar(10),
		@CustomerLongSettled bigint,
		@CustomerLongTraded bigint,
		@CustomerShortSettled bigint,
		@CustomerShortTraded bigint,
		@FirmLongSettled bigint,
		@FirmLongTraded bigint,
		@FirmShortSettled bigint,
		@FirmShortTraded bigint,
		@CustomerPledgeSettled bigint,
		@CustomerPledgeTraded bigint,
		@FirmPledgeSettled bigint,
		@FirmPledgeTraded bigint,
		@DvpFailInSettled bigint,
		@DvpFailInTraded bigint,
		@DvpFailOutSettled bigint,
		@DvpFailOutTraded bigint,
		@BrokerFailInSettled bigint,
		@BrokerFailInTraded bigint,
		@BrokerFailOutSettled bigint,
		@BrokerFailOutTraded bigint,
		@ClearingFailInSettled bigint,
		@ClearingFailInTraded bigint,
		@ClearingFailOutSettled bigint,
		@ClearingFailOutTraded bigint,
		@OtherFailInSettled bigint,
		@OtherFailInTraded bigint,
		@OtherFailOutSettled bigint,
		@OtherFailOutTraded bigint,
		@PoolCodeIgnore varchar(255) = Null
As
Begin

If (@PoolCodeIgnore Is Null)
	Select	@PoolCodeIgnore = ''
	Exec	spKeyValueGet 'BoxPositionPoolCodeIgnore', @PoolCodeIgnore Output

Declare @ExDeficitSettled bigint,
        @ExDeficitTraded bigint,
        @StockBorrowLoanSettled bigint,
        @StockBorrowLoanTraded bigint,
	@DvpFailInDayCount int,
	@DvpFailOutDayCount int,
	@BrokerFailInDayCount int,
	@BrokerFailOutDayCount int,
	@ClearingFailInDayCount int,
	@ClearingFailOutDayCount int,
	@OtherFailInDayCount int,
	@OtherFailOutDayCount int,
	@DeficitDayCount int
        
Select  @StockBorrowLoanSettled = IsNull(Sum(QuantitySettled * CharIndex(ContractType, 'B')), 0)
		- IsNull(Sum(QuantitySettled * CharIndex(ContractType, 'L')), 0),
        @StockBorrowLoanTraded = IsNull(Sum(Quantity * CharIndex(ContractType, 'B')), 0)
		- IsNull(Sum(Quantity * CharIndex(ContractType, 'L')), 0)
From 	tbContracts
Where	BizDate = @BizDate
    And	BookGroup = @BookGroup 
    And	SecId = @SecId
    And CharIndex(PoolCode, @PoolCodeIgnore) = 0

Select  @ExDeficitSettled = ((@CustomerLongSettled - @CustomerShortSettled) + (@FirmLongSettled - @FirmShortSettled)
		+ @StockBorrowLoanTraded - @OtherFailInTraded + @OtherFailOutSettled
		- @DvpFailInSettled + @DvpFailOutSettled - @ClearingFailInSettled + @ClearingFailOutSettled
		- @BrokerFailInSettled + @BrokerFailOutSettled - @CustomerPledgeSettled - @FirmPledgeSettled)

Select  @ExDeficitTraded = ((@CustomerLongTraded - @CustomerShortTraded) + (@FirmLongTraded - @FirmShortTraded)
		+ @StockBorrowLoanTraded  - @OtherFailInTraded + @OtherFailOutTraded
		- @DvpFailInTraded + @DvpFailOutTraded - @ClearingFailInTraded + @ClearingFailOutTraded
		- @BrokerFailInTraded + @BrokerFailOutTraded - @CustomerPledgeTraded - @FirmPledgeTraded)

Update	tbBoxPosition
Set	CustomerLongSettled = @CustomerLongSettled,
	CustomerLongTraded = @CustomerLongTraded,
	CustomerShortSettled = @CustomerShortSettled,
	CustomerShortTraded = @CustomerShortTraded,
	FirmLongSettled = @FirmLongSettled,
	FirmLongTraded = @FirmLongTraded,
	FirmShortSettled = @FirmShortSettled,
	FirmShortTraded = @FirmShortTraded,
	CustomerPledgeSettled = @CustomerPledgeSettled,
	CustomerPledgeTraded = @CustomerPledgeTraded,
	FirmPledgeSettled = @FirmPledgeSettled,
	FirmPledgeTraded = @FirmPledgeTraded,
	DvpFailInSettled = @DvpFailInSettled,
	DvpFailInTraded = @DvpFailInTraded,
	DvpFailOutSettled = @DvpFailOutSettled,
	DvpFailOutTraded = @DvpFailOutTraded,
	BrokerFailInSettled = @BrokerFailInSettled,
	BrokerFailInTraded = @BrokerFailInTraded,
	BrokerFailOutSettled = @BrokerFailOutSettled,
	BrokerFailOutTraded = @BrokerFailOutTraded,
	ClearingFailInSettled = @ClearingFailInSettled,
	ClearingFailInTraded = @ClearingFailInTraded,
	ClearingFailOutSettled = @ClearingFailOutSettled,
	ClearingFailOutTraded = @ClearingFailOutTraded,
	OtherFailInSettled = @OtherFailInSettled,
	OtherFailInTraded = @OtherFailInTraded,
	OtherFailOutSettled = @OtherFailOutSettled,
	OtherFailOutTraded = @OtherFailOutTraded,
	ExDeficitSettled = @ExDeficitSettled,
	ExDeficitTraded = @ExDeficitTraded,
	State = 1
Where	BizDate = @BizDate
    And	SecId = @SecId
    And	BookGroup = @BookGroup

If (@@RowCount = 0) -- New record.
	Begin
	
	Select	@DvpFailInDayCount = 0,
		@DvpFailOutDayCount = 0,
		@BrokerFailInDayCount = 0,
		@BrokerFailOutDayCount = 0,
		@ClearingFailInDayCount = 0,
		@ClearingFailOutDayCount = 0,
		@OtherFailInDayCount = 0,
		@OtherFailOutDayCount = 0,
		@DeficitDayCount = 0
		
	Select	@DvpFailInDayCount = IsNull(DvpFailInDayCount, 0) + 1
	From	tbBoxPosition
	Where	BizDate = @BizDatePrior
	And	SecId = @SecId
	And	BookGroup = @BookGroup
	And	DvpFailInSettled > 0
			
	Select	@DvpFailOutDayCount = IsNull(DvpFailOutDayCount, 0) + 1
	From	tbBoxPosition
	Where	BizDate = @BizDatePrior
	And	SecId = @SecId
	And	BookGroup = @BookGroup
	And	DvpFailOutSettled > 0
			
	Select	@BrokerFailInDayCount = IsNull(BrokerFailInDayCount, 0) + 1
	From	tbBoxPosition
	Where	BizDate = @BizDatePrior
	And	SecId = @SecId
	And	BookGroup = @BookGroup
	And	BrokerFailInSettled > 0
			
	Select	@BrokerFailOutDayCount = IsNull(BrokerFailOutDayCount, 0) + 1
	From	tbBoxPosition
	Where	BizDate = @BizDatePrior
	And	SecId = @SecId
	And	BookGroup = @BookGroup
	And	BrokerFailOutSettled > 0
			
	Select	@ClearingFailInDayCount = IsNull(ClearingFailInDayCount, 0) + 1
	From	tbBoxPosition
	Where	BizDate = @BizDatePrior
	And	SecId = @SecId
	And	BookGroup = @BookGroup
	And	ClearingFailInSettled > 0
			
	Select	@ClearingFailOutDayCount = IsNull(ClearingFailOutDayCount, 0) + 1
	From	tbBoxPosition
	Where	BizDate = @BizDatePrior
	And	SecId = @SecId
	And	BookGroup = @BookGroup
	And	ClearingFailOutSettled > 0
			
	Select	@OtherFailInDayCount = IsNull(OtherFailInDayCount, 0) + 1
	From	tbBoxPosition
	Where	BizDate = @BizDatePrior
	And	SecId = @SecId
	And	BookGroup = @BookGroup
	And	OtherFailInSettled > 0
			
	Select	@OtherFailOutDayCount = IsNull(OtherFailOutDayCount, 0) + 1
	From	tbBoxPosition
	Where	BizDate = @BizDatePrior
	And	SecId = @SecId
	And	BookGroup = @BookGroup
	And	OtherFailOutSettled > 0
			
	Select	@DeficitDayCount = IsNull(DeficitDayCount, 0) + 1
	From	tbBoxPosition
	Where	BizDate = @BizDatePrior
	And	SecId = @SecId
	And	BookGroup = @BookGroup
	And	ExDeficitSettled < 0
			
	Insert 	tbBoxPosition
	Values	(
		@BizDate,
		@SecId,
		@BookGroup,
		@CustomerLongSettled,
		@CustomerLongTraded,
		@CustomerShortSettled,
		@CustomerShortTraded,
		@FirmLongSettled,
		@FirmLongTraded,
		@FirmShortSettled,
		@FirmShortTraded,
		@CustomerPledgeSettled,
		@CustomerPledgeTraded,
		@FirmPledgeSettled,
		@FirmPledgeTraded,
		@DvpFailInSettled,
		@DvpFailInTraded,
		@DvpFailOutSettled,
		@DvpFailOutTraded,
		@BrokerFailInSettled,
		@BrokerFailInTraded,
		@BrokerFailOutSettled,
		@BrokerFailOutTraded,
		@ClearingFailInSettled,
		@ClearingFailInTraded,
		@ClearingFailOutSettled,
		@ClearingFailOutTraded,
		@OtherFailInSettled,
		@OtherFailInTraded,
		@OtherFailOutSettled,
		@OtherFailOutTraded,
		@ExDeficitSettled,
		@ExDeficitTraded,
		@DvpFailInDayCount,
		@DvpFailOutDayCount,
		@BrokerFailInDayCount,
		@BrokerFailOutDayCount,
		@ClearingFailInDayCount,
		@ClearingFailOutDayCount,
		@OtherFailInDayCount,
		@OtherFailOutDayCount,
		@DeficitDayCount,
		1
		)
	End

Return	1

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxPositionItemSet]  TO [roleClient]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxFailHistoryGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBoxFailHistoryGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) 2005  All rights reserved.

CREATE Procedure dbo.spBoxFailHistoryGet
	@SecId varchar(12)
As
Begin

Set RowCount 15

Select Distinct	BizDate
Into		#Dates
From		tbBookGroups
Where		BizDate > (GetDate() - 25)
Order  By	1 Desc

Set RowCount 0

Select		D.BizDate,
		BP.ExDeficitSettled,
		BP.DvpFailInSettled,
		BP.DvpFailOutSettled,
		BP.BrokerFailInSettled,
		BP.BrokerFailOutSettled,
		BP.ClearingFailInSettled,
		BP.ClearingFailOutSettled,
		BP.OtherFailInSettled,
		BP.OtherFailOutSettled,
		BP.DvpFailInDayCount,
		BP.DvpFailOutDayCount
From		#Dates D,
		tbBoxPosition BP
Where		D.BizDate *= BP.BizDate
	And	BP.SecId = @SecId

Drop Table	#Dates

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxFailHistoryGet]  TO [medalistService]
GO

--------------------------------------------------------------------------------
--| End Box Position modifications.


--| Box Location modifications.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbBoxLocation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbBoxLocation]
GO

CREATE TABLE [dbo].[tbBoxLocation] (
	[SecId] [varchar] (12) NOT NULL ,
	[BookGroup] [varchar] (10) NOT NULL ,
	[Custodian] [varchar] (25) NOT NULL ,
	[QuantitySettled] [bigint] NOT NULL ,
	[QuantityTraded] [bigint] NOT NULL ,
	[State] [bit] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbBoxLocation] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbBoxLocation] PRIMARY KEY  CLUSTERED 
	(
		[SecId],
		[BookGroup],
		[Custodian]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxLocationGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBoxLocationGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure spBoxLocationGet
	@SecId varchar(12) = Null,
	@BookGroup varchar(10) = Null,
	@Custodian varchar(25) = Null
As
Begin

Select		SecId, BookGroup, Custodian, QuantitySettled, QuantityTraded
From		tbBoxLocation
Where		SecId = IsNull(@SecId, SecId)
And		BookGroup = IsNull(@BookGroup, BookGroup)
And		Custodian = IsNull(@Custodian, Custodian)
Order By	1, 2, 3

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxLocationGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxLocationStateSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBoxLocationStateSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure spBoxLocationStateSet

As
Begin

Update	tbBoxLocation
Set	State = 0

Return	@@RowCount

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxLocationStateSet] TO [roleClient]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxLocationPurge]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBoxLocationPurge]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure spBoxLocationPurge

As
Begin

Delete
From	tbBoxLocation
Where	State = 0

Return	@@RowCount

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxLocationPurge]  TO [roleClient]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxLocationItemSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBoxLocationItemSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spBoxLocationItemSet
		@SecId varchar(12),
		@BookGroup varchar(10),
		@Custodian varchar(25),
		@QuantitySettled bigint,
		@QuantityTraded bigint
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

	Update	tbBoxLocation
	Set	QuantitySettled = Case When State = 1 Then QuantitySettled + @QuantitySettled Else @QuantitySettled End,
		QuantityTraded = Case When State = 1 Then QuantityTraded + @QuantityTraded Else @QuantityTraded End,
		State = 1
	Where	SecId = @SecId
	And	BookGroup = @BookGroup
	And	Custodian = @Custodian

	If (@@RowCount = 0)
		Insert 	tbBoxLocation
		Values	(
			@SecId,
			@BookGroup,
			@Custodian,
			@QuantitySettled,
			@QuantityTraded,
			1
			)
Commit Transaction

Return	1

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxLocationItemSet]  TO [roleClient]
GO

--------------------------------------------------------------------------------
--| End Box Location modifications.


--| Book Credit.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBookCreditAvailableGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBookCreditAvailableGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBookCreditAvailableGet
	@BookGroup varchar(10),
	@Book varchar(10),
	@ContractType char(1),
	@AvailableCredit bigint = 0 Output
As
Begin

Declare @BorrowAmount		bigint
Declare	@LoanAmount		bigint
Declare @BorrowCredit		bigint
Declare @LoanCredit		bigint
Declare	@BizDate		datetime
Declare @IsAggregateCredit	varchar(5)
Declare	@KeyId			varchar(25)
Declare @BookParent		varchar(10)

Select @BizDate = Convert(datetime, (Select KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate'))
Select @KeyId = 'AggregateCredit' + @BookGroup
Select @IsAggregateCredit = 'False'

Exec spKeyValueGet @KeyId, @KeyValue = @IsAggregateCredit Output

Select		@BookParent = BookParent
From		tbBooks
Where		BookGroup = @BookGroup
And		Book = @Book

Select 		Case	When	(C.Amount > C.AmountSettled)
			Then	C.Amount * CharIndex(C.ContractType, 'B')
			Else	C.AmountSettled * CharIndex(C.ContractType, 'B')
			End As	BorrowAmount,
		Case 	When 	(C.Amount > C.AmountSettled)
			Then	C.Amount * CharIndex(C.ContractType, 'L')
			Else	C.AmountSettled * CharIndex(C.ContractType, 'L')
			End As	LoanAmount
Into 		#Amounts
From 		tbBooks B,
		tbContracts C
Where		C.BizDate = @BizDate
	And	C.BookGroup = @BookGroup
	And	C.Book = B.Book
	And	B.BookParent = @BookParent


Select 	@BorrowAmount = Sum(BorrowAmount),
	@LoanAmount   =  Sum(LoanAmount)
From 	#Amounts

Drop Table #Amounts

If(Lower(@IsAggregateCredit) = 'true')
	Select @AvailableCredit = (AmountLimitBorrow + AmountLimitLoan) - (@BorrowAmount + @LoanAmount)
	From	tbBooks
	Where	BookGroup = @BookGroup
	And	Book = @BookParent
Else If (Lower(@IsAggregateCredit) = 'false')
	If (Lower(@ContractType) = 'l')	
		Select @AvailableCredit = AmountLimitLoan - @LoanAmount
		From	tbBooks
		Where	BookGroup = @BookGroup
		And	Book = @BookParent	
	Else If (Lower(@ContractType) = 'b')
		Select @AvailableCredit = AmountLimitBorrow - @BorrowAmount
		From	tbBooks
		Where	BookGroup = @BookGroup
		And	Book = @BookParent
End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBookCreditAvailableGet]  TO [roleMedalist]
GO

--------------------------------------------------------------------------------
--| Book Credit.


--| Init values.
--------------------------------------------------------------------------------

If Not Exists (Select * From tbFunctions Where FunctionPath = 'PositionContractBlotter')
	Insert Into tbFunctions Values ('PositionContractBlotter', '0', '0', '[NONE]')
GO

/*
Stock-Borrow.com addition.
*/			

If Not Exists (Select Firm From tbFirms Where FirmCode = 'SB')
	Insert into tbFirms
	Values('SB', 'Stock-Borrow')
	Go

If Not Exists (Select DeskType From tbDeskTypes Where DeskTypeCode = 'SP')
	Insert into tbDeskTypes
	Values('SP', 'Service Provider')
	Go
 
If Not Exists (Select Desk From tbDesks Where Desk = 'SB.US.SP')
	Insert into tbDesks
	Values('SB.US.SP', 'SB', 'US', 'SP')
	Go
 
/*
Initialize tbInventorySubscriber.
*/

If Not Exists (Select Desk From tbInventorySubscriber Where Desk = 'BOFA.US.S')
	Insert	tbInventorySubscriber (Desk, FilePathName, FileHost, FileUserName, FilePassword, ActUserId, ActTime, IsActive)
	Values ('BOFA.US.S', '/BOFAAVL_{yyyyMMdd}_0530.txt', 'ftp.primebroker.com', 'penson', 'p3ns0n', 'ADMIN', GetUtcDate(), 1)
	Go

If Not Exists (Select Desk From tbInventorySubscriber Where Desk = 'CS.US.S')
	Insert	tbInventorySubscriber (Desk, FilePathName, FileHost, FileUserName, FilePassword, ActUserId, ActTime, IsActive)
	Values ('CS.US.S', '/0234/out/D01O0164.TXT', 'ftp.loanet.com', '0234', 'rid7bwk3bs', 'ADMIN', GetUtcDate(), 1)
	Go

If Not Exists (Select Desk From tbInventorySubscriber Where Desk = 'RJ.US.S')
	Insert	tbInventorySubscriber (Desk, FilePathName, FileHost, FileUserName, FilePassword, ActUserId, ActTime, IsActive)
	Values ('RJ.US.S','/0234/out/D01O0725.TXT','ftp.loanet.com','0234','rid7bwk3bs','ADMIN',GetUtcDate(),1)
	Go

If Not Exists (Select Desk From tbInventorySubscriber Where Desk = 'NMRA.US.S')
	Insert tbInventorySubscriber (Desk, FilePathName, FileHost, FileUserName, FilePassword, ActUserId, ActTime, IsActive)
	Values('NMRA.US.S','/home/penson/inv/nsl_inv/box_{yyyyMMdd}.gs','ftpnsiseclend.us.nomura.com','penson','p3ns1n0','ADMIN',GetUtcDate(),1)
	Go

If Not Exists (Select Desk From tbInventorySubscriber Where Desk = 'CITI.US.S')
	Insert tbInventorySubscriber (Desk, FilePathName, FileHost, FileUserName, FilePassword, ActUserId, ActTime, IsActive)
	Values('CITI.US.S', '/citi.avail_{yyyyMMdd}', 'ftp.penson.com', 'Citibank', '477kr61', 'ADMIN', GetUtcDate(), 1) 
	Go
 
If Not Exists (Select Desk From tbInventorySubscriber Where Desk = 'SB.US.SP')
	Insert tbInventorySubscriber (Desk, FilePathName, FileHost, FileUserName, FilePassword, ActUserId, ActTime, IsActive)
	Values('SB.US.SP','/stockborrow.com(penson)/{M-d-yyyy}-1.txt','ftp.americaneagle.com','stock-borrow-penson','sbp9331!q','ADMIN',GetUtcDate(),1)
	Go

/*
Initialize tbInventoryFileDataMasks.
*/

If Not Exists (Select Desk From tbInventoryFileDataMasks Where Desk = 'BOFA.US.S')
	Insert	tbInventoryFileDataMasks
	Values	('BOFA.US.S',-1,'H','D','T',-1,'|',-1,2,4,-1,-1,-1,8,6,4,-1,-1,-1,-1,-1,-1,'ADMIN',GetUtcDate())
	Go

If Not Exists (Select Desk From tbInventoryFileDataMasks Where Desk = 'CS.US.S')
	Insert	tbInventoryFileDataMasks
	Values	('CS.US.S',-1,'1','5','9',0,'0',-1,-1,-1,-1,1,4,11,9,13,1,9,10,9,5,5,'ADMIN',GetUtcDate())
	Go

If Not Exists (Select Desk From tbInventoryFileDataMasks Where Desk = 'RJ.US.S')
	Insert	tbInventoryFileDataMasks
	Values	('RJ.US.S',-1,'1','5','9',0,'0',-1,-1,-1,-1,1,4,11,9,13,1,9,10,9,5,5,'ADMIN',GetUtcDate())
	Go

If Not Exists (Select Desk From tbInventoryFileDataMasks Where Desk = 'NMRA.US.S')
	Insert	tbInventoryFileDataMasks
	Values	('NMRA.US.S',-1,'0','1','9',0,'|',4,2,3,1,-1,-1,22,20,18,-1,-1,-1,-1,-1,-1,'ADMIN',GetUTCDate())
	Go

If Not Exists (Select Desk From tbInventoryFileDataMasks Where Desk = 'CITI.US.S')
	Insert	tbInventoryFileDataMasks
	Values	('CITI.US.S',-1,'1','2','9',0,'|',2,1,4,1,-1,-1,8,6,4,-1,-1,-1,-1,-1,-1,'ADMIN',GetUtcDate())
	Go

If Not Exists (Select Desk From tbInventoryFileDataMasks Where Desk = 'SB.US.SP')
	Insert	tbInventoryFileDataMasks
	Values	('SB.US.SP',-1,'=','=','=',1,',',2,0,1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,'ADMIN',GetUtcDate());
	Go

/*
Initialize book groups.
*/

If Not Exists (Select * From tbBooks Where BookGroup = '7380' And Book = '7380')
	Insert	tbBooks
		(
		BookGroup,
		Book,
		BookParent,
		BookName,
		AddressLine1,
		AddressLine2,
		AddressLine3,
		Phone,
		DtcDeliver,
		DtcMark,
		ActUserId,
		ActTime,
		IsActive
		)
	Values	(
		'7380',
		'7380',
		'7380',
		'PENSON FINANCIAL CONDUIT',
		'1700 PACIFIC AVENUE',
		'SUITE 1400',
		'DALLAS, TX  75201',
		'[214] 765-1077',
		'0234',
		'0234',
		'ADMIN',
		GetUtcDate(),
		1
		)
	Go
	
If Not Exists (Select * From tbBooks Where BookGroup = '0234' And Book = '0234')
	Insert	tbBooks
		(
		BookGroup,
		Book,
		BookParent,
		BookName,
		AddressLine1,
		AddressLine2,
		AddressLine3,
		Phone,
		DtcDeliver,
		DtcMark,
		ActUserId,
		ActTime,
		IsActive
		)
	Values	(
		'0234',
		'0234',
		'0234',
		'PENSON FINANCIAL SERVICES',
		'1700 PACIFIC AVENUE',
		'SUITE 1400',
		'DALLAS, TX  75201',
		'[214] 765-1077',
		'0234',
		'0234',
		'ADMIN',
		GetUtcDate(),
		1
		)
	Go
	
--------------------------------------------------------------------------------
--| End Init values.


--| PositionBoxSummary modifications.
--------------------------------------------------------------------------------

If Not Exists (Select FunctionPath From tbFunctions Where FunctionPath = 'PositionBoxSummary')
	Insert Into tbFunctions Values ('PositionBoxSummary', '0', '0', '[NONE]')
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbBoxSummary]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbBoxSummary]
GO

CREATE TABLE [dbo].[tbBoxSummary] (
	[BookGroup] [varchar] (10) NOT NULL ,
	[SecId] [varchar] (12) NOT NULL ,
	[DoNotRecall] [bit] NOT NULL ,
	[Comment] [varchar] (50) NOT NULL ,
	[ActUserId] [varchar] (50) NOT NULL ,
	[ActTime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbBoxSummary] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbBoxSummary] PRIMARY KEY  CLUSTERED 
	(
		[BookGroup],
		[SecId]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxSummarySet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBoxSummarySet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spBoxSummarySet
	@BookGroup varchar(10),
	@SecId varchar(12),
	@DoNotRecall bit = Null,
	@Comment varchar(50) = Null,
	@ActUserId varchar(50) = Null
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update	tbBoxSummary
Set	DoNotRecall = IsNull(@DoNotRecall, DoNotRecall),
	Comment = IsNull(@Comment, Comment),
	ActUserId = IsNull(@ActUserId, ActUserId),
	ActTime = Case When @ActUserId Is Not Null Then GetUtcDate() Else ActTime End
Where	BookGroup = @BookGroup
And	SecId = @SecId

If (@@RowCount = 0)
	Insert 	tbBoxSummary
	Values	(
		@BookGroup,
		@SecId,
		IsNull(@DoNotRecall, 0),
		IsNull(@Comment, ''),
		@ActUserId,
		GetUTCDate()
		)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxSummarySet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBoxSummaryGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spBoxSummaryGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spBoxSummaryGet
	@BizDate  datetime,
	@UtcOffset smallint = 0,
	@PoolCodeIgnore varchar(255) = Null
As
Begin

If (@PoolCodeIgnore Is Null)
	Select	@PoolCodeIgnore = ''
	Exec	spKeyValueGet 'BoxPositionPoolCodeIgnore', @PoolCodeIgnore Output

Select Distinct	BookGroup, SecId
Into		#SecIdList
From		tbBoxPosition
Where		BizDate = @BizDate

Insert		#SecIdList
Select Distinct	BookGroup, SecId
From		tbContracts
Where		BizDate = @BizDate
	And	Not Exists (Select SecId From #SecIdList)

Select		SIL.BookGroup,
		SIL.SecId,
		IsNull(BP.CustomerLongSettled, 0) As CustomerLongSettled,
		IsNull(BP.CustomerShortSettled, 0) As CustomerShortSettled,
		IsNull(BP.FirmLongSettled, 0) As FirmLongSettled,
		IsNull(BP.FirmShortSettled, 0) As FirmShortSettled,
		IsNull(BP.CustomerPledgeSettled, 0) As CustomerPledgeSettled,
		IsNull(BP.FirmPledgeSettled, 0) As FirmPledgeSettled,
		IsNull(BP.DvpFailInSettled, 0) As DvpFailInSettled,
		IsNull(BP.DvpFailOutSettled, 0) As DvpFailOutSettled,
		IsNull(BP.BrokerFailInSettled, 0) As BrokerFailInSettled,
		IsNull(BP.BrokerFailOutSettled, 0) As BrokerFailOutSettled,
		IsNull(BP.ClearingFailInSettled, 0) As ClearingFailInSettled,
		IsNull(BP.ClearingFailOutSettled, 0) As ClearingFailOutSettled,
		IsNull(BP.OtherFailInSettled, 0) As OtherFailInSettled,
		IsNull(BP.OtherFailOutSettled, 0) As OtherFailOutSettled,
		IsNull(BP.ExDeficitSettled, 0) As ExDeficitSettled,
		Convert(bigint, 0) As StockBorrowSettled,
		Convert(bigint, 0) As StockBorrowTraded,
		Convert(bigint, 0) As StockLoanSettled,
		Convert(bigint, 0) As StockLoanTraded
Into		#Position
From		tbBoxPosition BP,
		#SecIdList SIL
Where		BP.BizDate = @BizDate
	And	SIL.BookGroup *= BP.BookGroup
	And	SIL.SecId *= BP.SecId

Drop Table #SecIdList

Select		BookGroup,
		SecId,
		IsNull(Sum(QuantitySettled * CharIndex(ContractType, 'B')), 0) As StockBorrowSettled,
		IsNull(Sum(Quantity * CharIndex(ContractType, 'B')), 0) As StockBorrowTraded,
		IsNull(Sum(QuantitySettled * CharIndex(ContractType, 'L')), 0) As StockLoanSettled,
		IsNull(Sum(Quantity * CharIndex(ContractType, 'L')), 0) As StockLoanTraded
Into		#BorrowLoan
From		tbContracts
Where		BizDate = @BizDate
	And	CharIndex(PoolCode, @PoolCodeIgnore) = 0
Group By	BookGroup, SecId

Update		#Position
Set		StockBorrowSettled = BL.StockBorrowSettled,
		StockBorrowTraded = BL.StockBorrowTraded,
		StockLoanSettled = BL.StockLoanSettled,
		StockLoanTraded = BL.StockLoanTraded
From		#Position P,
		#BorrowLoan BL
Where		P.BookGroup = BL.BookGroup
	And	P.Secid = BL.SecId

Drop Table	#BorrowLoan

Update		#Position
Set		ExDeficitSettled = (CustomerLongSettled - CustomerShortSettled + FirmLongSettled - FirmShortSettled
				+ StockBorrowTraded - StockLoanTraded - BrokerFailInSettled + BrokerFailOutSettled
				- DvpFailInSettled + DvpFailOutSettled - ClearingFailInSettled + ClearingFailOutSettled
				- OtherFailInSettled + OtherFailOutSettled - CustomerPledgeSettled - FirmPledgeSettled)

Select 		BookGroup,
		SecId,
		Sum(Quantity * CharIndex('B', ContractType)) As BorrowQuantity,
		Sum(Quantity * CharIndex('L', ContractType)) As LoanQuantity
Into		#Recalls
From		tbRecalls
Where		Status = 'O'
Group By	BookGroup, SecId

Select		P.BookGroup,
		P.SecId,
		IsNull(SM.BaseType, 'U') As BaseType,
		SM.ClassGroup,
		SM.IsEasy,
		SM.IsHard,
		SM.IsNoLend,
		SM.IsThreshold,
		SM.LastPrice,
		P.DvpFailInSettled + P.BrokerFailInSettled + P.ClearingFailInSettled  As ReceiveFails,
		P.DvpFailOutSettled + P.BrokerFailOutSettled + P.ClearingFailOutSettled As DeliveryFails,
		P.ExDeficitSettled As ExDeficit,
		P.CustomerPledgeSettled + P.FirmPledgeSettled As OnPledge,
		IsNull(R.BorrowQuantity, 0) As BorrowRecalls,
		IsNull(R.LoanQuantity, 0) As LoanRecalls,
		P.StockBorrowTraded As Borrows,
		P.StockLoanTraded As Loans,
		BS.DoNotRecall,
		BS.ActUserId,
		DateAdd(n, @UtcOffset, BS.ActTime) As ActTime,
		BS.Comment
Into		#Results
From		#Position P,
		#Recalls R,
		tbBoxSummary BS,
		tbSecMaster SM
Where		P.SecId *= SM.SecId
	And	P.BookGroup *= R.BookGroup
	And	P.SecId *= R.SecId
	And	P.BookGroup *= BS.BookGroup
	And	P.SecId *= BS.SecId

Drop Table 	#Recalls

Select		R.BookGroup,
		R.SecId,
		IsNull(SIL.SecIdLink, '') As Symbol,
		R.BaseType,
		R.ClassGroup,
		R.IsEasy,
		R.IsHard,
		R.IsNoLend,
		R.IsThreshold,
		R.LastPrice,
		R.ReceiveFails,
		R.DeliveryFails,
		R.ExDeficit,
		R.OnPledge,
		R.Borrows,
		R.Loans,
		R.BorrowRecalls,
		R.LoanRecalls,
		ISNull(R.DoNotRecall, 0) As DoNotRecall,
		IsNull(U.ShortName, R.ActUserId) As ActUserShortName,
		R.ActTime,
		R.Comment
From		#Results R,
		tbUsers U,
		tbSecIdLinks SIL
Where		(((R.ExDeficit > 99) And (IsNull(R.LastPrice, 1.00) > 0.25))
		Or ((R.ExDeficit > 99) And (IsNull(R.LastPrice, 1.00) > 0.25))
		Or (R.DeliveryFails > 99)
		Or (Borrows > Loans + 100)
		Or (BorrowRecalls != 0)
		Or (LoanRecalls != 0))
	
	And	R.ActUserId *= U.UserId
	And	R.SecId *= SIL.SecId
	And	SIL.SecIdTypeIndex = 2
Order By	1, 2

Drop Table	#Results

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBoxSummaryGet]  TO [roleMedalist]
GO

--------------------------------------------------------------------------------
--| End PositionBoxSummary modifications.


--| ProcessStatus modifications.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbProcessStatus]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbProcessStatus]
GO

CREATE TABLE [dbo].[tbProcessStatus] (
	[ProcessId] [varchar] (16) NOT NULL ,
	[SystemCode] [char] (1) NOT NULL ,
	[ActCode] [char] (3) NOT NULL ,
	[Act] [varchar] (255) NULL ,
	[ActTime] [datetime] NOT NULL ,
	[ActUserId] [varchar] (50) NOT NULL ,
	[HasError] [bit] NOT NULL ,
	[BookGroup] [varchar] (10) NULL ,
	[ContractId] [varchar] (15) NULL ,
	[ContractType] [char] (1) NULL ,
	[Book] [varchar] (10) NULL ,
	[SecId] [varchar] (12) NULL ,
	[Quantity] [bigint] NULL ,
	[Amount] [money] NULL ,
	[Status] [varchar] (255) NULL ,
	[StatusTime] [datetime] NULL,
	[Tag] [varchar] (255) NULL
) ON [PRIMARY]
GO

CREATE  CLUSTERED  INDEX [IX_tbProcessStatus] ON [dbo].[tbProcessStatus]([ActTime]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbProcessStatus] ADD 
	CONSTRAINT [PK_tbProcessStatus] PRIMARY KEY  NONCLUSTERED 
	(
		[ProcessId],
		[SystemCode],
		[ActCode]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spProcessStatusGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spProcessStatusGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spProcessStatusGet
	@ProcessId varchar(16) = Null,
	@SystemCode char(1) = Null,
	@ActCode char(3) = Null,	
	@BizDate datetime = Null,
	@UtcOffset smallint = 0
As
Begin

Select		PS.ProcessId,
		PS.SystemCode,
		PS.ActCode,
		PS.Act,
		DateAdd(n, @UtcOffset, PS.ActTime) As ActTime,
		IsNull(U.ShortName, PS.ActUserId) As ActUserShortName,
		PS.HasError,
		PS.BookGroup,
		PS.ContractId,
		PS.ContractType,
		PS.Book,
		PS.SecId,
		IsNull(SIL.SecIdLink, '') As Symbol,		
		PS.Quantity,
		PS.Amount,
		PS.Status,
		DateAdd(n, @UtcOffset, PS.StatusTime) As StatusTime,
		PS.Tag
From		tbProcessStatus PS,
		tbUsers U,
		tbSecIdLinks SIL
Where		PS.ProcessId = IsNull(@ProcessId, PS.ProcessId)
	And	PS.SystemCode = IsNull(@SystemCode, PS.SystemCode)
	And	PS.ActCode = IsNull(@ActCode, PS.ActCode)
	And	PS.ActUserId *= U.UserId
	And	PS.SecId *= SIL.SecId
	And	SIL.SecIdTypeIndex = 2
	And	PS.ActTime >= IsNull(@BizDate, PS.ActTime)
	And	PS.ActTime < (IsNull(@BizDate, PS.ActTime) + 1)
Order By	PS.ActTime Desc

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spProcessStatusGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spProcessStatusSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spProcessStatusSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spProcessStatusSet
	@ProcessId varchar(16),
	@SystemCode char(1),
	@ActCode char(3),  
	@Act varchar(255) = Null,   
	@ActUserId varchar(50) = Null,
	@HasError bit = Null,
	@BookGroup varchar(10) = Null,
	@ContractId varchar(15) = Null, 
	@ContractType char(1) = Null,
	@Book varchar(10) = Null,
	@SecId varchar(12) = Null,
	@Quantity bigint = Null,
	@Amount	money = Null,
	@Status varchar(255) = Null,
	@StatusTime datetime = Null,
	@Tag varchar(255) = Null,
	@ReturnData bit = 0,
	@TagConcat bit = 1
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update 	tbProcessStatus
Set 	Act = IsNull(@Act, Act),
	ActUserId = IsNull(@ActUserId, ActUserId),
	HasError = IsNull(@HasError, HasError),
	BookGroup = IsNull(@BookGroup, BookGroup),
	ContractId = IsNull(@ContractId, ContractId),
	ContractType = IsNull(@ContractType, ContractType),
	Book = IsNull(@Book, Book),
	SecId = IsNull(@SecId, SecId),
	Quantity = IsNull(@Quantity, Quantity),
	Amount = IsNull(@Amount, Amount),
	Status = IsNull(@Status, Status),
	StatusTime = GetUtcDate(),
	Tag = Case 	When (@Tag Is Not Null) And (@TagConcat = 1) And (Tag Is Not Null) Then	Tag + '|' + @Tag
			Else IsNull(@Tag, Tag) End
					
Where 	ProcessId = @ProcessId
And	SystemCode = @SystemCode
And	ActCode	= @ActCode

If (@@RowCount = 0)
	Insert Into tbProcessStatus
	Values (
		@ProcessId,
		@SystemCode,
		@ActCode,  
		@Act,   
		GetUtcDate(),
		IsNull(@ActUserId, 'ADMIN'),
		IsNull(@HasError, 0),
		@BookGroup,
		@ContractId, 
		@ContractType ,
		@Book,
		@SecId,
		@Quantity,
		@Amount,
		@Status,
		@StatusTime,
		@Tag		
	       )

If (@ReturnData = 1)
	Exec spProcessStatusGet @ProcessId, @SystemCode, @ActCode

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spProcessStatusSet]  TO [roleLoanet]
GO

--------------------------------------------------------------------------------
--| End ProcessStatus modifications.


--| Deliveries modifications.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbDeliveries]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	drop table [dbo].[tbDeliveries]
GO

CREATE TABLE [dbo].[tbDeliveries] (
	[BizDate] [datetime] NOT NULL ,
	[BookGroup] [varchar] (10) NOT NULL ,
	[Book] [varchar] (10) NOT NULL ,
	[PendMadeFlag] [char] (1) NOT NULL ,
	[ReasonCode] [varchar] (3) NOT NULL ,
	[Timestamp] [char] (6) NOT NULL ,
	[SecId] [char] (9) NOT NULL ,
	[Quantity] [bigint] NOT NULL ,
	[Amount] [bigint] NOT NULL ,
	[Sequence] [bigint] NOT NULL ,
	[SequenceRelated] [bigint] NOT NULL ,
	[Status] [char] (1) NOT NULL 
) ON [PRIMARY]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spDeliveriesSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spDeliveriesSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spDeliveriesSet
	@BookGroup varchar(10),
	@Book varchar(10),
	@PendMadeFlag char(1),
	@MatchFlag char(1),
	@ReasonCode varchar(3),
	@Timestamp varchar(6),
	@SecId char(9),
	@Quantity bigint,
	@Amount money,
	@Sequence int,
	@SequenceRelated int
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Declare @BizDate datetime
Select  @BizDate = (Select KeyValue From tbKeyValues Where KeyId = 'ContractsBizDate')

Update 	tbDeliveries
Set	PendMadeFlag = @PendMadeFlag,
	Status 	     = @MatchFlag,
	[Sequence]   = @Sequence
Where	SequenceRelated = @Sequence
And	BizDate = @BizDate
	
If (@@RowCount = 0)
Insert Into tbDeliveries
	Values
	(
	@BizDate,
	@BookGroup,
	@Book,
	@PendMadeFlag,
	@ReasonCode,
	@Timestamp,
	@SecId,
	@Quantity,
	@Amount,
	@Sequence,
	@SequenceRelated,
	@MatchFlag
	)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spDeliveriesSet]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetRecallDebitUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetRecallDebitUpdate]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetRecallDebitUpdate
	@BookGroup char(4),
	@ContractId char(9),
	@DeliveredQuantity bigint output,
	@RecallId char(16) output,
	@Status varchar(255) output
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Select 	* Into #Recalls
From 	tbRecalls
Where	BookGroup  = @BookGroup
And	ContractId = @ContractId
And	ContractType = 'L'
And 	Status	   <> 'C'
And	Substring(RecallId, 1, 1) <> 'R'
And	IsNull(WillNeed, Quantity) > IsNull(DeliveredToday, 0)
Order   by BaseDueDate Asc

Set RowCount 1

Declare @RecallQuantity bigint
Declare @DeliveredToday bigint

Select 	@RecallQuantity = IsNull(WillNeed, Quantity),
	@RecallId 	= RecallId,
	@DeliveredToday = IsNull(DeliveredToday, 0)
From 	#Recalls

Select @RecallQuantity = @RecallQuantity - @DeliveredToday

If (@RecallId = Null)
Begin
	Rollback Transaction
	Return
End

If (@DeliveredQuantity > @RecallQuantity)
Begin
	exec spRecallSet @RecallId = @RecallId, 
		         @DeliveredToday = @RecallQuantity

	Select @DeliveredQuantity = @DeliveredQuantity - @RecallQuantity

	Select @RecallId
	Select @DeliveredQuantity
	Select @Status = 'Delivery of ' +  Convert(varchar(15), @RecallQuantity) + " units for lender reference:" + @RecallId

End
Else If (@DeliveredQuantity <= @RecallQuantity)
Begin	
	exec spRecallSet @RecallId = @RecallId, 
		         @DeliveredToday = @DeliveredQuantity

	Select @Status = 'Delivery of ' + Convert(varchar(15), @DeliveredQuantity) + " units for lender reference:" + @RecallId	
	Select @DeliveredQuantity = 0

	Select @RecallId
	Select @DeliveredQuantity
End

Drop Table #Recalls

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetRecallDebitUpdate]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetDatagramDebitUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spLoanetDatagramDebitUpdate]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetDatagramDebitUpdate
	@ClientId char(4),
	@Sequence int,
	@PendMadeFlag char(1),
	@MatchFlag char(1),
	@ReasonCode varchar(3),
	@DtcTimestamp char(6),
	@SecId char(9),
	@ContraDtcId varchar(4),
	@Quantity bigint,
	@Amount money,
	@ContractType char(1) = Null,
	@ContractId char(9) = Null,
	@OtherBook varchar(4),
	@PoolCode varchar(1),
	@SequenceRelated int,
	@Act varchar(255) output,
	@ProcessId char(16) = Null
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Declare @BizDate datetime

Select  @BizDate = KeyValue 
From	tbKeyValues
Where 	KeyId = 'ContractsBizDate'

If (((@PendMadeFlag = 'M') Or (@PendMadeFlag = 'C')) And (@MatchFlag = 'M') And (@ContractType = 'B'))
	Exec spContractSet 
		@BizDate = @BizDate,
		@BookGroup = @ClientId,
		@ContractId = @ContractId,
		@ContractType = @ContractType,
		@QuantitySettled = @Quantity,
		@AmountSettled = @Amount,
		@IsIncremental = 1

If (((@PendMadeFlag = 'M') Or (@PendMadeFlag = 'C')) And (@MatchFlag = 'U'))
	Exec spDealSet 
		@DealId = @ProcessId,
		@BookGroup = @ClientId,
		@DealType = 'B',
		@Book = @ContraDtcId,
		@BookContact = 'Unmatched',
		@SecId = @SecId,
		@Quantity = @Quantity,
		@Amount = @Amount

Exec spDeliveriesSet @ClientId, @ContraDtcId, @PendMadeFlag, @MatchFlag, @ReasonCode, @DtcTimestamp, @SecId, @Quantity, @Amount, @Sequence, @SequenceRelated

Commit Transaction

If (@PendMadeFlag = 'P')
	Select @Act = 'Receive pending ' + Convert(varchar(9), @Quantity) + ' [' + Convert(varchar(12), @Amount)
If ((@PendMadeFlag = 'M') Or (@PendMadeFlag = 'C'))
	Select @Act = 'Receive made ' + Convert(varchar(9), @Quantity) + ' [' + Convert(varchar(12), @Amount)
If (@PendMadeFlag = 'K')
	Select @Act = 'Receive pending killed ' + Convert(varchar(9), @Quantity) + ' [' + Convert(varchar(12), @Amount)
If (@PendMadeFlag = 'U')
	Select @Act = 'Receive user entry ' + Convert(varchar(9), @Quantity) + ' [' + Convert(varchar(12), @Amount)

If (@MatchFlag = 'M')
	Select @Act = @Act + '] matched [' + RTrim(@ReasonCode) + ']'
If (@MatchFlag = 'U')
	Select @Act = @Act + '] unmatched [' + RTrim(@ReasonCode) + ']'

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetDatagramDebitUpdate]  TO [roleLoanet]
GO

--------------------------------------------------------------------------------
--| End Deliveries modifications.


--| General modifications.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spDeskGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spDeskGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spDeskGet 
	@Desk varchar(12) = null,
	@IsNotSubscriber bit = 0
AS
Begin

If (@IsNotSubscriber = 0)
	Select 	D.Desk,
		F.Firm
	From 	tbDesks D,
		tbFirms F
	Where	D.FirmCode = F.FirmCode
	And	D.Desk = IsNull(@Desk, D.Desk)
Else
	Select 	D.Desk,
		F.Firm
	From 	tbDesks D,
		tbFirms F
	Where	D.FirmCode = F.FirmCode
	And	D.Desk Not In (Select Desk From tbInventorySubscriber Where IsActive = 1)
	And	D.Desk = IsNull(@Desk, D.Desk)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spDeskGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCountryGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spCountryGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spCountryGet
	@CountryCode char(2) = Null
As
Begin

Select	*
From 	tbCountries
Where	CountryCode = IsNull(@CountryCode, CountryCode)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spCountryGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCountrySet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spCountrySet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spCountrySet 
	@CountryCode varchar(5),
	@Country varchar(50) = Null,
	@IsActive bit = Null
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update		tbCountries
Set		Country = IsNull(@Country, Country),
		IsActive = IsNull(@IsActive, IsActive)			
Where		CountryCode = @CountryCode

If (@@RowCount = 0)
	Insert 	tbCountries
	Values	(
		@CountryCode,
		@Country,
		Null,
		IsNull(@IsActive, 1)
		)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spCountrySet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spCurrencySet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spCurrencySet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spCurrencySet 
	@CurrencyCode varchar(5),
	@Currency varchar(50) = Null,
	@IsActive bit = Null
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update		tbCurrencies
Set		Currency = IsNull(@Currency, Currency),
		IsActive = IsNull(@IsActive, IsActive)	
Where		CurrencyCode = @CurrencyCode

If (@@RowCount = 0)
	Insert 	tbCurrencies
	Values	(
		@CurrencyCode,
		@Currency,
		IsNull(@IsActive, 1)
		)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spCurrencySet]  TO [roleMedalist]
GO


if not exists (select * from syscolumns where name = 'IsActive' And id = (select id from sysobjects where name = 'tbDeskTypes'))
	Alter Table tbDeskTypes Add IsActive bit Not Null DEFAULT (1)
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spDeskTypeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spDeskTypeGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spDeskTypeGet
	@DeskTypeCode varchar(3) = Null
As
Begin

Select	*
From 	tbDeskTypes
Where	DeskTypeCode = IsNull(@DeskTypeCode, DeskTypeCode)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spDeskTypeGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spDeskTypeSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spDeskTypeSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spDeskTypeSet 
	@DeskTypeCode varchar(5),
	@DeskType varchar(50) = Null,
	@IsActive bit = Null
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update		tbDeskTypes
Set		DeskType = IsNull(@DeskType, DeskType),
		IsActive = IsNull(@IsActive, IsActive)			
Where		DeskTypeCode = @DeskTypeCode

If (@@RowCount = 0)
	Insert 	tbDeskTypes
	Values	(
		@DeskTypeCode,
		@DeskType,
		IsNull(@IsActive, 1)
		)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spDeskTypeSet]  TO [roleMedalist]
GO


if not exists (select * from syscolumns where name = 'IsActive' And id = (select id from sysobjects where name = 'tbFirms'))
	Alter Table tbFirms Add IsActive bit Not Null DEFAULT (1)
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spFirmGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spFirmGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spFirmGet
	@FirmCode varchar(5) = Null
As
Begin

Select	*
From 	tbFirms
Where	FirmCode = IsNull(@FirmCode, FirmCode)

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spFirmGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spFirmSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spFirmSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spFirmSet 
	@FirmCode varchar(5),
	@Firm varchar(50) = Null,
	@IsActive bit = Null
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update		tbFirms
Set		Firm = IsNull(@Firm, Firm),
		IsActive = IsNull(@IsActive, IsActive)			
Where		FirmCode = @FirmCode

If (@@RowCount = 0)
	Insert 	tbFirms
	Values	(
		@FirmCode,
		@Firm,
		IsNull(@IsActive, 1)
		)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spFirmSet]  TO [roleMedalist]
GO


if not exists (select * from syscolumns where name = 'IsActive' And id = (select id from sysobjects where name = 'tbHolidays'))
	Alter Table tbHolidays Add IsActive bit Not Null DEFAULT (1)
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spHolidayGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spHolidayGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE Procedure dbo.spHolidayGet
	@AnyDate  datetime = Null,
	@CountryCode char(2) = Null,
	@IsAny bit = Null Output,
	@IsBank bit = Null Output,
	@IsExchange bit = Null Output
As
Begin

If (@AnyDate Is Not Null) And (@CountryCode Is Not Null) -- Get results for a specific date as output parameters.
Begin
	Select		@IsAny = Count(*)
	From		tbHolidays
	Where		HolidayDate = @AnyDate
		And	CountryCode = @CountryCode
		And	IsActive = 1

	Select		@IsBank = Count(*)
	From		tbHolidays
	Where		HolidayDate = @AnyDate
		And	CountryCode = @CountryCode
		And	IsBankHoliday = 1
		And	IsActive = 1

	Select		@IsExchange = Count(*)
	From		tbHolidays
	Where		HolidayDate = @AnyDate
		And	CountryCode = @CountryCode
		And	IsExchangeHoliday = 1
		And	IsActive = 1
End
Else
	Select		*
	From		tbHolidays
	Where		HolidayDate = IsNull(@AnyDate, HolidayDate)
		And	CountryCode = IsNull(@CountryCode, CountryCode)
		And	IsActive = 1

End
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spHolidayGet]  TO [roleCourier]
GO

GRANT  EXECUTE  ON [dbo].[spHolidayGet]  TO [roleMedalist]
GO

GRANT  EXECUTE  ON [dbo].[spHolidayGet]  TO [roleLoanet]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spHolidaySet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
	drop procedure [dbo].[spHolidaySet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004  All rights reserved.

CREATE PROCEDURE dbo.spHolidaySet 
	@Date datetime ,
	@CountryCode varchar(2),
	@IsExchangeHoliday bit = Null,
	@IsBankHoliday bit = Null,
	@IsHoliday bit = Null,
	@IsActive bit = Null
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update		tbHolidays
Set		IsExchangeHoliday = IsNull(@IsExchangeHoliday, IsExchangeHoliday),
		IsBankHoliday = IsNull(@IsBankHoliday, IsBankHoliday),
		IsHoliday = IsNull(@IsHoliday, IsHoliday),
		IsActive = IsNull(@IsActive, IsActive)
Where		HolidayDate = @Date
	And	CountryCode = @CountryCode

If (@@RowCount = 0)
	Insert 	tbHolidays
	Values	(
		@Date,
		@CountryCode,
		IsNull(@IsBankHoliday, 0),
		IsNull(@IsExchangeHoliday, 0),
		IsNull(@IsHoliday, 0),
		IsNull(@IsActive, 1)
		)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spHolidaySet]  TO [roleMedalist]
GO

If Not Exists (Select * from tbFunctions Where FunctionPath = 'InventoryPublisher')
	Insert Into tbFunctions Values ('InventoryPublisher','0','0', Null)
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbInventoryPublisher]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbInventoryPublisher]
GO

CREATE TABLE [dbo].[tbInventoryPublisher] (
	[Desk] [varchar] (12) NOT NULL ,
	[FilePathName] [varchar] (100) NULL ,
	[FileHost] [varchar] (50) NULL ,
	[FileUserName] [varchar] (50) NULL ,
	[FilePassword] [varchar] (25) NULL ,
	[FilePutTime] [datetime] NULL ,
	[FileStatus] [varchar] (100) NULL ,
	[UsePgp] [bit] NULL ,
	[LoadExtensionPgp] [varchar] (50) NULL ,
	[MailAddress] [varchar] (50) NULL ,
	[MailSubject] [varchar] (100) NULL ,
	[MailSendTime] [datetime] NULL ,
	[MailStatus] [varchar] (100) NULL ,
	[BizDate] [datetime] NULL ,
	[Comment] [char] (50) NULL ,
	[ActUserId] [varchar] (50) NOT NULL ,
	[ActTime] [datetime] NOT NULL ,
	[IsActive] [bit] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbInventoryPublisher] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbInventoryPublisherList] PRIMARY KEY  CLUSTERED 
	(
		[Desk]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tbInventoryPublisher] ADD 
	CONSTRAINT [FK_tbInventoryPublisherList_tbDesks] FOREIGN KEY 
	(
		[Desk]
	) REFERENCES [dbo].[tbDesks] (
		[Desk]
	) ON UPDATE CASCADE 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInventoryPublisherGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spInventoryPublisherGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spInventoryPublisherGet
	        @Desk varchar(12) = Null,
        	@UtcOffset smallint = 0
As
Begin

Select  	S.Desk,
        	S.FilePathName,
        	S.FileHost,
        	S.FileUserName,
        	S.FilePassword,
        	DateAdd(n, @UtcOffset, S.FilePutTime) As FilePutTime,
        	S.FileStatus,
        	IsNull(S.UsePgp, 0) As UsePgp,
        	IsNull(S.LoadExtensionPgp, '') As LoadExtensionPgp,
        	S.MailAddress,
        	S.MailSubject,
        	DateAdd(n, @UtcOffset, S.MailSendTime) As MailSendTime,
        	S.MailStatus,
        	S.BizDate,
        	S.Comment,
        	U.ShortName As ActUserShortName,
        	DateAdd(n, @UtcOffset, S.ActTime) As ActTime
From    	tbInventoryPublisher S,
		tbUsers U
Where   	S.Desk = IsNull(@Desk, S.Desk)
        And	S.IsActive = 1
	And	S.ActUserId = U.UserId
Order By	S.Desk
    
End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spInventoryPublisherGet]  TO [roleCourier]
GO

GRANT  EXECUTE  ON [dbo].[spInventoryPublisherGet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInventoryPublisherSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spInventoryPublisherSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spInventoryPublisherSet
	@Desk varchar(12),
	@FilePathName varchar (100) =  Null,
	@FileHost varchar(50) =  Null,
	@FileUserName varchar(50) =  Null,
	@FilePassword varchar(25) =  Null,
	@FilePutTime datetime =  Null,
	@FileStatus varchar(100) =  Null,
	@UsePgp bit =  Null,
	@LoadExtensionPgp varchar(50) = Null,
	@MailAddress varchar(50) = Null,
	@MailSubject varchar(100) = Null,
	@MailSendTime datetime =  Null,
	@MailStatus varchar(100) =  Null,
	@BizDate datetime =  Null,
	@Comment varchar(50) = Null,
	@ActUserId varchar(25) = Null,
	@IsActive bit = 1
As
Begin

Declare @ActTime datetime

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

If ((@FileStatus Is Null) And (@MailStatus Is Null)) --> This is a user update, not a service update.
	Select @ActTime = GetUTCDate()

Update	tbInventoryPublisher
Set	FilePathName = IsNull(@FilePathName, FilePathName),
	FileHost = IsNull(@FileHost, FileHost),
	FileUserName = IsNull(@FileUserName, FileUserName),
	FilePassword = IsNull(@FilePassword, FilePassword),
	FilePutTime = IsNull(@FilePutTime, FilePutTime),
	FileStatus = IsNull(@FileStatus, FileStatus),
	UsePgp = IsNull(@UsePgp, UsePgp),
	LoadExtensionPgp = IsNull(@LoadExtensionPgp, LoadExtensionPgp),
	MailAddress = IsNull(@MailAddress, MailAddress),
	MailSubject = IsNull(@MailSubject, MailSubject),
	MailSendTime = IsNull(@MailSendTime, MailSendTime),
	MailStatus = IsNull(@MailStatus, MailStatus),
	BizDate = IsNull(@BizDate, BizDate),
	Comment = IsNull(@Comment, Comment),
	ActUserId = IsNull(@ActUserId, ActUserId),
	ActTime = IsNull(@ActTime, ActTime),
	IsActive = IsNull(@IsActive, IsActive)
Where	Desk = @Desk

If (@@RowCount = 0)
	Insert  tbInventoryPublisher
	Values  (
		@Desk,
		@FilePathName,
		@FileHost,
		@FileUserName,
		@FilePassword,
		@FilePutTime,
		@FileStatus,
		@UsePgp,
		@LoadExtensionPgp,
		@MailAddress,
		@MailSubject,
		@MailSendTime,
		@MailStatus,
		@BizDate,
		@Comment,
		@ActUserId,
		GetUTCDate(),
		@IsActive
		)
    
Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spInventoryPublisherSet]  TO [roleCourier]
GO

GRANT  EXECUTE  ON [dbo].[spInventoryPublisherSet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInventoryItemSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spInventoryItemSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2003, 2004, 2005  All rights reserved.

CREATE Procedure dbo.spInventoryItemSet
	@BizDate datetime = Null,
	@Desk varchar(12),
	@Account varchar(15) = '',
	@SecId varchar(12),
	@ModeCode char(1) = 'F',
	@Quantity bigint = 0,
	@IncrementCurrentQuantity bit = 0
As
Begin

If (@BizDate Is Null)
	Exec spKeyValueGet @KeyId = "BizDate", @KeyValue = @BizDate Output

If (Len(@SecId) != 9) -- Resolve to potentially stronger sec id.
	Exec spSecIdSymbolLookup @SecId, @SecId = @SecId Output

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update	tbInventory
Set	ModeCode = @ModeCode,
	Quantity = Case When (@IncrementCurrentQuantity = 0) Then @Quantity
			Else Quantity + @Quantity End
Where	BizDate = @BizDate
And	Desk = @Desk
And	SecId = @SecId
And	Account = @Account

If (@@RowCount = 0)
	Insert 	tbInventory
	Values	(
		@BizDate,
		@Desk,
		@SecId,
		@Account,
		@ModeCode,
		@Quantity
		)

Update	tbInventoryControl
Set	BizDate = @BizDate,
	ScribeTime = GetUtcDate()
Where	SecId = @SecId
And	Desk = @Desk
And	Account = @Account

If (@@RowCount = 0)
	Insert 	tbInventoryControl
	Values	(
		@SecId,
		@Desk,
		@Account,
		@BizDate,
		GetUtcDate()
		)

Commit Transaction

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spInventoryItemSet]  TO [roleCourier]
GO

GRANT  EXECUTE  ON [dbo].[spInventoryItemSet]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBorrowHardChanges]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBorrowHardChanges]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spBorrowHardChanges
	@BizDate datetime = Null,
	@WasRemoved bit = 1,
	@WasAdded bit = 1
As
Begin

If (@BizDate Is Null)
	Select		@BizDate = KeyValue
	From		tbKeyValues
	Where		KeyId = 'BizDatePrior'

If (@WasRemoved = 1)
	Select		BH.SecId, SIL.SecIdLink As Symbol, SM.Description
	From		tbBorrowHard BH,
			tbSecIdLinks SIL,
			tbSecMaster SM
	Where		BH.SecId *= SIL.SecId
		And	SIL.SecIdTypeIndex = 2
		And	BH.SecId *= SM.SecId
		And	Convert(char(10), BH.StartTime, 120) < @BizDate
		And	Convert(char(10), BH.EndTime, 120) = @BizDate
	Order By	1

If (@WasAdded = 1)
	Select		BH.SecId, SIL.SecIdLink As Symbol, SM.Description
	From		tbBorrowHard BH,
			tbSecIdLinks SIL,
			tbSecMaster SM
	Where		BH.SecId *= SIL.SecId
		And	SIL.SecIdTypeIndex = 2
		And	BH.SecId *= SM.SecId
		And	Convert(char(10), BH.StartTime, 120) = @BizDate
		And	((BH.EndTime Is Null) Or (BH.EndTime > @BizDate))
	Order By	1

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBorrowHardChanges]  TO [medalistService]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBorrowNoChanges]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBorrowNoChanges]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spBorrowNoChanges
	@BizDate datetime = Null,
	@WasRemoved bit = 1,
	@WasAdded bit = 1
As
Begin

If (@BizDate Is Null)
	Select		@BizDate = KeyValue
	From		tbKeyValues
	Where		KeyId = 'BizDatePrior'

If (@WasRemoved = 1)
	Select		BN.SecId, SIL.SecIdLink As Symbol, SM.Description
	From		tbBorrowNo BN,
			tbSecIdLinks SIL,
			tbSecMaster SM
	Where		BN.SecId *= SIL.SecId
		And	SIL.SecIdTypeIndex = 2
		And	BN.SecId *= SM.SecId
		And	Convert(char(10), BN.StartTime, 120) < @BizDate
		And	Convert(char(10), BN.EndTime, 120) = @BizDate
	Order By	1

If (@WasAdded = 1)
	Select		BN.SecId, SIL.SecIdLink As Symbol, SM.Description
	From		tbBorrowNo BN,
			tbSecIdLinks SIL,
			tbSecMaster SM
	Where		BN.SecId *= SIL.SecId
		And	SIL.SecIdTypeIndex = 2
		And	BN.SecId *= SM.SecId
		And	Convert(char(10), BN.StartTime, 120) = @BizDate
		And	((BN.EndTime Is Null) Or (BN.EndTime > @BizDate))
	Order By	1

End
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBorrowNoChanges]  TO [roleMedalist]
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spInventoryDeskListGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spInventoryDeskListGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO
--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spInventoryDeskListGet
	@BizDate datetime,
	@Desk varchar(12)
As
Begin

Select		I.SecId,
		IsNull(SIL.SecIdLink, '') As Symbol,
		I.Quantity
From		tbInventory I,
		tbSecIdLinks SIL
Where		I.BizDate = @BizDate
	And	I.Desk = @Desk
	And	I.SecId *= SIL.SecId
	And	SIL.SecIdTypeIndex = 2
Order By	1

End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spInventoryDeskListGet]  TO [roleMedalist]
GO

--------------------------------------------------------------------------------
--| End General modifications.




--| Bank Loan.
--------------------------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbBanks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbBanks]
GO

CREATE TABLE [dbo].[tbBanks] (
	[BookGroup] [char] (10)  NOT NULL ,
	[Account] [char] (4)  NOT NULL ,
	[Name] [varchar] (25)  NULL ,
	[Contact] [varchar] (25)  NOT NULL ,
	[Phone] [varchar] (25)  NULL ,
	[Fax] [varchar] (25)  NULL ,
	[ActUserId] [varchar] (50)  NOT NULL ,
	[ActTime] [datetime] NOT NULL ,
	[IsActive] [bit] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbBanks] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbBanks] PRIMARY KEY  CLUSTERED 
	(
		[BookGroup],
		[Account]
	)  ON [PRIMARY] 
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbBankLoans]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbBankLoans]
GO

CREATE TABLE [dbo].[tbBankLoans] (
	[BookGroup] [varchar] (10)  NOT NULL ,
	[Account] [char] (4)  NOT NULL ,
	[LoanDate] [datetime] NOT NULL ,
	[LoanType] [char] (1)  NOT NULL ,
	[ActivityType] [varchar] (3)  NOT NULL ,
	[HairCut] [decimal](5, 0) NOT NULL ,
	[SpMin] [varchar] (5)  NULL ,
	[MoodyMin] [varchar] (5)  NULL ,
	[PriceMin] [float] NOT NULL ,
	[LoanAmountCOB] [decimal](18, 0) NULL ,
	[LoanAmount] [decimal](18, 0) NOT NULL ,
	[PreviousLoanAmount] [decimal](18, 0) NOT NULL ,
	[Comment] [varchar] (50)  NULL ,
	[ActUserId] [varchar] (50)  NOT NULL ,
	[ActTime] [datetime] NOT NULL ,
	[IsActive] [bit] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbBankLoans] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbBankLoans] PRIMARY KEY  CLUSTERED 
	(
		[BookGroup],
		[Account],
		[LoanDate]
	)  ON [PRIMARY] 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbBankLoanTypes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbBankLoanTypes]
GO

CREATE TABLE [dbo].[tbBankLoanTypes] (
	[LoanTypeId] [char] (1)  NOT NULL ,
	[LoanTypeCode] [char] (1)  NOT NULL ,
	[LoanTypeName] [varchar] (10)  NOT NULL 
) ON [PRIMARY]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbBankLoanActivity]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbBankLoanActivity]
GO

CREATE TABLE [dbo].[tbBankLoanActivity] (
	[BookGroup] [varchar] (10)  NOT NULL ,
	[ProcessId] [char] (16)  NOT NULL ,
	[Account] [char] (4)  NOT NULL ,
	[LoanDate] [datetime] NOT NULL ,
	[LoanType] [char] (1)  NULL ,
	[ActivityType] [char] (1)  NOT NULL ,
	[SecId] [char] (9)  NOT NULL ,
	[Quantity] [bigint] NOT NULL ,
	[Status] [char] (2)  NOT NULL ,
	[ActUserId] [varchar] (50)  NOT NULL ,
	[ActTime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbBankLoanActivity] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbBankLoanPledges] PRIMARY KEY  CLUSTERED 
	(
		[ProcessId],
		[BookGroup]
	)  ON [PRIMARY] 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tbBankLoanActivityTypes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tbBankLoanActivityTypes]
GO

CREATE TABLE [dbo].[tbBankLoanActivityTypes] (
	[ActivityTypeCode] [varchar] (1)  NOT NULL ,
	[ActivityTypeName] [varchar] (25)  NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbBankLoanActivityTypes] WITH NOCHECK ADD 
	CONSTRAINT [PK_tbBankLoanPledgeTypes] PRIMARY KEY  CLUSTERED 
	(
		[ActivityTypeCode]
	)  ON [PRIMARY] 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBankLoanActivityGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBankLoanActivityGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO




--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBankLoanActivityGet 
	@BookGroup varchar(10) = Null,
	@ProcessId varchar(16) = Null,
	@Account char(4) = Null,
	@LoanDate datetime = Null,
	@LoanType char(1) = Null,		
	@UtcOffset SmallInt = 0
AS
Begin

Declare @BizDateBank datetime
Select	@BizDateBank = KeyValue From tbKeyValues Where KeyId = 'BizDateBank'

Select	BA.BookGroup,
	BA.ProcessId,
	BA.Account,
	BA.LoanDate,
	BA.LoanType,
	BA.ActivityType,
	BA.SecId,
	SIL.SecIdLink As Symbol,
	BA.Quantity,
	(BA.Quantity * IsNull(SM.LastPrice, 0)) As Amount,
	BA.Status,
	IsNull(U.ShortName, BA.ActUserId) As ActUserShortName,
	DateAdd(n, @UtcOffset, BA.ActTime) As ActTime
From	tbBankLoanActivity BA,
	tbUsers U,
	tbSecIdLinks SIL,
	tbSecMaster SM
Where   U.UserId  = BA.ActUserId
And	BA.SecId *= SIL.SecId
And	SIL.SecIdTypeIndex = 2
And	BA.BookGroup = IsNull(@BookGroup, BA.BookGroup)
And	BA.ProcessId = IsNull(@ProcessId, BA.ProcessId)
And	BA.LoanDate = IsNull(@LoanDate, BA.LoanDate)
And	BA.SecId *= SM.SecId
And	(  (BA.Status In ('PC', 'RC', 'PF', 'RF', 'RC') AND @BizDateBank < BA. ActTime)
	OR
	BA.Status Not in ('PC','RC', 'PF', 'RF'))

Order By 1,2
End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBankLoanActivityGet]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBankLoanActivitySet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBankLoanActivitySet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBankLoanActivitySet 
	@BookGroup varchar(10),
	@ProcessId varchar(16),
	@LoanDate datetime = Null,	
	@Account char(4) = Null,
	@LoanType char(1) = Null,
	@ActivityType char(1) = Null,
	@SecId char(9) = Null,
	@Quantity bigint = Null,
	@Status char(2) = Null,
	@ActUserId varchar(50) = Null,
	@ReturnData bit = 1
AS
Begin

Update tbBankLoanActivity
Set	Status = IsNull(@Status, Status),
	ActTime = GetUtcDate()
Where	BookGroup = @BookGroup
And	ProcessId =  @ProcessId

If (@@RowCount = 0)
	Insert Into tbBankLoanActivity
	Values(
		@BookGroup,
		@ProcessId,
		@Account,
		@LoanDate,
		@LoanType,
		@ActivityType,
		@SecId,
		@Quantity,
		@Status,
		@ActUserId,
		GetUtcDate()
		);


If (@ReturnData = 1)
	exec spBankLoanActivityGet 	@BookGroup, @ProcessId
End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBankLoanActivitySet]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBankLoanActivityTypeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBankLoanActivityTypeGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBankLoanActivityTypeGet 

AS
Begin

Select	ActivityTypeCode,
	ActivityTypeName
From	tbBankLoanActivityTypes

End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBankLoanActivityTypeGet]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBankLoanBizDateRoll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBankLoanBizDateRoll]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO



--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBankLoanBizDateRoll 
	@RecordsUpdated bigint output
AS
Begin

Declare @BizDateBank datetime
Select	@BizDateBank = KeyValue From tbKeyValues Where KeyId = 'BizDateBank'

Select @RecordsUpdated = 0

Update tbBankLoanActivity
Set	Status = 'PC'
Where	Status In ('PR', 'PP')
And	ActTime < @BizDateBank

Select @RecordsUpdated = @@RowCount

Update tbBankLoanActivity
Set	Status = 'RC'
Where	Status = 'RM'
And	ActTime < @BizDateBank

Select @RecordsUpdated = @RecordsUpdated + @@RowCount

Update tbBankLoanActivity
Set	Status = 'PM'
Where	Status In ('RR', 'RP')
And	ActTime < @BizDateBank

Select @RecordsUpdated = @RecordsUpdated + @@RowCount

Update 	tbBankLoans
Set	LoanAmountCOB = LoanAmount,
	PreviousLoanAmount = LoanAmount

End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBankLoanBizDateRoll]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBankLoanPledgeSummaryGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBankLoanPledgeSummaryGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE Procedure dbo.spBankLoanPledgeSummaryGet
	@BizDate  datetime,
	@UtcOffset smallint = 0
As
Begin

Select Distinct	BookGroup, SecId
Into		#SecIdList
From		tbBoxPosition
Where		BizDate = @BizDate

Insert		#SecIdList
Select Distinct	BookGroup, SecId
From		tbContracts
Where		BizDate = @BizDate
	And	Not Exists (Select SecId From #SecIdList)

Select		SIL.BookGroup,
		SIL.SecId,
		SL.SecIdLink As Symbol,
		SM.LastPrice As Price,
		SM.ClassGroup,
		IsNull(SM.Sp, '') As Sp,
		IsNull(SM.Moody, '') As Moody,
		IsNull((Select SUM(Quantity) From tbBankLoanActivity Where BookGroup = SIL.BookGroup And SecId = BP.SecId And LoanType = 'C' And Status In ('PR', 'PP', 'PM')), 0)  As CustomerQuantityPledged,
		IsNull((Select SUM(Quantity) From tbBankLoanActivity Where BookGroup = SIL.BookGroup And SecId = BP.SecId And LoanType = 'F' And Status In ('PR', 'PP', 'PM')), 0)  As FirmQuantityPledged,
		(((IsNull(BP.CustomerLongSettled, 0)  + IsNull(BP.CustomerShortSettled, 0)))  - IsNull((Select SUM(Quantity) From tbBankLoanActivity Where BookGroup = SIL.BookGroup And SecId = BP.SecId And LoanType = 'C' And Status In ('PR', 'PP', 'PM', 'RR', 'RP')),0)) As CustomerQuantityAvailable,
		(((IsNull(BP.FirmLongSettled, 0)  + IsNull(BP.FirmShortSettled, 0))) - IsNull((Select SUM(Quantity) From tbBankLoanActivity Where BookGroup = SIL.BookGroup And SecId = BP.SecId And LoanType = 'F' And Status In ('PR', 'PP', 'PM', 'RR', 'RP')),0))  As FirmQuantityAvailable,
		((((IsNull(BP.CustomerLongSettled, 0)  + IsNull(BP.CustomerShortSettled, 0))) - IsNull((Select SUM(Quantity) From tbBankLoanActivity Where BookGroup = SIL.BookGroup And SecId = BP.SecId And LoanType = 'C' And Status In ('PR', 'PP', 'PM', 'RR', 'RP')),0))* IsNull(SM.LastPrice, 0)) As CustomerAmountAvailable,
		((((IsNull(BP.FirmLongSettled, 0)  + IsNull(BP.FirmShortSettled, 0))) - IsNull((Select SUM(Quantity) From tbBankLoanActivity Where BookGroup = SIL.BookGroup And SecId = BP.SecId And LoanType = 'F' And Status In ('PR', 'PP', 'PM', 'RR', 'RP')),0)) * IsNull(SM.LastPrice, 0)) As FirmAmountAvailable
From		tbBoxPosition BP,
		tbSecMaster SM,
		tbSecIdLinks SL,
		#SecIdList SIL		
Where		BP.BizDate = @BizDate
	And	SIL.BookGroup = BP.BookGroup
	And	BP.SecId = SIL.SecId
	And	SIL.SecId *= SL.SecId
	And	SIL.SecId *= SM.SecId
	And	SL.SecIdTypeIndex = 2
	And	IsNull(SM.LastPrice, 0) > 0

Drop Table #SecIdList
End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBankLoanPledgeSummaryGet]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBankLoanReleaseSummaryGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBankLoanReleaseSummaryGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBankLoanReleaseSummaryGet 	
	@UtcOffset smallint
AS
Begin

Select	BA.BookGroup,
	BA.ProcessId,
	BA.Account,
	BA.LoanDate,
	BA.LoanType,
	BA.ActivityType,
	BA.SecId,
	SIL.SecIdLink As Symbol,
	BA.Quantity,
	(BA.Quantity * IsNull(SM.LastPrice, 0)) As Amount,
	BA.Status,
	IsNull(U.ShortName, BA.ActUserId) As ActUserShortName,
	DateAdd(n, @UtcOffset, BA.ActTime) As ActTime
From	tbBankLoanActivity BA,
	tbUsers U,
	tbSecIdLinks SIL,
	tbBanks B,
	tbSecMaster SM
Where   U.UserId = B.ActUserId
And	B.Account = BA.Account
And	B.IsActive = 1
And	BA.SecId *= SIL.SecId
And	SIL.SecIdTypeIndex = 2
And	BA.SecId *= SM.SecId
And	BA.Status  = 'PM'
Order By 1,2
End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBankLoanReleaseSummaryGet]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBankLoanSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBankLoanSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE  dbo.spBankLoanSet 
	@BookGroup varchar(10),
	@Account char(4),
	@LoanDate datetime,
	@LoanType char(1),
	@PledgeType varchar(3),
	@HairCut decimal,
	@SpMin varchar(5),
	@MoodyMin varchar(5),
	@PriceMin float,
	@LoanAmount decimal(18, 0),
	@Comment varchar(50),
	@ActUserId varchar(50),
	@IsActive bit = 1
AS
Begin

Update	tbBankLoans
Set	LoanType = IsNull(@LoanType, LoanType),
	HairCut = IsNull(@HairCut, HairCut),
	SpMin = IsNull(@SpMin, SpMin),
	MoodyMin = IsNull(@MoodyMin, MoodyMin),
	PriceMin = IsNull(@PriceMin, PriceMin),	
	PreviousLoanAmount = Case When @LoanAmount Is Not Null Then LoanAmount Else PreviousLoanAmount End,
	LoanAmount = IsNull(@LoanAmount, LoanAmount),
	Comment = IsNull(@Comment, Comment),
	ActUserId = IsNull(@ActUserId, ActUserId),
	ActTime = GetUtcDate(),
	IsActive = IsNull(@IsActive, IsActive)
Where	BookGroup = @BookGroup
And	Account = @Account
And	LoanDate = @LoanDate


If (@@RowCount = 0)
	Insert Into tbBankLoans
	Values (
		@BookGroup,
		@Account,
		@LoanDate,
		@LoanType,
		@PledgeType,
		@HairCut,
		@SpMin,
		@MoodyMin,
		@PriceMin,
		0,
		@LoanAmount,
		0,
		@Comment,
		@ActUserId,
		GetUtcDate(),
		@IsActive	
		);

End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBankLoanSet]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBankLoansGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBankLoansGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBankLoansGet 
	@UtcOffset Smallint = 0
AS
Begin

Select	BL.BookGroup,
	BL.Account,
	BL.LoanDate,
	BL.LoanType,
	BL.ActivityType,
	BL.HairCut,
	BL.SpMin,
	BL.MoodyMin,
	BL.PriceMin,
	BL.LoanAmountCOB,
	BL.LoanAmount,
	BL.PreviousLoanAmount,
	'0' As PledgeAmountRequested,
	'0' As PledgedAmount,
	BL.Comment,
	U.ShortName As ActUserShortName,
	DateAdd(n, @UtcOffset, BL.ActTime) As ActTime
From	tbBankLoans BL,
	tbUsers U,
	tbBanks B
Where	 U.UserId = BL.ActUserId
And	B.BookGroup = BL.BookGroup
And	B.Account = BL.Account
And	B.IsActive = 1
And	BL.IsActive = B.IsActive

Order by 1,2,4

End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBankLoanTypeGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBankLoanTypeGet]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBankLoanTypeGet 

AS
Begin

Select	LoanTypeCode,
	LoanTypeName
From	tbBankLoanTypes

End



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBankLoanTypeGet]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBankSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBankSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBankSet 
	@BookGroup varchar(10),
	@Account char(4),
	@Name	 varchar(25),
	@Contact varchar(25),
	@Phone varchar(14) = Null,
	@Fax	varchar(14) = Null,
	@ActUserId varchar(25),
	@IsActive bit = 0
AS
Begin

If (@IsActive = 0)
Begin
	Select	*
	From 	tbBankLoans
	Where	BookGroup = @BookGroup
	And	Account = @Account
	And	IsActive = 1
	
	If (@@RowCount > 0)
	Begin  
		RAISERROR('Cannot remove account, has active loans', 11, 100)
		Return
	End
End


Update tbBanks
Set	[Name] 	= IsNull(@Name, [Name]),
	Contact = IsNull(@Contact, Contact),
	Phone 	= IsNull(@Phone, Phone),
	Fax	= IsNull(@Fax, Fax),
	ActTime= GetUtcDate(),
	IsActive = IsNull(@IsActive, IsActive)
Where	BookGroup = @BookGroup
And	Account = @Account

If (@@RowCount = 0)
Insert Into tbBanks
Values 	(
	@BookGroup,
	@Account,
	@Name,
	@Contact,
	@Phone,
	@Fax,
	@ActUserId,
	GetUtcDate(),
	1
	);

End

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBankSet]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spBanksGet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spBanksGet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spBanksGet 
	@UtcOffset SmallInt = 0
AS
Begin

Select	B.BookGroup,
	B.Account,
	B.[Name],
	B.Contact,
	B.Phone,
	B.Fax,
	U.ShortName As ActUserShortName,
	DateAdd(n, @UtcOffset, B.ActTime) As ActTime
From	tbBanks B,
	tbUsers U
Where	U.UserId = B.ActUserId
And	B.IsActive = 1

Order by Account
End


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spBanksGet]  TO [roleMedalist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetBankLoanSet]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLoanetBankLoanSet]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetBankLoanSet
	@BookGroup char(4),
	@ProcessId char(16),
	@Status char(1)
As
Begin

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction


If (@Status = 'F')
Begin
	Update  tbBankLoanActivity
	Set	Status = (Case 	
			When 	SubString(Status, 1 ,1) = 'P' 
				Then 	'PF'
			When 	SubString(Status, 1 ,1) = 'R' 
				Then 	'PM' End)
	Where	BookGroup = @BookGroup
	And	ProcessId = @ProcessId
End


Commit Transaction

End
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetBankLoanSet]  TO [roleLoanet]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLoanetDatagramBankLoanUpdate]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLoanetDatagramBankLoanUpdate]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



--| Licensed Materials - Property of Anetics, LLC.
--| Copyright (C) Anetics, LLC. 2005  All rights reserved.

CREATE PROCEDURE dbo.spLoanetDatagramBankLoanUpdate 
	@ClientId  char(4),
	@LoanDate datetime,
	@Account char(4),
	@SecId char(9),
	@Quantity bigint,
	@Amount decimal,
	@ActivityType char(1),
	@Status char(2) = 'XX',
	@DtcPendReason char(1) = Null,
	@Comment varchar(56) = Null,
	@ProcessId char(16) output
AS
Begin

Set 	RowCount 1

Set Transaction Isolation Level SERIALIZABLE
Begin Transaction

Update tbBankLoanActivity
Set	Status = @Status,
	@ProcessId = ProcessId
Where	BookGroup = @ClientId
And	LoanDate  = @LoanDate
And	SecId 	  = @SecId
And	Quantity  = @Quantity
And	Status	 != @Status
And 	(
	(Status != 'PM'	 And (@Status Not In ('PP', 'PR',  'PK')))
Or	(Status != 'RM'	 And (@Status Not In ('RP', 'RR',  'RK'))))


If (@@RowCount = 0)
Begin
	Declare @LoanType char(1)

	Select	@LoanType = IsNull(LoanType, ' '),
		@Account  = IsNull(Account, ' ')
	From	tbBankLoans
	Where	BookGroup = @ClientId
	And	LoanDate  = @LoanDate

	Insert tbBankLoanActivity
		Values (@ClientId,
			@ProcessId,
			@Account,
			@LoanDate,
			@LoanType,
			@ActivityType,
			@SecId,
			@Quantity,
			@Status,
			'ADMIN',
			GetUtcDate()
			);
End

Commit Transaction

Set	RowCount 0

End
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[spLoanetDatagramBankLoanUpdate]  TO [roleLoanet]
GO


-- Init Values

If Not Exists (Select FunctionPath From tbFunctions Where FunctionPath = 'PositionBankLoan')
Insert Into tbFunctions
Values ('PositionBankLoan', 0, 0, '[NONE]')

Insert Into tbBankLoanTypes
Values ('1', 'C', 'Customer')

Insert Into tbBankLoanTypes
Values ('2', 'F', 'Firm')

Insert Into tbBankLoanTypes
Values ('3', 'S', 'Special')

Insert Into tbBankLoanTypes
Values ('4', 'U', 'Unsecured')

Insert Into tbBankLoanActivityTypes
Values ('F', 'Free')

Insert Into tbBankLoanActivityTypes
Values ('V', 'Valued')

--------------------------------------------------------------------------------
--| End Bank Loan.








