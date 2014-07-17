using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class StudyType
    {
        public static List<DataAccess.StudyType> GetList()
        {
            return DataAccess.StudyType.GetList();
        }
    }
}
