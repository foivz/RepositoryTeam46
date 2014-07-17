using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Seds.Web.UserControls
{
    public partial class MessageBox : UserControl
    {
        private const string _pageMessageSessionKey = "PageMessageToShow_{0}";

        protected void Page_Load(object sender, EventArgs e)
        {
            PageMessage message = GetMessageFromSession();

            if (message != null)
            {
                DisplayPageMessage(message);
            }
        }

        string _errorCssClass = "alert alert-error";
        public string ErrorCssClass
        {
            get
            {
                return _errorCssClass;
            }
            set
            {
                _errorCssClass = value;
            }
        }

        string _infoCssClass = "alert alert-info";
        public string InfoCssClass
        {
            get
            {
                return _infoCssClass;
            }
            set
            {
                _infoCssClass = value;
            }
        }

        string _successCssClass = "alert alert-success";
        public string SuccessCssClass
        {
            get
            {
                return _successCssClass;
            }
            set
            {
                _successCssClass = value;
            }
        }

        public static string ExcludedQueryParameters { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            RegisterControlOnClient();
            SetVisibleContainer();
        }

        protected void DisplayPageMessage(PageMessage message)
        {
            switch (message.Type)
            {
                case PageMessageType.Message:
                    lblMessageBox.Text = message.Message;
                    divMessageBoxSuccess.Style.Add("display", "block");
                    break;
                case PageMessageType.Error:
                    lblMessageBoxError.Text = message.Message;
                    divMessageBoxError.Style.Add("display", "block");
                    break;
                case PageMessageType.Info:
                    lblMessageBoxInfo.Text = message.Message;
                    divMessageBoxInfo.Style.Add("display", "block");
                    break;
            }

        }

        private void RegisterControlOnClient()
        {
            string js = "$(function() {"
                            + "if(SadaCore) {"
                                + "SadaCore.RegisterNameSpace('SadaCore.MessageBox');"
                                + "SadaCore.MessageBox = new SadaCore.MessageBoxControl('',"
                                + "{"
                                    + "serviceUrl: '" + ResolveUrl("~/Admin/UserControls/Message/MessageBoxService.asmx") + "',"
                                    + "showMessageClass: '" + SuccessCssClass + "',"
                                    + "showErrorClass: '" + ErrorCssClass + "',"
                                    + "showInfoClass: '" + InfoCssClass + "',"
                                    + "customHtmlEnabled: true,"
                                    + "divMessageBoxId: '" + divMessageBoxSuccess.ClientID + "',"
                                    + "divPortalMessageErrorId: '" + divMessageBoxError.ClientID + "',"
                                    + "divPortalMessageInfoId: '" + divMessageBoxInfo.ClientID + "'";
            js += "});"
            + "}"
        + "});";
            ScriptManager.RegisterClientScriptBlock(this, this.Page.GetType(), "MessageBox_" + this.ClientID, js, true);
        }

        private void SetVisibleContainer()
        {
            divMessageBoxSuccess.Attributes.Add("class", SuccessCssClass);            
            divMessageBoxInfo.Attributes.Add("class", InfoCssClass);
            divMessageBoxError.Attributes.Add("class", ErrorCssClass);
        }

        #region ShowMessage

        public virtual void ShowMessage(string messageText)
        {
            ShowMessage(messageText, false);
        }

        public void ShowMessage(string messageText, bool redirect)
        {
            ShowMessage(messageText, redirect, null);
        }

        public void ShowMessage(string messageText, bool redirect, string redirectUrl)
        {
            ProcessMessage(messageText, redirect, redirectUrl, PageMessageType.Message);
        }

        #endregion

        #region ShowError

        public void ShowError(string messageText)
        {
            ShowError(messageText, false);
        }

        public void ShowError(string messageText, bool redirect)
        {
            ShowError(messageText, redirect, null);
        }

        public void ShowError(string messageText, bool redirect, string redirectUrl)
        {
            ProcessMessage(messageText, redirect, redirectUrl, PageMessageType.Error);
        }

        #endregion

        #region ShowInfo

        public void ShowInfo(string messageText)
        {
            ShowInfo(messageText, false);
        }

        public void ShowInfo(string messageText, bool redirect)
        {
            ShowInfo(messageText, redirect, null);
        }

        public void ShowInfo(string messageText, bool redirect, string redirectUrl)
        {
            ProcessMessage(messageText, redirect, redirectUrl, PageMessageType.Info);
        }

        #endregion

        #region GetMessageFromSession

        private PageMessage GetMessageFromSession()
        {
            PageMessage message = null;
            string requestUrl = HttpContext.Current.Request.Url.ToString();

            string currentPage = (requestUrl.LastIndexOf("?") > 0) ? requestUrl.Substring(0, requestUrl.LastIndexOf("?")) : requestUrl;
            currentPage = requestUrl.Substring(currentPage.LastIndexOf("/") + 1);
            string sessionKey = string.Format(_pageMessageSessionKey, RemoveExcludedQueryParams(currentPage));

            message = HttpContext.Current.Session[sessionKey] as PageMessage;

            HttpContext.Current.Session.Remove(sessionKey);

            return message;
        }

        #endregion

        #region ProcessMessage

        private void ProcessMessage(string messageText, bool redirect, string redirectUrl, PageMessageType type)
        {
            if (redirect)
            {
                if (string.IsNullOrEmpty(redirectUrl))
                {
                    redirectUrl = HttpContext.Current.Request.Url.ToString();
                }

                PutMessageInSession(messageText, redirectUrl, type);

                HttpContext.Current.Response.Redirect(redirectUrl, false);
            }
            else
            {
                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    PutMessageInSession(messageText, redirectUrl, type);
                }

                // Display a message without redirect.
                PageMessage message = new PageMessage(messageText, null, type);
                DisplayPageMessage(message);
            }
        }

        #endregion

        #region PutMessageInSession

        public static void PutMessageInSession(string message, string redirectUrl, PageMessageType type)
        {
            string currentPage = (redirectUrl.LastIndexOf("?") > 0) ? redirectUrl.Substring(0, redirectUrl.LastIndexOf("?")) : redirectUrl;
            redirectUrl = redirectUrl.Substring(currentPage.LastIndexOf("/") + 1);

            PageMessage msg = new PageMessage(message, redirectUrl, type);

            string sessionKey = string.Format(_pageMessageSessionKey, RemoveExcludedQueryParams(redirectUrl));

            HttpContext.Current.Session[sessionKey] = msg;
        }

        #endregion

        private static string RemoveExcludedQueryParams(string url)
        {
            string excludedQueries = ExcludedQueryParameters;
            string cleanUrl = url;

            if (excludedQueries != null && !string.IsNullOrEmpty(url))
            {
                string[] excludedQueryParams = excludedQueries.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (excludedQueryParams != null && excludedQueryParams.Length > 0)
                {
                    int indexOfQuery = url.IndexOf('?');
                    if (indexOfQuery > -1)
                    {
                        if (url.IndexOf('+') == -1)
                        {
                            url = HttpUtility.UrlDecode(url);
                        }

                        string query = url.Substring(indexOfQuery);
                        StringBuilder queryBuilder = new StringBuilder();
                        NameValueCollection querys = HttpUtility.ParseQueryString(query);
                        foreach (string key in querys)
                        {
                            bool isExcluded = false;

                            if (string.IsNullOrWhiteSpace(key))
                            {
                                isExcluded = true;
                            }
                            else
                            {
                                foreach (string excludedQueryParam in excludedQueryParams)
                                {
                                    if ((excludedQueryParam.Trim().ToUpperInvariant() == key.ToUpperInvariant()))
                                    {
                                        isExcluded = true;
                                        break;
                                    }
                                }
                            }
                            if (!isExcluded)
                            {
                                queryBuilder.AppendFormat("&{0}={1}", key, querys[key]);
                            }
                        }
                        string baseName = url.Replace(query, string.Empty);
                        if (queryBuilder.Length > 0)
                        {
                            queryBuilder.Remove(0, 1);

                            cleanUrl = string.Format("{0}?{1}", baseName, queryBuilder.ToString());
                        }
                        else
                        {
                            cleanUrl = baseName;
                        }
                    }
                }
            }
            return cleanUrl;
        }
    }

    [Serializable]
    public class PageMessage
    {
        public string Page { get; set; }
        public PageMessageType Type { get; set; }
        public string Message { get; set; }

        public PageMessage(string message, string page, PageMessageType type)
        {
            Message = message;
            Page = page;
            Type = type;
        }
    }

    [Serializable]
    public enum PageMessageType
    {
        Undefined = 0,
        Message = 1,
        Error = 2,
        Info = 3
    }
}