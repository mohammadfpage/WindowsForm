using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;
using System.IO;

public static class DatabaseHelper
{
    private static readonly string _connectionString;

    static DatabaseHelper()
    {
        // مسیر نصب (فایل اصلی دیتابیس در کنار exe)
        string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "School.mdf");

        // مسیر مقصد امن برای دیتابیس (داخل LocalAppData)
        string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string appFolder = Path.Combine(localAppData, "SchoolApp", "Database");
        string targetPath = Path.Combine(appFolder, "School.mdf");

        // اگر دیتابیس هنوز به LocalAppData کپی نشده
        if (!File.Exists(targetPath))
        {
            Directory.CreateDirectory(appFolder);
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, targetPath);
            }
            else
            {
                throw new FileNotFoundException("فایل دیتابیس در مسیر نصب پیدا نشد.", sourcePath);
            }
        }

        // رشته اتصال به دیتابیس از مسیر LocalAppData
        _connectionString = $@"Data Source=(LocalDB)\ProjectsV16;
                            AttachDbFilename={targetPath};
                            Integrated Security=True;
                            Connect Timeout=30;";
    }

    public static SqlConnection GetConnection() => new SqlConnection(_connectionString);
}
