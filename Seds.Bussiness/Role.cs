using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class Role
    {
        public static List<DataAccess.Role> GetList()
        {
            return DataAccess.Role.GetList();
        }
    }
}
