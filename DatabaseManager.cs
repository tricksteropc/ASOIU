using Microsoft.Data.Sqlite;
/// <summary>
/// Управление базой данных SQLite.
/// Инкапсулирует все операции с БД: создание таблиц,
/// импорт CSV, CRUD-операции, выполнение запросов для отчётов.
/// </summary>
class DatabaseManager
{
    private string _connectionString;

    /// <summary>
    /// Конструктор. Принимает путь к файлу БД.
    /// </summary>
    public DatabaseManager(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
    }

    // ---------- Инициализация ----------

    /// <summary>
    /// Создаёт таблицы (если не существуют) и загружает CSV при первом запуске
    /// </summary>
    public void InitializeDatabase(string projectsCsvPath, string tasksCsvPath)
    {
        CreateTables();

        if (GetAllProjects().Count == 0 && File.Exists(projectsCsvPath))
        {
            ImportProjectsFromCsv(projectsCsvPath);
            Console.WriteLine($"[OK] Загружены проекты из {projectsCsvPath}");
        }

        if (GetAllTasks().Count == 0 && File.Exists(tasksCsvPath))
        {
            ImportTasksFromCsv(tasksCsvPath);
            Console.WriteLine($"[OK] Загружены задачи из {tasksCsvPath}");
        }
    }

    /// <summary>Создание таблиц</summary>
    private void CreateTables()
    {
        try
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
            CREATE TABLE IF NOT EXISTS projects (
                project_id INTEGER PRIMARY KEY AUTOINCREMENT,
                project_name TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS tasks (
                task_id INTEGER PRIMARY KEY AUTOINCREMENT,
                project_id INTEGER NOT NULL,
                task_name TEXT NOT NULL,
                hours INTEGER NOT NULL,
                FOREIGN KEY (project_id) REFERENCES projects(project_id)
            );";

            cmd.ExecuteNonQuery();
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"Ошибка при создании таблиц: {ex.Message}");
            throw;
        }
    }

    /// <summary>Импорт проектов из CSV</summary>
    private void ImportProjectsFromCsv(string path)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        string[] lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(';');
            if (parts.Length < 2) continue;
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO projects (project_id, project_name) VALUES (@id, @name)";
            cmd.Parameters.AddWithValue("@id", int.Parse(parts[0]));
            cmd.Parameters.AddWithValue("@name", parts[1]);
            cmd.ExecuteNonQuery();
        }
    }

    /// <summary>Импорт задач из CSV</summary>
    private void ImportTasksFromCsv(string path)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        string[] lines = File.ReadAllLines(path);
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(';');
            if (parts.Length < 4) continue;
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO tasks (task_id, project_id, task_name, hours)
                VALUES (@id, @projectId, @name, @hours)";
            cmd.Parameters.AddWithValue("@id", int.Parse(parts[0]));
            cmd.Parameters.AddWithValue("@projectId", int.Parse(parts[1]));
            cmd.Parameters.AddWithValue("@name", parts[2]);
            cmd.Parameters.AddWithValue("@hours", int.Parse(parts[3]));
            cmd.ExecuteNonQuery();
        }
    }

    // ---------- Чтение данных ----------

    /// <summary>Получить все проекты</summary>
    public List<Project> GetAllProjects()
    {
        var result = new List<Project>();
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT project_id, project_name FROM projects ORDER BY project_id";
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            result.Add(new Project(reader.GetInt32(0), reader.GetString(1)));
        }
        return result;
    }

    /// <summary>Получить все задачи</summary>
    public List<Task> GetAllTasks()
    {
        var result = new List<Task>();
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT task_id, project_id, task_name, hours FROM tasks ORDER BY task_id";
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            result.Add(new Task(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetString(2),
                reader.GetInt32(3)));
        }
        return result;
    }

    /// <summary>Получить задачу по Id</summary>
    public Task GetTaskById(int id)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT task_id, project_id, task_name, hours FROM tasks WHERE task_id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Task(
                reader.GetInt32(0), reader.GetInt32(1),
                reader.GetString(2), reader.GetInt32(3));
        }
        return null;
    }

    // ---------- Изменение данных ----------

    /// <summary>Добавить задачу (Id генерируется автоматически)</summary>
    public void AddTask(Task task)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO tasks (project_id, task_name, hours)
            VALUES (@projectId, @name, @hours)";
        cmd.Parameters.AddWithValue("@projectId", task.ProjectId);
        cmd.Parameters.AddWithValue("@name", task.Name);
        cmd.Parameters.AddWithValue("@hours", task.Hours);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Обновить данные задачи</summary>
    public void UpdateTask(Task task)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            UPDATE tasks
            SET project_id = @projectId, task_name = @name, hours = @hours
            WHERE task_id = @id";
        cmd.Parameters.AddWithValue("@id", task.Id);
        cmd.Parameters.AddWithValue("@projectId", task.ProjectId);
        cmd.Parameters.AddWithValue("@name", task.Name);
        cmd.Parameters.AddWithValue("@hours", task.Hours);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Удалить задачу по Id</summary>
    public void DeleteTask(int id)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM tasks WHERE task_id = @id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    // ---------- Выполнение произвольного запроса (для отчётов) ----------

    /// <summary>
    /// Выполняет SQL-запрос и возвращает имена столбцов и строки результата.
    /// Используется классом ReportBuilder.
    /// </summary>
    public (string[] columns, List<string[]> rows) ExecuteQuery(string sql)
    {
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();
        var cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        using var reader = cmd.ExecuteReader();

        string[] columns = new string[reader.FieldCount];
        for (int i = 0; i < reader.FieldCount; i++)
            columns[i] = reader.GetName(i);

        var rows = new List<string[]>();
        while (reader.Read())
        {
            string[] row = new string[reader.FieldCount];
            for (int i = 0; i < reader.FieldCount; i++)
                row[i] = reader.GetValue(i)?.ToString() ?? "";
            rows.Add(row);
        }
        return (columns, rows);
    }

    // ---------- Фильтр по проекту (Группа Г) ----------

    /// <summary>Получить задачи конкретного проекта</summary>
    public List<Task> GetTasksByProject(int projectId)
    {
        var result = new List<Task>();
        using var conn = new SqliteConnection(_connectionString);
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT task_id, project_id, task_name, hours
            FROM tasks WHERE project_id = @projectId ORDER BY task_name";
        cmd.Parameters.AddWithValue("@projectId", projectId);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            result.Add(new Task(
                reader.GetInt32(0), reader.GetInt32(1),
                reader.GetString(2), reader.GetInt32(3)));
        }
        return result;
    }
}