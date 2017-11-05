using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Entities
{
    public class DayInWeek
    {
        public DayOfWeek DayName { get; set; }
        public List<WeekyWork> MorningTask { get; set; }
        public List<WeekyWork> EverningTask { get; set; }
        public DayInWeek(DayOfWeek dayName)
        {
            DayName = dayName;
            MorningTask = new List<WeekyWork>();
            EverningTask = new List<WeekyWork>();
        }
    }
}
