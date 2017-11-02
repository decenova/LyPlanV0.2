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
    public class WeekyTask
    {
        public readonly int STATUS_NOT_DONE = 1;
        public readonly int STATUS_EARLY = 2;
        public readonly int STATUS_DOING = 3;
        public readonly int STATUS_LATE = 4;
        public readonly int STATUS_DONE = 5;
        public readonly int STATUS_REMOVED = 6;

        public readonly int TYPE_TODO = 1;
        public readonly int TYPE_DAILY = 2;

        /// <summary>
        /// Lấy ra list các  RootWeekyTask để hiển thị
        /// </summary>
        /// <returns>List các RootTask gồm Id, Title, Description</returns>
        public List<Task> GetListAllRootWeekyTaskForShow()
        {
            List<Task> result = new List<Task>();

            foreach (DataRow rootRow in GetListRootWeekyTask().Rows)
            {
                Task rootTask = new Task();
                rootTask.Id = int.Parse(rootRow["Id"].ToString());
                rootTask.Title = rootRow["Title"].ToString();
                rootTask.Description = rootRow["Description"].ToString();

                foreach (DataRow nodeRow in GetListNodeWeekyTaskByTaskId(rootTask.Id).Rows)
                {
                    Task nodeTask = new Task();
                    nodeTask.Id = int.Parse(nodeRow["Id"].ToString());
                    nodeTask.Title = nodeRow["Title"].ToString();
                    nodeTask.Description = nodeRow["Description"].ToString();

                    rootTask.Items.Add(nodeTask);
                }

                result.Add(rootTask);
            }

            return result;
        }

        /// <summary>
        /// Lấy DataTable các rootWeekytask
        /// </summary>
        /// <returns>DataTable RootWeekyTask gồm Id, Title, Description</returns>
        public DataTable GetListRootWeekyTask()
        {
            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = $"select Id, Title, [Description] from Task where TypeId = {TYPE_DAILY} and SuperTask is null";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtWeekyTask = new DataTable();

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                da.Fill(dtWeekyTask);
            }
            catch (SqlException se)
            {
                throw new Exception("Error: " + se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return dtWeekyTask;
        }

        /// <summary>
        /// Lấy ra DataTable các NodeWeekyTask của 1 RootTask xác định
        /// </summary>
        /// <param name="taskId">taskId</param>
        /// <returns>DataTable các NodeWeekytask</returns>
        public DataTable GetListNodeWeekyTaskByTaskId(int taskId)
        {

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = $"select Id, Title, [Description] from Task where TypeId = {TYPE_DAILY} and SuperTask = " + taskId;
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtWeekyTask = new DataTable();

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                da.Fill(dtWeekyTask);
            }
            catch (SqlException se)
            {
                throw new Exception("Error: " + se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return dtWeekyTask;
        }

        /// <summary>
        /// Tạo 1 root task
        /// </summary>
        /// <param name="task">Title, Description</param>
        /// <returns>Success: true</returns>
        public Boolean SaveRootTask(Task task)
        {
            Boolean result = false;

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = $"insert into Task (Title, [Description], TypeId) values ('{task.Title}', '{task.Description}', {TYPE_DAILY})";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

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

        /// <summary>
        /// Tạo 1 node task
        /// </summary>
        /// <param name="task">Title, Description, SuperTask</param>
        /// <returns>Success: true</returns>
        public Boolean SaveNodeTask(Task task)
        {
            Boolean result = false;

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = $"insert into Task (Title, [Description], TypeId, SuperTask) values ('{task.Title}', '{task.Description}', {TYPE_DAILY}, {task.SuperTask})";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

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

        /// <summary>
        /// Tạo 1 Work từ cái NodeTask
        /// </summary>
        /// <param name="work">TaskId, Description, StartTime, ArletTime, DeadLine</param>
        /// <returns>Success: true</returns>
        public Boolean MakeWorkFromWeekyTask(Work work)
        {
            Boolean result = false;

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = $"insert into Work (TaskId, Description, StartTime, Deadline, AlertTime, StatusId) values ({work.TaskId}, '{work.Description}', '{work.StartTime.ToString("yyyy-MM-dd")}', '{work.DeadLine.ToString("yyyy-MM-dd")}', '{work.AlertTime.ToString("yyyy-MM-dd")}', {STATUS_EARLY})";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

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

        /// <summary>
        /// Lấy 1 list các DayInWeek để hiển thị
        /// </summary>
        /// <returns>List DayInWeek</returns>
        public List<DayInWeek> GetListDayInWeekForShow()
        {
            List<DayInWeek> result = new List<DayInWeek>();
            DayInWeek monday    = new DayInWeek("Monday");
            DayInWeek tuesday   = new DayInWeek("Tuesday");
            DayInWeek wednesday = new DayInWeek("Wednesday");
            DayInWeek thursday  = new DayInWeek("Thursday");
            DayInWeek friday    = new DayInWeek("Friday");
            DayInWeek saturday  = new DayInWeek("Saturday");
            DayInWeek sunday    = new DayInWeek("Sunday");

            result.Add(monday);
            result.Add(tuesday);
            result.Add(wednesday);
            result.Add(thursday);
            result.Add(friday);
            result.Add(saturday);
            result.Add(sunday);

            foreach (DataRow row in GetWorkForShow().Rows)
            {
                WeekyWork weekyWork = new WeekyWork();
                weekyWork.Id = int.Parse(row["Id"].ToString());
                weekyWork.TaskId = int.Parse(row["TaskId"].ToString());
                weekyWork.Title = row["Title"].ToString();
                weekyWork.Description = row["Description"].ToString();
                weekyWork.StartTime = DateTime.Parse(row["StartTime"].ToString());
                weekyWork.AlertTime = DateTime.Parse(row["AlertTime"].ToString());
                weekyWork.DeadLine = DateTime.Parse(row["Deadline"].ToString());
                weekyWork.StatusId = int.Parse(row["StatusId"].ToString());

                switch (weekyWork.StartTime.DayOfWeek.ToString())
                {
                    case "Monday":
                        monday.MorningTask.Add(weekyWork);
                        break;
                    case "Tuesday":
                        tuesday.MorningTask.Add(weekyWork);
                        break;
                    case "Wednesday":
                        wednesday.MorningTask.Add(weekyWork);
                        break;
                    case "Thursday":
                        thursday.MorningTask.Add(weekyWork);
                        break;
                    case "Friday":
                        friday.MorningTask.Add(weekyWork);
                        break;
                    case "Saturday":
                        saturday.MorningTask.Add(weekyWork);
                        break;
                    case "Sunday":
                        sunday.MorningTask.Add(weekyWork);
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Lấy DataTable Work chưa hoàn thành để hiển thị
        /// </summary>
        /// <returns>DataTable Work</returns>
        public DataTable GetWorkForShow()
        {
            DataTable result = new DataTable();

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = $"select w.Id, TaskId, t.Title, w.[Description], StartTime, AlertTime, Deadline, StatusId from Work w inner join Task t on w.TaskId = t.Id where w.StatusId = {STATUS_EARLY}";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                da.Fill(result);
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
        /// Check Done cho 1 WeekyWork
        /// </summary>
        /// <param name="workId">workId</param>
        /// <returns>Success: true</returns>
        public Boolean CheckDoneWeekyWork(int workId)
        {
            Boolean result = false;

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = $"update Work set StatusId = {STATUS_DONE} where Id = {workId}";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            
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
