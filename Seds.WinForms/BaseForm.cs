using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace Seds.WinForms
{
    public class BaseForm : Form
    {
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
    }
}