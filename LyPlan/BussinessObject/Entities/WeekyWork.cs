using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Entities
{
    public class WeekyWork
    {
        private int id;
        private int taskId;
        private string title;
        private string description;
        private DateTime startTime;
        private DateTime? deadline;
        private DateTime? alertTime;
        private int statusId;

        public WeekyWork()
        {

        }
        public WeekyWork(Task task, Work work)
        {
            id = work.Id;
            taskId = task.Id;
            title = task.Title;
            description = work.Description;
            startTime = work.StartTime;
            deadline = work.DeadLine;
            alertTime = work.AlertTime;
            statusId = work.StatusId;
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int TaskId
        {
            get { return taskId; }
            set { taskId = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public DateTime? DeadLine
        {
            get { return deadline; }
            set { deadline = value; }
        }

        public DateTime? AlertTime
        {
            get { return alertTime; }
            set { alertTime = value; }
        }

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }
    }
}
