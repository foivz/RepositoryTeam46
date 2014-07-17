//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Seds.DataAccess
//{
//    public partial class CourseGroup
//    {
//        public static List<CourseGroup> GetList()
//        {
//            using (var db = Repository.Instance)
//            {
//                return db.CourseGroups.ToList();
//            }
//        }

//        public static CourseGroup GetById(int id)
//        {
//            using (var db = new SEDSEntities())
//            {
//                return (from cg in db.CourseGroups
//                        where cg.Id == id
//                        select cg).FirstOrDefault();
//            }
//        }

//        public static bool Save (CourseGroup courseGroup)
//        {
//            using (var db=Repository.Instance)
//            {
//                if (courseGroup.Id ==0)
//                {
//                    db.CourseGroups.Add(courseGroup);
//                }
//                if (db.SaveChanges()==0)
//                {
//                    return false;
//                }
//                else
//                {
//                    return true;
//                }
//            }
//        }

//        public static bool Delete(CourseGroup courseGroup)
//        {
//            var db = Repository.Instance;
//            {
//                if (courseGroup.Id == 0)
//                {
//                    db.CourseGroups.Remove(courseGroup);
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
