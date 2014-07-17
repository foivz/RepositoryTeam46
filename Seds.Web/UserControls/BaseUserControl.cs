using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Seds.Web.UserControls
{
    public class BaseUserControl : UserControl
    {
        public SedsRepositories Repository
        {
            get
            {
                return ((BaseWebPage)this.Page).Repository;
            }
        }

        public string PageTitle
        {
            get
            {
                return ((BaseWebPage)this.Page).Title;
            }
            set
            {
                ((BaseWebPage)this.Page).Title = value;
            }
        }

        public MessageBox MessageBox
        {
            get
            {
                return ((BaseWebPage)this.Page).MessageBox;
            }
        }

        public LeftMenu LeftMenu
        {
            get
            {
                return ((BaseWebPage)this.Page).LeftMenu;
            }
        }

        public DataAccess.User UserProfile
        {
            get
            {
                return ((BaseWebPage)this.Page).UserProfile;
            }
        }
    }
}