using FleetManagementSystem.Helpers;
using FleetManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FleetManagementSystem.Repositories
{
    public class VehicleRepo : Repository<Vehicle>
    {
        public VehicleRepo(SqlHelper<Vehicle> sqlHelper)
            : base(sqlHelper)
        {
        }

        public async Task EnsureTableExistsAsync()
        {
            string createTableSql = @"
        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Vehicles')
        BEGIN
            CREATE TABLE Vehicle (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                VIN NVARCHAR(50) NOT NULL,
                Manufacturer NVARCHAR(100) NOT NULL,
                Model NVARCHAR(100) NOT NULL,
                OdometerReading DECIMAL(18, 2) NOT NULL,
                IsActive BIT NOT NULL DEFAULT 1,
                CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
            );
        END";

            await _sqlHelper.ExecuteNonQueryAsync(createTableSql);
        }

        public override async Task CreateAsync(Vehicle vehicle)
        {
            string query =
                @"INSERT INTO Vehicle
                (VIN, Manufacturer, Model, OdometerReading, IsActive, CreatedDate)
                VALUES
                (@VIN, @Manufacturer, @Model, @OdometerReading, @IsActive, @CreatedDate)";

            await _sqlHelper.ExecuteNonQueryAsync(
                query,
                new SqlParameter("@VIN", vehicle.VIN),
                new SqlParameter("@Manufacturer", vehicle.Manufacturer),
                new SqlParameter("@Model", vehicle.Model),
                new SqlParameter("@OdometerReading", vehicle.OdometerReading),
                new SqlParameter("@IsActive", vehicle.IsActive),
                new SqlParameter("@CreatedDate", vehicle.CreatedDate)
            );
        }

        public override async Task UpdateAsync(Vehicle vehicle)
        {
            string query =
                @"UPDATE Vehicle
                  SET VIN=@VIN,
                      Manufacturer=@Manufacturer,
                      Model=@Model,
                      OdometerReading=@OdometerReading,
                      IsActive=@IsActive
                  WHERE Id=@Id";

            await _sqlHelper.ExecuteNonQueryAsync(
                query,
                new SqlParameter("@Id", vehicle.Id),
                new SqlParameter("@VIN", vehicle.VIN),
                new SqlParameter("@Manufacturer", vehicle.Manufacturer),
                new SqlParameter("@Model", vehicle.Model),
                new SqlParameter("@OdometerReading", vehicle.OdometerReading),
                new SqlParameter("@IsActive", vehicle.IsActive)
            );
        }

        public override async Task DeleteAsync(int id)
        {
            string query =
                "DELETE FROM Vehicle WHERE Id=@Id";

            await _sqlHelper.ExecuteNonQueryAsync(
                query,
                new SqlParameter("@Id", id)
            );
        }

        public override async Task<Vehicle> GetByIdAsync(int id)
        {
            string query =
                "SELECT * FROM Vehicle WHERE Id=@Id";

            var result = await _sqlHelper.ExecuteReaderAsync(
                query,
                reader => new Vehicle
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    VIN = reader["VIN"].ToString(),
                    Manufacturer = reader["Manufacturer"].ToString(),
                    Model = reader["Model"].ToString(),
                    OdometerReading = Convert.ToDecimal(reader["OdometerReading"]),
                    IsActive = reader["IsActive"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                },
                new SqlParameter("@Id", id)
            );

            return result.FirstOrDefault();
        }

        public override async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            string query = "SELECT * FROM Vehicle";

            return await _sqlHelper.ExecuteReaderAsync(
                query,
                reader => new Vehicle
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    VIN = reader["VIN"].ToString(),
                    Manufacturer = reader["Manufacturer"].ToString(),
                    Model = reader["Model"].ToString(),
                    OdometerReading = Convert.ToDecimal(reader["OdometerReading"]),
                    IsActive = reader["IsActive"].ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
                }
            );
        }
    }
}