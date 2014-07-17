//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Seds.DataAccess
//{
//    public partial class OrganizationalUnit
//    {
//        public static List<OrganizationalUnit> GetList()
//        {
//            using (var db = Repository.Instance)
//            {
//                return db.OrganizationalUnits.ToList();
//            }
//        }

//        public static OrganizationalUnit GetById(int id)
//        {
//            using (var db = new SEDSEntities())
//            {
//                return (from ou in db.OrganizationalUnits
//                        where ou.Id == id
//                        select ou).FirstOrDefault();
//            }
//        }

//        public static bool Save(OrganizationalUnit organizationalUnit)
//        {
//            using (var db = Repository.Instance)
//            {
//                if (organizationalUnit.Id == 0)
//                {
//                    db.OrganizationalUnits.Add(organizationalUnit);
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

//        public static bool Delete(OrganizationalUnit organizationalUnit)
//        {
//            var db = Repository.Instance;
//            {
//                if (organizationalUnit.Id == 0)
//                {
//                    db.OrganizationalUnits.Remove(organizationalUnit);
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
