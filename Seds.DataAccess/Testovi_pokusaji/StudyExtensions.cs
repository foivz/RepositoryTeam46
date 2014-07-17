//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Seds.DataAccess
//{
//    public partial class Study
//    {
//        public static List<DataAccess.Study> GetList()
//        {
//            var db = Repository.Instance;
//            {
//                return db.Studies.ToList();
//            }
//        }

//        public static Study GetbyId(int id)
//        {
//            using (var db = new SEDSEntities())
//            {
//                return (from s in db.Studies
//                        where s.Id == id
//                        select s).FirstOrDefault();
//            }
//        }
        
//        public static bool Save(Study study)
//        {
//            using (var db = Repository.Instance)
//            {
//                if (study.Id == 0)
//                {
//                    db.Studies.Add(study);
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
//        public static bool Delete(Study study)
//        {
//            var db = Repository.Instance;
//            {
//                if (study.Id > 0)
//                {
//                    db.Studies.Remove(study);
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
