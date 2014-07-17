//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Seds.DataAccess
//{
//    public partial class Student
//    {
//        public static List<Student> GetList()
//        {
//            using (var db = Repository.Instance)
//            {
//                return db.Students.ToList();
//            }
//        }

//        public static Student GetById(int id)
//        {
//            using (var db = Repository.Instance)
//            {
//                return (from s in db.Students
//                        where s.Id == id
//                        select s).FirstOrDefault();
//            }
//        }
//        public static bool Save(Student student)
//        {
//            using (var db = Repository.Instance)
//            {
//                if (student.Id == 0)
//                {
//                    db.Students.Add(student);
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
//        public static bool Delete(Student student)
//        {
//            var db = Repository.Instance;
//            {
//                if (student.Id == 0)
//                {
//                    db.Students.Remove(student);
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
