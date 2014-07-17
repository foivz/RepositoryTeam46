using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class CourseGroupEdit : BaseWebPage
    {
        protected CourseGroup _selectedCourseGroup = null;

        protected int? SelectedCourseGroupId
        {
            get
            {
                if (Request.QueryString["CourseGroupId"] != null)
                {
                    return int.Parse(Request.QueryString["CourseGroupId"]);
                }
                return null;
            }
        }

        public CourseGroup SelectedCourseGroup
        {
            get
            {
                if (_selectedCourseGroup == null)
                {
                    if (SelectedCourseGroupId.HasValue)
                    {
                        _selectedCourseGroup = Repository.CourseGroups.GetById(SelectedCourseGroupId.Value);
                    }
                }
                return _selectedCourseGroup;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSetup();

            if (!Page.IsPostBack)
            {
                BindForm();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            btnSave.Click += btnSave_Click;
            base.OnInit(e);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }


        private void PageSetup()
        {
            string name = SelectedCourseGroup != null ? string.Format(" - {0}", SelectedCourseGroup.Title) : "";

            PageSubTitle = string.Format("Uređivanje podataka o studijskoj grupi {0}", name);
            PageTitle = PageSubTitle;

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.CourseGroups;

            //if (!IsPostBack)
            //{
            //    //ddlRole.Items.Add(new ListItem("Odaberi", string.Empty));

            //    foreach (Role role in Repository.Roles.GetAll())
            //    {
            //        ddlRole.Items.Add(new ListItem(role.Title, role.Id.ToString()));
            //    }

            //    ddlRole.Items.Insert(0, new ListItem("Odaberi", string.Empty));

            //    ddlRole.SelectedValue = string.Empty;
            //}

        }

        private bool Save()
        {
            if (UserProfile != null)
            {
                string message;

                if (IsFromValid(out message))
                {
                    if (SelectedCourseGroup == null)
                    {
                        _selectedCourseGroup = new CourseGroup();

                        Repository.CourseGroups.Add(SelectedCourseGroup);
                    }

                    SelectedCourseGroup.Title = txtTitle.Text.Trim();
                    SelectedCourseGroup.TitleName = txtTitleName.Text.Trim();

                    Repository.SaveChanges();

                    MessageBox.ShowMessage("Padaci su spremljeni.", true, ResolveUrl("~/CourseGroups.aspx"));

                }
                else
                {
                    MessageBox.ShowError(message);
                    return false;
                }
            }

            return true;
        }

        private bool IsFromValid(out string message)
        {
            message = null;

            if (string.IsNullOrWhiteSpace(txtTitle.Text.Trim()))
            {
                message = "Upišite naziv studijske grupe";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTitleName.Text.Trim()))
            {
                message = "Upišite puni naziv";
                return false;
            }

            return true;
        }

        private void BindForm()
        {
            if (SelectedCourseGroup != null)
            {
                txtTitle.Text = SelectedCourseGroup.Title;
                txtTitleName.Text = SelectedCourseGroup.TitleName;


            }
        }
    }
}