using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace JS.Utilities
{
    public static class ExecuteSP
    {
        public static IEnumerable<T> ExecuteStoredProcedure<T>(string storedProcedure, string connectionString, SqlParameter[] parameters, Func<SqlDataReader, T> body)
        {
            List<T> results = new List<T>();

            SqlConnection connection = new SqlConnection(connectionString);
            {
                connection.Open();

                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(body(reader));
                }
                reader.Close();
            }
            return results;
        }
    }
}
