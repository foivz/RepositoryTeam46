using Seds.DataAccess;
using Seds.Web.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Seds.Web
{
    public class BaseMasterPage : MasterPage
    {
        public string _pageTitle = null;
        public string PageSubTitle { get; set; }
        private SedsRepositories _repository = null;

        public SedsRepositories Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new SedsRepositories();
                }

                return _repository;
            }
        }

        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                _pageTitle = string.Format("Administracija - {0}", value);
            }
        }

        /*   public UserProfile UserProfile
           {
               get
               {
                   return ((BaseAdminWebPage)this.Page).UserProfile;
               }
           }
         */

        public MessageBox MessageBox
        {
            get
            {
                return (MessageBox)this.FindControl("ucMessageBox");
            }
        }

        public LeftMenu LeftMenu
        {
            get
            {
                return (LeftMenu)this.FindControl("ucLeftMenu");
            }
        }
    }
}