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
    public class TodoTaskData
    {
        public TodoTaskData()
        {

        }

        /// <summary>
        /// Lấy DataTable
        /// </summary>
        /// <returns>1 datatable các Task gồm (id, title)</returns>
        private DataTable GetTodoTasks()
        {
            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = "select t.Id as Id, t.Title as Title, w.Description, w.StatusId from Task t inner join Work w on t.Id = w.TaskId where TypeId = 1 and StatusId = 1";
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

        /// <summary>
        /// Lấy ra 1 Work xác định
        /// </summary>
        /// <param name="taskId">taskId</param>
        /// <returns>1 Work gồm Id và Description</returns>
        private Work GetTodoWorkForShow(int taskId)
        {
            Work result = null;

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;

            string SQL = "select Id, [Description] from Work where TaskId = @TaskId and StatusId = 1";

            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@TaskId", taskId);
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

        /// <summary>
        /// Lấy ra tất cả các TodoWork để show lên
        /// </summary>
        /// <returns>List các TodoWork</returns>
        public List<TodoWork> GetAllTodoWorkForShow()
        {
            List<TodoWork> result = new List<TodoWork>();

            foreach (DataRow row in GetTodoTasks().Rows)
            {
                TodoWork todo = new TodoWork();
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

        /// <summary>
        /// Lưu todo task và work
        /// </summary>
        /// <param name="todo">Title và Description</param>
        /// <returns>Success: True</returns>
        public Boolean SaveTodoTask(TodoWork todo)
        {
            Boolean result = false;

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;

            string SQL = "insert into Task (Title, TypeId) output Inserted.Id values (@Title, @TypeId)";

            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Title", todo.Title);
            cmd.Parameters.AddWithValue("TypeId", WeekyTaskData.TYPE_TODO);
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

                string nSQL = "insert into Work (TaskId, Description, StartTime, StatusId) values (@NewId, @Description, @StartTime, @StatusId)";

                cmd = new SqlCommand(nSQL, cnn);

                cmd.Parameters.AddWithValue("@NewId", newId);
                cmd.Parameters.AddWithValue("@Description", todo.Description);
                cmd.Parameters.AddWithValue("@StartTime", DateTime.Now.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@StatusId", WeekyTaskData.STATUS_NOT_DONE);

                result = cmd.ExecuteNonQuery() == 1;
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

        /// <summary>
        /// Update todo task và work
        /// </summary>
        /// <param name="newTodo">Title, Description, TaskId</param>
        /// <returns>Success: True</returns>
        public Boolean UpdateTodo(TodoWork newTodo)
        {
            Boolean result = false;

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = "update Task set Title = @Title where Id = @TaskId";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Title", newTodo.Title);
            cmd.Parameters.AddWithValue("@TaskId", newTodo.TaskId);

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                result = cmd.ExecuteNonQuery() > 0;

                string nSQL = "update Work set [Description] = @Description where TaskId = @TaskId";

                cmd = new SqlCommand(nSQL, cnn);

                cmd.Parameters.AddWithValue("@Description", newTodo.Description);
                cmd.Parameters.AddWithValue("@TaskId", newTodo.TaskId);

                result = cmd.ExecuteNonQuery() == 1;
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

        /// <summary>
        /// Thay đổi trạng thái todo work
        /// 1: Not Done
        /// 2: Early
        /// 3: Doing
        /// 4: Late
        /// 5: Done
        /// 6: Removed
        /// </summary>
        /// <param name="newTodo">StatusId, TaskId</param>
        /// <returns>Success: true</returns>
        public Boolean CheckTodo(TodoWork newTodo)
        {
            Boolean result = false;

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = "update Work set StatusId = @StatusId where TaskId = @TaskId";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);

            cmd.Parameters.AddWithValue("@StatusId", newTodo.StatusId);
            cmd.Parameters.AddWithValue("@TaskId", newTodo.TaskId);

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                result = cmd.ExecuteNonQuery() == 1;
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
