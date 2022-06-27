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

    public partial class searchDel : myLibrary
    {
        public int rowNum;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["userStat"] = "admin";
            if (!IsPostBack)
            {
                if (Session["userStat"] == null)
                    Response.Redirect("Menu.aspx");

                else if (Session["userStat"].ToString() != "admin")
                    Response.Redirect("Menu.aspx");

                ///ממלא את הדרופ דאון של הערים
                OleDbConnection Con1 = new OleDbConnection();
                Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con1.Open();

                string sqlstring = "select * from MyCities ORDER BY CityName";
                OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
                OleDbDataReader Dr = Cmd.ExecuteReader();
                cityBox.DataSource = Dr;
                cityBox.DataTextField = "CityName";
                cityBox.DataBind();
                Con1.Close();
                cityBox.Items.Insert(0, new ListItem(""));

                //בנייה התחלתית של הדף. לפני שהוא עובר לגלגול הבא
            }
        }

        //מדפיס משתמש
        protected void searchB_Click(object sender, EventArgs e)
        {
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            string sqlstring = "select * from MyUsersList WHERE MyUser = '" + usernameBox.Text + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            //אם אני שם ריד אז הוא מוחק את השורה שמצאתי//Dr.Read();
            rowNum = 0;
            if (Dr.HasRows)
            {
                isFoundLbl.Text = " נמצא!";
                GridView1.DataSource = Dr;
            }
            else
            {
                isFoundLbl.Text = "אין תוצאות..";
                GridView1.DataSource = null;
            }
            GridView1.DataBind();
            Con1.Close();

        }
        //מדפיס את כל הטבלה
        protected void searchAll_Click(object sender, EventArgs e)
        {
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();
            string sqlstring = "select * from MyUsersList";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();

            if (Dr.HasRows)
            {
                isFoundLbl.Text = " רשימת כל השמתמשים," + getRowsNum("MyUsersList") + "משתמשים";
                GridView1.DataSource = Dr;
            }
            else
            {
                isFoundLbl.Text = "אין משתמשים";
                GridView1.DataSource = null;
            }
            GridView1.DataBind();
            Con1.Close();
        }

        //מוחק משתמש
        protected void delete_Click(object sender, EventArgs e)
        {
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            string sqlstring = "select * from MyUsersList WHERE MyUser = '" + deleteBox.Text + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();

            if (Dr.HasRows)
            {
                Con1.Close();

                OleDbConnection Con2 = new OleDbConnection();
                Con2.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con2.Open();

                string sqlstring2 = "DELETE from MyUsersList WHERE MyUser='" + deleteBox.Text + "'";
                OleDbCommand Cmd2 = new OleDbCommand(sqlstring2, Con2);
                Cmd2.ExecuteNonQuery();
                isDeletedLbl.Text = "משתמש נמחק!";
                Con2.Close();

                //מעדכן את הטבלה
            }
            else
            {
                isDeletedLbl.Text = "המשתמש לא נמצא!";

            }
            this.searchAll_Click(this, null);
        }


        //מוחק את כל הטבלה
        protected void deleteAll_Click(object sender, EventArgs e)
        {
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            string sqlstring = "DELETE FROM MyUsersList";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            try
            {
                Cmd.ExecuteNonQuery();
                isDeletedLbl.Text = "מחקת את כל הטבלה";
            }
            catch
            {
                ////
            }
            Con1.Close();
            this.searchAll_Click(this, null);
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            rowNum++;
            // Response.Write("euad");
        }

        protected void usernameFilter_Click(object sender, EventArgs e)
        {
            string sqlstring = "select * from MyUsersList";
            Button b = sender as Button;
            if (b.CommandName == "ascF")
                sqlstring = "select * from MyUsersList ORDER BY MyUser ASC";
            else if (b.CommandName == "descF")
                sqlstring = "select * from MyUsersList ORDER BY MyUser DESC";
            else if (cityBox.SelectedValue != "" && genderbox.SelectedValue != "")
                sqlstring = "select * from MyUsersList WHERE MyCity='" + cityBox.SelectedValue + "' AND MyGender='" + genderbox.SelectedValue + "' ";
            else if (cityBox.SelectedValue != "" && genderbox.SelectedValue == "")
                sqlstring = "select * from MyUsersList WHERE MyCity='" + cityBox.SelectedValue+"'";
            else if (cityBox.SelectedValue == "" && genderbox.SelectedValue != "")
                sqlstring = "select * from MyUsersList WHERE MyGender='" + genderbox.SelectedValue + "' ";
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();

            if (Dr.HasRows)
            {
                isFoundLbl.Text = "רשימה מסוננת";
                GridView1.DataSource = Dr;
            }
            else
            {
                isFoundLbl.Text = "אין משתמשים";
                GridView1.DataSource = null;
            }
            GridView1.DataBind();
            Con1.Close();
        }

    }
}