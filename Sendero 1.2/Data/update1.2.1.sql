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

-- Re-populate the new table.
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
				Set	Rate = @Rate
				From	tbSecMaster SM,
					tbContracts C
				Where	C.BizDate = @BizDate
				And	C.BookGroup = @ClientId
				And	C.Book = @ContraClientId 			
				And	C.ContractType = 'L'
				And	C.RateCode = 'T'
				And	C.SecId = SM.SecId
				And	SM.BaseType <> 'B'

				Select @Act = 'Box rate for stock loan set to ' + Convert(varchar(15), @Rate)
			End
			Else
			Begin
				Update 	tbBooks
				Set 	RateStockBorrow = @Rate
				Where 	BookGroup = @ClientId
				And	Book	  = @ContraClientId
			
				Update	tbContracts 
				Set	Rate = @Rate
				From	tbSecMaster SM,
					tbContracts C
				Where	C.BizDate  = @BizDate
				And	C.BookGroup = @ClientId
				And	C.Book = @ContraClientId 			
				And	C.ContractType = 'B'
				And	C.RateCode = 'T'
				And	C.SecId = SM.SecId
				And	SM.BaseType <> 'B'

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
				Set	Rate = @Rate
				From	tbSecMaster SM,
					tbContracts C
				Where	C.BizDate  = @BizDate
				And	C.BookGroup = @ClientId
				And	C.Book = @ContraClientId 			
				And	C.ContractType = 'L'
				And	C.RateCode = 'T'
				And	C.SecId = SM.SecId
				And	SM.BaseType = 'B'

				Select @Act = 'Box rate for bond loan set to ' + Convert(varchar(15), @Rate)
			End
			Else
			Begin
				Update 	tbBooks
				Set 	RateBondBorrow = @Rate
				Where 	BookGroup = @ClientId
				And	Book	  = @ContraClientId

				Update	tbContracts 
				Set	Rate = @Rate
				From	tbSecMaster SM,
					tbContracts C
				Where	C.BizDate = @BizDate
				And	C.BookGroup = @ClientId
				And	C.Book = @ContraClientId 			
				And	C.ContractType = 'B'
				And	C.RateCode = 'T'
				And	C.SecId = SM.SecId
				And	SM.BaseType = 'B'
				
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

GRANT  EXECUTE  ON [dbo].[spLoanetDatagramTransactionUpdate]  TO [roleLoanet]



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

