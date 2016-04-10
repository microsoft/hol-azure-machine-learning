CREATE TABLE synth_data 
(
    x int, 
    ywnoise float
);
CREATE CLUSTERED INDEX i1 ON dbo.synth_data(x);

WITH Seq as (
	SELECT TOP (30) x = CONVERT(INT, ROW_NUMBER() OVER (ORDER BY s1.[object_id])) 
	FROM sys.all_objects AS s1 CROSS JOIN sys.all_objects AS s2
)
INSERT INTO synth_data
SELECT x, x + (RAND(convert(varbinary, newid())) * 2) - 1 as ywnoise FROM Seq