using Microsoft.Data.SqlClient;

namespace Stu.Data
{
    public static class DatabaseHelper
    {
        private static readonly string _connectionString =
            "Data Source=.\\SQL2022;Initial Catalog=School;User ID=sa;Password=MDev2025!!;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}