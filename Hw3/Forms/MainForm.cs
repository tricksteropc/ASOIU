using System;
using System.Windows.Forms;

namespace Homework3.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnProjects_Click(object sender, EventArgs e)
        {
            var form = new ProjectsForm();
            form.ShowDialog();
        }

        private void btnTasks_Click(object sender, EventArgs e)
        {
            var form = new TasksForm();
            form.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            var form = new ReportForm();
            form.ShowDialog();
        }
    }
}