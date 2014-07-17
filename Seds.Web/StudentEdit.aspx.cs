using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    /// <summary>
    /// 
    /// </summary>
    public partial class StudentEdit : BaseWebPage
    {
        protected Student _selectedStudent = null;
        private List<Study> _studies = null;

        private List<Study> Studies
        {
            get
            {
                if (_studies == null)
                {
                    _studies = Repository.Studies.GetAll().ToList();
                }
                return _studies;
            }
        }

        protected int SelectedStudentId
        {
            get
            {
                string studentId = Request.QueryString["StudentId"];
                if (string.IsNullOrWhiteSpace(studentId))
                {
                    return 0;
                }
                return int.Parse(studentId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Student SelectedStudent
        {
            get
            {
                if (_selectedStudent == null)
                {
                    if (SelectedStudentId > 0)
                    {
                        _selectedStudent = Repository.Students.GetById(SelectedStudentId);
                    }
                }
                return _selectedStudent;
            }
        }

        private StudentEditTab? _selectedTab = null;

        protected StudentEditTab SelectedTab
        {
            get
            {
                if (_selectedTab == null)
                {
                    if (Request.QueryString["StudentEditTab"] != null)
                    {
                        _selectedTab = (StudentEditTab)Enum.Parse(typeof(StudentEditTab), Request.QueryString["StudentEditTab"]);
                    }
                    else if (string.IsNullOrWhiteSpace(hndSelectedTab.Value))
                    {
                        _selectedTab = StudentEditTab.Main;
                    }
                    else
                    {
                        _selectedTab = (StudentEditTab)Enum.Parse(typeof(StudentEditTab), hndSelectedTab.Value); //eksplicitni cast
                    }
                }
                return _selectedTab.Value;
            }
            set
            {
                _selectedTab = value;
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

        /// <summary>
        /// 
        /// </summary>
        private void AddStudy()
        {
            if (UserProfile != null && SelectedStudent != null && !string.IsNullOrWhiteSpace(ddlStudies.SelectedValue))
            {
                int studyId = int.Parse(ddlStudies.SelectedValue);
                Student_Studies studentStudy = Repository.StudentStudies.GetAll().FirstOrDefault(ss => ss.StudyId == studyId && ss.StudentId == SelectedStudent.Id);

                if (studentStudy == null)
                {
                    studentStudy = new Student_Studies()
                    {
                        EnrollmentYear = dpEnrollmentYear.Date,
                        StudentId = SelectedStudent.Id,
                        StudyId = studyId
                    };

                    Repository.StudentStudies.Add(studentStudy);
                    Repository.SaveChanges();
                    MessageBox.ShowMessage("Dodano", true, string.Format("/StudentEdit.aspx?StudentId={0}&StudentEditTab={1}", SelectedStudent.Id, StudentEditTab.Study));
                }
                else
                {
                    MessageBox.ShowError("Odabir već postoji");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddCertificate()
        {
            if (UserProfile != null && SelectedStudent != null && !string.IsNullOrWhiteSpace(ddlCertificateType.SelectedValue))
            {
                int typeId = int.Parse(ddlCertificateType.SelectedValue);

                Certificate studentCertificate = new Certificate()
                  {
                      CertificateNumber = txtCertificateNumber.Text.Trim(),
                      TypeId = typeId,
                      GraduationDate = dpGraduationDate.Date
                  };

                studentCertificate.Students.Add(SelectedStudent);

                Repository.Certificates.Add(studentCertificate);
                Repository.SaveChanges();
                MessageBox.ShowMessage("Dodano", true, string.Format("/StudentEdit.aspx?StudentId={0}&StudentEditTab={1}", SelectedStudent.Id, StudentEditTab.Certificates));
            }
        }


        //void btnDelete_Click(object sender, EventArgs e)
        //{
        //    Delete();
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion

        #region Private

        /// <summary>
        /// 
        /// </summary>
        private void PageSetup()
        {
            string studentName = SelectedStudent != null ? string.Format(" - {0} {1}", SelectedStudent.FirstName, SelectedStudent.LastName) : "";

            PageSubTitle = string.Format("Uređivanje podataka o studentu{0}", studentName);
            PageTitle = PageSubTitle;

            LeftMenu.SelectedPage = UserControls.LeftMenuPage.Students;
            ucStudentStudiesGrid.SelectedStudent = SelectedStudent;
            ucStudentCertificateGrid.SelectedStudent = SelectedStudent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool Save()
        {
            if (UserProfile != null)
            {
                string message;

                if (IsFromValid(out message))
                {
                    if (SelectedStudent == null)
                    {
                        _selectedStudent = new Student();

                        Repository.Students.Add(SelectedStudent);
                    }

                    SelectedStudent.FirstName = txtFirstName.Text.Trim();
                    SelectedStudent.LastName = txtLastName.Text.Trim();
                    SelectedStudent.BirthLastName = txtBirthLastName.Text.Trim();
                    SelectedStudent.Gender = int.Parse(ddlGender.SelectedValue);
                    SelectedStudent.BirthDate = dpBirthDate.Date;
                    SelectedStudent.JMBG = txtJMBG.Text.Trim();
                    SelectedStudent.OIB = txtOIB.Text.Trim();
                    SelectedStudent.UniqueNumber = int.Parse(txtUniqueNumber.Text.Trim());

                    if (txtAverageRating.Text.Trim() != null && txtAverageRating.Text.Trim() != "")
                    {
                        SelectedStudent.AverageRating = decimal.Parse(txtAverageRating.Text.Trim());
                    }
                    else
                    {
                        SelectedStudent.AverageRating = 0.00m;
                    }

                    SelectedStudent.AverageGroup1 = decimal.Parse(txtAverageRating1.Text);
                    SelectedStudent.AverageGroup2 = decimal.Parse(txtAverageRating2.Text.Trim());
                    SelectedStudent.Verified = chkVerified.Checked;
                    SelectedStudent.Email = txtEmail.Text.Trim();
                    SelectedStudent.PhoneNumber = txtPhoneNumber.Text.Trim();
                    SelectedStudent.ImagePath = txtImagePath.Text.Trim();
                    SelectedStudent.Note = txtNote.Text.Trim();
                    SelectedStudent.City = txtCity.Text.Trim();
                    SelectedStudent.Address = txtAddress.Text.Trim();
                    SelectedStudent.IsDeleted = chkIsDeleted.Checked;

                    Repository.SaveChanges();

                    AddStudy();
                    AddCertificate();

                    MessageBox.ShowMessage("Padaci su spremljeni.", true, ResolveUrl("~/Students.aspx"));

                }
                else
                {
                    MessageBox.ShowError(message);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool IsFromValid(out string message)
        {
            message = null;

            if (!string.IsNullOrWhiteSpace(ddlStudies.SelectedValue) && dpEnrollmentYear.Date == DateTime.MinValue)
            {
                message = "Molimo unesite datum upisa na studij.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text.Trim()))
            {
                message = "Upišite ime.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text.Trim()))
            {
                message = "Upišite prezime.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(ddlGender.SelectedValue))
            {
                message = "Odaberite spol";
                return false;
            }

            if (dpBirthDate.Date == DateTime.MinValue)
            {
                message = "Odaberite datum";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtJMBG.Text.Trim()))
            {
                message = "Upišite jmbg";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtOIB.Text.Trim()))
            {
                message = "Upišite oib";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUniqueNumber.Text.Trim()))
            {
                message = "Upišite broj studenta";
                return false;
            }



            return true;
        }

        private void BindForm()
        {
            if (SelectedStudent != null)
            {
                txtAddress.Text = SelectedStudent.Address;
                txtEmail.Text = SelectedStudent.Email;
                txtFirstName.Text = SelectedStudent.FirstName;
                txtLastName.Text = SelectedStudent.LastName;
                txtJMBG.Text = SelectedStudent.JMBG;
                txtOIB.Text = SelectedStudent.OIB;
                txtUniqueNumber.Text = SelectedStudent.UniqueNumber.ToString();
                txtAverageRating.Text = SelectedStudent.AverageRating.ToString();
                txtAverageRating1.Text = SelectedStudent.AverageGroup1.ToString();
                txtAverageRating2.Text = SelectedStudent.AverageGroup2.ToString();
                dpBirthDate.Date = SelectedStudent.BirthDate;

                ucStudentStudiesGrid.GetItems = Repository.StudentStudies.GetAll().Where(s => s.StudentId == SelectedStudent.Id).AsQueryable;
                ucStudentCertificateGrid.GetItems = Repository.DbContext.Students.Where(s => s.Id == SelectedStudentId)
                                                                                 .SelectMany(s => s.Certificates).AsQueryable;

                Studies.ForEach(s => ddlStudies.Items.Add(new ListItem(s.Title, s.Id.ToString())));
                ddlStudies.Items.Insert(0, new ListItem("Odaberite", ""));

                List<CertificateType> certificateTypeList = Repository.CertificateTypes.GetAll().ToList();
                certificateTypeList.ForEach(c => ddlCertificateType.Items.Add(new ListItem(c.Title, c.Id.ToString())));
                ddlCertificateType.Items.Insert(0, new ListItem("Odaberite", ""));

            }
        }

        #endregion
    }

    public enum StudentEditTab
    {
        Main = 0,
        Study = 1,
        Certificates = 2
    }

}