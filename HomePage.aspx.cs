using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

namespace EyalProject
{

    public partial class HomePage : myLibrary
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userStat"] == null || Session["userStat"].ToString() == "admin")
            {
                Session["userStat"] = "guest";
            }
            if (Session["katalog"] == null)
                Response.Redirect("Menu.aspx");

            if (Session["userStat"].ToString() == "customer")//אם לקוח נכנס שלא יקרה אפשרות להתחבר או להרשם
            {
                toLogin.Visible = false;
                tosignUp.Visible = false;
            }
            

            if (!IsPostBack)
            {
                //first time entering this specific page, no matter have was before
                OleDbConnection Con = new OleDbConnection();
                Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                        + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con.Open();

                string sqlstring = "select * from MyInstruments";// נ////ותן !!!!!
                object[] arr = (object[])Session["katalog"];//מוציא את המערך עם שני התאים מהסשן/
                if (arr[0] == null)
                {// אם הראשון ריק אז שניהם ריקים
                    sqlstring = "select * from MyInstruments";
                    // אם שניהים ריקים אזי הגעתי מהתרפיט להצת מוצר בהומפייג 
                    //דרך התפריט הכי כללי. לכן הכפתור ששלח אותי לשם רצה שאציג את כל הפריטים
                }
                else if (arr[1] == null)
                {
                    sqlstring = "select * from MyInstruments WHERE Type='" + arr[0].ToString() + "' ";
                    //אם רק שני רק אזי הראשון מלא ויש לי  כותרת סוג מסוים
                    //של כלי נגינה כמו רק תופים או רק יוקללה או רק קלידים
                    //לכן הכפתור ששלח אותי מהמניו להופייג הצגת מוצרים רצה שאציג את כל המוצרים בכותרת הזו
                }
                else if (arr[0] != null && arr[1] != null) //אם כן יש הגדרה לסוג  
                {
                    sqlstring = "select * from MyInstruments WHERE Type='" + arr[0].ToString() +
                    "' AND InnerType ='" + arr[1].ToString() + "' ";
                }

                OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
                OleDbDataReader dr = Cmd.ExecuteReader();

                DataList.DataSource = dr;
                DataList.DataBind();

                Con.Close();


            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            object[] arr = (object[])Session["katalog"];
           
            foreach (DataListItem pro in DataList.Items)
            { //עובר בלוק בלוק או בדאטאליסט ובכל בלוק מחפש את הכפתור של ההוספה לעגלה
                Button addBtn = pro.FindControl("addTobasket") as Button;//מוצא אותו
                int proKey = (int)DataList.DataKeys[pro.ItemIndex];//לוקח ממנו את המקט שלו דרך הדאטאליסט
                int curSup = Int32.Parse(getInfoFromATbl(proKey, "Supply"));//שולף את האספקה שלו

                if (curSup <= 0)//אוזל המלאי
                {
                    addBtn.Enabled = false;
                    addBtn.Text = "אין במלאי";
                    addBtn.ForeColor = System.Drawing.Color.Brown;

                    if (Session["basket"] != null)// אם יש יוזר מחובר עם סל שיוציא לו מהסל
                    {
                        basket b = new basket();
                        b = Session["basket"] as basket;
                        b.RemItem(proKey);//אם קיים בסל אזי מוציא אם לא לא קורה כלום

                    }
                }
                else // אם יש מלאי
                {
                    if (Session["basket"] != null)// אם יש יוזר שיש לו בסל כמות צריך לדאוג שלא יעבור את האספקה
                    {
                        basket b = new basket();
                        b = Session["basket"] as basket;
                        foreach (item i in b.Basket)//מחפש אותו אם הוא נמצא בסל שלי
                        {
                            if (i.ProductKey == proKey)// אם כן קיים בסל אני צריך לבדוק כמה שמתי בסל כדי שלא אשים בסל יותר מהמלאי הקיים
                            {
                                if (i.Count >= curSup)
                                { //אם יש כמות בסל של אותו אייטם ששווה לאספקה או יותר אם אפשר להוסיף יותר לסל
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
                }
            }
        }

        protected void DataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            // Response.Write(e.Item.ItemIndex.ToString());
            if (e.CommandName == "AddtoBasket")
            {

                bool NewPro = true;
                if (Session["userStat"].ToString() == "customer")//רק ללקוחות ולא לאורחים!!
                {
                    basket b = new basket();
                    b = (basket)Session["basket"];
                    //  item choosenItem = b.Basket[(int)DataList.DataKeys[e.Item.ItemIndex]] as item;//לוקח את האייטם הנלחץ
                    //  int supply = Int32.Parse(getInfoFromATbl(choosenItem.ProductKey, "Supply"));// בודק את הכמות שלו

                    foreach (item BasketItem in b.Basket)
                    {
                        if (BasketItem.ProductKey == (int)DataList.DataKeys[e.Item.ItemIndex])
                        {
                            b.PlusMinus(BasketItem.ProductKey, 1);
                            NewPro = false;

                        }
                    }
                    if (NewPro)
                    {
                        item i = new item();
                        i.ProductKey = (int)DataList.DataKeys[e.Item.ItemIndex];
                        i.Count = 1;
                        b.AddItem(i);
                    }
                    Session["basket"] = b;//חייב שגם אם נוסף אייטם חדש וגם עם התווסף בכמות הסל חייב לזור לסשן


                }

                Panel cart = Master.FindControl("cartPanel") as Panel;
                cart.Style.Add("display", "block");
                //אחרי קנייה מציג את העגלה גם אם היא לא פתוחה

            }
            else if (e.CommandName == "toProduct")
            {
                int MyItemkey = (int)DataList.DataKeys[e.Item.ItemIndex];
                Session["product"] = MyItemkey;
                Response.Redirect("showProducts.aspx");
            }
        }

        protected void PriceOrder_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string sqlstring = "";
            object[] arr = (object[])Session["katalog"];//מוציא את המערך עם שני התאים מהסשן/
            if (arr[0] == null)//אם הראשון ריק אזיגם השני כלומר אני רוצה לסדר לפי מחיר ששניהם ריקים כלומר אני נמצא במצב שכל הכלים
                               //בחנות מוצגים לכן אבתקש מהמסד סדר של כל הכלי לפי מחיר
            {
                if (b.CommandName == "lowToHigh")
                    sqlstring = "select * from MyInstruments ORDER BY Price ASC";
                else if (b.CommandName == "highToLow")
                    sqlstring = "select * from MyInstruments ORDER BY Price DESC";
            }
            else if (arr[1] == null) {
                if (b.CommandName == "lowToHigh")
                    sqlstring = "select * from MyInstruments WHERE Type='" + arr[0].ToString() +"' ORDER BY Price ASC";
                else if (b.CommandName == "highToLow")
                    sqlstring = "select * from MyInstruments WHERE Type='" + arr[0].ToString() +"' ORDER BY Price DESC";
            }
            else { //מקרה שלישי ששני התאים מלאים וזה מקרה ספיציפי לבקש אבקש סדר עולה ויורד לפי סוג ותת סוג מסוים
                if (b.CommandName == "lowToHigh")
                    sqlstring = "select * from MyInstruments WHERE Type='" + arr[0].ToString() +
                       "' AND InnerType ='" + arr[1].ToString() + "' ORDER BY Price ASC";
                else if (b.CommandName == "highToLow")
                    sqlstring = "select * from MyInstruments WHERE Type='" + arr[0].ToString() +
                       "' AND InnerType ='" + arr[1].ToString() + "' ORDER BY Price DESC";
            }



            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();

            if (Dr.HasRows)
            {
                DataList.DataSource = Dr;
            }
            else
            {
                DataList.DataSource = null;
            }
            DataList.DataBind();
            Con1.Close();
        }
    }
}