using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Entities
{
    public class DayInWeek
    {
        public DayOfWeek DayName { get; set; }
        public ObservableCollection<WeekyWork> MorningTask { get; set; }
        public ObservableCollection<WeekyWork> EverningTask { get; set; }
        public DayInWeek(DayOfWeek dayName)
        {
            DayName = dayName;
            MorningTask = new ObservableCollection<WeekyWork>();
            EverningTask = new ObservableCollection<WeekyWork>();
        }
    }
}
