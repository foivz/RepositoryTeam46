using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class CertificateTypeEdit : BaseWebPage
    {
        protected CertificateType _selectedCertificateType = null;

        protected int? SelectedCertificateTypeId
        {
            get
            {
                if (Request.QueryString["CertificateTypeId"] != null)
                {
                    return int.Parse(Request.QueryString["CertificateTypeId"]);
                }
                return null;
            }
        }

        public CertificateType SelectedCertificateType
        {
            get
            {
                if (_selectedCertificateType == null)
                {
                    if (SelectedCertificateTypeId.HasValue)
                    {
                        _selectedCertificateType = Repository.CertificateTypes.GetById(SelectedCertificateTypeId.Value);
                    }
                }
                return _selectedCertificateType;
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
            string name = SelectedCertificateType != null ? string.Format(" - {0}", SelectedCertificateType.Title) : "";

            PageSubTitle = string.Format("Uređivanje podataka o dokumentima {0}", name);
            PageTitle = PageSubTitle;

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.CertificateTypes;

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
                    if (SelectedCertificateType == null)
                    {
                        _selectedCertificateType = new CertificateType();

                        Repository.CertificateTypes.Add(SelectedCertificateType);
                    }

                    SelectedCertificateType.Title = txtTitle.Text.Trim();

                    Repository.SaveChanges();

                    MessageBox.ShowMessage("Padaci su spremljeni.", true, ResolveUrl("~/CertificateType.aspx"));

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
                message = "Upišite naziv dokumenta ili tipa diplome";
                return false;
            }

            return true;
        }

        private void BindForm()
        {
            if (SelectedCertificateType != null)
            {
                txtTitle.Text = SelectedCertificateType.Title;


            }
        }
    }
}