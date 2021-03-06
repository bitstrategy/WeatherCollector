IF (OBJECT_ID(N'[dbo].[usp_UpdateGetdateWeather]', N'P') IS NOT NULL)
BEGIN
	DROP PROC [dbo].[usp_UpdateGetdateWeather]
END
GO

CREATE PROCEDURE [dbo].[usp_UpdateGetdateWeather]
AS
BEGIN TRAN
	DECLARE @err INT
	SET @err = 0

	UPDATE [dbo].[DER_WeatherData_Curr]
	SET [DER_WeatherData_Curr].[MinTemp] = [a2].[MinTemp], [DER_WeatherData_Curr].[MaxTemp] = [a2].[MaxTemp], [DER_WeatherData_Curr].[Weather] = [a2].[Weather]
	FROM [dbo].[DER_WeatherData_Curr] AS a1
	INNER JOIN [dbo].[DER_WeatherData_Getdate] AS a2
			ON ([a2].[DateKey] = [a1].[DateKey]
				AND [a2].[CityName] = [a1].[CityName])
	WHERE [a1].[DateKey] >= CONVERT(INT, CONVERT(NVARCHAR(8), GETDATE(), 112))

	SET @err = @err + @@ERROR

	INSERT INTO [dbo].[DER_WeatherData_Curr]
			(
			 [DateKey]
			,[CityName]
			,[MinTemp]
			,[MaxTemp]
			,[Weather]
			)
	SELECT
		[a1].[DateKey]
		,[a1].[CityName]
		,[a1].[MinTemp]
		,[a1].[MaxTemp]
		,[a1].[Weather]
	FROM [dbo].[DER_WeatherData_Getdate] AS a1
	LEFT JOIN [dbo].[DER_WeatherData_Curr] AS a2
			ON ([a2].[CityName] = [a1].[CityName]
				AND [a2].[DateKey] = [a1].[DateKey])
	WHERE [a2].[DateKey] IS NULL
	ORDER BY [a1].[CityName], [a1].[DateKey]
	SET @err = @err + @@ERROR

IF @err > 0 ROLLBACK TRAN
ELSE COMMIT TRAN
GO
