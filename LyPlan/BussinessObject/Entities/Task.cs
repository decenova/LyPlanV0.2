using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BussinessObject.Entities
{
   public class Task
    {
        //public string name { get; set; }
        //public observablecollection<task> items { get; set; }
        //public task()
        //{
        //    Items = new ObservableCollection<Task>();
        //}
        private int id;
        private string title;
        private string description;
        private int typeId;
        private int superTask; //Lưu id của task cha
        private ObservableCollection<Task> items;

        public Task()
        {
            items = new ObservableCollection<Task>();
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
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

        public ObservableCollection<Task> Items
        {
            get { return items; }

            //Không cho vào vì làm j có set
            set { items = value; }
        }
    }
}
