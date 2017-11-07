using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyPlan
{
    public class AlertMinute
    {
        public List<AMinute> ListMinute { get; set; }
        public class AMinute
        {
            public string Name { get; set; }
            public int? Value { get; set; }
            public AMinute(string name, int? value)
            {
                Name = name;
                Value = value;
            }
        }
        public AlertMinute()
        {
            ListMinute = new List<AMinute>();
            ListMinute.Add(new AMinute("None", null));
            ListMinute.Add(new AMinute("0 minute", 0));
            ListMinute.Add(new AMinute("5 minute", 5));
            ListMinute.Add(new AMinute("10 minute", 10));
            ListMinute.Add(new AMinute("15 minute", 15));
            ListMinute.Add(new AMinute("30 minute", 30));
            ListMinute.Add(new AMinute("45 minute", 45));
            ListMinute.Add(new AMinute("1 hour", 60));
            ListMinute.Add(new AMinute("12 hour", 720));
            ListMinute.Add(new AMinute("24 hour", 1400));
        }
    }
}
