using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyPlan
{
    class Task
    {
        public string Name { get; set; }
        public ObservableCollection<Task> Items { get; set; }
        public Task()
        {
            Items = new ObservableCollection<Task>();
        }
    }
}
