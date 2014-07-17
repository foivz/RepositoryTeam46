using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class UserEdit : BaseWebPage
    {
        protected User _selectedUser = null;

        protected int? SelectedUserId
        {
            get
            {
                if (Request.QueryString["UserId"] != null)
                {
                    return int.Parse(Request.QueryString["UserId"]);
                }
                return null;
            }
        }

        public User SelectedUser
        {
            get
            {
                if (_selectedUser == null)
                {
                    if (SelectedUserId.HasValue)
                    {
                        _selectedUser = Repository.Users.GetById(SelectedUserId.Value);
                    }
                }
                return _selectedUser;
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

        #region Private

        private void PageSetup()
        {
            string name = SelectedUser != null ? string.Format(" - {0} {1}", SelectedUser.FirstName, SelectedUser.LastName) : "";

            PageSubTitle = string.Format("Uređivanje podataka o korisniku {0}", name);
            PageTitle = PageSubTitle;

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.Users;

            if (!IsPostBack)
            {
                //ddlRole.Items.Add(new ListItem("Odaberi", string.Empty));

                foreach (Role role in Repository.Roles.GetAll())
                {
                    ddlRole.Items.Add(new ListItem(role.Title, role.Id.ToString()));
                }

                ddlRole.Items.Insert(0, new ListItem("Odaberi", string.Empty));

                ddlRole.SelectedValue = string.Empty;
            }

        }

        private bool Save()
        {
            if (UserProfile != null)
            {
                string message;

                if (IsFromValid(out message))
                {
                    if (SelectedUser == null)
                    {
                        _selectedUser = new User();

                        Repository.Users.Add(SelectedUser);
                    }

                    SelectedUser.UserName = txtUserName.Text.Trim();
                    SelectedUser.Email = txtEmail.Text.Trim();
                    SelectedUser.FirstName = txtFirstName.Text.Trim();
                    SelectedUser.LastName = txtLastName.Text.Trim();
                    SelectedUser.Password = Login.UnixCrypt.Crypt(txtPassword.Text.Trim());
                    SelectedUser.RoleId = int.Parse(ddlRole.SelectedValue);

                    Repository.SaveChanges();

                    MessageBox.ShowMessage("Padaci su spremljeni.", true, ResolveUrl("~/Users.aspx"));

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

            if (string.IsNullOrWhiteSpace(txtUserName.Text.Trim()))
            {
                message = "Upišite korisničko ime";
                return false;
            }

            if (ddlRole.SelectedValue == string.Empty)
            {
                message = "Odaberite ulogu";
                return false;
            }


            return true;
        }

        private void BindForm()
        {
            if (SelectedUser != null)
            {
                txtEmail.Text = SelectedUser.Email;
                txtFirstName.Text = SelectedUser.FirstName;
                txtLastName.Text = SelectedUser.LastName;
                txtUserName.Text = SelectedUser.UserName;
                ddlRole.SelectedValue = SelectedUser.RoleId.ToString();

            }
        }

        #endregion
    }
}