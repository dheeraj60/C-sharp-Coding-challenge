using System;

namespace ecommercemainapplication.util
{
    public static class PropertyUtil
    {
        private static readonly string connectionString = "Server=LAPTOP-E8Q2MF96\\SQLEXPRESS;Database=ecommercedatabase;Trusted_Connection=True;";

        public static string GetPropertyString(string key)
        {
            if (key == "DefaultConnection")
            {
                return connectionString;
            }

            throw new InvalidOperationException($"Connection string '{key}' not found in configuration.");
        }
    }
}
