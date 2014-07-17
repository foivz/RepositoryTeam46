using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.DataAccess;

namespace Seds.Web.UserControls
{
    public partial class StudentStudiesGrid : BaseGrid<Student_Studies>
    {
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

        public Student SelectedStudent { get; set; }

        protected override string InitialSortColumn
        {
            get
            {
                return "StudyId";
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

        protected override bool Delete(int studyId)
        {
            if (UserProfile != null)
            {
                if (studyId > 0 && SelectedStudent != null)
                {
                    Student_Studies student_Study = Repository.StudentStudies.GetAll().FirstOrDefault(ss => ss.StudentId == SelectedStudent.Id && 
                                                                                                            ss.StudyId == studyId);
                    if (student_Study != null)
                    {
                        Repository.StudentStudies.Delete(student_Study);
                        Repository.SaveChanges();
                        MessageBox.ShowMessage("Obrisan studij", true, string.Format("/StudentEdit.aspx?StudentId={0}&StudentEditTab={1}", SelectedStudent.Id, StudentEditTab.Study));
                    }
                }
            }

            return false;
        }

        public string GetStudyTitle(Student_Studies studentStudy)
        {
            return Studies.FirstOrDefault(s => s.Id == studentStudy.StudyId).Title;
        }
    }
}