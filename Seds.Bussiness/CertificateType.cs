using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.Bussiness
{
    public class CertificateType
    {
        public static List<DataAccess.CertificateType> GetList()
        {
            return DataAccess.CertificateType.GetList();
        }
    }
}
