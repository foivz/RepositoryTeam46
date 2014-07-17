using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.DataAccess;

namespace Seds.Web.UserControls
{
    public partial class UsersGrid : BaseGrid<User>
    {
        protected override bool HideIfNoItems
        {
            get
            {
                return true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSetup();


            if (!Page.IsPostBack)
            {
                BindGrid(SortingExpression, SortingDirection);
            }
        }
        private void PageSetup()
        {

        }

        protected override bool Delete(int id)
        {
            if (UserProfile != null)
            {
                if (id > 0)
                {
                    Repository.Users.Delete(id);
                    Repository.SaveChanges();
                    MessageBox.ShowMessage("Obrisano", true);

                }
            }

            return false;
        }

        public string GetRoleTitle(int? roleId)   // promjena tipa u nulabilni
        {

            Role role = Repository.Roles.GetById(roleId.Value);
            return role.Title;
        }

    }
}