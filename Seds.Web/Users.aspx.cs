using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.DataAccess;

namespace Seds.Web
{
    public partial class Users : BaseWebPage
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
            PageSubTitle = "Korisnici";
            PageTitle = PageSubTitle;
            LeftMenu.SelectedPage = UserControls.LeftMenuPage.Users;

            ucUsers.GetItems = Repository.Users.GetAll;

        }
       
    }
}