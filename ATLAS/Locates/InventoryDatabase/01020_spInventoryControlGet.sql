USE [Locates]
GO
/****** Object:  StoredProcedure [dbo].[spInventoryControlGet]    Script Date: 04/08/2009 13:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInventoryControlGet] 
		@BizDate datetime = Null
AS
Begin

Select	Desk,
		BizDate,
		count(SecId) As ItemCount
From	tbInventoryControl (nolock)
Where	BizDate = @BizDate
--And		Desk Not In (Select	Desk From tbInventorySubscriptions)
Group By Desk, BizDate

End
