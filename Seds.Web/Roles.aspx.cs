using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.DataAccess;

namespace Seds.Web
{
    public partial class Roles : BaseWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSetup();

            if (!Page.IsPostBack)
            {

            }

        }
        private void PageSetup()
        {
            PageSubTitle = "Korisničke uloge";
            PageTitle = PageSubTitle;
            LeftMenu.SelectedPage = UserControls.LeftMenuPage.Roles;

            ucRoles.GetItems = Repository.Roles.GetAll;
        }
    }
}