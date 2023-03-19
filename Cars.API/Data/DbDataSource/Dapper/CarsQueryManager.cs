using Cars.API.Models;

namespace Cars.API.Data.DbDataSource.Dapper;

public static class CarsQueryManager
{
    public const string TableName = "[dbo].[Cars]";
    public const string IdColumnName = "[Id]";
    public const string ModelColumnName = "[Model]";
    public const string ManufacturerColumnName = "[Manufacturer]";
    public const string YearOfManufactureColumnName = "[YearOfManufacture]";
    public const string RegistrationNumberColumnName = "[RegistrationNumber]";
    public const string VinCodeColumnName = "[VinCode]";
    public const string MileageColumnName = "[Mileage]";
    public const string ColourColumnName = "[Colour]";


    public static string GetInsertStatement(Car car)
    {
        return "INSERT INTO " +
               $"{TableName} " +
               $"({ModelColumnName}, {ManufacturerColumnName}," +
               $"{YearOfManufactureColumnName},{RegistrationNumberColumnName}," +
               $"{VinCodeColumnName}, {MileageColumnName}," +
               $"{ColourColumnName}) " +
               $"VALUES('{car.Model}', '{car.Manufacturer}'," +
               $" {car.YearOfManufacture}, '{car.RegistrationNumber}'," +
               $" '{car.VinCode}', {car.Mileage}," +
               $" '{car.Colour}');" +
               " SELECT CAST(SCOPE_IDENTITY() as BIGINT)";
    }

    public static string GetSelectByIdStatement(long id)
    {
        return $"SELECT * FROM {TableName} WHERE {IdColumnName} = {id}";
    }

    public static string GetDeleteStatement(long id)
    {
        return $"DELETE FROM {TableName} WHERE {IdColumnName} = {id}";
    }

    public static string GetSelectAllStatement()
    {
        return $"SELECT * FROM {TableName}";
    }

    public static string GetSelectRangeStatement(int skip, int take)
    {
        return $"SELECT * FROM {TableName} " +
               $"ORDER BY {IdColumnName} " +
               $"OFFSET {skip} ROWS " +
               $"FETCH NEXT {take} ROWS ONLY;";
    }

    public static string GetUpdateStatement(Car car)
    { 
        return $"UPDATE {TableName} " +
               $"SET {ModelColumnName} = '{car.Model}'," +
               $"{ManufacturerColumnName} = '{car.Manufacturer}'," +
               $"{YearOfManufactureColumnName} = {car.YearOfManufacture}," +
               $"{RegistrationNumberColumnName} = '{car.RegistrationNumber}'," +
               $"{VinCodeColumnName} = '{car.VinCode}'," +
               $"{MileageColumnName} = {car.Mileage}," +
               $"{ColourColumnName} = '{car.Colour}' " +
               $"WHERE {IdColumnName} = {car.Id}";
    }
}