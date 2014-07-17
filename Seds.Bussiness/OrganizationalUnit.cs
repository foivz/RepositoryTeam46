using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class OrganizationalUnit
    {
        public static List<DataAccess.OrganizationalUnit> GetList()
        {
            return DataAccess.OrganizationalUnit.GetList();
        }
    }
}
