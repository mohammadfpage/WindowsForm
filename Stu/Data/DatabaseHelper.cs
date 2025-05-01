using Microsoft.Data.SqlClient;
using System;

public static class DatabaseHelper
{
    private static readonly string _connectionString;

    static DatabaseHelper()
    {
        string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "School.mdf");

        _connectionString = $@"Data Source=(LocalDB)\ProjectsV16;
                            AttachDbFilename={dbPath};
                            Integrated Security=True;
                            Connect Timeout=30;";
    }

    public static SqlConnection GetConnection() => new SqlConnection(_connectionString);
}
