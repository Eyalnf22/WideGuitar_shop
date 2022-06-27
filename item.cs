using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

namespace EyalProject
{
    public partial class item : myLibrary
    {
        private int _productKey;
        private int _count;

        public int ProductKey
        {
            get { return _productKey; }
            set { _productKey = value; }
        }
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }


    }
}