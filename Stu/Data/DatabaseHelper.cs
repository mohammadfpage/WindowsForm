using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

public static class DatabaseHelper
{
    private static readonly string _connectionString;
    private static readonly string _databaseFolder;
    private static readonly string _targetMdfPath;
    private static readonly string _targetLdfPath;

    static DatabaseHelper()
    {
        // مسیرهای منبع (در پوشه نصب)
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string sourceMdf = Path.Combine(appDirectory, "Database", "School.mdf");
        string sourceLdf = Path.Combine(appDirectory, "Database", "School_log.ldf");

        // مسیرهای مقصد در LocalAppData
        _databaseFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "schoolApp",
            "Database");

        _targetMdfPath = Path.Combine(_databaseFolder, "school.mdf");
        _targetLdfPath = Path.Combine(_databaseFolder, "school_log.ldf");

        // ایجاد پوشه مقصد اگر وجود ندارد
        Directory.CreateDirectory(_databaseFolder);

        // کپی فایل‌های دیتابیس اگر نیاز باشد
        InitializeDatabaseFiles(sourceMdf, sourceLdf);

        // رشته اتصال
        _connectionString = $@"Data Source=(LocalDB)\ProjectsV16;
                            AttachDbFilename={_targetMdfPath};
                            Integrated Security=True;
                            Connect Timeout=30;
                            MultipleActiveResultSets=True;";
    }

    private static void InitializeDatabaseFiles(string sourceMdf, string sourceLdf)
    {
        try
        {
            // فقط اگر فایل مقصد وجود ندارد یا نسخه منبع جدیدتر است
            bool needCopy = !File.Exists(_targetMdfPath) ||
                          (File.Exists(sourceMdf) && File.GetLastWriteTime(sourceMdf) > File.GetLastWriteTime(_targetMdfPath));

            if (needCopy)
            {
                // دیتابیس موجود را detach کنیم (اگر در حال استفاده است)
                DetachDatabaseIfNeeded();

                // کپی فایل‌ها
                if (File.Exists(sourceMdf))
                {
                    File.Copy(sourceMdf, _targetMdfPath, overwrite: true);
                }

                if (File.Exists(sourceLdf))
                {
                    File.Copy(sourceLdf, _targetLdfPath, overwrite: true);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"خطا در مقداردهی اولیه دیتابیس: {ex.Message}");
        }
    }

    private static void DetachDatabaseIfNeeded()
    {
        try
        {
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\ProjectsV16;Initial Catalog=master;Integrated Security=True"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'school')
                    BEGIN
                        ALTER DATABASE school SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                        EXEC sp_detach_db 'School', 'true';
                    END";
                command.ExecuteNonQuery();
            }
        }
        catch { /* خطا را نادیده بگیر اگر detach ممکن نبود */ }
    }

    public static SqlConnection GetConnection() => new SqlConnection(_connectionString);

    public static async Task<SqlConnection> GetOpenConnectionAsync()
    {
        var connection = GetConnection();
        await connection.OpenAsync();
        return connection;
    }
}