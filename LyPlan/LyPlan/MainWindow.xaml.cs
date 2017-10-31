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
            todoTask.CheckTodo(new Todo()
            {
                TaskId = 1,
                StatusId = 5
            });

            SetTodo();
            SetWeeky();
            DayInWeek monday = new DayInWeek("Monday");
            monday.MorningTask.Add("Lala");
            monday.MorningTask.Add("Lala");
            cMonday.DataContext = monday;
            DayInWeek tuesday = new DayInWeek("Tuesday");
            cTuesday.DataContext = tuesday;
        }

        private void SetWeeky()
        {
            WeekyTask weekyTask = new WeekyTask();

            foreach (DataRow rootRow in weekyTask.GetListRootWeekyTask().Rows)
            {
                Task rootTask = new Task();
                rootTask.Id = int.Parse(rootRow["Id"].ToString());
                rootTask.Title = rootRow["Title"].ToString();
                rootTask.Description = rootRow["Description"].ToString();
                
                foreach (DataRow nodeRow in weekyTask.GetListNodeWeekyTaskByTaskId(rootTask.Id).Rows)
                {
                    Task nodeTask = new Task();
                    nodeTask.Id = int.Parse(nodeRow["Id"].ToString());
                    nodeTask.Title = nodeRow["Title"].ToString();
                    nodeTask.Description = nodeRow["Description"].ToString();

                    rootTask.Items.Add(nodeTask);
                }

                tvWeekylist.Items.Add(rootTask);
            }
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
    public class DayInWeek
    {
        public string Day { get; set; }
        public List<Object> MorningTask { get; set; }
        public List<Object> NightTask { get; set; }
        public DayInWeek(string day)
        {
            Day = day;
            MorningTask = new List<Object>();
            NightTask = new List<Object>();
        }
    }
}
