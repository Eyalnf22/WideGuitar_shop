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
using System.Collections;


namespace EyalProject
{
    public partial class account : myLibrary
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userStat"] == null)
                    Response.Redirect("Menu.aspx");

                if (Session["cookie"] == null)
                    Response.Redirect("HomePage.aspx");
                //לוקח פרטים גנריים ש משתמש 
                OleDbConnection Con = new OleDbConnection();
                Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con.Open();

                string sqlstring = "select * from MyUsersList WHERE MyUser = '" + Session["cookie"].ToString() + "' ";
                OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
                OleDbDataReader Dr = Cmd.ExecuteReader();
                if (Dr.HasRows)
                {
                    userInfo.DataSource = Dr;
                    userInfo.DataBind();
                    Con.Close();

                    Label usernameLbl = (Label)userInfo.Items[0].FindControl("usernameLbl");
                    usernameLbl.Text = Session["cookie"].ToString();
                    //לוקח מהסשן ושם בלייבל של שם המשמש
                    // אם השם משתמש ישתנה אז הלייבל יוחלף כי ככה הגדרתי בפונקציה מעדכנת שם משתמש

                }
                //

                //סוכם את סכום הכסף שבזבת בחנות

                //לוקח את כל הרשומות של הדילים של אותו לקוח
                OleDbConnection Con1 = new OleDbConnection();
                Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con1.Open();
                string sqlstring1 = "select * from MyDeals WHERE Username = '" + Session["cookie"].ToString() + "' ";
                OleDbCommand Cmd1 = new OleDbCommand(sqlstring1, Con1);
                OleDbDataReader Dr1 = Cmd1.ExecuteReader();
                double sum = 0;
                int DealsNum = 0;
                int ItemNum = 0;
                if (Dr1.HasRows)
                {
                    while (Dr1.Read())
                    {
                        DealsNum++;
                        sum += Convert.ToDouble(Dr1["SumPrice"]);
                        ItemNum += Int32.Parse(Dr1["ItemNum"].ToString());
                    }
                }

                Label dealNumLbl = userInfo.Items[0].FindControl("dealNumLbl") as Label;
                Label dealSumLbl = userInfo.Items[0].FindControl("dealSumLbl") as Label;
                Label ItemSumLbl = userInfo.Items[0].FindControl("ItemSumLbl") as Label;

                dealNumLbl.Text = DealsNum.ToString();//מציג מזפר עסקאות
                dealSumLbl.Text = sum.ToString();//מציג סכום שבוזבז עד כה
                ItemSumLbl.Text = ItemNum.ToString();//מציג סכום שבוזבז עד כה


                Con1.Close();
                //פותח קשר חדש לטבלה אם אותו תנאי, כלומר אותן רשומות של דילים של אותו יוזר
                OleDbConnection Con2 = new OleDbConnection();
                Con2.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con2.Open();
                string sqlstring2 = "select TOP 1 * from MyDeals WHERE Username = '" +
                    Session["cookie"].ToString() +
                    "' ORDER BY DealID DESC ";
                OleDbCommand Cmd2 = new OleDbCommand(sqlstring2, Con2);
                OleDbDataReader Dr2 = Cmd2.ExecuteReader();//עשיתי לימיט מגדול לקטן של אחד לכן אקבל את העסקה האחרונה ואחבר אותה לדאטא ליסט
                if (Dr2.HasRows)
                {
                    lastDeal.DataSource = Dr2;
                    lastDeal.DataBind();
                }
                else
                {
                    lastDeal.Visible = false;
                }
                Con2.Close();
            }

        }

        protected void updateEmail_Click(object sender, EventArgs e)
        {
            //לוקח את האייטם הראשון בדאטאליסט כי היא מורכבת רק מאייטם אחד.. ושם עושה פינד קונטרול
            TextBox emailBox = (TextBox)userInfo.Items[0].FindControl("emailBox");
            string email = emailBox.Text;
            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con.Open();

            string sqlstring = "select * from MyUsersList WHERE MyUser = '" + Session["cookie"].ToString() + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();


            Label takenUserLbl = (Label)userInfo.Items[0].FindControl("takenUserLbl");



            //מעדכן אימייל
            Con.Close();
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
    + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();
            string sqlstring1 = "UPDATE MyUsersList SET MyEmail = '" + email + "' WHERE MyUser ='" + Session["cookie"].ToString() + "' ";
            OleDbCommand cmd1 = new OleDbCommand(sqlstring1, Con1);
            cmd1.ExecuteNonQuery();
            Con1.Close();

            //משנה את הלייבל של שם המשתמש בפרטים שלי
            Label emailLbl = (Label)userInfo.Items[0].FindControl("emailLbl");
            emailLbl.Text = email;
            //משנה את הקוקי כלומר השם של המשתמש בסשן
            Response.Redirect("Account.aspx");//פה אני עם ריפרש לדף ובאותו הזמן הדף מעדכן את הערכים שלו כדי שהאיז פוסט באק יקרה שוב

        } 
        protected void upload_Click(object sender, EventArgs e)
        {
            FileUpload FileUploaded = userInfo.Items[0].FindControl("FileUploaded") as FileUpload;
            Image ProfilePic = userInfo.Items[0].FindControl("ProfilePic") as Image;
            Label IsUserExistPrmt = userInfo.Items[0].FindControl("IsUserExistPrmt") as Label;

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
                        UpProPic("~/UsersProflePics/" + Path.GetFileName(FileUploaded.FileName));
                        Response.Redirect("Account.aspx");
                        //redirect to ame page so is post back will happend and Html code will be updated to the new photo
                       // the photo source is provided in the is post back process
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

        //מעדכן תמונת פרופיל ליוזר 
        //הפונקציה מקבלת מחרוזת קישור לתמונה
        protected void UpProPic(string url)
        {
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
    + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();
            string sqlstring1 = "UPDATE MyUsersList SET MyImage = '" + url + "' WHERE MyUser ='" + Session["cookie"].ToString() + "' ";
            OleDbCommand cmd1 = new OleDbCommand(sqlstring1, Con1);
            cmd1.ExecuteNonQuery();
            Con1.Close();
        }
    }
}