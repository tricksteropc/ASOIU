using System.Text;

namespace Homework2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            string dbPath = "tasks.db";
            string projectsCsv = Path.Combine(AppContext.BaseDirectory, "projects.csv");
            string tasksCsv = Path.Combine(AppContext.BaseDirectory, "tasks.csv");

            var db = new DatabaseManager(dbPath);

            try
            {
                db.InitializeDatabase(projectsCsv, tasksCsv);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации базы данных: {ex.Message}");
                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=== Управление проектами и задачами ===");
            Console.WriteLine();

            string choice;
            do
            {
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║            ГЛАВНОЕ МЕНЮ                    ║");
                Console.WriteLine("╠════════════════════════════════════════════╣");
                Console.WriteLine("║  1 — Показать все проекты                  ║");
                Console.WriteLine("║  2 — Показать все задачи                   ║");
                Console.WriteLine("║  3 — Добавить задачу                       ║");
                Console.WriteLine("║  4 — Редактировать задачу                  ║");
                Console.WriteLine("║  5 — Удалить задачу                        ║");
                Console.WriteLine("║  6 — Отчёты                                ║");
                Console.WriteLine("║  7 — Фильтр по проекту                     ║");
                Console.WriteLine("║  0 — Выход                                 ║");
                Console.WriteLine("╚════════════════════════════════════════════╝");
                Console.Write("Ваш выбор: ");

                choice = Console.ReadLine()?.Trim() ?? "";
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowProjects(db); break;
                    case "2": ShowTasks(db); break;
                    case "3": AddTask(db); break;
                    case "4": EditTask(db); break;
                    case "5": DeleteTask(db); break;
                    case "6": ReportsMenu(db); break;
                    case "7": FilterByProject(db); break;
                    case "0": Console.WriteLine("До свидания!"); break;
                    default: Console.WriteLine("Неверный пункт меню."); break;
                }
                Console.WriteLine();
            } while (choice != "0");
        }

        // ---------- Показать все проекты ----------
        static void ShowProjects(DatabaseManager db)
        {
            Console.WriteLine("---- Все проекты ----");
            var projects = db.GetAllProjects();
            foreach (var project in projects)
                Console.WriteLine($" [{project.Id}] {project.Name}");
            Console.WriteLine($"Итого: {projects.Count}");
        }

        // ---------- Показать все задачи ----------
        static void ShowTasks(DatabaseManager db)
        {
            Console.WriteLine("---- Все задачи ----");
            var tasks = db.GetAllTasks();
            foreach (var task in tasks)
                Console.WriteLine($" [{task.Id}] {task.Name}, проект #{task.ProjectId}, часов: {task.Hours}");
            Console.WriteLine($"Итого: {tasks.Count}");
        }

        // ---------- Добавить задачу ----------
        static void AddTask(DatabaseManager db)
        {
            Console.WriteLine("---- Добавление задачи ----");

            Console.WriteLine("Доступные проекты:");
            var projects = db.GetAllProjects();
            foreach (var project in projects)
                Console.WriteLine($" [{project.Id}] {project.Name}");

            Console.Write("ID проекта: ");
            if (!int.TryParse(Console.ReadLine(), out int projectId))
            {
                Console.WriteLine("Ошибка: введите целое число.");
                return;
            }

            Console.Write("Название задачи: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            if (name.Length == 0)
            {
                Console.WriteLine("Ошибка: название не может быть пустым.");
                return;
            }

            Console.Write("Трудоёмкость (часы): ");
            if (!int.TryParse(Console.ReadLine(), out int hours))
            {
                Console.WriteLine("Ошибка: введите целое число.");
                return;
            }

            if (hours < 0)
            {
                Console.WriteLine("Ошибка: трудоёмкость не может быть отрицательной.");
                return;
            }

            try
            {
                var task = new Task(0, projectId, name, hours);
                db.AddTask(task);
                Console.WriteLine("Задача добавлена.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // ---------- Редактировать задачу ----------
        static void EditTask(DatabaseManager db)
        {
            Console.WriteLine("---- Редактирование задачи ----");
            Console.Write("Введите ID задачи: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ошибка: введите целое число.");
                return;
            }

            var task = db.GetTaskById(id);
            if (task == null)
            {
                Console.WriteLine($"Задача с ID={id} не найдена.");
                return;
            }

            Console.WriteLine($"Текущие данные: [{task.Id}] {task.Name}, проект #{task.ProjectId}, часов: {task.Hours}");
            Console.WriteLine("(Нажмите Enter, чтобы оставить значение без изменений)");

            // Название
            Console.Write($"Название [{task.Name}]: ");
            string input = Console.ReadLine()?.Trim() ?? "";
            if (input.Length > 0) task.Name = input;

            // Проект
            Console.Write($"ID проекта [{task.ProjectId}]: ");
            input = Console.ReadLine()?.Trim() ?? "";
            if (input.Length > 0 && int.TryParse(input, out int newProjectId))
                task.ProjectId = newProjectId;

            // Часы
            Console.Write($"Трудоёмкость [{task.Hours}]: ");
            input = Console.ReadLine()?.Trim() ?? "";
            if (input.Length > 0 && int.TryParse(input, out int newHours))
            {
                if (newHours < 0)
                {
                    Console.WriteLine("Ошибка: трудоёмкость не может быть отрицательной.");
                    return;
                }
                task.Hours = newHours;
            }

            try
            {
                db.UpdateTask(task);
                Console.WriteLine("Данные обновлены.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // ---------- Удалить задачу ----------
        static void DeleteTask(DatabaseManager db)
        {
            Console.WriteLine("---- Удаление задачи ----");
            Console.Write("Введите ID задачи: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ошибка: введите целое число.");
                return;
            }

            var task = db.GetTaskById(id);
            if (task == null)
            {
                Console.WriteLine($"Задача с ID={id} не найдена.");
                return;
            }

            Console.Write($"Удалить «{task.Name}»? (да/нет): ");
            string confirm = Console.ReadLine()?.Trim().ToLower() ?? "";
            if (confirm == "да" || confirm == "yes" || confirm == "y")
            {
                db.DeleteTask(id);
                Console.WriteLine("Задача удалена.");
            }
            else
            {
                Console.WriteLine("Удаление отменено.");
            }
        }

        // ---------- Подменю отчётов ----------
        static void ReportsMenu(DatabaseManager db)
        {
            string choice;
            do
            {
                Console.WriteLine("--- Отчёты ---");
                Console.WriteLine(" 1 - Список задач с названиями проектов");
                Console.WriteLine(" 2 - Количество задач по проектам");
                Console.WriteLine(" 3 - Средняя трудоёмкость по проектам");
                Console.WriteLine(" 0 - Назад");
                Console.Write("Ваш выбор: ");

                choice = Console.ReadLine()?.Trim() ?? "";
                switch (choice)
                {
                    case "1": Report1_TasksWithProjects(db); break;
                    case "2": Report2_CountByProject(db); break;
                    case "3": Report3_AvgHoursByProject(db); break;
                    case "0": break;
                    default: Console.WriteLine("Неверный пункт."); break;
                }
                Console.WriteLine();
            } while (choice != "0");
        }

        // Отчёт 1: Список задач с названиями проектов
        static void Report1_TasksWithProjects(DatabaseManager db)
        {
            new ReportBuilder(db)
                .Query(@"SELECT t.task_name, p.project_name, t.hours
                         FROM tasks t
                         JOIN projects p ON t.project_id = p.project_id
                         ORDER BY t.task_name")
                .Title("Задачи по проектам")
                .Header("Задача", "Проект", "Часы")
                .ColumnWidths(35, 25, 10)
                .Numbered()
                .Footer("Всего задач")
                .Print();
        }

        // Отчёт 2: Количество задач по проектам
        static void Report2_CountByProject(DatabaseManager db)
        {
            new ReportBuilder(db)
                .Query(@"SELECT p.project_name, COUNT(*) AS task_count
                         FROM tasks t
                         JOIN projects p ON t.project_id = p.project_id
                         GROUP BY p.project_name
                         ORDER BY p.project_name")
                .Title("Количество задач по проектам")
                .Header("Проект", "Кол-во задач")
                .ColumnWidths(35, 15)
                .Print();
        }

        // Отчёт 3: Средняя трудоёмкость по проектам
        static void Report3_AvgHoursByProject(DatabaseManager db)
        {
            new ReportBuilder(db)
                .Query(@"SELECT p.project_name, ROUND(AVG(t.hours), 1) AS avg_hours
                         FROM tasks t
                         JOIN projects p ON t.project_id = p.project_id
                         GROUP BY p.project_name
                         ORDER BY avg_hours DESC")
                .Title("Средняя трудоёмкость по проектам")
                .Header("Проект", "Ср. часы")
                .ColumnWidths(35, 15)
                .Print();
        }

        // ---------- Фильтр по проекту ----------
        static void FilterByProject(DatabaseManager db)
        {
            Console.WriteLine("--- Фильтр по проекту ---");
            Console.WriteLine("Доступные проекты:");
            var projects = db.GetAllProjects();
            foreach (var project in projects)
            {
                Console.WriteLine($" [{project.Id}] {project.Name}");
            }

            Console.Write("Введите ID проекта: ");
            if (!int.TryParse(Console.ReadLine(), out int projectId))
            {
                Console.WriteLine("Ошибка: введите целое число.");
                return;
            }

            var selectedProject = projects.FirstOrDefault(p => p.Id == projectId);
            if (selectedProject == null)
            {
                Console.WriteLine("Проект с таким ID не найден.");
                return;
            }

            var tasks = db.GetTasksByProject(projectId);
            if (tasks.Count == 0)
            {
                Console.WriteLine($"В проекте \"{selectedProject.Name}\" нет задач.");
                return;
            }

            Console.WriteLine($"\nЗадачи проекта \"{selectedProject.Name}\":");
            foreach (var task in tasks)
            {
                Console.WriteLine($" [{task.Id}] {task.Name}, часов: {task.Hours}");
            }
            Console.WriteLine($"Итого: {tasks.Count}");
        }
    }
}