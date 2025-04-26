using Microsoft.Data.SqlClient;

namespace Stu.Data
{
    public static class DatabaseHelper
    {
        private static readonly string _connectionString =
            "Data Source=.;Initial Catalog=School;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
