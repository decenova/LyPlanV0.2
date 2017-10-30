using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    public class DataProvider
    {
        public DataProvider()
        {

        }

        public static string getConnectionString()
        {
            string strConnection = ConfigurationManager.ConnectionStrings["LyPlan"].ConnectionString;
            return strConnection;
        }
    }
}
