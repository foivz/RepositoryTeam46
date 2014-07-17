using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class Student
    {
        public static List<DataAccess.Student> GetList()
        {
            return DataAccess.Student.GetList();

        }
    }
}
