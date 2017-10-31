using BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BussinessObject.DataAccess
{
    public class TodoTask
    {
        public TodoTask()
        {

        }

        //lấy về 1 datatable các Task gồm (id, title)
        private DataTable GetTodoTasks()
        {
            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = "select Id, Title from Task where TypeId = 1";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtTodoTask = new DataTable();

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                
                da.Fill(dtTodoTask);
            }
            catch (SqlException se)
            {
                throw new Exception("Error: " + se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return dtTodoTask;
        }

        //Lấy ra WorkId và Description
        private Work GetTodoWorkForShow(int taskId)
        {
            Work result = null;

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;

            string SQL = "select Id, [Description] from Work where TaskId = " + taskId + " and StatusId = 1";

            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtTodoWork = new DataTable();

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                da.Fill(dtTodoWork);
                result = new Work();


                if (dtTodoWork.Rows.Count > 0)
                {
                    DataRow row = dtTodoWork.Rows[0];

                    result.Id = int.Parse(row["Id"].ToString());
                    result.Description = row["Description"].ToString();
                }
                
            }
            catch (SqlException se)
            {
                throw new Exception("Error: " + se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }

        public List<Todo> GetTodoListForShow()
        {
            List<Todo> result = new List<Todo>();

            foreach (DataRow row in GetTodoTasks().Rows)
            {
                Todo todo = new Todo();
                Work work = GetTodoWorkForShow(int.Parse(row["Id"].ToString()));

                Console.WriteLine(row["Id"].ToString());

                todo.TaskId = int.Parse(row["Id"].ToString());
                todo.WorkId = work.Id;
                todo.Title = row["Title"].ToString();
                todo.Description = work.Description;
                todo.StatusId = 1;

                result.Add(todo);
            }

            return result;
        }

        public Boolean SaveTodoTask(Todo todo)
        {
            Boolean result = false;

            string title = todo.Title;
            string description = todo.Description;

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;

            string SQL = $"insert into Task (Title, TypeId) output Inserted.Id values ('{title}', 1)";

            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                da.Fill(dt);

                int newId = int.Parse(dt.Rows[0]["Id"].ToString());

                SQL = $"insert into Work (TaskId, Description, StartTime, StatusId) values ({newId}, '{todo.Description}', '{DateTime.Now.ToString("yyyy-MM-dd")}', 1)";

                cmd = new SqlCommand(SQL, cnn);

                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException se)
            {
                throw new Exception("Error: " + se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }
    }
}
