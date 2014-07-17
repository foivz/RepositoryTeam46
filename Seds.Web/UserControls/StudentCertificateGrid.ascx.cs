using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web.UserControls
{
    public partial class StudentCertificateGrid : BaseGrid<DataAccess.Certificate>
    {
        public Student SelectedStudent { get; set; }

        private List<CertificateType> _certificateType = null;

        private List<CertificateType> CertificateTypes
        {
            get
            {
                if (_certificateType == null)
                {
                    _certificateType = Repository.CertificateTypes.GetAll().ToList();
                }
                return _certificateType;
            }
        }

        protected override string InitialSortColumn
        {
            get
            {
                return "Id";
            }
        }

        protected override bool HideIfNoItems
        {
            get
            {
                return true;
            }
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            PageSetup();

            if (!Page.IsPostBack)
            {
                BindGrid(SortingExpression, SortingDirection);
            }
        }

        private void PageSetup()
        {

        }

        protected override bool Delete(int certificateId)
        {
            if (UserProfile != null)
            {
                if (certificateId > 0 && SelectedStudent != null)
                {
                    Certificate certificate = Repository.DbContext.Students.Where(s => s.Id == SelectedStudent.Id)
                                                                           .SelectMany(s => s.Certificates)
                                                                           .FirstOrDefault(c => c.Id == certificateId);
                    if (certificate != null)
                    {
                        Repository.Certificates.Delete(certificate);
                        Repository.SaveChanges();
                        MessageBox.ShowMessage("Diploma je obrisana", true, string.Format("/StudentEdit.aspx?StudentId={0}&StudentEditTab={1}", SelectedStudent.Id, StudentEditTab.Certificates));
                    }
                }
            }

            return false;
        }

        public string GetTypeTitle(Certificate certificate)
        {
            return CertificateTypes.FirstOrDefault(c => c.Id == certificate.TypeId).Title;
        }
    }
}