IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Cars]')
AND OBJECTPROPERTY(id, N'IsUserTable') = 1)IF NOT EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[Cars]')
AND OBJECTPROPERTY(id, N'IsUserTable') = 1)

CREATE TABLE [dbo].[Cars]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Model] NVARCHAR(50) NOT NULL, 
    [Manufacturer] NVARCHAR(50) NOT NULL, 
    [YearOfManufacture] INT NOT NULL, 
    [RegistrationNumber] NVARCHAR(15) NOT NULL, 
    [VinCode] VARCHAR(17) NOT NULL, 
    [Mileage] INT NULL, 
    [Colour] NVARCHAR(25) NULL
)