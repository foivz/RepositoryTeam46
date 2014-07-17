using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class Home : BaseWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LeftMenu.SelectedPage = UserControls.LeftMenuPage.Search;   
        }
    }
}