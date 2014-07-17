using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web
{
    public partial class Logout : BaseWebPage
    {
        public override bool AllowAnnonymous
        {
            get
            {
                return true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ClearProfile();

            Response.Redirect(ResolveUrl("~/Login.aspx"));
        }

        public static void ClearProfile()
        {
            HttpContext.Current.Session[Login._sessionName] = null;
            Login.Clear();
        }
    }
}