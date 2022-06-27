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
    public partial class Pay : myLibrary
    {
        public static int productKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            // fake();

            if (!IsPostBack)
            {
                isSessionValid();
                FillGrid(); // בודק האם שסשנים פושטקים בלי סשן לא יכנסו
            }

        }
        //הקטע הוא שכשאני לוחץ על כפתור כל עדכון כמו מחיקה או שינוי כמות, אני נשלח לסרבר ותמיד הפייג לואד רץ ראשון ואז
        //ואז רץ הכפתור שלחצתי עליו, כלומר רק אז נמחק המוצר המסל או שונתה כמותו. לאחר מכו אני רוצה למלא את הגריד מחדש מעודכן
        //לכן אני קובע פונקציה שקוראת לאחר מחיקת המוצר שתעדכן את הגריד
        //אם קודם הייתי מעדכן את הגריד ואז מוחק אזי היית בעיה
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            FillGrid();
        }

        public void FillGrid()
        {
            basket b = new basket();
            b = (basket)Session["basket"];
            shopGrid.DataSource = null;
            shopGrid.DataSource = b.dt;
            shopGrid.DataBind();
            sumLbl.Text = b.sum.ToString();
            itemCount.Text = b.itemSum.ToString();

            if (b.itemSum == 0)
            {
                noItem.Text = "אין פריטים בעגלת בקניות.";
                PayB.Visible = false;
            }
            Session["basket"] = b;
        }



        protected void PayB_Click(object sender, EventArgs e)
        {
            //פונקצית תשלום
            basket b = new basket();
            b = (basket)Session["basket"];

            foreach (item i in b.Basket)
            {

                int curSup = int.Parse(getInfoFromATbl(i.ProductKey, "Supply").ToString());
                curSup = curSup - i.Count;//מוריד את המלאי בכמות


                // update with connection the suppy
                OleDbConnection Con = new OleDbConnection();
                Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
        + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con.Open();
                string sqlstring = "UPDATE MyInstruments SET Supply='" + curSup + "'" +
                    " WHERE InstID =" + i.ProductKey + " ";
                OleDbCommand cmd = new OleDbCommand(sqlstring, Con);
                cmd.ExecuteNonQuery();
                Con.Close();


                // update with connection the sold
                int curSold = Int32.Parse(getInfoFromATbl(i.ProductKey, "Sold").ToString());
                curSold++;//מעלה את המכירות ב1

                OleDbConnection Con1 = new OleDbConnection();
                Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
        + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con1.Open();
                string sqlstring1 = "UPDATE MyInstruments SET Sold=" + curSold + " " +
                    " WHERE InstID =" + i.ProductKey + " ";
                OleDbCommand cmd1 = new OleDbCommand(sqlstring1, Con1);
                cmd1.ExecuteNonQuery();
                Con1.Close();


            }

            PayB.Visible = false;
            Receipt.Text = "הקנייה בוצעה בהצלחה!";
            shopGrid.DataSource = null;
            shopGrid.DataBind();
           
           

            //מכניס עקסב חדשה לטבלת העסקאות
            OleDbConnection Con2 = new OleDbConnection();
            Con2.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source=" 
+ Server.MapPath("") + "\\eyalDataBase.accdb";
            Con2.Open();

            string sqlstring2 = "INSERT INTO MyDeals(Username,MyDate,ItemNum,SumPrice) VALUES ('"
                + Session["cookie"].ToString() + "','" + DateTime.Now.ToString() + "','" +
                b.itemSum.ToString() + "','" + b.sum.ToString() +  "')";
            OleDbCommand cmd2 = new OleDbCommand(sqlstring2, Con2);
            cmd2.ExecuteNonQuery();
            Con2.Close();

            //חובה לקחת את המספר הזמנה 454332 לדוגמא מהטבלה שהמספר הזה נוצר אוטומטית
            OleDbConnection Con4 = new OleDbConnection();
            Con4.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con4.Open();

            string sqlstring4 = "select DealID from MyDeals WHERE Username = '" + Session["cookie"].ToString()+ "' ORDER BY DealID DESC ";
            OleDbCommand Cmd4 = new OleDbCommand(sqlstring4, Con4);
            OleDbDataReader Dr4 = Cmd4.ExecuteReader();
            Dr4.Read();
            int ThisDealID =Int32.Parse(Dr4["DealID"].ToString());
            Con4.Close();

            
            foreach (item i in b.Basket)
            {
                double singlePrice = double.Parse(getInfoFromATbl(i.ProductKey, "Price"));

                double sale = 100 - double.Parse(getInfoFromATbl(i.ProductKey, "Discount"));
                sale = sale * 0.01;
                double afterPrice = sale * singlePrice;
                //מכניס הזמנות לבדידים
                OleDbConnection Con3 = new OleDbConnection();
                Con3.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con3.Open();

                string sqlstring3 = "INSERT INTO MyOrders(DealID,InstID,SinglePrice,Username,Amount) VALUES ( "
                + ThisDealID + ","+i.ProductKey+",'"+afterPrice.ToString()+"','"+ Session["cookie"].ToString()+"','"+i.Count.ToString()+"' )";
                OleDbCommand cmd3 = new OleDbCommand(sqlstring3, Con3);
                cmd3.ExecuteNonQuery();
                Con3.Close();
            }
            
            b.ClearArrAndDT();//מנקה את הסל

        }

        //מעביר לדף של תמונה
        protected void ImgView_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton i = (ImageButton)sender;
            Session["product"] = i.CommandName;
            Response.Redirect("showProducts.aspx");
        }
        //מעביר לדף של שם מוצר
        protected void proName_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            Session["product"] = link.CommandName;
            Response.Redirect("showProducts.aspx");
        }
        //מוחק המעגלה
        protected void trash_Click(object sender, ImageClickEventArgs e)
        {

            ImageButton icon = (ImageButton)sender;
            int key = Int32.Parse(icon.CommandName);
            basket b = new basket();
            b = (basket)Session["basket"];
            b.RemItem(key);
            Session["basket"] = b;


        }
        //פלוסס כמות או מינוס
        protected void plusMinus_Click(object sender, EventArgs e)
        {
            int num = -1;
            Button btn = (Button)sender;
            if (btn.Text == "+")
                num = 1;
            int key = Int32.Parse(btn.CommandName);
            basket b = new basket();
            b = (basket)Session["basket"];
            b.PlusMinus(key, num);
            Session["basket"] = b;

        }

    }
}