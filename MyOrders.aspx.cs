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
using System.Collections;


namespace EyalProject
{
    public partial class MyOrders : myLibrary
    {
        public double AllPrice = 0;
        public string headOrderNum = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userStat"] == null)
                    Response.Redirect("Menu.aspx");

                if (Session["cookie"] == null)
                    Response.Redirect("HomePage.aspx");

                if (Session["DealID"] == null)
                    Response.Redirect("Account.aspx");

                orderNum.Text = "הזמנה " + Session["DealID"].ToString() + "#";//מדפיס את מספר ההזמנה בכותרת

                OleDbConnection Con = new OleDbConnection();
                Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con.Open();
                string sqlstring = "select  * from MyOrders WHERE DealID = " +
                    Session["DealID"].ToString();
                OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
                OleDbDataReader Dr = Cmd.ExecuteReader();
                allOrders.DataSource = Dr;
                allOrders.DataBind();

                Con.Close();
            }

        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            headOrderNum = "הזמנה " + Session["DealID"].ToString() + "#";//מדפיס את מספר ההזמנה בטייטל
        }
        protected void allOrders_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "toProduct")
            {
                int key = (int)allOrders.DataKeys[e.Item.ItemIndex];
                Session["product"] = key;
                Response.Redirect("showProducts.aspx");
            }
        }
    }
}