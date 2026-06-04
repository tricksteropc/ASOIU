using Microsoft.EntityFrameworkCore;

namespace Homework3.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkTask> Tasks { get; set; }  // Изменено с Task на WorkTask

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=homework3.db");
        }

        public void Initialize()
        {
            Database.EnsureCreated();

            if (!Projects.Any())
            {
                var projects = new[]
                {
                    new Project { Name = "Разработка CRM" },
                    new Project { Name = "Мобильное приложение" },
                    new Project { Name = "Аналитическая платформа" },
                    new Project { Name = "Сайт компании" }
                };
                Projects.AddRange(projects);
                SaveChanges();

                var tasks = new[]
                {
                    new WorkTask { Name = "Проектирование БД", Hours = 40, ProjectId = 1 },
                    new WorkTask { Name = "API Gateway", Hours = 60, ProjectId = 1 },
                    new WorkTask { Name = "Фронтенд админки", Hours = 80, ProjectId = 1 },
                    new WorkTask { Name = "Дизайн мобилки", Hours = 30, ProjectId = 2 },
                    new WorkTask { Name = "Разработка iOS", Hours = 120, ProjectId = 2 },
                    new WorkTask { Name = "Разработка Android", Hours = 110, ProjectId = 2 },
                    new WorkTask { Name = "ETL процессы", Hours = 90, ProjectId = 3 },
                    new WorkTask { Name = "Дашборды", Hours = 50, ProjectId = 3 },
                    new WorkTask { Name = "ML модель", Hours = 70, ProjectId = 3 },
                    new WorkTask { Name = "Вёрстка", Hours = 40, ProjectId = 4 },
                    new WorkTask { Name = "Бэкенд на .NET", Hours = 60, ProjectId = 4 },
                    new WorkTask { Name = "SEO оптимизация", Hours = 20, ProjectId = 4 }
                };
                Tasks.AddRange(tasks);
                SaveChanges();
            }
        }
    }
}