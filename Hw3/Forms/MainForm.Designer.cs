namespace Homework3.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnProjects;
        private System.Windows.Forms.Button btnTasks;
        private System.Windows.Forms.Button btnReport;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnProjects = new System.Windows.Forms.Button();
            this.btnTasks = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnProjects
            // 
            this.btnProjects.Location = new System.Drawing.Point(50, 50);
            this.btnProjects.Name = "btnProjects";
            this.btnProjects.Size = new System.Drawing.Size(200, 50);
            this.btnProjects.TabIndex = 0;
            this.btnProjects.Text = "Управление проектами";
            this.btnProjects.UseVisualStyleBackColor = true;
            this.btnProjects.Click += new System.EventHandler(this.btnProjects_Click);
            // 
            // btnTasks
            // 
            this.btnTasks.Location = new System.Drawing.Point(50, 120);
            this.btnTasks.Name = "btnTasks";
            this.btnTasks.Size = new System.Drawing.Size(200, 50);
            this.btnTasks.TabIndex = 1;
            this.btnTasks.Text = "Управление задачами";
            this.btnTasks.UseVisualStyleBackColor = true;
            this.btnTasks.Click += new System.EventHandler(this.btnTasks_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(50, 190);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(200, 50);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Отчёты";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnTasks);
            this.Controls.Add(this.btnProjects);
            this.Name = "MainForm";
            this.Text = "Главное меню";
            this.ResumeLayout(false);
        }
    }
}