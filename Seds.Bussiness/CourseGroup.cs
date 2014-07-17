using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class CourseGroup
    {
        public static List<DataAccess.CourseGroup> GetList()
        {
            return DataAccess.CourseGroup.GetList();
        }
    }
}
