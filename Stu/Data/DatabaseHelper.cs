using Microsoft.Data.SqlClient;
using System.IO;

public static class DatabaseHelper
{
    private static readonly string _connectionString;

    static DatabaseHelper()
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "School.mdf");
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath));

        _connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;
                            AttachDbFilename={dbPath};
                            Integrated Security=True;
                            Connect Timeout=30;
                            Pooling=True";
    }

    public static SqlConnection GetConnection() => new SqlConnection(_connectionString);
}