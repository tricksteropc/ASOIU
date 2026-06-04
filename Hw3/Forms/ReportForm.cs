using System;
using System.Linq;
using System.Windows.Forms;
using Homework3.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework3.Forms
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            GenerateReport();
        }

        private void GenerateReport()
        {
            using var db = new AppDbContext();

            // ========== РАЗДЕЛ 1 ==========
            // Полный список всех записей основной таблицы с названием категории
            // LINQ: Include + OrderBy
            var report1 = db.Tasks
                .Include(t => t.Project)
                .OrderBy(t => t.Name)
                .Select(t => new
                {
                    Название_задачи = t.Name,
                    Проект = t.Project != null ? t.Project.Name : "Без проекта",
                    Трудоёмкость_часы = t.Hours
                })
                .ToList();

            dataGridViewReport1.DataSource = report1;
            labelReport1Count.Text = $"Всего задач: {report1.Count}";

            // ========== РАЗДЕЛ 2 ==========
            // Количество записей по категориям
            // LINQ: GroupBy + Count
            var report2 = db.Tasks
                .GroupBy(t => t.Project != null ? t.Project.Name : "Без проекта")
                .Select(g => new
                {
                    Проект = g.Key,
                    Количество_задач = g.Count()
                })
                .OrderBy(r => r.Проект)
                .ToList();

            dataGridViewReport2.DataSource = report2;

            // ========== РАЗДЕЛ 3 ==========
            // Среднее значение числового поля по категориям
            // LINQ: GroupBy + Average + OrderByDescending
            var report3 = db.Tasks
                .Where(t => t.Project != null)
                .GroupBy(t => t.Project!.Name)
                .Select(g => new
                {
                    Проект = g.Key,
                    Средняя_трудоёмкость = Math.Round(g.Average(t => t.Hours), 2)
                })
                .OrderByDescending(r => r.Средняя_трудоёмкость)
                .ToList();

            dataGridViewReport3.DataSource = report3;

            // Дополнительная статистика (общая средняя)
            double overallAverage = db.Tasks.Any() ? db.Tasks.Average(t => t.Hours) : 0;
            labelOverallAverage.Text = $"📊 Общая средняя трудоёмкость по всем задачам: {Math.Round(overallAverage, 2)} часов";
        }
    }
}
