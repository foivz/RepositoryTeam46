//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Seds.DataAccess
//{
//    public partial class StudyType
//    {
//        public static List<StudyType> GetList()
//        {
//            using (var db = new SEDSEntities())
//            {
//                return db.StudyTypes.ToList();
//            }
//        }
//        public static StudyType GetById(int id)
//        {
//            using (var db = new SEDSEntities())
//            {
//                return (from st in db.StudyTypes
//                        where st.Id == id
//                        select st).FirstOrDefault();
//            }
//        }

//        public static bool Save(StudyType studyType)
//        {
//            using (var db = Repository.Instance)
//            {
//                if (studyType.Id == 0)
//                {
//                    db.StudyTypes.Add(studyType);
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
//        public static bool Delete(StudyType studyType)
//        {
//            var db = Repository.Instance;
//            {
//                if (studyType.Id == 0)
//                {
//                    db.StudyTypes.Remove(studyType);
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
