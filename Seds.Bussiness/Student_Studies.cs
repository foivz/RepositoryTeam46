using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class Student_Studies
    {
        public static List<DataAccess.Student_Studies> GetList()
        {
            return DataAccess.Student_Studies.GetList();
        }
    }
}
