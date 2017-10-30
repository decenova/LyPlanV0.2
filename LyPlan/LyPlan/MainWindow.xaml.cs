using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LyPlan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Task task = new Task() { Name = "Reading book" };
            tvTodolist.Items.Add(task);
            tvTodolist.Items.Add(new Task() { Name = "Play game" });
            tvTodolist.Items.Add(new Task() { Name = "Learn" });
            tvTodolist.Items.Add(new Task() { Name = "Vu vu" });
        }

        private void SetTodoTask(DataTable dt)
        {
            
            foreach (DataRow row in dt.Rows)
            {
                BussinessObject.Entities.Task task = new BussinessObject.Entities.Task();
                task.Id = int.Parse(row["Id"].ToString());
                task.Title = row["Title"].ToString();
                task.Description = row["Description"].ToString();
            }
        }


    }
}
