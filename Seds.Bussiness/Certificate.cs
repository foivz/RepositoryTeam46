using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class Certificate
    {
        public static List<DataAccess.Certificate> GetList()
        {
            return DataAccess.Certificate.GetList();

        }
       
    }
}
