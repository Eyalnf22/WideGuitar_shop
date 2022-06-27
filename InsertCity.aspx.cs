using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

namespace EyalProject
{
    public partial class InsertCity : myLibrary
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userStat"] = "admin";
        }

        protected void InsertCity_Click(object sender, EventArgs e)
        {


            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con.Open();

            string sqlstring = "select * from Mycities WHERE CityName = '" + cityNameBox.Text + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            if (!Dr.HasRows)//לא קיים משתמש כזה בטבלה
            {
                Con.Close();//סוגר קוקשן של select

                OleDbConnection Con1 = new OleDbConnection();
                Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con1.Open();

                string sqlstring1 = "INSERT INTO Mycities(CityName,MyLocation) " +
                    "VALUES ('" + cityNameBox.Text + "','" + locationBox.Text + "')";
                OleDbCommand cmd1 = new OleDbCommand(sqlstring1, Con1);
                cmd1.ExecuteNonQuery();
                Con1.Close();
                promptCity.Text = "עיר נוספה בהצלחה";
                promptCity.ForeColor = System.Drawing.Color.Green;



            }
            else
            {
                promptCity.Text = "קיימת עיר כזו!";
                promptCity.ForeColor = System.Drawing.Color.Red;

            }



        }
    }
}