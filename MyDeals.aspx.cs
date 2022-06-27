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
    public partial class MyDeals : myLibrary
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
                if (Session["userStat"] == null)
                    Response.Redirect("Menu.aspx");

                if (Session["cookie"] == null)
                    Response.Redirect("HomePage.aspx");

            if (!IsPostBack)
            {
                
                OleDbConnection Con = new OleDbConnection();
                Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con.Open();
                string sqlstring = "select  * from MyDeals WHERE Username = '" +
                    Session["cookie"].ToString() +
                    "' ORDER BY DealID DESC ";
                OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
                OleDbDataReader Dr = Cmd.ExecuteReader();//עשיתי לימיט מגדול לקטן של אחד לכן אקבל את העסקה האחרונה וא
                allDeals.DataSource = Dr;
                allDeals.DataBind();
                Con.Close();    
            }
        }

        protected void allDeals_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "showOrder")
            {
                string DealId = allDeals.DataKeys[e.Item.ItemIndex].ToString();
                Session["DealID"] = DealId;
                Response.Redirect("MyOrders.aspx");
            }

            if (e.CommandName == "toProduct")
            {

                DataList thisD = source as DataList;
                int key = (int)thisD.DataKeys[e.Item.ItemIndex];
                Session["product"] = key;
                Response.Redirect("showProducts.aspx");
            }

        }

        protected void allDeals_ItemDataBound(object sender, DataListItemEventArgs e)
        {
           DataList child =  e.Item.FindControl("chidList") as DataList;
            int key = (int)allDeals.DataKeys[e.Item.ItemIndex];
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();
            string sqlstring1 = "select  * from MyOrders WHERE DealID = " +
               key.ToString();
            OleDbCommand Cmd1 = new OleDbCommand(sqlstring1, Con1);
            OleDbDataReader Dr1 = Cmd1.ExecuteReader();

            child.DataSource = Dr1;
            child.DataBind();
            Con1.Close();
        }

     
    }
}