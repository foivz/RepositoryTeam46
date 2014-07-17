using Seds.DataAccess;
using Seds.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Seds.Web
{
    public class BaseWebPage : System.Web.UI.Page
    {
        private User _userProfile = null;

        public User UserProfile //svojstvo za korisnika
        {
            get
            {
                if (_userProfile == null)
                {
                    _userProfile = GetCurrentUser();
                }

                return _userProfile;
            }
        }

        public bool IsModalPage
        {
            get
            {
                return this.Master is ModalMasterPage;
            }
        }

        public SedsRepositories Repository
        {
            get
            {
                return ((BaseMasterPage)this.Master).Repository;
            }
        }

        public string PageTitle
        {
            get
            {
                return ((BaseMasterPage)this.Master).PageTitle;
            }
            set
            {
                ((BaseMasterPage)this.Master).PageTitle = value;
            }
        }

        public string PageSubTitle
        {
            get
            {
                return ((BaseMasterPage)this.Master).PageSubTitle;
            }
            set
            {
                ((BaseMasterPage)this.Master).PageSubTitle = value;
            }
        }

        public MessageBox MessageBox
        {
            get
            {
                return ((BaseMasterPage)this.Master).MessageBox;
            }
        }

        public LeftMenu LeftMenu
        {
            get
            {
                return ((BaseMasterPage)this.Master).LeftMenu;
            }
        }

        public virtual bool AllowAnnonymous
        {
            get
            {
                return false;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!AllowAnnonymous)
            {
                if (UserProfile == null)
                {
                    Response.Redirect(ResolveUrl("~/Login.aspx"), true);
                }
            }
        }
        
        public DataAccess.User GetCurrentUser()
        {
            string sessionKey = Login._sessionName;
            HttpSessionState session = HttpContext.Current.Session;

            DataAccess.User user = session[sessionKey] as DataAccess.User;

            if (user == null)
            {
                string userName = Login.GetUserNameFromCookie();
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    user = Repository.Users.GetAll().FirstOrDefault(u => u.UserName == userName);
                }

                session[sessionKey] = user;
            }
            return user;
        }
    }
}