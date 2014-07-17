using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class StudyEdit : BaseWebPage
    {
        protected Study _selectedStudy = null;

        protected int? SelectedStudyId
        {
            get
            {
                if (Request.QueryString["StudyId"] != null)
                {
                    return int.Parse(Request.QueryString["StudyId"]);
                }
                return null;
            }
        }

        public Study SelectedStudy
        {
            get
            {
                if (_selectedStudy == null)
                {
                    if (SelectedStudyId.HasValue)
                    {
                        _selectedStudy = Repository.Studies.GetById(SelectedStudyId.Value);
                    }
                }
                return _selectedStudy;
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

        #region Events

        protected override void OnInit(EventArgs e)
        {
            btnSave.Click += btnSave_Click;
            base.OnInit(e);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion

        private void PageSetup()
        {
            string name = SelectedStudy != null ? string.Format(" - {0}", SelectedStudy.Title) : "";

            PageSubTitle = string.Format("Uređivanje podataka o studiju {0}", name);
            PageTitle = PageSubTitle;

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.Studies;

            if (!IsPostBack)
            {
                //ddlRole.Items.Add(new ListItem("Odaberi", string.Empty));

                foreach (CourseGroup courseGroup in Repository.CourseGroups.GetAll())
                {
                    ddlCourseGroup.Items.Add(new ListItem(courseGroup.Title, courseGroup.Id.ToString()));
                }

                ddlCourseGroup.Items.Insert(0, new ListItem("-- Odaberi --", string.Empty));
                ddlCourseGroup.SelectedValue = string.Empty;

                foreach (StudyType studyType in Repository.StudyTypes.GetAll())
                {
                    ddlStudyType.Items.Add(new ListItem(studyType.Title, studyType.Id.ToString()));
                }
                ddlStudyType.Items.Insert(0, new ListItem("-- Odaberi --", string.Empty));
                ddlStudyType.SelectedValue = string.Empty;
            }

        }

        private bool Save()
        {
            if (UserProfile != null)
            {
                string message;

                if (IsFromValid(out message))
                {
                    if (SelectedStudy == null)
                    {
                        _selectedStudy = new Study();

                        Repository.Studies.Add(SelectedStudy);
                    }

                    SelectedStudy.CourseGroupId = int.Parse(ddlCourseGroup.SelectedValue);
                    SelectedStudy.Title = txtTitle.Text.Trim();
                    SelectedStudy.Duration = int.Parse(txtDuration.Text.Trim());
                    SelectedStudy.ECTS = int.Parse(txtEcts.Text.Trim());
                    SelectedStudy.TypeId = int.Parse(ddlStudyType.SelectedValue);

                    Repository.SaveChanges();

                    MessageBox.ShowMessage("Padaci su spremljeni.", true, ResolveUrl("~/Studies.aspx"));
                    return true;
                }
                else
                {
                    MessageBox.ShowError(message);
                    return false;
                }
            }
            else
            {
                MessageBox.ShowError("Nepostojeci korisnik");
                return false;
            }
                       
        }

        private bool IsFromValid(out string message)
        {
            message = null;

            if (string.IsNullOrWhiteSpace(txtTitle.Text.Trim()))
            {
                message = "Upišite naziv";
                return false;
            }

            if (ddlCourseGroup.SelectedValue == string.Empty)
            {
                message = "Odaberite studijsku grupu";
                return false;
            }

            if (ddlStudyType.SelectedValue == string.Empty)
            {
                message = "Odaberite tip studija";
                return false;
            }


            return true;
        }

        private void BindForm()
        {
            if (SelectedStudy != null)
            {
                ddlCourseGroup.SelectedValue = SelectedStudy.CourseGroupId.ToString();
                txtTitle.Text = SelectedStudy.Title;
                txtDuration.Text = SelectedStudy.Duration.ToString();
                txtEcts.Text = SelectedStudy.ECTS.ToString();
                ddlStudyType.SelectedValue = SelectedStudy.TypeId.ToString();

            }
        }
    }
}