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
        public DataTable GetListRootWeekyTask()
        {
            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            string SQL = "select Id, Title, [Description] from Task where TypeId = 2 and SuperTask is null";
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
            string SQL = "select Id, Title, [Description] from Task where TypeId = 2 and SuperTask = " + taskId;
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
            string SQL = $"insert into Task (Title, [Description], TypeId) values ('{task.Title}', '{task.Description}', 2)";
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
            string SQL = $"insert into Task (Title, [Description], TypeId, SuperTask) values ('{task.Title}', '{task.Description}', 2, {task.SuperTask})";
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
            string SQL = $"insert into Work (TaskId, Description, StartTime, Deadline, AlertTime, StatusId) values ({work.TaskId}, '{work.Description}', '{work.StartTime.ToString("yyyy-MM-dd")}', '{work.DeadLine.ToString("yyyy-MM-dd")}', '{work.AlertTime.ToString("yyyy-MM-dd")}', 2)";
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
    }
}
