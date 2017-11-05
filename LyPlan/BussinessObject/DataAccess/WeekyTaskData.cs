using BussinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DataProvider;

namespace BussinessObject.DataAccess
{
    public class WeekyTaskData
    {
        public static readonly int STATUS_NOT_DONE = 1;
        public static readonly int STATUS_EARLY = 2;
        public static readonly int STATUS_DOING = 3;
        public static readonly int STATUS_LATE = 4;
        public static readonly int STATUS_DONE = 5;
        public static readonly int STATUS_REMOVED = 6;

        public static readonly int TYPE_TODO = 1;
        public static readonly int TYPE_DAILY = 2;

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
                    nodeTask.SuperTask = rootTask.Id;
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
            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "select Id, Title, [Description] from Task where TypeId = @TypeId and SuperTask is null";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@TypeId", TYPE_DAILY);

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
                //throw new Exception("Error: " + se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return dtWeekyTask;
        }

        //Lay id cua nut moi vua insert
        public DataTable GetInsertTaskId()
        {
            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "select top 1 Id from Task where TypeId = 2 order by Id desc";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtRootTaskId = new DataTable();

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                da.Fill(dtRootTaskId);
            }
            catch (SqlException se)
            {
                //throw new Exception("Error: " + se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return dtRootTaskId;
        }

        /// <summary>
        /// Lấy ra DataTable các NodeWeekyTask của 1 RootTask xác định
        /// </summary>
        /// <param name="taskId">taskId</param>
        /// <returns>DataTable các NodeWeekytask</returns>
        public DataTable GetListNodeWeekyTaskByTaskId(int taskId)
        {

            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "select Id, Title, [Description] from Task where TypeId = @TypeId and SuperTask = @TaskId";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@TypeId", TYPE_DAILY);
            cmd.Parameters.AddWithValue("@TaskId", taskId);

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
                //throw new Exception("Error: " + se.Message);
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

            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "insert into Task (Title, [Description], TypeId) values (@Title, @Description, @TypeId)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Title", task.Title);
            cmd.Parameters.AddWithValue("@Description", task.Description);
            cmd.Parameters.AddWithValue("@TypeId", TYPE_DAILY);

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
                result = false;
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

            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "insert into Task (Title, [Description], TypeId, SuperTask) values (@Title, @Description, @TypeId, @SuperTask)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Title", task.Title);
            cmd.Parameters.AddWithValue("@Description", task.Description);
            cmd.Parameters.AddWithValue("@TypeId", TYPE_DAILY);
            cmd.Parameters.AddWithValue("@SuperTask", task.SuperTask);

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
                result = false;
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

            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "insert into Work (TaskId, StartTime, StatusId)" +
                " values (@TaskId, @StartTime, @StatusId)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@TaskId", work.TaskId);
            cmd.Parameters.AddWithValue("@StartTime", work.StartTime);
            cmd.Parameters.AddWithValue("@StatusId", STATUS_EARLY);


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
                result = false;
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
        //Lay id cua nut moi vua insert
        public DataTable GetInsertWorkId()
        {
            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "select top 1 Id from Work order by Id desc";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtRootTaskId = new DataTable();

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                da.Fill(dtRootTaskId);
            }
            catch (SqlException se)
            {
                //throw new Exception("Error: " + se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return dtRootTaskId;
        }
        /// <summary>
        /// Lấy 1 list các DayInWeek để hiển thị
        /// </summary>
        /// <returns>List DayInWeek</returns>
        public List<DayInWeek> GetListDayInWeekForShow()
        {
            List<DayInWeek> result = new List<DayInWeek>();
            DayInWeek monday = new DayInWeek(DayOfWeek.Monday);
            DayInWeek tuesday = new DayInWeek(DayOfWeek.Tuesday);
            DayInWeek wednesday = new DayInWeek(DayOfWeek.Wednesday);
            DayInWeek thursday = new DayInWeek(DayOfWeek.Thursday);
            DayInWeek friday = new DayInWeek(DayOfWeek.Friday);
            DayInWeek saturday = new DayInWeek(DayOfWeek.Saturday);
            DayInWeek sunday = new DayInWeek(DayOfWeek.Sunday);

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
                if (row["StartTime"].ToString().Length > 0)
                    weekyWork.StartTime = DateTime.Parse(row["StartTime"].ToString());
                if (row["AlertTime"].ToString().Length > 0)
                    weekyWork.AlertTime = DateTime.Parse(row["AlertTime"].ToString());
                if (row["Deadline"].ToString().Length > 0)
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
        /// Lấy 1 list các DayInWeek để hiển thị
        /// </summary>
        /// <returns>List DayInWeek</returns>
        public List<DayInWeek> GetListDayInWeekForShow(DateTime startTime, DateTime endTime)
        {
            List<DayInWeek> result = new List<DayInWeek>();
            DayInWeek monday = new DayInWeek(DayOfWeek.Monday);
            DayInWeek tuesday = new DayInWeek(DayOfWeek.Tuesday);
            DayInWeek wednesday = new DayInWeek(DayOfWeek.Wednesday);
            DayInWeek thursday = new DayInWeek(DayOfWeek.Thursday);
            DayInWeek friday = new DayInWeek(DayOfWeek.Friday);
            DayInWeek saturday = new DayInWeek(DayOfWeek.Saturday);
            DayInWeek sunday = new DayInWeek(DayOfWeek.Sunday);

            result.Add(monday);
            result.Add(tuesday);
            result.Add(wednesday);
            result.Add(thursday);
            result.Add(friday);
            result.Add(saturday);
            result.Add(sunday);

            foreach (DataRow row in GetWorkForShow(startTime,endTime).Rows)
            {
                WeekyWork weekyWork = new WeekyWork();
                weekyWork.Id = int.Parse(row["Id"].ToString());
                weekyWork.TaskId = int.Parse(row["TaskId"].ToString());
                weekyWork.Title = row["Title"].ToString();
                weekyWork.Description = row["Description"].ToString();
                if (row["StartTime"].ToString().Length > 0)
                    weekyWork.StartTime = DateTime.Parse(row["StartTime"].ToString());
                if (row["AlertTime"].ToString().Length > 0)
                    weekyWork.AlertTime = DateTime.Parse(row["AlertTime"].ToString());
                if (row["Deadline"].ToString().Length > 0)
                    weekyWork.DeadLine = DateTime.Parse(row["Deadline"].ToString());
                weekyWork.StatusId = int.Parse(row["StatusId"].ToString());

                switch (weekyWork.StartTime.DayOfWeek.ToString())
                {
                    case "Monday":
                        addWeekyWorkToDay(monday, weekyWork);
                        break;
                    case "Tuesday":
                        addWeekyWorkToDay(tuesday, weekyWork);
                        break;
                    case "Wednesday":
                        addWeekyWorkToDay(wednesday, weekyWork);
                        break;
                    case "Thursday":
                        addWeekyWorkToDay(thursday, weekyWork);
                        break;
                    case "Friday":
                        addWeekyWorkToDay(friday, weekyWork);
                        break;
                    case "Saturday":
                        addWeekyWorkToDay(saturday, weekyWork);
                        break;
                    case "Sunday":
                        addWeekyWorkToDay(sunday, weekyWork);
                        break;
                }
            }

            return result;
        }

        private void addWeekyWorkToDay(DayInWeek dayInWeek, WeekyWork weekyWork)
        {
            switch (weekyWork.StatusId)
            {
                case 2:
                case 3:
                case 4:
                case 1:
                    dayInWeek.MorningTask.Add(weekyWork);
                    break;
                case 5:
                    dayInWeek.EverningTask.Add(weekyWork);
                    break;
            }
        }
        /// <summary>
        /// Lấy DataTable Work chưa hoàn thành để hiển thị
        /// </summary>
        /// <returns>DataTable Work</returns>
        public DataTable GetWorkForShow()
        {
            DataTable result = new DataTable();

            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "select w.Id, TaskId, t.Title, w.[Description], StartTime, AlertTime, Deadline, StatusId" +
                " from Work w inner join Task t on w.TaskId = t.Id" +
                " where t.TypeId = @TypeId" +
                " and StatusId in (1,2,3,4,5)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@TypeId", TYPE_DAILY);

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
                //throw new Exception("Error: " + se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }

        /// <summary>
        /// Lấy DataTable Work chưa hoàn thành để hiển thị
        /// </summary>
        /// <returns>DataTable Work</returns>
        public DataTable GetWorkForShow(DateTime startTime, DateTime endTime)
        {
            DataTable result = new DataTable();

            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "select w.Id, TaskId, t.Title, w.[Description], StartTime, AlertTime, Deadline, StatusId" +
                " from Work w inner join Task t on w.TaskId = t.Id" +
                " where t.TypeId = @TypeId" +
                " and StartTime between @start and @end" +
                " and StatusId in (1,2,3,4,5)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@TypeId", TYPE_DAILY);
            cmd.Parameters.AddWithValue("@start", startTime);
            cmd.Parameters.AddWithValue("@end", endTime);

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
                //throw new Exception("Error: " + se.Message);
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }

        /// <summary>
        /// Change status cho WeekyWok
        /// </summary>
        /// <param name="workId">workId</param>
        /// <param name="statusId">statusId</param>
        /// <returns>Success: true</returns>
        public Boolean ChangeWeekyWorkStatus(int workId, int statusId)
        {
            Boolean result = false;

            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "update Work set StatusId = @statusId where Id = @WorkId";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@WorkId", workId);
            cmd.Parameters.AddWithValue("@statusId", statusId);

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
                result = false;
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }


        /// <summary>
        /// Update Task
        /// </summary>
        /// <param name="task">Title, Description, TaskId</param>
        /// <returns>Success: true</returns>
        public Boolean UpdateTask(Task task)
        {
            Boolean result = false;

            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "update Task Set Title = @Title, [Description] = @Description where Id = @TaskId";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Title", task.Title);
            cmd.Parameters.AddWithValue("@Description", task.Description);
            cmd.Parameters.AddWithValue("@TaskId", task.Id);

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
                result = false;
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }

        /// <summary>
        /// Update 1 WeekyWork
        /// </summary>
        /// <param name="weekyWork">Id, Description, StartTime, AlertTime, Deadline</param>
        /// <returns></returns>
        public Boolean UpdateWeekyWork(WeekyWork weekyWork)
        {
            Boolean result = false;

            string strConnection = DataProvider.DataProvider.getConnectionString();
            //string SQL = "update Work set [Description] = @Description, StartTime = @StartTime, AlertTime = @AlertTime, Deadline = @Deadline where Id = @WorkId";
            string SQL = "update Work set [Description] = @Description where Id = @WorkId";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@Description", weekyWork.Description);
            cmd.Parameters.AddWithValue("@WorkId", weekyWork.Id);

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
                result = false;
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

        public Boolean RemoveTask(Task task)
        {
            Boolean result = false;

            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "update Task set TypeId = @TypeId where Id = @Id";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);

            cmd.Parameters.AddWithValue("@TypeId", 3);
            cmd.Parameters.AddWithValue("@Id", task.Id);

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
                result = false;
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }
        public Boolean DeleteWork(WeekyWork weekyWork)
        {
            Boolean result = false;

            string strConnection = DataProvider.DataProvider.getConnectionString();
            string SQL = "delete Work where Id = @Id";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);

            cmd.Parameters.AddWithValue("@Id", weekyWork.Id);

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
                result = false;
            }
            finally
            {
                cnn.Close();
            }

            return result;
        }
    }
}