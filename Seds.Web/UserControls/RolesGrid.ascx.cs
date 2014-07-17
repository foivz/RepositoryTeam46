using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.DataAccess;

namespace Seds.Web.UserControls
{
    public partial class RolesGrid : BaseGrid<Role>
    {
        protected override bool HideIfNoItems
        {
            get
            {
                return true;
            }
        }
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    PageSetup();


        //    if (!Page.IsPostBack)
        //    {
        //        BindGrid(SortingExpression, SortingDirection);  
                  // zbog ovog sortingexpression nije radilo jer u 
        //        //toj metodi se sortira po lastname a ja nemam 
        //        //last name pa sam stavio return ID u InitialSortColumn

        //    }
        //}
        //private void PageSetup()
        //{

        //}

        protected override bool Delete(int id)
        {
            if (UserProfile != null)
            {
                if (id > 0)
                {
                    Repository.Roles.Delete(id);
                    Repository.SaveChanges();
                    MessageBox.ShowMessage("Obrisano", true);
                }
            }
            return false;
        }
    }
}