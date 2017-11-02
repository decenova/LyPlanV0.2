using BussinessObject.DataAccess;
using BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Testing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //testing

            TodoTask todoTask = new TodoTask();
            WeekyTask weekyTask = new WeekyTask();

            //Create Update Check Todo

            //------------------------------------
            //Create TodoTask and TodoWork
            //todoTask.SaveTodoTask(new TodoWork()
            //{
            //    Title = "Đi chợ cho mẹ"
            //});
            //dgvTask.DataSource = todoTask.GetAllTodoWorkForShow();

            //------------------------------------
            //Update TodoTask and TodoWork
            //todoTask.UpdateTodo(new TodoWork()
            //{
            //    Title = "Đi chợ cho ba",
            //    Description = "Mua 5 con chó",
            //    TaskId = 32
            //});
            //dgvTask.DataSource = todoTask.GetAllTodoWorkForShow();

            //------------------------------------
            //Check TodoWork
            //todoTask.CheckTodo(new TodoWork()
            //{
            //    StatusId = weekyTask.STATUS_DONE,
            //    TaskId = 32
            //});
            //dgvTask.DataSource = todoTask.GetAllTodoWorkForShow();

            //Create Update RootTask

            //-----------------------------------
            //Create RootTask
            //weekyTask.SaveRootTask(new Task()
            //{
            //    Title = "New Root Task",
            //    Description = "The is the new root task"
            //});
            //dgvTask.DataSource = weekyTask.GetListAllRootWeekyTaskForShow();

            //----------------------------------
            //Update Root Task
            //weekyTask.UpdateTask(new Task()
            //{
            //    Title = "This task has been update",
            //    Description = "Update by Trung",
            //    Id = 33
            //});
            //dgvTask.DataSource = weekyTask.GetListAllRootWeekyTaskForShow();

            //Create Update Node Task

            //----------------------------------
            //Create Node Task
            //weekyTask.SaveNodeTask(new Task()
            //{
            //    Title = "this is a node task",
            //    Description = "This node task created by trung",
            //    SuperTask = 33
            //});

            //------------------------------------
            //Update Node Task
            //weekyTask.UpdateTask(new Task()
            //{
            //    Title = "This node task has been updated",
            //    Description = "Update by Trung",
            //    Id = 34
            //});

            //Create Update WeekyWork

            //-----------------------------------
            //Create WeekyWork
            //weekyTask.MakeWorkFromWeekyTask(new Work()
            //{
            //    TaskId = 34,
            //    Description = "Do it ASAP",
            //    StartTime = DateTime.Now,
            //    AlertTime = DateTime.Now.AddDays(1),
            //    DeadLine = DateTime.Now.AddDays(2)
            //});
            //dgvTask.DataSource = weekyTask.GetWorkForShow();

            //------------------------------------
            //Update WeekyWork
            //weekyTask.UpdateWeekyWork(new WeekyWork()
            //{
            //    Id = 19,
            //    Description = "This WeekyWork has been update",
            //    StartTime = DateTime.Now.AddYears(1),
            //    AlertTime = DateTime.Now.AddDays(1).AddYears(1),
            //    DeadLine = DateTime.Now.AddDays(2).AddYears(1)
            //});
            //dgvTask.DataSource = weekyTask.GetWorkForShow();

            //-----------------------------------
            //Check WeekyWork
            //weekyTask.ChangeWeekyWorkStatus(19, 5);
            //dgvTask.DataSource = weekyTask.GetWorkForShow();
            

        }

        private void dgvTask_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
