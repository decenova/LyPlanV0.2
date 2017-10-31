﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Entities
{
    public class Todo
    {
        private int workId;
        private int taskId;
        private string title;
        private string description;
        private int statusId;

        public Todo()
        {

        }

        public int WorkId
        {
            get { return workId; }
            set { workId = value; }
        }

        public int TaskId
        {
            get { return taskId; }
            set { taskId = value; }
        }

        public String Title
        {
            get { return title; }
            set { title = value; }
        }

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }
    }

}
