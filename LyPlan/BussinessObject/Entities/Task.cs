using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Entities
{
    class Task
    {
        private int id;
        private string title;
        private string description;
        private int typeId;
        private int superTask; //Lưu id của task cha

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public string Title
        {
            get { return title; }
            set
            {
                if (value.Length >= 225)
                {
                    throw new Exception("Title is too long (<= 255 character)");
                }

                title = value;
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                //Nếu description là rỗng thì đưa vào database là null
                if (value.Equals(string.Empty))
                {
                    value = null;
                }

                description = value;
            }
        }

        public int TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        public int SuperTask
        {
            get { return superTask; }

            //Chưa kiểm tra task tầng thứ 3
            set { superTask = value; }
        }
    }
}
