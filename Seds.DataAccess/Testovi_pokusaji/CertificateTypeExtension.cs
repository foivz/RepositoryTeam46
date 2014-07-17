//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Seds.DataAccess
//{
//    public partial class CertificateType
//    {
//        public static List<CertificateType> GetList()
//        {
//            using (var db = new SEDSEntities())
//            {
//                return db.CertificateTypes.ToList();
//            }
//        }
//        public static CertificateType GetById(int id)
//        {
//            using (var db = new SEDSEntities())
//            {
//                return (from ct in db.CertificateTypes
//                        where ct.Id == id
//                        select ct).FirstOrDefault();
//            }
//        }

//        public static bool Save(CertificateType certificateType)
//        {
//            using (var db = Repository.Instance)
//            {
//                if (certificateType.Id == 0)
//                {
//                    db.CertificateTypes.Add(certificateType);
//                }
//                if (db.SaveChanges() == 0)
//                {
//                    return false;
//                }
//                else
//                {
//                    return true;
//                }
//            }
//        }

//        public static bool Delete(CertificateType certificateType)
//        {
//            var db = Repository.Instance;
//            {
//                if (certificateType.Id == 0)
//                {
//                    db.CertificateTypes.Remove(certificateType);
//                }
//                if (db.SaveChanges() == 0)
//                {
//                    return false;
//                }
//                else
//                {
//                    return true;
//                }
//            }
//        }
//    }
//}
