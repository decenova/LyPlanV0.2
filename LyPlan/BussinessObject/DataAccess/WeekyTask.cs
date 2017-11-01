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

        public readonly int TYPE_TODO = 1;
        public readonly int TYPE_DAILY = 2;
        public List<Task> GetListRootWeekyTaskForShow()
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
                Work work = new Work();
                work.Id = int.Parse(row["Id"].ToString());
                work.TaskId = int.Parse(row["TaskId"].ToString());
                work.Description = row["Description"].ToString();
                work.StartTime = DateTime.Parse(row["StartTime"].ToString());
                work.AlertTime = DateTime.Parse(row["AlertTime"].ToString());
                work.DeadLine = DateTime.Parse(row["Deadline"].ToString());
                work.StatusId = int.Parse(row["StatusId"].ToString());

                switch (work.StartTime.DayOfWeek.ToString())
                {
                    case "Monday":
                        monday.MorningTask.Add(work);
                        break;
                    case "Tuesday":
                        tuesday.MorningTask.Add(work);
                        break;
                    case "Wednesday":
                        wednesday.MorningTask.Add(work);
                        break;
                    case "Thursday":
                        thursday.MorningTask.Add(work);
                        break;
                    case "Friday":
                        friday.MorningTask.Add(work);
                        break;
                    case "Saturday":
                        saturday.MorningTask.Add(work);
                        break;
                    case "Sunday":
                        sunday.MorningTask.Add(work);
                        break;
                }
            }

            return result;
        }

        public DataTable GetWorkForShow()
        {
            DataTable result = new DataTable();

            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = $"select Id, TaskId, [Description], StartTime, AlertTime, Deadline, StatusId from Work where StatusId = {STATUS_EARLY}";
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
    }
}
