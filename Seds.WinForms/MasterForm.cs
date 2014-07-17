using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seds.WinForms
{
    public partial class MasterForm : BaseForm
    {
        public MasterForm()
        {
            InitializeComponent();

            OsvjeziListuStudija();

            dgvStudents.CellContentClick += dgvStudents_CellContentClick;
        }

        void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStudents.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex != -1)
            {
                int id = (int)((DataGridView)sender).Rows[e.RowIndex].Cells[2].Value;
                
                DeleteStudy(id);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OsvjeziListuStudija();
        }

        private void btnAddNewStudy_Click(object sender, EventArgs e)
        {
            Study study = new Study();
            
            study.CourseGroupId = 1;
            study.Title = "Preddiplomski studij poljskog";
            study.Duration = 3;
            study.ECTS = 180;
            study.TypeId = 1;

            Repository.Studies.Add(study);

            Repository.SaveChanges();

            OsvjeziListuStudija();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //    Study study = Repository.Studies.GetAll().OrderByDescending(s => s.Id).FirstOrDefault();

            //    if (study != null)
            //    {
            //        DeleteStudy(study);
            //    }
        }

        private void DeleteStudy(int id)
        {
            Repository.Studies.Delete(id);

            Repository.SaveChanges();

            OsvjeziListuStudija();
        }

        private void OsvjeziListuStudija()
        {
            dgvStudents.DataSource = Repository.Studies.GetAll().ToList();

            DataGridViewButtonColumn delColumn = new DataGridViewButtonColumn();
            delColumn.Text = "Delete";
            delColumn.Name = "Delete";
            delColumn.DataPropertyName = "Delete";

            dgvStudents.Columns.Add(delColumn);


            DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn();
            editColumn.Text = "Edit";
            editColumn.Name = "Edit";
            editColumn.DataPropertyName = "Edit";

            dgvStudents.Columns.Add(editColumn);
        }

        void dgvStudents_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnCertificateType_Click(object sender, EventArgs e)
        {
            CertificateTypeForm certificateTypeForm = new CertificateTypeForm();
            certificateTypeForm.Show();
        }
    }
}
