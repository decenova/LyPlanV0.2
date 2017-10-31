using BussinessObject.DataAccess;
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
using BussinessObject.Entities;

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
            //tvTodolist.Items.Add(new TaskDemo() { Name = "aha" });


            //TaskDemo taskDemo = new TaskDemo() { Name = "Haha" };
            //taskDemo.Items.Add(new TaskDemo() { Name = "vl" });
            //tvTodolist.Items.Add(taskDemo);

            SetTodo();

            //tvTodolist.Items.Add(new Todo() { Title = "Lalaland" });
        }

        private void SetTodo()
        {
            TodoTask todoTask = new TodoTask();
            foreach (Todo todo in todoTask.GetTodoListForShow()) 
            {
                tvTodolist.Items.Add(todo);
            }
        }


    }
}
