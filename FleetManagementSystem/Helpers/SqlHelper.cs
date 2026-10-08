using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FleetManagementSystem.Helpers
{
    public class SqlHelper<T> : IDisposable
    {
        private readonly SqlConnection _connection;


        public SqlHelper(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        private async Task OpenConnectionAsync()
        {
            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
        }

        public async Task<int> ExecuteNonQueryAsync(
            string query,
            params SqlParameter[] parameters)
        {
            await OpenConnectionAsync();

            using (SqlCommand cmd =
                new SqlCommand(query, _connection))
            {

                cmd.Parameters.AddRange(parameters);

                return await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<object> ExecuteScalarAsync(
            string query,
            params SqlParameter[] parameters)
        {
            await OpenConnectionAsync();

            using (SqlCommand cmd =
                new SqlCommand(query, _connection))
            {

                cmd.Parameters.AddRange(parameters);

                return await cmd.ExecuteScalarAsync();
            }
        }

        public async Task<List<T>> ExecuteReaderAsync(
    string query,
    Func<SqlDataReader, T> mapper,
    params SqlParameter[] parameters)
        {
            await OpenConnectionAsync();

            List<T> result = new List<T>();

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddRange(parameters);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(mapper(reader));
                    }
                }
            }

            return result;
        }
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
