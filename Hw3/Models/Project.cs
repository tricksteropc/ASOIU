using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Homework3.Models
{
    /// <summary>
    /// Справочная таблица "Проекты" (сторона "один")
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Идентификатор проекта (первичный ключ)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название проекта
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Навигационное свойство: список задач проекта
        /// </summary>
        public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();
    }
}