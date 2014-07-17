using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class RoleEdit : BaseWebPage
    {
        protected Role _selectedRole = null;

        protected int? SelectedRoleId
        {
            get
            {
                if (Request.QueryString["RoleId"] != null)
                {
                    return int.Parse(Request.QueryString["RoleId"]);
                }
                return null;
            }
        }

        public Role SelectedRole
        {
            get
            {
                if (_selectedRole == null)
                {
                    if (SelectedRoleId.HasValue)
                    {
                        _selectedRole = Repository.Roles.GetById(SelectedRoleId.Value);
                    }
                }
                return _selectedRole;
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
            string name = SelectedRole != null ? string.Format(" - {0}", SelectedRole.Title) : "";

            PageSubTitle = string.Format("Uređivanje podataka o ulogama {0}", name);
            PageTitle = PageSubTitle;

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.Users;

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
                    if (SelectedRole == null)
                    {
                        _selectedRole = new Role();

                        Repository.Roles.Add(SelectedRole);
                    }

                    SelectedRole.Title = txtTitle.Text.Trim();

                    Repository.SaveChanges();

                    MessageBox.ShowMessage("Padaci su spremljeni.", true, ResolveUrl("~/Roles.aspx"));

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
                message = "Upišite naziv role";
                return false;
            }

            return true;
        }

        private void BindForm()
        {
            if (SelectedRole != null)
            {
                txtTitle.Text = SelectedRole.Title;
                

            }
        }
    }
}