SET NOCOUNT ON

MERGE INTO [dbo].[Customer] AS Target
USING (VALUES
('9f35b48d-cb87-4783-bfdb-21e36012930a', 'Wolfgang','Ofner', '1989-11-23', 31),
('654b7573-9501-436a-ad36-94c5696ac28f', 'Darth','Vader', '1977-05-25', 43),
('971316e1-4966-4426-b1ea-a36c9dde1066', 'Son','Goku', '1937-04-16', 84)
) AS Source (Id, FirstName, LastName, Birthday, Age)
ON (Target.Id = Source.Id)
WHEN MATCHED AND (
	NULLIF(Source.Id, Target.Id) IS NOT NULL OR 
	NULLIF(Target.Id, Source.Id) IS NOT NULL OR 
	NULLIF(Source.FirstName, Target.FirstName) IS NOT NULL OR 
	NULLIF(Target.FirstName, Source.FirstName) IS NOT NULL OR 
	NULLIF(Source.LastName, Target.LastName) IS NOT NULL OR 
	NULLIF(Target.LastName, Source.LastName) IS NOT NULL OR
	NULLIF(Source.Birthday, Target.Birthday) IS NOT NULL OR 
	NULLIF(Target.Birthday, Source.Birthday) IS NOT NULL)THEN
 UPDATE SET
  Id = Source.Id,
  FirstName = Source.FirstName,
  LastName = Source.LastName,
  Birthday = Source.Birthday,
  Age = Source.Age
  
WHEN NOT MATCHED BY TARGET THEN
 INSERT(Id, FirstName, LastName, Birthday, Age)
 VALUES(Source.Id, Source.FirstName, Source.LastName, Source.Birthday, Source.Age)
WHEN NOT MATCHED BY SOURCE  THEN
 DELETE;

DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [dbo].[Customer]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[dbo].[Customer] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END