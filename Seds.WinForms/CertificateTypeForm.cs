using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seds.DataAccess;

namespace Seds.WinForms
{
    public partial class CertificateTypeForm : BaseForm
    {
        public CertificateTypeForm()
        {
            InitializeComponent();

            SelectCertificateType();
            
            dgvCertificateType.CellContentClick += dgvCertificateType_CellContentClick;
        }

        void dgvCertificateType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void SelectCertificateType()
        {
            dgvCertificateType.DataSource = Repository.CertificateTypes.GetAll().ToList();

        }

        private void btnSaveCertificateType_Click(object sender, EventArgs e)
        {
            CertificateType certificateType = new CertificateType();

            certificateType.Title = textBox1.Text;

            Repository.CertificateTypes.Add(certificateType);
            Repository.SaveChanges();
            
        }
    }
}
