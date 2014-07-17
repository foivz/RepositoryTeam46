using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Seds.Web.UserControls
{
    public partial class StudentsGrid : BaseGrid<Student>
    {
        protected override bool Delete(int id)
        {
            if (UserProfile != null)
            {
                if (id > 0)
                {
                    var student = Repository.Students.GetById(id);

                    if (student != null)
                    {
                        Repository.Students.Delete(student);

                        Repository.SaveChanges();

                        MessageBox.ShowMessage("Obrisano", true);
                    }
                }
            }

            return false;
        }
    }
}