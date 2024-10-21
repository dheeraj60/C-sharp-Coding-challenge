using System;
using System.Data.SqlClient;

namespace ecommercemainapplication.util
{
    public static class DBConnection
    {
        private static SqlConnection? connection;

        public static SqlConnection GetConnection()
        {
            if (connection == null)
            {
                // Get the connection string directly from PropertyUtil
                string connectionString = PropertyUtil.GetPropertyString("DefaultConnection");

                try
                {
                    connection = new SqlConnection(connectionString);
                    connection.Open(); 
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while connecting to database: " + ex.Message);
                    throw; 
                }
            }
            return connection;
        }
    }
}
