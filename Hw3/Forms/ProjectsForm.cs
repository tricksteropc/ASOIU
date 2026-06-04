using Homework3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Homework3.Forms
{
    public partial class ProjectsForm : Form
    {
        public ProjectsForm()
        {
            InitializeComponent();
            LoadProjects();
        }

        private void LoadProjects()
        {
            using var db = new AppDbContext();
            var projects = db.Projects.OrderBy(p => p.Name).ToList();
            dataGridViewProjects.DataSource = projects;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Введите название проекта:", "Добавление проекта", "");
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Название не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var db = new AppDbContext();
            db.Projects.Add(new Project { Name = name });
            db.SaveChanges();
            LoadProjects();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewProjects.CurrentRow?.DataBoundItem is not Project project)
            {
                MessageBox.Show("Выберите проект для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newName = Microsoft.VisualBasic.Interaction.InputBox("Редактирование проекта:", "Изменить название", project.Name);
            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Название не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var db = new AppDbContext();
            var toUpdate = db.Projects.Find(project.Id);
            if (toUpdate != null)
            {
                toUpdate.Name = newName;
                db.SaveChanges();
                LoadProjects();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewProjects.CurrentRow?.DataBoundItem is not Project project)
            {
                MessageBox.Show("Выберите проект для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var db = new AppDbContext();
            var toDelete = db.Projects.Include(p => p.Tasks).FirstOrDefault(p => p.Id == project.Id);
            if (toDelete != null)
            {
                if (toDelete.Tasks.Any())
                {
                    MessageBox.Show("Невозможно удалить проект, так как у него есть связанные задачи.", "Запрещено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Удалить проект \"{toDelete.Name}\"?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    db.Projects.Remove(toDelete);
                    db.SaveChanges();
                    LoadProjects();
                }
            }
        }
    }
}