using Homework3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Homework3.Forms
{
    public partial class TasksForm : Form
    {
        public TasksForm()
        {
            InitializeComponent();
            LoadTasks();
            LoadProjectsToComboBox();
        }

        private void LoadProjectsToComboBox()
        {
            using var db = new AppDbContext();
            comboBoxProjects.DataSource = db.Projects.OrderBy(p => p.Name).ToList();
            comboBoxProjects.DisplayMember = "Name";
            comboBoxProjects.ValueMember = "Id";
        }

        private void LoadTasks()
        {
            using var db = new AppDbContext();
            var tasks = db.Tasks
                .Include(t => t.Project)
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    Hours = t.Hours,
                    ProjectName = t.Project != null ? t.Project.Name : ""
                })
                .ToList();

            dataGridViewTasks.DataSource = tasks;
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtHours.Text = "";
            comboBoxProjects.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название задачи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtHours.Text, out double hours) || hours < 0)
            {
                MessageBox.Show("Трудоёмкость должна быть неотрицательным числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxProjects.SelectedValue == null)
            {
                MessageBox.Show("Выберите проект.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var db = new AppDbContext();
            db.Tasks.Add(new WorkTask
            {
                Name = txtName.Text,
                Hours = hours,
                ProjectId = (int)comboBoxProjects.SelectedValue
            });
            db.SaveChanges();
            LoadTasks();
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewTasks.CurrentRow == null)
            {
                MessageBox.Show("Выберите задачу для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridViewTasks.CurrentRow.Cells["Id"].Value;

            if (MessageBox.Show("Удалить выбранную задачу?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using var db = new AppDbContext();
                var task = db.Tasks.Find(id);
                if (task != null)
                {
                    db.Tasks.Remove(task);
                    db.SaveChanges();
                    LoadTasks();
                }
            }
        }
    }
}