using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Seds.Web.UserControls
{
    public partial class DatePicker : System.Web.UI.UserControl
    {
        private string _format = "dd.MM.yyyy.";
        private CultureInfo _culture = new CultureInfo("hr-HR");

        public CultureInfo Culture
        {
            get
            {
                return _culture;
            }
            set
            {
                _culture = value;
            }
        }

        public DateTime Date
        {
            get 
            { 
                return ParseDate(txtDate.Text); 
            }
            set
            {
                if (value != DateTime.MinValue)
                {
                    txtDate.Text = value.ToString(Format);
                }
                else
                {
                    txtDate.Text = string.Empty;
                }
            }
        }

        public string Format
        {
            get
            {
                return _format;
            }
            set
            {
                _format = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public DateTime ParseDate(string dateText)
        {
            DateTime date = DateTime.MinValue;

            if (!string.IsNullOrWhiteSpace(dateText))
            {
                if (!DateTime.TryParseExact(dateText, Format, Culture, DateTimeStyles.None, out date))
                {
                    date = DateTime.MinValue;
                }
            }

            return date;
        }
    }
}