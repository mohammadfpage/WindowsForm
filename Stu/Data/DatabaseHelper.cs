using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;
using System.IO;

public static class DatabaseHelper
{
    private static readonly string _connectionString;

    static DatabaseHelper()
    {
        EnsureLocalDbInstance("ProjectsV16");

        // مسیر امن برای ذخیره‌سازی دیتابیس
        string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string appFolder = Path.Combine(localAppData, "SchoolApp", "Database");
        string dbPath = Path.Combine(appFolder, "School.mdf");

        // مسیر نصب شده برای کپی اولیه
        string sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "School.mdf");

        // اگر دیتابیس هنوز کپی نشده بود
        if (!File.Exists(dbPath))
        {
            Directory.CreateDirectory(appFolder);
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, dbPath);
            }
            else
            {
                throw new FileNotFoundException("فایل دیتابیس در مسیر نصب پیدا نشد.", sourcePath);
            }
        }

        // رشته اتصال
        _connectionString = $@"Data Source=(LocalDB)\ProjectsV16;
                            AttachDbFilename={dbPath};
                            Integrated Security=True;
                            Connect Timeout=30;
                            Pooling=True";
    }

    public static SqlConnection GetConnection() => new SqlConnection(_connectionString);

    private static void EnsureLocalDbInstance(string instanceName)
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "sqllocaldb",
                Arguments = "i",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (!output.Contains(instanceName))
                {
                    RunCommand($"sqllocaldb create {instanceName}");
                }

                RunCommand($"sqllocaldb start {instanceName}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"خطا در بررسی یا ساخت LocalDB instance:\n{ex.Message}");
        }
    }

    private static void RunCommand(string command)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c {command}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(psi))
        {
            process.WaitForExit();
            string error = process.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception($"خطا در اجرای دستور {command}:\n{error}");
            }
        }
    }
}
