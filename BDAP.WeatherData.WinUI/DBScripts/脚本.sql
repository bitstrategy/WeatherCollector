USE [WeatherInfoDB]
GO
/****** Object:  Table [dbo].[DER_WeatherData]    Script Date: 2017/12/18 11:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DER_WeatherData](
	[DateKey] [nvarchar](50) NULL,
	[CityName] [nvarchar](50) NULL,
	[MinTemp] [nvarchar](50) NULL,
	[MaxTemp] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DER_WeatherData_Curr]    Script Date: 2017/12/18 11:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DER_WeatherData_Curr](
	[DateKey] [nvarchar](50) NULL,
	[CityName] [nvarchar](50) NULL,
	[MinTemp] [nvarchar](50) NULL,
	[MaxTemp] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HP_WeatherCity]    Script Date: 2017/12/18 11:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HP_WeatherCity](
	[CityName] [nvarchar](255) NULL,
	[CityShortName] [nvarchar](255) NULL,
	[CityShortName_ls] [nvarchar](255) NULL,
	[WeatherUrl] [nvarchar](255) NULL,
	[WeatherUrl_EN] [nvarchar](255) NULL,
	[CityName_EN] [nvarchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[V_BI_HP_GetFFWeatherList]    Script Date: 2017/12/18 11:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[V_BI_HP_GetFFWeatherList]
AS
SELECT
	 [t].[CityName]
	,[t].[CityShortName]
	,[t].[CityName_EN]
	--https://www.tianqi.com/suqian/15/
	,N'https://www.tianqi.com/' + [t].[CityName_EN] + '/15/' AS [Url]
FROM (
SELECT
	[CityName]
   ,[CityShortName]
   ,[CityName_EN]
   ,ROW_NUMBER() OVER(PARTITION BY [a].[CityName], [a].[CityShortName] ORDER BY [a].[CityName_EN]) AS [Num]
FROM [dbo].[HP_WeatherCity] AS a WITH(NOLOCK)
) t
WHERE [t].[Num] = 1


GO
/****** Object:  View [dbo].[V_BI_HP_GetWeatherList]    Script Date: 2017/12/18 11:15:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_BI_HP_GetWeatherList]
AS
SELECT
	 [t].[CityName]
	,[dd].[DateKey]
	,[t].[CityShortName]
	,[t].[CityName_EN]
	,'http://lishi.tianqi.com/' + [t].[CityName_EN] + '/' + [dd].[DateKey] + '.html' AS [Url]
FROM (
SELECT
	[CityName]
   ,[CityShortName]
   ,[CityName_EN]
   ,ROW_NUMBER() OVER(PARTITION BY [a].[CityName], [a].[CityShortName] ORDER BY [a].[CityName_EN]) AS [Num]
FROM [dbo].[HP_WeatherCity] AS a WITH(NOLOCK)
) t
INNER JOIN (
SELECT DISTINCT
	LEFT([a].[DateKey], 6) AS [DateKey]
FROM [dbo].[Dim_Date] AS a WITH(NOLOCK)
WHERE [a].[DateKey] BETWEEN CONVERT(NVARCHAR(8), DATEADD(YEAR, -2, DATEADD(dd, -DAY(GETDATE() - 1), GETDATE() - 1)), 112) AND CONVERT(INT, CONVERT(NVARCHAR(8), DATEADD(dd, -DAY(GETDATE() - 1), GETDATE() - 1), 112))
) AS dd ON (1 = 1)
WHERE [t].[Num] = 1

GO
