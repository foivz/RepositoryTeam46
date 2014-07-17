using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web.UserControls
{
    /// <summary>
    /// Provides list of avaiable OAuth providers, or authorized user information.
    /// </summary>
    public partial class LoginMenu : BaseUserControl
    {
        #region Private

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

              //string s =  GetLocalResourceObject("Students").ToString();

                Bind();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //rptLoginLinks.ItemDataBound += new RepeaterItemEventHandler(rptLoginLinks_ItemDataBound);
            
        }

        void rptLoginLinks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //string oAuthSite = (string)e.Item.DataItem;

            //HyperLink hlLogin = (HyperLink)e.Item.FindControl("hlLogin");
            //Literal ltDelimiter = (Literal)e.Item.FindControl("ltDelimiter");

            //hlLogin.Text = oAuthSite;
            //hlLogin.NavigateUrl = OAuthAuthenticationManager.GetLoginUrl(oAuthSite);

            //if (rptLoginLinks.Items.Count < OAuthAuthenticationManager.AvaiableClientConfigurations.Count - 1)
            //{
            //    ltDelimiter.Text = "|";
            //}
        }

        private void Bind()
        {
            //if (UserProfile != null)
            //{
            //    phUserStatus.Visible = true;
            //    phLoginLinks.Visible = false;

            //    hlProfileInfo.Text = UserProfile.DisplayName;
            //    //hlProfileInfo.NavigateUrl = UserProfile.SsoUser.Url;
                
            //    hlLogout.NavigateUrl = Sada.Core.Config.Security.AuthenticationManager.GetLogoutUrl();
            //    hlLogout.Visible = true;

            //    imgProfile.ToolTip = UserProfile.DisplayName;
            //    imgProfile.ImageUrl = UserProfile.ImagePath;
            //    imgProfile.Visible = !string.IsNullOrWhiteSpace(imgProfile.ImageUrl);
            //}
            //else 
            //{
            //    phLoginLinks.Visible = true;
            //    phUserStatus.Visible = false;

            //    rptLoginLinks.DataSource = OAuthAuthenticationManager.AvaiableClientConfigurations;
            //    rptLoginLinks.DataBind();
            //}
        }

        #endregion
    }
}