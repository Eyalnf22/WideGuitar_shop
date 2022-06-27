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
    public partial class InsertPage : myLibrary
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userStat"] = "admin";//השורה הזו חובה! כי אחרת הוא יהיה לא מוגדר
            if (!IsPostBack)
            { 
                OleDbConnection Con1 = new OleDbConnection();
                Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con1.Open();

                string sqlstring = "select * from MyCities  ORDER BY CityName";
                OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
                OleDbDataReader Dr = Cmd.ExecuteReader();
                cityBox.DataSource = Dr;
                cityBox.DataTextField = "CityName";
                cityBox.DataBind();
            }
        }

        protected void loginB_Click(object sender, EventArgs e)
        {
            /*
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();
            
            string sqlstring = "select * from MyUsersList WHERE MyUser = '" + userNameBox.Text + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            Dr.Read();

            if (Dr.HasRows)
            {
                Session["userStat"] = Dr["MyCity"];
                Con1.Close();
                Response.Write("Found!");
                Response.Write(Session["userStat"]);
            }
            else
            {
                Con1.Close();
                Response.Write("Not Found");
            }


            */
            //מגדיר תמונה לדיפולט אם הוא לא העלה תמונה משל עצמו
            string proPic = " pics / NanProfile.png";
            proPic = ProfilePic.ImageUrl;

            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con.Open();

            string sqlstring = "select * from MyUsersList WHERE MyUser = '" + usernameBox.Text + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            if (!Dr.HasRows)//לא קיים משתמש כזה בטבלה
            {
                Con.Close();
                OleDbConnection Con1 = new OleDbConnection();
                Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source=" + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con1.Open();

                string sqlstring1 = "INSERT INTO MyUsersList(MyID,MyName,MyPhone,MyFamilyName,MyCity,MyAddres,MyUser,MyPassword,MyEmail,MyGender,MyDay,MyMonth,MyYear,MyImage) VALUES ('" + IdBox.Text + "','" + nameBox.Text + "','" + phoneBox.Text + "','" + familyBox.Text +
                    "','" + cityBox.SelectedValue + "','" + AddressBox.Text + "','" + usernameBox.Text + "','" + passwordBox.Text + "','" + EmailBox.Text + "','" + genderBox.SelectedValue +
                    "','" + dayBox.SelectedValue + "','" + monthBox.SelectedValue + "','" + yearBox.SelectedValue + "','" + proPic + "')";
                OleDbCommand cmd1 = new OleDbCommand(sqlstring1, Con1);
                cmd1.ExecuteNonQuery();
                Con1.Close();


               
                ;
            }
            else
            {
                IsUserExistPrmt.Text = "שם משתמש זה תפוס!";
            }
            //  Response.Write(y);

            /*
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source=" + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();
            string sqlstring = "DELETE from MyUsersList WHERE MyUser='" + userNameBox.Text + "' ";
            OleDbCommand cmd = new OleDbCommand(sqlstring, Con1);
            int y = 0;
            y = cmd.ExecuteNonQuery();
            Response.Write(y);
            

            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source=" + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();
            string sqlstring = "UPDATE MyUsersList SET MyName = '" + userNameBox.Text + "' WHERE MyUser ='" + userNameBox.Text + "' ";
            OleDbCommand cmd = new OleDbCommand(sqlstring, Con1);
            int y = 0;
            y = cmd.ExecuteNonQuery();
            Response.Write(y);*/

        }
        protected void upload_Click(object sender, EventArgs e)
        {
            string fileExtention = System.IO.Path.GetExtension(Path.GetFileName(FileUploaded.FileName));

            if (FileUploaded.HasFile)
            {
                if (FileUploaded.PostedFile.ContentLength < 20000000)
                {
                    if (fileExtention.ToLower() == ".jpg" || fileExtention.ToLower() == ".png" || fileExtention.ToLower() == ".jpeg")
                    {


                        IsUserExistPrmt.Text = "";
                        //לוקח מיקום של הקובץ בדירקטורי
                        string folderPath = Server.MapPath("~/UsersProflePics/");

                        //שומר את הקובץ בתיקייה של תמונות פרופיל
                        FileUploaded.SaveAs(folderPath + Path.GetFileName(FileUploaded.FileName));

                        //מראה את התמונה
                        ProfilePic.ImageUrl = "~/UsersProflePics/" + Path.GetFileName(FileUploaded.FileName);
                    }
                    else
                    {
                        IsUserExistPrmt.Text = "הקובץ שהועלה אינו תמונה.";
                    }
                }
                else
                {
                    IsUserExistPrmt.Text = "הקובץ שהועלה גדול מדי!";
                }
            }
        }
    }
}