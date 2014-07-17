using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class Study
    {
        public static List<DataAccess.Study> GetList()
        {
            return DataAccess.Study.GetList();
        }
    }
}
