using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Entities
{
    class Work
    {
        private int id;
        private int taskId;
        private string description;
        private DateTime startTime;
        private DateTime deadline;
        private DateTime alertTime;
        private int statusId;

        public int Id
        {
            get { return id; }

            //vì id k có set mà tự động tăng
            //set { id = value; }
        }

        public int TaskId
        {
            get { return taskId; }
            set { taskId = value; }
        }

        public string Description
        {
            get { return description; }
            set
            {
                //Nếu string rỗng thì lưu vào database là null
                if (value.Equals(String.Empty))
                {
                    value = null;
                }
                description = value;
            }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                //Thời gian bắt đầu phải sau giờ hiện tại
                if (value.CompareTo(DateTime.Now) < 0)
                {
                    throw new Exception("You can't start work in the past");
                }

                startTime = value;
            }
        }

        public DateTime DeadLine
        {
            get { return deadline; }
            set
            {
                //Thời gian kết thúc phải sau giờ bắt đầu
                if (value.CompareTo(startTime) < 0)
                {
                    throw new Exception("You can't finish befor you begin");
                }

                deadline = value;
            }
        }

        public DateTime AlertTime
        {
            get { return alertTime; }
            set
            {
                //Thời gian thông báo phải giữa thời gian bắt đầu và thời gian kết thúc
                if (value.CompareTo(startTime) < 0 || value.CompareTo(deadline) > 0)
                {
                    throw new Exception("Alert time must between StartTime: " + startTime + " and Deadline: " + deadline);
                }

                alertTime = value;
            }
        }

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }
    }
}
