using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web.UserControls
{
    public partial class DeleteConfirmation : System.Web.UI.UserControl
    {
        private string _message = "Jeste li sigurni da želite obristati stavku?";

        public string Message { get { return _message; } set { _message = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}