using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyPlan
{
    class TaskDemo
    {
        public string Name { get; set; }
        public ObservableCollection<TaskDemo> Items { get; set; }
        public TaskDemo()
        {
            Items = new ObservableCollection<TaskDemo>();
        }
    }
}
