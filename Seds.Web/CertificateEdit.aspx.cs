using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class CertificateEdit : BaseWebPage
    {
        protected Certificate _selectedCertificate = null;

        protected int? SelectedCertificateId
        {
            get
            {
                if (Request.QueryString["CertificateId"] != null)
                {
                    return int.Parse(Request.QueryString["CertificateId"]);
                }
                return null;
            }
        }

        public Certificate SelectedCertificate
        {
            get
            {
                if (_selectedCertificate == null)
                {
                    if (SelectedCertificateId.HasValue)
                    {
                        _selectedCertificate = Repository.Certificates.GetById(SelectedCertificateId.Value);
                    }
                }
                return _selectedCertificate;
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
            string name = SelectedCertificate != null ? string.Format(" - {0}", SelectedCertificate.CertificateNumber) : "";

            PageSubTitle = string.Format("Uređivanje podataka o diplomama {0}", name);
            PageTitle = PageSubTitle;

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.Certificates;

            if (!IsPostBack)
            {
                //ddlRole.Items.Add(new ListItem("Odaberi", string.Empty));

                foreach (CertificateType certificateType in Repository.CertificateTypes.GetAll())
                {
                    ddlTypeId.Items.Add(new ListItem(certificateType.Title, certificateType.Id.ToString()));
                }

                ddlTypeId.Items.Insert(0, new ListItem("-- Odaberi --", string.Empty));

                ddlTypeId.SelectedValue = string.Empty;
            }

        }

        private bool Save()
        {
            if (UserProfile != null)
            {
                string message;

                if (IsFromValid(out message))
                {
                    if (SelectedCertificate == null)
                    {
                        _selectedCertificate = new Certificate();

                        Repository.Certificates.Add(SelectedCertificate);
                    }

                    SelectedCertificate.CertificateNumber = txtCertificateNumber.Text.Trim();
                    SelectedCertificate.TypeId = int.Parse(ddlTypeId.SelectedValue);
                    SelectedCertificate.GraduationDate = dpGraduationDate.Date;

                    Repository.SaveChanges();

                    MessageBox.ShowMessage("Padaci su spremljeni.", true, ResolveUrl("~/Certificates.aspx"));

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

            if (string.IsNullOrWhiteSpace(txtCertificateNumber.Text.Trim()))
            {
                message = "Upišite broj diplome";
                return false;
            }

            if (ddlTypeId.SelectedValue == string.Empty)
            {
                message = "Odaberite ulogu";
                return false;
            }


            return true;
        }

        private void BindForm()
        {
            if (SelectedCertificate != null)
            {
                txtCertificateNumber.Text = SelectedCertificate.CertificateNumber;
                ddlTypeId.SelectedValue = SelectedCertificate.TypeId.ToString();
                dpGraduationDate.Date = SelectedCertificate.GraduationDate;

            }
        }

        #endregion
    }
}