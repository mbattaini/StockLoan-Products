USE [Locates]
GO
/****** Object:  StoredProcedure [dbo].[spInventoryGet]    Script Date: 04/08/2009 13:29:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

----------------------------------------------------------------------------------------------------------
-- PROCEDURE NAME:  dbo.spInventoryGet
-- REFERENCED BY: 
-- DESCRIPTION:     Creates the easy borrow list
----------------------------------------------------------------------------------------------------------
-- Change     	History:
-- Date         Modified / Label    Brief Description of Changes
----------------------------------------------------------------------------------------------------------
-- 01.02.2006	MBattaini  M000     Initial Creation
-- 07.13.2006	MBattaini  M001	    Addition Of decrementing inventory when a group code is passed
-- 02.12.2007	MBattaini  M002     Changed group by statement to reference column number.	
-- 05.20.2008   DChen      M003     Convert from Sendero db to Locates db.  Make it SQL:1999 compliant.
----------------------------------------------------------------------------------------------------------

CREATE Procedure [dbo].[spInventoryGet]
	    @GroupCode varchar(12) = Null,
	    @SecId varchar(12),
	    @UtcOffset smallint = 0,
	    @AgeDayCount smallint = 10
As
Begin

    Set NOCOUNT On 

    Declare @BizDate datetime
    Exec    dbo.spKeyValueGet @KeyId = 'BizDate', @KeyValue = @BizDate Output

    Exec    dbo.spSecIdSymbolLookup @SecId, @SecId Output
    	
    Create  Table dbo.#Supply(
            SecId varchar(12),
            ScribeTime datetime,
            BizDate datetime,
            Desk varchar(12),
            Account varchar(12),
            ModeCode varchar(1),
            Quantity bigint )

    Insert Into dbo.#Supply(
		    SecId,
		    ScribeTime,
		    BizDate,
		    Desk,
		    Account,
		    ModeCode,
		    Quantity)
    Select	IC.SecId,
		    DateAdd(n, @UtcOffset, IC.ScribeTime) As ScribeTime,
		    I.BizDate,
		    I.Desk,
		    I.Account,
		    I.ModeCode,
		    I.Quantity
    From	dbo.tbInventory I  With (nolock) 
            Inner Join dbo.tbInventoryControl IC  With (nolock)
	            On  I.BizDate = IC.BizDate
	            And	I.Desk = IC.Desk
	            And	I.SecId = IC.SecId
	            And	I.Account = IC.Account
    Where	IC.SecId = @SecId
	  And	IC.ScribeTime > DateAdd(d, - @AgeDayCount, GetUtcDate())
    Order By ScribeTime  Desc
 
 
    Insert Into dbo.#Supply(
		    SecId,
		    ScribeTime,
		    BizDate,
		    Desk,
		    Account,
		    ModeCode,
		    Quantity )
    Select	SecId,
		    BizDate,
		    BizDate,
		    BookGroup,
		    BookGroup,
		    'B' As ModeCode,
		    (Case When (OtherFailOutSettled > (ExDeficitSettled + CustomerPledgeSettled + FirmPledgeSettled - DvpFailOutSettled - BrokerFailOutSettled - ClearingFailOutSettled)And ((ExDeficitSettled + CustomerPledgeSettled + FirmPledgeSettled - DvpFailOutSettled - BrokerFailOutSettled - ClearingFailOutSettled) > 0)) 
                  Then 0
		          When ((ExDeficitSettled + CustomerPledgeSettled + FirmPledgeSettled) > 0)
                  Then (ExDeficitSettled + CustomerPledgeSettled + FirmPledgeSettled - DvpFailOutSettled - BrokerFailOutSettled - ClearingFailOutSettled - OtherFailOutSettled)
		          When ((ExDeficitSettled + CustomerPledgeSettled + FirmPledgeSettled) <= 0)
                  Then (ExDeficitSettled + CustomerPledgeSettled + FirmPledgeSettled - DvpFailOutSettled - BrokerFailOutSettled - ClearingFailOutSettled) 
            End) As Quantity 
    From	dbo.tbBoxPosition B  With (nolock)
    Where	BizDate = @BizDate
      And	SecId = @SecId
 
 
    If (@GroupCode Is Null)
    Begin
	    Select	SecId,
			    ScribeTime,
			    BizDate,
			    Desk,
			    Account,
			    ModeCode,
			    Quantity
	    From	dbo.#Supply
	    Order	By BizDate Desc

	    Return;	
    End


    Create	Table dbo.#LocateSupply(
            GroupCode varchar(12),
            Desk varchar(12) ,
            Account varchar(12),
            SecId varchar(12),
            Quantity bigint )

    Insert Into dbo.#LocateSupply(
		    GroupCode,
		    Desk,
		    Account,
		    SecId,
		    Quantity)	
    Select	GroupCode,
		    Desk,
		    Account,
		    SecId,
		    SUM(Quantity)
    From	dbo.tbShortSaleDailyQuantities With (nolock)
    Where	GroupCode = @GroupCode
    Group By GroupCode, Desk, Account, SecId
 
 
    Declare	@BoxPositionHaircut float
    Exec    dbo.spKeyValueGet @KeyId = 'ShortSaleLocateBoxPositionHaircut', @KeyValue = @BoxPositionHaircut Output

    Update	dbo.#Supply 
    Set	    Quantity = Quantity - (Quantity * (IsNull(@BoxPositionHaircut, 0)/100))
    Where	Desk = '0234'
    And 	Quantity > 0

    Update 	dbo.#Supply
    Set	    Quantity = S.Quantity - LS.Quantity
    From	dbo.#Supply S 
	        Inner Join dbo.#LocateSupply LS  With (nolock)
                On  S.Desk  = LS.Desk
                And S.Account = LS.Account
                And S.SecId = LS.SecId

    Select	SecId,
	        ScribeTime,
	        BizDate,
	        Desk,
	        Account,
	        ModeCode,
	        Quantity
    From	dbo.#Supply With (nolock)
    Order	By BizDate Desc


    -- Clean Up -------------------
    Drop	Table dbo.#Supply
    Drop	Table dbo.#LocateSupply

    Set NOCOUNT Off 

End
