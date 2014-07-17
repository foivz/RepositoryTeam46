using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class OrganizationalUnits : BaseWebPage
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
            PageSubTitle = "Ustrojbene jedinice";
            PageTitle = PageSubTitle;
            LeftMenu.SelectedPage = UserControls.LeftMenuPage.OrganizationalUnits;

            ucOrganizationalUnits.GetItems = Repository.OrganizationalUnits.GetAll;
        }
    }
}