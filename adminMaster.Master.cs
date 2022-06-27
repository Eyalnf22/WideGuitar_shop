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
    public partial class adminMaster : System.Web.UI.MasterPage
    {
        public string userStat = "";
        public string logoutMgs = "no session";

        //השארתי את המשנה הזה עבור הצהבת שאתה שם בכפתור של הלוג אווט
        protected void Page_Load(object sender, EventArgs e)
        {
            /*fake user
            Session["cookie"] = "fake des";
            Session["userStat"] = "customer";
            Session["basket"] = new basket();
            */
           
            if (Session["katalog"] == null)
            {
                //Response.Write("אין קטלוג ");
                object[] katArr = new object[2];
                katArr[0] = null;
                katArr[1] = null;
                Session["katalog"] = katArr;
            }

            if (!IsPostBack)
            {
                if (Session["userstat"] == null)
                {
                    Session["userstat"] = "geust";
                    Response.Redirect("Menu.aspx");
                }

                cartPanel.Style.Add("display", "none");
                if (Session["userStat"].ToString() != "customer")
                {
                    cartIcon.Style.Add("display", "none");

                }
                if (Session["userStat"] != null)
                {
                    Response.Write(updateUserStat());//print the current user status\type + create logout btn
                    updateSiteType();//מעדכן מה יראו בתפריט לפי סוגי דף. למשל המהנל לא יראה מעבר לתשלום
                    //וכשאני בעמוד של תשלום אני לא רוצה לראות מעבר לתשלום
                }
                else
                    Response.Redirect("login.aspx");
            }


        }
        public string updateUserStat()//what to response to different types of users
        {

            switch (Session["userStat"])
            {
                case "admin":
                    userStat = "אתה מנהל";
                    logoutMgs = "התנתק,מנהל";
                    toPay.Visible = false;
                    toHomepage.Visible = false;
                    break;
                case "guest":
                    userStat = "שלום אורח";
                    break;
                case "customer":
                    userStat = "שלום, " + Session["cookie"];
                    logoutMgs = "התנתק, " + Session["cookie"];
                    break;
            }
            exit.Text = logoutMgs;
            return userStat;
        }
        protected void logout(object sender, EventArgs e)
        {
            Session["userStat"] = "guest";
            Response.Redirect("Menu.aspx");
        }

        //הפונקציה נקראת כאשר האתר רוצה לתקשר עם המשתמש ולהודיע לו הודעה גנרית

        public void updateSiteType()
        {
            string pageN = GetCurrentPageName();
            if (pageN == "Pay.aspx")//לא רוצה לראות מעבר לתשלום כשאני במעבר לתשלום
                toPay.Visible = false;
            if (pageN == "Account.aspx")////לא רוצה לראות מעבר לחשבון שלי כשאני בחשבון שלי
                toAccount.Visible = false;
            



            if (Session["userStat"].ToString() == "guest")// disable logout and gotoPay btn if am i guest
            {
                exit.Visible = false;
                toPay.Visible = false;
                toAccount.Visible = false;
            }
            if (Session["userStat"].ToString() == "admin")// disable logout and gotoPay btn if am i guest
            {
                toPay.Visible = false;
                toAccount.Visible = false;
                randomBtn.Visible = false;
            }
        }


        public string GetCurrentPageName()// מחזיר את השם של האתר
        {
            string sPath = Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }



        protected void toPay_Click(object sender, EventArgs e) { Response.Redirect("Pay.aspx"); }
        protected void toHomepage_Click(object sender, EventArgs e) { Response.Redirect("Homepage.aspx"); }
        protected void toLogin_Click(object sender, EventArgs e) { Response.Redirect("Login.aspx"); }
        protected void toSignUp_Click(object sender, EventArgs e) { Response.Redirect("SignUp.aspx"); }
        protected void toAccount_Click(object sender, EventArgs e) { Response.Redirect("Account.aspx"); }
        protected void toMenu_Click(object sender, EventArgs e) { Response.Redirect("Menu.aspx"); }

        protected void toLoginSession_Click(object sender, EventArgs e) {
            if (GetCurrentPageName() == "showProducts.aspx")//אם זה דרך דף המוצר עצמו
            {
                Session["wasPro"] = Session["product"];
            }
                Response.Redirect("Login.aspx");
            

        }

        protected void ImageExitError_Click(object sender, ImageClickEventArgs e)
        {
            errorPrmt.Visible = false;//מעלים את הפרומפט השגיאתי
        }




        protected void myCart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            basket b = new basket();
            b = (basket)Session["basket"];
            if (e.CommandName == "deleteItem")
            {

                b.RemoveArrAndDT(e.Item.ItemIndex);
            }
            else if (e.CommandName == "showPro")
            {
                item i = b.Basket[e.Item.ItemIndex] as item;
                Session["product"] = i.ProductKey;
                Response.Redirect("showProducts.aspx");
            }
            else if (e.CommandName == "plus")
            {
                item i = b.Basket[e.Item.ItemIndex] as item;
                b.PlusMinus(i.ProductKey, 1);
            }
            else if (e.CommandName == "minus")
            {
                item i = b.Basket[e.Item.ItemIndex] as item;
                b.PlusMinus(i.ProductKey, -1);
            }

            Session["basket"] = b;
            cartPanel.Style.Add("display", "block");

        }


        protected void clearCart(object sender, EventArgs e)
        {
            basket b = new basket();
            b = (basket)Session["basket"];
            b.ClearArrAndDT();
            Session["basket"] = b;

        }

        public void myCart_LoadFill(object sender, EventArgs e)
        {
            
            if (Session["userStat"].ToString() == "customer")
            {
                basket b = new basket();
                b = (basket)Session["basket"];
                b.UpdateDT();
                myCart.DataSource = b.dt;
                myCart.DataBind();
                if (b.itemSum == 0)
                {

                    cartHead.Text = "עגלה ריקה.";
                    emptyCart.Visible = false;

                }
                else
                {
                    cartHead.Text = "עגלת הקניות";
                    emptyCart.Visible = true;
                }
                sumItems.Text = b.itemSum.ToString() + " מוצרים ";

                sum.Text = "סך: " + b.sum.ToString() + " ₪";

                Session["basket"] = b;
            }
        }

        protected void RandomProduct(object sender, EventArgs e)
        {
            myLibrary L = new myLibrary();
            int n = L.getRowsNum("MyInstruments");
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            Random rnd = new Random();
            int ran = rnd.Next(1,  n+1);
            //Response.Write(ran);
            string sqlstring = "select * from MyInstruments";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            for (int i = 0; i < ran; i++)
            {
                Dr.Read();
            }
           
           Session["product"] =Convert.ToInt32(Dr["InstID"]);
           Response.Redirect("showProducts.aspx");
            Con1.Close();
        }

    }
}