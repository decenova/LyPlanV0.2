using BussinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
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
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Globalization;

namespace LyPlan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GoalForm goalForm;
        private TodoForm todoForm;
        private TaskForm taskForm;
        private ObservableCollection<Task> weekyList;
        private ObservableCollection<TodoWork> todoList;
        private ObservableCollection<TodoWork> doneList;

        private DateTimeFormatInfo dfi;
        private System.Globalization.Calendar cal;
        public MainWindow()
        {
            InitializeComponent();
            weekyList = new ObservableCollection<Task>();
            todoList = new ObservableCollection<TodoWork>();
            doneList = new ObservableCollection<TodoWork>();
            dfi = DateTimeFormatInfo.CurrentInfo;
            cal = dfi.Calendar;

            setDateTime();
            SetTodo();
            SetWeeky();
            SetWeekyWork();
            tvDonelist.ItemsSource = doneList;
        }

        private void setDateTime()
        {
            dpTime.SelectedDate = DateTime.Now;
            dynamic setTime = dpTime.SelectedDate;
            tbWeek.Text = "Week " + cal.GetWeekOfYear(setTime, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }
        
        private DateTime getDateTimeOfWeek(DateTime dateTime, DayOfWeek dayOfWeek)
        {
            int input1 = dateTime.DayOfWeek - DayOfWeek.Sunday;
            if (input1 == 0)
                input1 = 7;
            int input2 = dayOfWeek - DayOfWeek.Sunday;
            if (input2 == 0)
                input2 = 7;
            int distance = input1 - input2;
            return dateTime.AddDays(-distance).Date;
        }

        private void SetWeekyWork()
        {
            WeekyTaskData weekyTask = new WeekyTaskData();
            dynamic setTime = dpTime.SelectedDate;
            DateTime startTime = getDateTimeOfWeek(setTime,DayOfWeek.Monday);
            DateTime endTime = startTime.AddDays(7).Date;
            List<DayInWeek> listDayInWeek = weekyTask.GetListDayInWeekForShow(startTime, endTime);

            foreach (DayInWeek day in listDayInWeek)
            {
                switch (day.DayName)
                {
                    case DayOfWeek.Monday:
                        cMonday.DataContext = day;
                        break;
                    case DayOfWeek.Tuesday:
                        cTuesday.DataContext = day;
                        break;
                    case DayOfWeek.Wednesday:
                        cWednesday.DataContext = day;
                        break;
                    case DayOfWeek.Thursday:
                        cThursday.DataContext = day;
                        break;
                    case DayOfWeek.Friday:
                        cFriday.DataContext = day;
                        break;
                    case DayOfWeek.Saturday:
                        cSaturday.DataContext = day;
                        break;
                    case DayOfWeek.Sunday:
                        cSunday.DataContext = day;
                        break;
                }
            }
        }

        private void SetWeeky()
        {
            WeekyTaskData weekyTask = new WeekyTaskData();
            weekyList.Clear();
            foreach (Task task in weekyTask.GetListAllRootWeekyTaskForShow())
            {
                weekyList.Add(task);
            }
            tvWeekylist.ItemsSource = weekyList;
        }

        private void SetTodo()
        {
            TodoTaskData todoTask = new TodoTaskData();
            todoList.Clear();
            foreach (TodoWork todo in todoTask.GetAllTodoWorkForShow())
            {
                todoList.Add(todo);
            }
            tvTodolist.ItemsSource = todoList;
        }

        public void ClearSelection(TreeView inputTreeview)
        {
            var selected = inputTreeview.SelectedItem;
            if (selected == null)
                return;
            var treeViewItem = ContainerFromItem(inputTreeview, selected) as TreeViewItem;
            if (treeViewItem != null)
                treeViewItem.IsSelected = false;
        }

        public TreeViewItem ContainerFromItem(TreeView treeView, object item)
        {
            var treeViewItem = treeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
            if (treeViewItem != null)
                return treeViewItem;
            return ContainerFromItem(treeView.ItemContainerGenerator, treeView.Items, item);
        }

        private TreeViewItem ContainerFromItem(ItemContainerGenerator parentItemContainerGenerator, ItemCollection itemCollection, object item)
        {
            foreach (var node in itemCollection)
            {
                var treeViewItemParent = parentItemContainerGenerator.ContainerFromItem(node) as TreeViewItem;
                if (treeViewItemParent == null)
                    return null;
                var treeViewItemNode = treeViewItemParent.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeViewItemNode != null)
                    return treeViewItemNode;
                var res = ContainerFromItem(treeViewItemParent.ItemContainerGenerator, treeViewItemParent.Items, item);
                if (res != null)
                    return res;
            }
            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tabWeekylist.IsSelected)
            {
                goalForm = new GoalForm(weekyList);
                goalForm.ShowDialog();
            }
            else
            {
                todoForm = new TodoForm(todoList);
                todoForm.ShowDialog();
            }
        }

        private void tvTodolist_Item_Selected(object sender, RoutedEventArgs e)
        {
            dynamic data = sender;
            TodoWork todoWork = data.SelectedItem;
            if (btnEdit.IsChecked == true)
            {
                todoForm = new TodoForm(todoWork, todoList);
                todoForm.ShowDialog();
            }
            else
            {
                TodoTaskData todoTaskData = new TodoTaskData();
                if (todoWork.StatusId == 5)
                {
                    todoWork.StatusId = 1;
                    if (todoTaskData.CheckTodo(todoWork))
                    {
                        todoList.Add(todoWork);
                        doneList.Remove(todoWork);
                    }
                }
                else
                {
                    todoWork.StatusId = 5;
                    if (todoTaskData.CheckTodo(todoWork))
                    {
                        doneList.Add(todoWork);
                        todoList.Remove(todoWork);
                    }
                }
            }
        }

        private void tvWeekylist_Item_Selected(object sender, RoutedEventArgs e)
        {
            if (btnEdit.IsChecked == true)
            {
                dynamic data = sender;
                BussinessObject.Entities.Task task = data.SelectedItem;
                if (task.SuperTask == -1)
                {
                    goalForm = new GoalForm(task, weekyList);
                    goalForm.ShowDialog();
                }
                else
                {
                    taskForm = new TaskForm(task, weekyList);
                    taskForm.ShowDialog();
                    SetWeeky();
                }
                //todoForm.ShowDialog();
            }
        }

        private void btnEdit_Checked(object sender, RoutedEventArgs e)
        {
            btnMove.IsChecked = false;
            ClearSelection(tvTodolist);
            ClearSelection(tvWeekylist);
        }

        private void btnMove_Checked(object sender, RoutedEventArgs e)
        {
            btnEdit.IsChecked = false;
        }

        private void tabTodolist_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClearSelection(tvTodolist);
            ClearSelection(tvWeekylist);
        }

        private void tabWeekylist_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClearSelection(tvTodolist);
            ClearSelection(tvWeekylist);
        }

        private void dpTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic setTime = dpTime.SelectedDate;
            tbWeek.Text = "Week " + cal.GetWeekOfYear(setTime, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            SetWeekyWork();
        }

        private void ListView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (tabWeekylist.IsSelected) { 
                if (btnMove.IsChecked == true && tvWeekylist.SelectedItem != null)
                {
                    BussinessObject.Entities.Task task = tvWeekylist.SelectedItem as dynamic;
                    dynamic data = sender as dynamic;
                    DayInWeek dayInWeek = data.DataContext as DayInWeek;
                    dynamic setTime = dpTime.SelectedDate;
                    DateTime startDay = getDateTimeOfWeek(setTime, dayInWeek.DayName);
                    Work work = new Work()
                    {
                        TaskId = task.Id,
                        StartTime = startDay
                    };
                    WeekyTaskData weekyTaskData = new WeekyTaskData();
                    if (weekyTaskData.MakeWorkFromWeekyTask(work))
                    {
                        DayInWeek dataContext = data.DataContext as DayInWeek;
                        dataContext.MorningTask.Add(new WeekyWork(task, work));
                        CollectionViewSource.GetDefaultView(dataContext.MorningTask).Refresh();
                    }
                }
            }
        }
    }

}
