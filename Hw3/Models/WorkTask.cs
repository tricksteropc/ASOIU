using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework3.Models
{
    /// <summary>
    /// Основная таблица "Задачи" (сторона "много")
    /// </summary>
    public class WorkTask
    {
        /// <summary>
        /// Идентификатор задачи (первичный ключ)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название задачи
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Трудоёмкость в человеко-часах (не может быть отрицательной)
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Трудоёмкость не может быть отрицательной")]
        public double Hours { get; set; }

        /// <summary>
        /// Внешний ключ на проект
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Навигационное свойство: проект задачи
        /// </summary>
        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }
    }
}