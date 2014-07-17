using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class StudyTypes : BaseWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSetup();
            if (!Page.IsPostBack)
            {

            }

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.StudyTypes;
        }
        private void PageSetup()
        {
            PageSubTitle = "Vrsta studija";
            PageTitle = PageSubTitle;
            LeftMenu.SelectedPage = UserControls.LeftMenuPage.StudyTypes;
            ucStudyTypes.GetItems = Repository.StudyTypes.GetAll;

        }
    }
}