using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//
namespace Seds.Web
{
    public partial class Certificates : BaseWebPage
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
            PageSubTitle = "Diplome";
            PageTitle = PageSubTitle;
            LeftMenu.SelectedPage = UserControls.LeftMenuPage.Certificates;
            ucCertificates.GetItems = Repository.Certificates.GetAll;
        }
    }
}