using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.DataAccess;

namespace Seds.Web.UserControls
{
    public partial class CertificatesGrid : BaseGrid<Certificate>
    {
        protected override bool HideIfNoItems
        {
            get
            {
                return true;
            }
        }
        private void PageSetup()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageSetup();

            if (!Page.IsPostBack)
            {
                BindGrid(SortingExpression, SortingDirection);
            }
        }

        protected override bool Delete(int id)
        {

            if (UserProfile != null)
            {
                if (id > 0)
                {
                    Repository.Certificates.Delete(id);
                    Repository.SaveChanges();
                    MessageBox.ShowMessage("Obrisano", true);

                }

            }

            return false;
        }

        public string GetStudyTypeTitle(int? TypeId)
        {
            StudyType studyType = Repository.StudyTypes.GetById(TypeId.Value);
            return studyType.Title;
        }
    }
}