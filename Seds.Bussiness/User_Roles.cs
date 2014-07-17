using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class User_Roles
    {
        public static List<DataAccess.User_Roles> GetList()
        {
            return DataAccess.User_Roles.GetList();
        }
    }
}
