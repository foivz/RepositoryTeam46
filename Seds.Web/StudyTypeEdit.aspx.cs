using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class StudyTypeEdit : BaseWebPage
    {
        protected StudyType _selectedStudyType = null;

        protected int? SelectedStudyTypeId
        {
            get
            {
                if (Request.QueryString["StudyTypeId"] != null)
                {
                    return int.Parse(Request.QueryString["StudyTypeId"]);
                }
                return null;
            }
        }

        public StudyType SelectedStudyType
        {
            get
            {
                if (_selectedStudyType == null)
                {
                    if (SelectedStudyTypeId.HasValue)
                    {
                        _selectedStudyType = Repository.StudyTypes.GetById(SelectedStudyTypeId.Value);
                    }
                }
                return _selectedStudyType;
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
            string name = SelectedStudyType != null ? string.Format(" - {0}", SelectedStudyType.Title) : "";

            PageSubTitle = string.Format("Uređivanje podataka o vrsti studija {0}", name);
            PageTitle = PageSubTitle;

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.StudyTypes;

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
                    if (SelectedStudyType == null)
                    {
                        _selectedStudyType = new StudyType();

                        Repository.StudyTypes.Add(SelectedStudyType);
                    }

                    SelectedStudyType.Title = txtTitle.Text.Trim();

                    Repository.SaveChanges();

                    MessageBox.ShowMessage("Padaci su spremljeni.", true, ResolveUrl("~/StudyTypes.aspx"));

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
                message = "Upišite naziv vrstes studija";
                return false;
            }

            return true;
        }

        private void BindForm()
        {
            if (SelectedStudyType != null)
            {
                txtTitle.Text = SelectedStudyType.Title;


            }
        }
    }
}