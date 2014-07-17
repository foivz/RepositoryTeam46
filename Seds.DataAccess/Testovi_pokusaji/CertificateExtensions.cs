//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Seds.DataAccess
//{
//    public partial class Certificate
//    {
//        public static List<Certificate> GetList()
//        {
//            using (var db = Repository.Instance)
//            {
//                return db.Certificates.ToList();
//            }
//        }

//        public static Certificate GetById(int id)
//        {
//            using (var db = Repository.Instance)
//            {
//                return (from c in db.Certificates
//                        where c.Id == id
//                        select c).FirstOrDefault();
//            }
//        }
//        public static bool Save(Certificate certificate)
//        {
//            using (var db = Repository.Instance)
//            {
//                if (certificate.Id == 0)
//                {
//                    db.Certificates.Add(certificate);
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

//        public static bool Delete(Certificate certificate)
//        {
//            var db = Repository.Instance;
//            {
//                if (certificate.Id > 0)
//                {
//                    db.Certificates.Remove(certificate);
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
