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