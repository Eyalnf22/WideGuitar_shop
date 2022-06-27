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
    public partial class showProducts : myLibrary
    {
        public string headName = "";
        public static int proKeyFromssn;//זה הסשן שסוחב את מפתח המוצר והוא סטטי כדי שהוא יוגדר בלידה
        public string Description = "DESCRIPTIOsN";                       //בלידה של הדף באיז פוסט ואז הוא יהיה נגיש בכל פונקציה

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            headName = getInfoFromATbl(proKeyFromssn, "InstName");
            headLbl.Text = headName;

            if (Session["userStat"].ToString() == "customer")//אם לקוח נכנס שלא יקרה אפשרות להתחבר או להרשם
            {
                toLogin.Visible = false;
                tosignUp.Visible = false;
            }
            int curSup = Int32.Parse(getInfoFromATbl(proKeyFromssn, "Supply"));//אספקה נוכחית
            if (curSup <= 0)
            {
                addBtn.Enabled = false;
                addBtn.Text = "אין במלאי";
                addBtn.ForeColor = System.Drawing.Color.DarkRed;
                if (Session["basket"] != null)
                {
                    basket b = new basket();
                    b = Session["basket"] as basket;
                    b.RemItem(proKeyFromssn);
                }
            }
            else// אם יש מלאי אז האיום היחידי שלי זה שאני לא אשים בסל יותר מהמלאי 
            {
                if (Session["basket"] != null)
                {

                    basket b = new basket();
                    b = Session["basket"] as basket;
                    foreach (item i in b.Basket)
                    {
                        if (i.Count >= curSup)//אם יש כמות בסל של אותו אייטם ששווה לאספקה או יותר אם אפשר להוסיף יותר לסל
                        {
                            addBtn.Enabled = false;
                            i.Count = curSup;
                        }
                        else
                        {
                            addBtn.Enabled = true;
                        }
                    }
                }
            }
            if (Session["wasPro"] != null)
                Session["wasPro"] = null; 
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            addBtn.Width = 150;
            ////חשוב רצח שהפייק יהיה פה כדי שכל פעם שהאתר עולה הסשן של מוצר יוגדר שוב אחרת יש דיליי
            //fake();
            if (!IsPostBack)
            {

                //session checks. status must be not null and product ass well.
                //those are the minimum conditions
                if (Session["userStat"] == null)
                    Response.Redirect("Menu.aspx");

                if (Session["product"] == null)
                    Response.Redirect("HomePage.aspx");

                proKeyFromssn = Int32.Parse(Session["product"].ToString());//מגדיר את המקט של המוצר

                //אם יש סל כלומר יש לקוח אס צריך לבדוק אם הוא לא גמר את האפסקה
                // יש הוא קנה את כל האספקה אז צריך לעצור אותו להקפיא את כפתור הקניה

                headName = getInfoFromATbl(proKeyFromssn, "InstName");
                headLbl.Text = headName;

                Description = getInfoFromATbl(proKeyFromssn, "Description");

                productImg.ImageUrl = imgSrc + getInfoFromATbl(proKeyFromssn, "MyImage");
                zoomImg.ImageUrl = imgSrc + getInfoFromATbl(proKeyFromssn, "MyImage");



                //הולך להביא את הפרטים הראשיים 
                OleDbConnection Con = new OleDbConnection();
                Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con.Open();

                string sqlstring = "select * from MyInstruments WHERE InstID = " + proKeyFromssn + " ";
                OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
                OleDbDataReader Dr = Cmd.ExecuteReader();

                if (Dr.HasRows)
                {
                    MainDetalis.DataSource = Dr;
                    MainDetalis.DataBind();
                }
                Con.Close();

                //הולך להביא את הפרטים המשניים
                OleDbConnection Con1 = new OleDbConnection();
                Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con1.Open();

                string sqlstring1 = "select * from MyInstFields WHERE ID = " + proKeyFromssn + " ";
                OleDbCommand Cmd1 = new OleDbCommand(sqlstring1, Con1);
                OleDbDataReader Dr1 = Cmd1.ExecuteReader();

                if (Dr1.HasRows)
                {
                    detailList.DataSource = Dr1;
                    detailList.DataBind();
                }
                Con1.Close();
            }
        }

        public void AddProduct(object sender, EventArgs e)
        {
            bool NewPro = true;
            basket b = new basket();
            b = (basket)Session["basket"];
            foreach (item BasketItem in b.Basket)
            {
                if (BasketItem.ProductKey == proKeyFromssn)
                {
                    b.PlusMinus(BasketItem.ProductKey, 1);
                    NewPro = false;

                }
            }
            if (NewPro)
            {
                item i = new item();
                i.ProductKey = proKeyFromssn;
                i.Count = 1;
                b.AddItem(i);
            }
            Session["basket"] = b;//חייב שגם אם נוסף אייטם חדש וגם עם התווסף בכמות הסל חייב לזור לסשן
            Panel cart = Master.FindControl("cartPanel") as Panel;
            cart.Style.Add("display", "block");

        }
    }
}