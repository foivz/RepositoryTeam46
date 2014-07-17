using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seds.DataAccess;

namespace Seds.Web.UserControls
{
    public partial class StudiesGrid : BaseGrid<Study>
    {
        protected override bool HideIfNoItems
        {
            get
            {
                return true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
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

        protected override bool Delete(int id)
        {

            if (UserProfile != null)
            {
                if (id > 0)
                {
                    Repository.Studies.Delete(id);
                    Repository.SaveChanges();
                    MessageBox.ShowMessage("Obrisano", true);

                }

            }

            return false;
        }

        public string GetCourseGroupTitle (int? CourseGroupId)
        {

            CourseGroup courseGroup = Repository.CourseGroups.GetById(CourseGroupId.Value);
            return courseGroup.Title;
        }

        public string GetStudyTypeTitle(int? TypeId)
        {
            StudyType studyType = Repository.StudyTypes.GetById(TypeId.Value);
            return studyType.Title;
        }
    }
}