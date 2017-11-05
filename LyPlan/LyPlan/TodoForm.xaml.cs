using BussinessObject.DataAccess;
using BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace LyPlan
{
    /// <summary>
    /// Interaction logic for TodoForm.xaml
    /// </summary>
    public partial class TodoForm : Window
    {
        private TodoWork todoWork;
        private ObservableCollection<TodoWork> todoList;
        public TodoForm(ObservableCollection<TodoWork> todoList)
        {
            InitializeComponent();
            btnDelete.Visibility = Visibility.Hidden;
            this.todoList = todoList;
            btnEdit.Content = "Add";
        }

        public TodoForm(TodoWork todoWork ,ObservableCollection<TodoWork> todoList)
        {
            InitializeComponent();
            btnDelete.Visibility = Visibility.Visible;
            this.todoList = todoList;
            this.todoWork = todoWork;
            btnEdit.Content = "Update";
            txtTitle.Text = todoWork.Title;
            txtDescription.Text = todoWork.Description;
        }

        private bool validInput()
        {
            if (txtTitle.Text.Length == 0)
            {
                tbMessage.Text = "Title can't be blank";
                return false;
            }
            return true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!validInput())
            {
                return;
            }

            TodoTaskData todoTaskData = new TodoTaskData();
            if (btnEdit.Content.Equals("Add"))
            {
                TodoWork newTodoWork = new TodoWork()
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text
                };
                if (todoTaskData.SaveTodoTask(newTodoWork)) {
                    DataTable InsertId = todoTaskData.GetInsertTodoTaskId();
                    newTodoWork.TaskId = InsertId.Select()[0].ItemArray[0] as dynamic;
                    todoList.Add(newTodoWork);
                    this.Close();
                } else
                {
                    tbMessage.Text = "Add fail! Please try again";
                }
                
            } else
            {
                todoWork.Title = txtTitle.Text;
                todoWork.Description = txtDescription.Text;
                if (todoTaskData.UpdateTodo(todoWork))
                {
                    CollectionViewSource.GetDefaultView(todoList).Refresh();
                    this.Close();
                }
                 else
                {
                    tbMessage.Text = "Update fail! Please try again";
                }
            }
            
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            TodoTaskData todoTaskData = new TodoTaskData();
            todoWork.StatusId = 6;
            if (todoTaskData.CheckTodo(todoWork))
            {
                todoList.Remove(todoWork);
                this.Close();
            }
            else
            {
                tbMessage.Text = "Delete fail! Please try again";
            }
        }
    }
    
}
