USE [Locates]
GO
/****** Object:  StoredProcedure [dbo].[spInventoryImportExecutionInsert]    Script Date: 04/08/2009 08:32:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Blake Pritchard>
-- Create date: <3-26-2009>
-- Description:	<This should run every time the Inventory Service Executes>
-- =============================================
CREATE PROCEDURE [dbo].[spInventoryImportExecutionInsert] @SubscriberID bigint, @HostName varchar(255)
	-- Add the parameters for the stored procedure here
	--<@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	--<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

declare @ExecutionID as bigint

Insert Into tbInventoryImportExecution  
(ExecutionTime, SubscriberID, ExecutionHost)
Values
(GetUtcDate(), @SubscriberID, @HostName)

select @ExecutionID = SCOPE_IDENTITY()
return @ExecutionID

END

