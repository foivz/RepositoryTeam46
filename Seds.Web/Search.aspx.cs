using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class Search : BaseWebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LeftMenu.SelectedPage = UserControls.LeftMenuPage.Search;
            ucStudents.GetItems = new Func<IQueryable<spSearchStudents_Result>>(() => SearchStudents());
        }
        //
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            btnSearch.Click += btnSearch_Click;
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            ucStudents.BindGrid();

            //string phase = txtPharse.Text.Trim();

            //if (Repository.DbContext != null)
            //{
            //    var results = Repository.DbContext.spSearchStudents(phase);
            //    if (results != null)
            //    {
            //        ucStudents.GetItems = results.AsQueryable;

            //        ucStudents.BindGrid(results.AsQueryable());
            //    }
            //}
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IQueryable<spSearchStudents_Result> SearchStudents()
        {
            string phase = txtPharse.Text.Trim();
            if (!string.IsNullOrEmpty(phase))
            {
                ucStudents.Visible = true;

                if (Repository.DbContext != null)
                {
                    var results = Repository.DbContext.spSearchStudents(phase);
                    if (results != null)
                    {
                        return results.AsQueryable();
                    }
                }
            }
            else
            {
                ucStudents.Visible = false;
            }
            return null;
        }
    }
}