using BussinessObject.DataAccess;
using BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace LyPlan
{
    /// <summary>
    /// Interaction logic for TodoForm.xaml
    /// </summary>
    public partial class TodoForm : Window
    {
        private ObservableCollection<TodoWork> todoList;
        public TodoForm(ObservableCollection<TodoWork> todoList)
        {
            InitializeComponent();
            this.todoList = todoList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            TodoTaskData todoTask = new TodoTaskData();
            TodoWork todoWork = new TodoWork()
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text
            };
            todoTask.SaveTodoTask(todoWork);
            todoList.Add(todoWork);
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
}
