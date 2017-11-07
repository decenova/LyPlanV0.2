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
            setUpCbAlert();
            btnDelete.Visibility = Visibility.Hidden;
            this.todoList = todoList;
            btnEdit.Content = "Add";
        }

        public TodoForm(TodoWork todoWork, ObservableCollection<TodoWork> todoList)
        {
            InitializeComponent();
            setUpCbAlert();
            btnDelete.Visibility = Visibility.Visible;
            this.todoList = todoList;
            this.todoWork = todoWork;
            btnEdit.Content = "Update";
            txtTitle.Text = todoWork.Title;
            txtDescription.Text = todoWork.Description;
            setTimeForm();
        }

        private void setTimeForm()
        {
            if (!todoWork.Deadline.Equals(DateTime.MinValue))
            {
                txtHour.Text = todoWork.Deadline.Hour.ToString();
                txtMinute.Text = todoWork.Deadline.Minute.ToString();
                dpDate.SelectedDate = todoWork.Deadline;
                if (!todoWork.AlertTime.Equals(DateTime.MinValue))
                {
                    TimeSpan time = todoWork.Deadline.Subtract(todoWork.AlertTime);
                    int minute = time.Minutes;
                    foreach (AlertMinute.AMinute item in cbAlert.ItemsSource as List<AlertMinute.AMinute>)
                    {
                        if (item.Value == minute)
                        {
                            cbAlert.SelectedItem = item;
                        }
                    }
                }
            }
        }

        private void setUpCbAlert()
        {
            cbAlert.ItemsSource = new AlertMinute().ListMinute;
            cbAlert.DisplayMemberPath = "Name";
            cbAlert.SelectedIndex = 0;
        }

        private bool validInput()
        {
            if (txtTitle.Text.Trim().Length == 0)
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

            bool check = false;
            TodoTaskData todoTaskData = new TodoTaskData();
            if (btnEdit.Content.Equals("Add"))
            {
                TodoWork newTodoWork = new TodoWork()
                {
                    Title = txtTitle.Text,
                    Description = txtDescription.Text
                };
                DateTime? deadline = GetDateTimeFromForm();
                if (deadline == null)
                    check = todoTaskData.SaveTodoTask(newTodoWork);
                else
                {
                    newTodoWork.Deadline = deadline as dynamic;
                    dynamic minute = (cbAlert.SelectedValue as dynamic).Value;
                    if (minute != null)
                    {
                        newTodoWork.AlertTime = newTodoWork.Deadline.AddMinutes(-1 * minute);
                        check = todoTaskData.SaveTodoTaskWithAlert(newTodoWork);
                    }
                    else
                        check = todoTaskData.SaveTodoTaskWithDealine(newTodoWork);
                }
                if (check)
                {
                    DataTable InsertId = todoTaskData.GetInsertTodoTaskId();
                    try
                    {
                        newTodoWork.TaskId = InsertId.Select()[0].ItemArray[0] as dynamic;
                    }
                    catch (Exception ex)
                    {
                    }
                    todoList.Add(newTodoWork);
                    this.Close();
                }
                else
                {
                    tbMessage.Text = "Add fail! Please try again";
                }

            }
            else
            {
                todoWork.Title = txtTitle.Text;
                todoWork.Description = txtDescription.Text;
                DateTime? deadline = GetDateTimeFromForm();
                if (deadline == null)
                {
                    check = todoTaskData.UpdateTodo(todoWork);
                }
                else
                {
                    todoWork.Deadline = deadline as dynamic;
                    dynamic minute = (cbAlert.SelectedValue as dynamic).Value;
                    if (minute != null)
                    {
                        todoWork.AlertTime = todoWork.Deadline.AddMinutes(-1 * minute);
                        check = todoTaskData.UpdateTodoWithAlertTime(todoWork);
                    }
                    else
                    {
                        check = todoTaskData.UpdateTodoWithDeadline(todoWork);
                        todoWork.AlertTime = new DateTime();
                    }
                }
                if (check)
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


        private void txtHour_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtHour_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtHour.Text.Length == 0)
                txtHour.Text = "0";
            int hour = int.Parse(txtHour.Text);
            if (hour > 23)
                hour = 23;
            if (hour < 0)
                hour = 0;
            txtHour.Text = hour + "";
        }

        private void txtMinute_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtMinute_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtMinute.Text.Length == 0)
                txtMinute.Text = "0";
            int minute = int.Parse(txtMinute.Text);
            if (minute > 59)
                minute = 59;
            if (minute < 0)
                minute = 0;
            txtMinute.Text = minute + "";
        }

        private DateTime? GetDateTimeFromForm()
        {
            DateTime? dateTime = dpDate.SelectedDate;
            if (dateTime != null)
            {
                DateTime d = dateTime as dynamic;
                d = d.AddHours(int.Parse(txtHour.Text));
                d = d.AddMinutes(int.Parse(txtMinute.Text));
                dateTime = d;
            }
            return dateTime;
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpDate.SelectedDate != null)
                cbAlert.IsEnabled = true;
            else
                cbAlert.IsEnabled = false;
        }
    }

}
