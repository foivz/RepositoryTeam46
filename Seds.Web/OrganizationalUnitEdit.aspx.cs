using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class OrganizationalUnitEdit : BaseWebPage
    {
        protected OrganizationalUnit _selectedOrganizationalUnit = null;

        protected int? SelectedOrganizationalUnitId
        {
            get
            {
                if (Request.QueryString["OrganizationalUnitId"] != null)
                {
                    return int.Parse(Request.QueryString["OrganizationalUnitId"]);
                }
                return null;
            }
        }

        public OrganizationalUnit SelectedOrganizationalUnit
        {
            get
            {
                if (_selectedOrganizationalUnit == null)
                {
                    if (SelectedOrganizationalUnitId.HasValue)
                    {
                        _selectedOrganizationalUnit = Repository.OrganizationalUnits.GetById(SelectedOrganizationalUnitId.Value);
                    }
                }
                return _selectedOrganizationalUnit;
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
            string name = SelectedOrganizationalUnit != null ? string.Format(" - {0}", SelectedOrganizationalUnit.Title) : "";

            PageSubTitle = string.Format("Uređivanje podataka o ustrojbenoj jedinici {0}", name);
            PageTitle = PageSubTitle;

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.OrganizationalUnits;

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
                    if (SelectedOrganizationalUnit == null)
                    {
                        _selectedOrganizationalUnit = new OrganizationalUnit();

                        Repository.OrganizationalUnits.Add(SelectedOrganizationalUnit);
                    }

                    SelectedOrganizationalUnit.Title = txtTitle.Text.Trim();
                    SelectedOrganizationalUnit.TitleName = txtTitleName.Text.Trim();
                    SelectedOrganizationalUnit.IdIsvu = txtIsvuId.Text.Trim();

                    Repository.SaveChanges();

                    MessageBox.ShowMessage("Padaci su spremljeni.", true, ResolveUrl("~/OrganizationalUnits.aspx"));

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
                message = "Upišite naziv ustrojbene jedinice";
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
            if (SelectedOrganizationalUnit != null)
            {
                txtTitle.Text = SelectedOrganizationalUnit.Title;
                txtTitleName.Text = SelectedOrganizationalUnit.TitleName;
                txtIsvuId.Text = SelectedOrganizationalUnit.IdIsvu;


            }
        }
    }
}