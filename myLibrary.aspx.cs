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

    public partial class myLibrary : System.Web.UI.Page
    {
        //יוצר אובייקט אדמין של מאסטר פייג,בספרייה המשותפת ככה שיש לי
        //תמיד גישה לצד שרת של מאסטר כי כולן יורשים מהספרייה
        public adminMaster admin = new adminMaster();
        public String imgSrc = "intrumentsImagesDB/";



        protected void print<t>(t p)
        {
            Response.Write(p);
        }
        public void isSessionValid()
        {
            string pageN = GetCurrentPageName();
            if (Session["userStat"] == null)//מחזיר להתחברות אם
                Response.Redirect("Menu.aspx");

            else if (Session["basket"] == null && pageN != "Menu.aspx")//אסור שיהיה בדף הבבית ויפקוץ לעצמו זה stack overflow
                Response.Redirect("Menu.aspx");
        }

        public  adminMaster a = new adminMaster();
        //מקבל שם משתמש ובודק האם הוא קיים
        public bool isUserExist(string userName)
        {
            //פןנקציה בעייתית מאוד בגלל שהיא עושה ריטרן ולא סוגרת את הקונקשן!!!
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            string sqlstring = "select * from MyUsersList WHERE MyUser = '" + userName + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            return Dr.HasRows;
        }
        //gets a key and wanted field and return the value of the field at the key product row
        public string getInfoFromATbl(int key,string wantedField)
        {
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();
           
            string sqlstring = "select "+wantedField+" from MyInstruments WHERE InstID = " +key+ " ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();
             Dr.Read();
            
            string value = Dr[wantedField].ToString();
            Con1.Close();
            return value;

        }
    



        public void Catchz()
        {
            Master.FindControl("errorMsg").Visible = true;
            //חובה להשתמש בפייד קונארול במאסטר כדי לקבל גישה לאלמנטים שהמאסטר יוצר
            //הפונקציה נקראת כאשר יש שגיאה בטריי וקאץ ומופיעה הודעת שגיאה
        }

        public int getRowsNum(string TblName)
        {
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();


            string sqlstring = "SELECT COUNT(*) FROM " +TblName +"";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            int rowNum = Convert.ToInt32(Cmd.ExecuteScalar());
            Con1.Close();
            return rowNum;
        }

        public string GetCurrentPageName()// מחזיר את השם של האתר
        {
            string sPath = Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }

        //יוצר משתמש מזויף כדי שלא אצטרך להתחבר כל פעם מחדש
        public void fake()
        {
            Session["userStat"] = "customer";
            Session["cookie"] = "fake user";
            Session["product"] = 1;
            basket b = new basket();
            item i = new item();
            item i1 = new item();
            i.ProductKey = 1;
            i.Count = 1;
            b.AddItem(i);
            i1.ProductKey = 4;
            i1.Count = 1;
            b.AddItem(i1);
            Session["basket"] = b;
        }

        public void printB()
        {
            basket b = new basket();
            b = (basket)Session["basket"];
            foreach (item i in b.Basket)
            {
                print(i.ProductKey);
            }
            Session["basket"] = b;
        }


        protected void toSearchDel_Click(object sender, EventArgs e) { Response.Redirect("searchDel.aspx"); }
        protected void toInsert_Click(object sender, EventArgs e) { Response.Redirect("InsertPage.aspx"); }
        protected void toLogin_Click(object sender, EventArgs e) { Response.Redirect("Login.aspx"); }
        protected void toHomepage_Click(object sender, EventArgs e) { Response.Redirect("Homepage.aspx"); }
        protected void toSignUp_Click(object sender, EventArgs e) { Response.Redirect("SignUp.aspx"); }

        protected void toUpdate_Click(object sender, EventArgs e){ Response.Redirect("update.aspx"); }
        protected void toPay_Click(object sender, EventArgs e){ Response.Redirect("Pay.aspx"); }
        protected void toDeal_Click(object sender, EventArgs e){ Response.Redirect("MyDeals.aspx"); }
        protected void toAcc_Click(object sender, EventArgs e){ Response.Redirect("Account.aspx"); }
        protected void toCity_Click(object sender, EventArgs e){ Response.Redirect("InsertCity.aspx"); }
        protected void toInst_Click(object sender, EventArgs e){ Response.Redirect("InsertInst.aspx"); }
        protected void toStat_Click(object sender, EventArgs e){ Response.Redirect("Stat.aspx"); }
        

    }
}