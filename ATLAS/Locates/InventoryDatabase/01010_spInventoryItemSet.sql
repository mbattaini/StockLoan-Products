USE [Locates]
GO
/****** Object:  StoredProcedure [dbo].[spInventoryItemSet]    Script Date: 04/08/2009 08:56:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------------------------------------------------------------------------
-- PROCEDURE NAME: dbo.spInventoryItemSet
-- REFERENCED BY: 
-- DESCRIPTION: Sets Iventory quantites and rates.
----------------------------------------------------------------------------------------------------------
-- Change     	History:
-- Date       	Modified By      Brief Description of Changes
----------------------------------------------------------------------------------------------------------
-- 10/25/2004 	RSammons        Initial Creation
-- 06/23/2006	MBattaini	Added rate functionality
----------------------------------------------------------------------------------------------------------

CREATE Procedure [dbo].[spInventoryItemSet]
	@BizDate datetime = Null,
	@Desk varchar(12) = Null,
	@Account varchar(15) = '',
	@SecId varchar(12),
	@ModeCode char(1) = 'F',
	@Quantity bigint = 0,
	@ExecutionID bigint = 0,
	@IncrementCurrentQuantity bit = 0
As
Begin

If (@BizDate Is Null)
	Exec dbo.spKeyValueGet @KeyId = "BizDate", @KeyValue = @BizDate Output

If (Len(@SecId) != 9) -- Resolve to potentially stronger sec id.
	Exec dbo.spSecIdSymbolLookup @SecId, @SecId = @SecId Output

Update	dbo.tbInventory
Set	ModeCode = @ModeCode,
	Quantity = Case When (@IncrementCurrentQuantity = 0) Then @Quantity
			Else Quantity + @Quantity End
Where	BizDate = @BizDate
And	Desk = @Desk
And	SecId = @SecId
And	Account = @Account

If (@@RowCount = 0)
	Insert 	dbo.tbInventory
	Values	(
				@BizDate,
				@Desk,
				@SecId,
				@Account,
				@ModeCode,
				@Quantity,
				@ExecutionID
			)


Update	dbo.tbInventoryControl
Set	BizDate = @BizDate,
	ScribeTime = GetUtcDate()
Where	SecId = @SecId
And	Desk = @Desk
And	Account = @Account

If (@@RowCount = 0)
Insert 	dbo.tbInventoryControl
	Values	(
				@SecId,
				@Desk,
				@Account,
				@BizDate,
				GetUtcDate()
			)


End
