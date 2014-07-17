using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class User
    {
        public static List<DataAccess.User> GetList()
        {
            return DataAccess.User.GetList();
        }
    }
}
