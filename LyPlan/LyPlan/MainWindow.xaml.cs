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

            //testing
            WeekyTask weekyTask = new WeekyTask();
            TodoTask todoTask = new TodoTask();
            //test insert root task
            //weekyTask.SaveRootTask(new Task() { Title = "Lalaland", Description = "Lalaland is a movie" 
            //test insert node task
            //weekyTask.SaveNodeTask(new Task() { Title = "Chapter 1", Description = "Chapter1", SuperTask = 20 });
            //test make work from weeky task
            //weekyTask.MakeWorkFromWeekyTask(new Work()
            //{
            //    TaskId = 23,
            //    Description = "Monday",
            //    StartTime = DateTime.Now,
            //    AlertTime = DateTime.Now.AddDays(1),
            //    DeadLine = DateTime.Now.AddDays(2)                
            //});
            //test update todo
            //todoTask.UpdateTodo(new Todo()
            //{
            //    TaskId = 1,
            //    WorkId = 1,
            //    Title = "clean cai con cac",
            //    Description = "clean con cacccccccc"
            //});
            //test check todo
            //todoTask.CheckTodo(new Todo()
            //{
            //    TaskId = 1,
            //    StatusId = 5
            //});

            
            SetTodo();
            SetWeeky();
            SetWeekyWork();

            //DayInWeek monday = new DayInWeek("Monday");
            //monday.MorningTask.Add("Lala");
            //monday.MorningTask.Add("Lala");
            //cMonday.DataContext = monday;
            //DayInWeek tuesday = new DayInWeek("Tuesday");
            //cTuesday.DataContext = tuesday;
        }

        private void SetWeekyWork()
        {
            WeekyTask weekyTask = new WeekyTask();

            List<DayInWeek> listDayInWeek = weekyTask.GetListDayInWeekForShow();
            
            foreach (DayInWeek day in listDayInWeek)
            {
                switch (day.DayName)
                {
                    case "Monday":
                        cMonday.DataContext = day;
                        break;
                    case "Tuesday":
                        cTuesday.DataContext = day;
                        break;
                    case "Wednesday":
                        cWednesday.DataContext = day;
                        break;
                    case "Thursday":
                        cThursday.DataContext = day;
                        break;
                    case "Friday":
                        cFriday.DataContext = day;
                        break;
                    case "Saturday":
                        cSaturday.DataContext = day;
                        break;
                    case "Sunday":
                        cSunday.DataContext = day;
                        break;
                }
            }
        }

        private void SetWeeky()
        {
            WeekyTask weekyTask = new WeekyTask();

            foreach (Task task in weekyTask.GetListRootWeekyTaskForShow())
            {
                tvWeekylist.Items.Add(task);
            }
        }

        private void SetTodo()
        {
            TodoTask todoTask = new TodoTask();
            foreach (Todo todo in todoTask.GetAllTodoListForShow()) 
            {
                tvTodolist.Items.Add(todo);
            }
        }


    }
    
}
