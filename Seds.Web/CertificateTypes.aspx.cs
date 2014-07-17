using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.DataAccess;

namespace Seds.Web
{
    public partial class CertificateTypes : BaseWebPage
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

            PageSubTitle = "Vrste diploma";
            PageTitle = PageSubTitle;

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.CertificateTypes;

            ucCertificateTypes.GetItems = Repository.CertificateTypes.GetAll;

        }
    }
}