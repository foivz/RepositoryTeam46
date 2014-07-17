//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Seds.DataAccess
//{
//    public partial class Role
//    {
//        public static List<Role> GetList()
//        {
//            using (var db = Repository.Instance)
//            {
//                return db.Roles.ToList();
//            }
//        }

//        public static Role GetbyId(int id)
//        {
//            using (var db = new SEDSEntities())
//            {
//                return (from r in db.Roles
//                        where r.Id == id
//                        select r).FirstOrDefault();
//            }
//        }
//        public static bool Save(Role role)
//        {
//            using (var db = Repository.Instance)
//            {
//                if (role.Id == 0)
//                {
//                    db.Roles.Add(role);
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
//        public static bool Delete(Role role)
//        {
//            var db = Repository.Instance;
//            {
//                if (role.Id == 0)
//                {
//                    db.Roles.Remove(role);
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
