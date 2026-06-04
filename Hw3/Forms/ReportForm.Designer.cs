namespace Homework3.Forms
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewReport1;
        private System.Windows.Forms.DataGridView dataGridViewReport2;
        private System.Windows.Forms.DataGridView dataGridViewReport3;
        private System.Windows.Forms.Label labelReport1Title;
        private System.Windows.Forms.Label labelReport2Title;
        private System.Windows.Forms.Label labelReport3Title;
        private System.Windows.Forms.Label labelReport1Count;
        private System.Windows.Forms.Label labelOverallAverage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;

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
            this.dataGridViewReport1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewReport2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewReport3 = new System.Windows.Forms.DataGridView();
            this.labelReport1Title = new System.Windows.Forms.Label();
            this.labelReport2Title = new System.Windows.Forms.Label();
            this.labelReport3Title = new System.Windows.Forms.Label();
            this.labelReport1Count = new System.Windows.Forms.Label();
            this.labelOverallAverage = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewReport1
            // 
            this.dataGridViewReport1.AllowUserToAddRows = false;
            this.dataGridViewReport1.AllowUserToDeleteRows = false;
            this.dataGridViewReport1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReport1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewReport1.Location = new System.Drawing.Point(3, 26);
            this.dataGridViewReport1.Name = "dataGridViewReport1";
            this.dataGridViewReport1.ReadOnly = true;
            this.dataGridViewReport1.RowHeadersWidth = 51;
            this.dataGridViewReport1.RowTemplate.Height = 29;
            this.dataGridViewReport1.Size = new System.Drawing.Size(744, 150);
            this.dataGridViewReport1.TabIndex = 0;
            // 
            // dataGridViewReport2
            // 
            this.dataGridViewReport2.AllowUserToAddRows = false;
            this.dataGridViewReport2.AllowUserToDeleteRows = false;
            this.dataGridViewReport2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReport2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewReport2.Location = new System.Drawing.Point(3, 26);
            this.dataGridViewReport2.Name = "dataGridViewReport2";
            this.dataGridViewReport2.ReadOnly = true;
            this.dataGridViewReport2.RowHeadersWidth = 51;
            this.dataGridViewReport2.RowTemplate.Height = 29;
            this.dataGridViewReport2.Size = new System.Drawing.Size(744, 100);
            this.dataGridViewReport2.TabIndex = 1;
            // 
            // dataGridViewReport3
            // 
            this.dataGridViewReport3.AllowUserToAddRows = false;
            this.dataGridViewReport3.AllowUserToDeleteRows = false;
            this.dataGridViewReport3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReport3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewReport3.Location = new System.Drawing.Point(3, 26);
            this.dataGridViewReport3.Name = "dataGridViewReport3";
            this.dataGridViewReport3.ReadOnly = true;
            this.dataGridViewReport3.RowHeadersWidth = 51;
            this.dataGridViewReport3.RowTemplate.Height = 29;
            this.dataGridViewReport3.Size = new System.Drawing.Size(744, 100);
            this.dataGridViewReport3.TabIndex = 2;
            // 
            // labelReport1Title
            // 
            this.labelReport1Title.AutoSize = true;
            this.labelReport1Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelReport1Title.Location = new System.Drawing.Point(6, 19);
            this.labelReport1Title.Name = "labelReport1Title";
            this.labelReport1Title.Size = new System.Drawing.Size(290, 23);
            this.labelReport1Title.TabIndex = 3;
            this.labelReport1Title.Text = "📋 Раздел 1: Полный список задач";
            // 
            // labelReport2Title
            // 
            this.labelReport2Title.AutoSize = true;
            this.labelReport2Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelReport2Title.Location = new System.Drawing.Point(6, 19);
            this.labelReport2Title.Name = "labelReport2Title";
            this.labelReport2Title.Size = new System.Drawing.Size(311, 23);
            this.labelReport2Title.TabIndex = 4;
            this.labelReport2Title.Text = "📊 Раздел 2: Количество задач по проектам";
            // 
            // labelReport3Title
            // 
            this.labelReport3Title.AutoSize = true;
            this.labelReport3Title.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelReport3Title.Location = new System.Drawing.Point(6, 19);
            this.labelReport3Title.Name = "labelReport3Title";
            this.labelReport3Title.Size = new System.Drawing.Size(381, 23);
            this.labelReport3Title.TabIndex = 5;
            this.labelReport3Title.Text = "📈 Раздел 3: Средняя трудоёмкость по проектам";
            // 
            // labelReport1Count
            // 
            this.labelReport1Count.AutoSize = true;
            this.labelReport1Count.Location = new System.Drawing.Point(6, 179);
            this.labelReport1Count.Name = "labelReport1Count";
            this.labelReport1Count.Size = new System.Drawing.Size(108, 20);
            this.labelReport1Count.TabIndex = 6;
            this.labelReport1Count.Text = "Всего задач: 0";
            // 
            // labelOverallAverage
            // 
            this.labelOverallAverage.AutoSize = true;
            this.labelOverallAverage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelOverallAverage.Location = new System.Drawing.Point(12, 620);
            this.labelOverallAverage.Name = "labelOverallAverage";
            this.labelOverallAverage.Size = new System.Drawing.Size(430, 23);
            this.labelOverallAverage.TabIndex = 7;
            this.labelOverallAverage.Text = "📊 Общая средняя трудоёмкость по всем задачам: 0 часов";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewReport1);
            this.groupBox1.Controls.Add(this.labelReport1Count);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 210);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Раздел 1: Полный список задач с проектами";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewReport2);
            this.groupBox2.Location = new System.Drawing.Point(12, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(750, 130);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Раздел 2: Количество задач по проектам";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridViewReport3);
            this.groupBox3.Location = new System.Drawing.Point(12, 390);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(750, 130);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Раздел 3: Средняя трудоёмкость по проектам (сортировка по убыванию)";
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.labelOverallAverage);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "📊 Отчёт по задачам и проектам";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}