SET NOCOUNT ON

MERGE INTO [dbo].[Order] AS Target
USING (VALUES
('8356c427-d6cb-41bf-9b6a-88cd6578e234', 1, '9F35B48D-CB87-4783-BFDB-21E36012930A', 'Wolfgang Ofner'),
('bffcf83a-0224-4a7c-a278-5aae00a02c1e', 1, '654B7573-9501-436A-AD36-94C5696AC28F', 'Darth Vader'),
('58e5cd7d-856b-4224-bdff-bd8f85bf5a6d', 2, '971316E1-4966-4426-B1EA-A36C9DDE1066', 'Son Goku')
) AS Source (Id, OrderState, CustomerGuid, CustomerFullName)
ON (Target.Id = Source.Id)
WHEN MATCHED AND (
	NULLIF(Source.Id, Target.Id) IS NOT NULL OR 
	NULLIF(Target.Id, Source.Id) IS NOT NULL OR 
	NULLIF(Source.CustomerGuid, Target.CustomerGuid) IS NOT NULL OR 
	NULLIF(Target.CustomerGuid, Source.CustomerGuid) IS NOT NULL OR 
	NULLIF(Source.CustomerFullName, Target.CustomerFullName) IS NOT NULL OR 
	NULLIF(Target.CustomerFullName, Source.CustomerFullName) IS NOT NULL) THEN
 UPDATE SET
  Id = Source.Id,
  OrderState = Source.OrderState,
  CustomerGuid = Source.CustomerGuid,
  CustomerFullName = Source.CustomerFullName
  
WHEN NOT MATCHED BY TARGET THEN
 INSERT(Id, OrderState, CustomerGuid, CustomerFullName)
 VALUES(Source.Id, Source.OrderState, Source.CustomerGuid, Source.CustomerFullName)
WHEN NOT MATCHED BY SOURCE  THEN
 DELETE;

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [dbo].[Order]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[dbo].[Order] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END