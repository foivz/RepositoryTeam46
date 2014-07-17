//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Seds.DataAccess
//{
//    public partial class User
//    {
//        public static List<User> GetList()
//        {
//            using (var db = Repository.Instance)
//            {
//                return db.Users.ToList();
//            }
//        }
//        public static User GetById(int id)
//        {
//            using (var db = new SEDSEntities())
//            {
//                return (from u in db.Users
//                        where u.Id == id
//                        select u).FirstOrDefault();
//            }
//        }
//        public static bool Save(User user)
//        {
//            using (var db = Repository.Instance)
//            {
//                if (user.Id == 0)
//                {
//                    db.Users.Add(user);
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
//        public static bool Delete(User user)
//        {
//            var db = Repository.Instance;
//            {
//                if (user.Id == 0)
//                {
//                    db.Users.Remove(user);
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
