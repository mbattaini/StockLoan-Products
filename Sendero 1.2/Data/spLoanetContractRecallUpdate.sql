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


