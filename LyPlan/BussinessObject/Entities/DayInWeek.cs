using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Entities
{
    public class DayInWeek
    {
        public string DayName { get; set; }
        public List<Work> MorningTask { get; set; }
        public List<Work> EverningTask { get; set; }
        public DayInWeek(string day)
        {
            DayName = day;
            MorningTask = new List<Work>();
            EverningTask = new List<Work>();
        }
    }
}
