using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web.UserControls
{
    public partial class LeftMenu : System.Web.UI.UserControl
    {
        public LeftMenuPage SelectedPage { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string GetMenuSectionCssClass(LeftMenuSection section)
        {
            string selected = " in";

            if (section == LeftMenuSection.Page &&
                    (SelectedPage == LeftMenuPage.StudentsEdit ||
                     SelectedPage == LeftMenuPage.Students ||
                     SelectedPage == LeftMenuPage.CertificatesEdit ||
                     SelectedPage == LeftMenuPage.Certificates ||
                     SelectedPage == LeftMenuPage.Search
                     )
                )
            {
                return selected;
            }
            else if (section == LeftMenuSection.Settings &&
                        (SelectedPage == LeftMenuPage.StudyTypes ||
                        SelectedPage == LeftMenuPage.Studies ||
                        SelectedPage == LeftMenuPage.OrganizationalUnits ||
                        SelectedPage == LeftMenuPage.CourseGroups ||
                        SelectedPage == LeftMenuPage.CertificateTypes
                        )
                )
            {
                return selected;
            }

            else if (section == LeftMenuSection.Administration &&
                        (SelectedPage == LeftMenuPage.Users ||
                        SelectedPage == LeftMenuPage.Roles
                        )
                    )
            {
                return selected;
            }
            else
            {
                return "";
            }
        }

        protected string GetMenuPageCssClass(LeftMenuPage page)
        {
            string selected = "selectedItem";

            if (page == SelectedPage)
            {
                return selected;
            }
            else
            {
                return "";
            }
        }
    }

    public enum LeftMenuSection
    {
        Page,
        Settings,
        Administration
    }

    public enum LeftMenuPage
    {
        Search,
        Students,
        StudentsEdit,
        Certificates,
        CertificatesEdit,
        UserRoles,
        UserRolesEdit,
        Users,
        UsersEdit,
        StudyTypes,
        StudyTypesEdit,
        Studies,
        StudiesEdit,
        Roles,
        RolesEdit,
        OrganizationalUnits,
        OrganizationalUnitsEdit,
        CourseGroups,
        CourseGroupsEdit,
        CertificateTypes,
        CertificateTypesEdit



    }
}