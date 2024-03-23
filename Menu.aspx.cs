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
    public partial class Menu : myLibrary
    {
        protected void Page_Load(object sender, EventArgs e)
        {//בדיקות תקינות
            if (Session["userStat"] == null || Session["userStat"].ToString() == "admin")
            {
                Session["userStat"] = "guest";
            }

            if (Session["userStat"].ToString() == "customer")//אם לקוח נכנס שלא יקרה אפשרות להתחבר או להרשם
            {
                toLogin.Visible = false;
                tosignUp.Visible = false;
            }

           

            //פעם ראשונה נוצר מערך הקטלוג
            if (!IsPostBack)
            {
                //יוצר שרשור של קטלוג 
                //שרשור נראה כך:
                // גיטרה >> בס
                //או קלידים >> חשמלי
                //צריך ליצור כל פעם שנגמר הסשן
                object[] katArr = new object[2];
                katArr[0] = null;
                katArr[1] = null;
                Session["katalog"] = katArr;

                ///לוקח את ההכי נמכרים
                OleDbConnection Con3 = new OleDbConnection();
                Con3.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con3.Open();

                string sqlstring3 = "Select TOP 8 PERCENT * from MyInstruments Order By Sold DESC";
                OleDbCommand Cmd3 = new OleDbCommand(sqlstring3, Con3);
                OleDbDataReader Dr3 = Cmd3.ExecuteReader();
                BestSellers.DataSource = Dr3;
                BestSellers.DataBind();
                Con3.Close();

                //אם קיים משתמש מחובר הצע לו פרטים לפי הקנייה עאחרונה שלו
                if (Session["userStat"].ToString() == "customer")
                {
                    forUlbl.Text = "במיוחד עבורך:";
                    ///לוקח את מה שעבורי פור יו
                    OleDbConnection Con4 = new OleDbConnection();
                    Con4.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                        + Server.MapPath("") + "\\eyalDataBase.accdb";
                    Con4.Open();

                    string sqlstring4 = "Select TOP 1 * from MyOrders" +
                        " WHERE Username ='" + Session["cookie"].ToString() + "' Order By DealID DESC";
                    OleDbCommand Cmd4 = new OleDbCommand(sqlstring4, Con4);
                    OleDbDataReader Dr4 = Cmd4.ExecuteReader();
                    Dr4.Read();
                     //מוציא את כלי הנגינה האחרון שקנית
                    int lastInst;
                    Boolean everBoghtItem = false;
                    if (Dr4.HasRows)
                        everBoghtItem = true;

                    string InnerType = "";
                    string Type = "";
                    if (everBoghtItem)
                    {
                        lastInst = (int)Dr4["InstID"];
                        //מוציא את הסוג והתת סוג שלו
                         Type = getInfoFromATbl(lastInst, "Type");
                        InnerType = getInfoFromATbl(lastInst, "InnerType");
                    }

                    Con4.Close();

                    if (everBoghtItem)
                    {
                        //שולף מהטבלה את כל הכלים שמאותו סוג ותת סוג
                        OleDbConnection Con5 = new OleDbConnection();
                        Con5.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                            + Server.MapPath("") + "\\eyalDataBase.accdb";
                        Con5.Open();

                        string sqlstring5 = "Select TOP 4 * from MyInstruments" +
                            " WHERE Type ='" + Type + "' AND  InnerType = '" + InnerType + "' ";
                        OleDbCommand Cmd5 = new OleDbCommand(sqlstring5, Con5);
                        OleDbDataReader Dr5 = Cmd5.ExecuteReader();
                        forU.DataSource = Dr5;
                        forU.DataBind();

                        Con5.Close();
                    }


                    string sqlstring5 = "Select TOP 4 * from MyInstruments" +
                        " WHERE Type ='" + Type + "' AND  InnerType = '" + InnerType + "' ";
                    OleDbCommand Cmd5 = new OleDbCommand(sqlstring5, Con5);
                    OleDbDataReader Dr5 = Cmd5.ExecuteReader();
                    forU.DataSource = Dr5;
                    forU.DataBind();
                    Con5.Close();


                }
            }
        }
        //עובד לשני הדאטאליסטים כי אני לוקח את ה סורס source 
        protected void DataList_ItemCommand(object source, DataListCommandEventArgs e)
        {
            DataList dataList = (DataList)source;// לוקח את מקור הדאטאליסט כדי שזה יעבוד על שני ליסטים שונים בעלי אותו שם

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
                        if (BasketItem.ProductKey == (int)dataList.DataKeys[e.Item.ItemIndex])
                        {
                            b.PlusMinus(BasketItem.ProductKey, 1);
                            NewPro = false;
                        }
                    }
                    if (NewPro)
                    {
                        item i = new item();
                        i.ProductKey = (int)dataList.DataKeys[e.Item.ItemIndex];
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
                int MyItemkey = (int)dataList.DataKeys[e.Item.ItemIndex];
                Session["product"] = MyItemkey;
                Response.Redirect("showProducts.aspx");
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            //תמיד כל קריאה לשרת הוא מעדכן את הפריטים השלופים לפי הסשן של הקטלוג
            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";

            Con.Open();
            string sqlstring = "";// למקרה שלא קורה שום תנאי זה מחייב לצערי
            object[] arr = (object[])Session["katalog"];//מוציא את המערך עם שני התאים מהסשן/

            string arr0, arr1;
            if (arr[0] == null)
                arr0 = "null";
            else
                arr0 = arr[0].ToString();

            if (arr[1] == null)
                arr1 = "null";
            else
                arr1 = arr[1].ToString();

           // Response.Write(" _>" + arr0 + "-->" + arr1);



            if (arr[0] == null)
            {//אם התא הראשון לא מוגדר אם מן הסתם גם השני לא יהיה לכן ברירת מחדל של כל הפרטים
                sqlstring = "SELECT * from MyMenu";
                menuLbl.Text = "בחר קטגוריה";
                AllInst.Text = "כל הכלים";
                menuImg.ImageUrl = "pics/wideGuitarLogo.png";
            }
            else//אם כן יש הגדרה לסוג  
            {
                if (arr[1] == null)// אם אין תת הגדרה אזת ראה רק סוג z
                {
                    sqlstring = "select * from MyInnerMenu WHERE Type='" + arr[0].ToString() + "'";
                    string katName = getInnerMenuTBL(arr[0].ToString(), "MyTitle");
                    string katImage = getInnerMenuTBL(arr[0].ToString(), "MyImage");
                    menuLbl.Text = katName;
                    AllInst.Text = "כל ה" + katName;
                    menuImg.ImageUrl = imgSrc + katImage;
                }
                else// המצב הכי מורכב בו יש שני הגדרות לשני התאים
                {
                    Response.Redirect("HomePage.aspx");
                }
            }
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader dr = Cmd.ExecuteReader();

            MyMenu.DataSource = dr;
            MyMenu.DataBind();

            Con.Close();
        }

        public string getInnerMenuTBL(string KatID, string wantedField)
        {
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            string sqlstring = "select " + wantedField + " from myMenu WHERE KatID = '" + KatID + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            Dr.Read();

            string value = Dr[wantedField].ToString();
            Con1.Close();
            return value;
        }


        protected void MyMenu_ItemCommand(object source, DataListCommandEventArgs e)
        {
            string KatID = MyMenu.DataKeys[e.Item.ItemIndex].ToString();// לוקח את המכוון
            object[] arr = new object[2];
            arr = (object[])Session["katalog"];//מוציא את המערך עם שני התאים מהסשן/
            if (arr[0] == null)
                arr[0] = KatID;
            else
                arr[1] = KatID;

        }

        protected void AllInst_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
            //לוקח אותי להצגת דף ומה שוקטע מה שאני אראה שם יהיה הסשן קטלוג- המערך שלי
        }
    }
}
