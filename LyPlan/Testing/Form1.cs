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
using System.Threading.Tasks;
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
            

        }

        private void dgvTask_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
