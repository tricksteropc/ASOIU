using System;
using System.Windows.Forms;
using Homework3.Forms;
using Homework3.Models;

namespace Homework3
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var db = new AppDbContext();
            db.Initialize();

            Application.Run(new MainForm());
        }
    }
}