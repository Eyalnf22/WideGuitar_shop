using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.OleDb;

namespace EyalProject
{
    public partial class Login : myLibrary
    {
        public string isLogin = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userStat"] = "guest";
        }
        protected void loginB_Click(object sender, EventArgs e)
        {
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source=" + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            string sqlstring = "select * from MyUsersList WHERE MyUser='" + usernameBox.Text.Trim() + "'AND MyPassword='" + passwordBox.Text.Trim() + "'";
            OleDbCommand cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = cmd.ExecuteReader();

            if (usernameBox.Text.Trim() == "admin" && passwordBox.Text.Trim() == "1234")
            {
                Session["userStat"] = "admin";
                Response.Redirect("searchDel.aspx");
            }

            if (Dr.HasRows)
            {
                Session["userStat"] = "customer";
                Session["cookie"] = usernameBox.Text.Trim();
                basket b = new basket();
                Session["basket"] = b;
                if (Session["wasPro"] != null)
                    Response.Redirect("showProducts.aspx");
                else
                    Response.Redirect("Menu.aspx");
            }
            else
            {
                isLogin = "הזנת פרטים לא נכונים";
            }
            Con1.Close();
        }
    }
}
