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
    public partial class InsertInst : myLibrary
    {
        public ArrayList localInstArr;
        public static int newInstNum = 0;
        public static bool isUploaded = false;
        public static DataTable DT;
        FarFarAway.WebService1 F = new FarFarAway.WebService1();

        protected void Page_Load(object sender, EventArgs e)
        {

            Session["userStat"] = "admin";
            if (!IsPostBack)
            {


                ////
                OleDbConnection Con1 = new OleDbConnection();
                Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                    + Server.MapPath("") + "\\eyalDataBase.accdb";
                Con1.Open();

                string sqlstring = "select * from MyMenu";
                OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
                OleDbDataReader Dr = Cmd.ExecuteReader();
                typeBox.DataSource = Dr;
                typeBox.DataTextField = "MyTitle";
                typeBox.DataBind();
                Con1.Close();

                Session["SapakPrd"] = new DataTable();

            }


        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            typeBox_TextChanged(sender, e);//מעדכן את הסוגים והתת סוגים פעם ראשונה
            SyncWithSapak();//כשאני נכנס כאדמיין אני רוצה ישר שהספק והטבלת כלים שלי יסתנכרנו
        }


        protected void InsertInst_Click(object sender, EventArgs e)
        {
            if (isUploaded)
            {
                if (F.NameDontExist(instNameBox.Text))
                {
                    OleDbConnection Con1 = new OleDbConnection();
                    Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
        + Server.MapPath("") + "\\eyalDataBase.accdb";
                    Con1.Open();

                    //גוזר את ההתחלה של קובץ התמונה שיהיה בלי ~instrumensrDN
                    string pic = ProfilePic.ImageUrl;
                    int pos = pic.IndexOf("/", 2);
                    string shortUrl = pic.Substring(pos + 1);



                    //מכניס לטבלה של הכלים שלי קודם
                    string sqlstring1 = "INSERT INTO MyInstruments(InstName,Price,Supply,Company,Discount," +
                        "InstYear,Type,InnerType,Sold,Description,MyImage) " +
                        "VALUES ('" + instNameBox.Text + "'," + priceBox.Text + ",'" + 0 + "','"
                        + companyBox.Text + "','" + discountBox.Text + "','" + instYear.Text + "','"
                        + TitleToKatID(typeBox.SelectedValue, "MyMenu")
                        + "','" + TitleToKatID(innerTypeBox.SelectedValue, "MyInnerMenu") +
                        "',0,'" + description.Text + "','" + shortUrl + "')";

                    OleDbCommand cmd1 = new OleDbCommand(sqlstring1, Con1);
                    cmd1.ExecuteNonQuery();
                    prompt.Text = "כלי נוסף בהצלחה";
                    prompt.ForeColor = System.Drawing.Color.Green;
                    Con1.Close();

                    //לוקח את המקט של הכלי שנוסף ממש זה עתה
                    int thisID = getLastInstID();
                    string thisName = instNameBox.Text;

                    //מוסיף את הכלי לטבלת הספק
                    //שם אספקה ות.ז.
                   F.InsertSapak(thisName, Convert.ToInt32(supplyBox.Text), shortUrl, instYear.Text, companyBox.Text, typeBox.Text, innerTypeBox.Text);

                    //לאחר שהמוצר נכנס לטבלה אני מוסיף לו אספקה
                    int moreSupply = F.GetSapak(thisName, Convert.ToInt32(supplyBox.Text));//לוקח מהספק
                    addSupply(thisID, moreSupply);//שיפ לב שפה אני בכל זאת משתמש במקט כי זה רק על הטבלה המקומית בלי ספק בכלל
                                                  //מוסיף לקובץ אקסס המקורי כמות של אספקה לכלי מסוים
                }
                else
                {
                    prompt.Text = "שם הכלי כבר קיים!!";
                    prompt.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                prompt.Text = "לא צורפה תמונה!!";
                prompt.ForeColor = System.Drawing.Color.Red;
            }
        }

        //הוספה ידנית של אספקה לטבלה של הכלים זה בדיוק כמו להוסיף דרך האקסס עצמו עוד אספקה
        //אין פה עבודה עם הספק, זה למקרה שיש טעות במלאי וובמקום 50 רוצים לעדכן ל30 אז במקום לפתוח את הטבלה ולחפש
        //מה צריך לעדכן ואיפה אז פה זה עושה את זה באופן אוטומטי בלי טעויות אנוש
        public void addSupply(int InstID, int amount)
        {
            int cursup = Convert.ToInt32(getInfoFromATbl(InstID, "Supply"));
            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
    + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con.Open();
            int after = amount + cursup;

            string sqlstring = "UPDATE MyInstruments SET Supply = '" + after + "' WHERE InstID = " + InstID + " ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            Cmd.ExecuteNonQuery();
            Con.Close();
        }
       

        public int getLastInstID()
        {
            OleDbConnection Con2 = new OleDbConnection();
            Con2.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con2.Open();
            string sqlstring2 = "select TOP 1 * from MyInstruments ORDER BY InstID DESC ";
            OleDbCommand Cmd2 = new OleDbCommand(sqlstring2, Con2);
            OleDbDataReader Dr2 = Cmd2.ExecuteReader();
            Dr2.Read();
            int id = Convert.ToInt32(Dr2["InstID"]);
            Con2.Close();
            return id;
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


                        prompt.Text = "";
                        //לוקח מיקום של הקובץ בדירקטורי
                        string folderPath = Server.MapPath("~/intrumentsImagesDB/");

                        //שומר את הקובץ בתיקייה של תמונות פרופיל
                        FileUploaded.SaveAs(folderPath + Path.GetFileName(FileUploaded.FileName));

                        //מראה את התמונה
                        ProfilePic.ImageUrl = "~/intrumentsImagesDB/" + Path.GetFileName(FileUploaded.FileName);
                        isUploaded = true;

                    }
                    else
                    {
                        prompt.Text = "הקובץ שהועלה אינו תמונה.";
                    }
                }
                else
                {
                    prompt.Text = "הקובץ שהועלה גדול מדי!";
                }
            }
        }
        //title to type
        protected void typeBox_TextChanged(object sender, EventArgs e)
        {
            //לוקח את השם תופים מהדרופ און הרשון והולך לטבלה כדי לראות איך קוראים לזה באנגלית כלומר מה הטייפ של הטייטל הזה
            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con.Open();

            string sqlstring = "select KatID from MyMenu WHERE MyTitle ='" + typeBox.SelectedValue + "'";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            Dr.Read();
            string uptitle = Dr["KatID"].ToString();
            Con.Close();

            //////////////////
            ///type ->> inner type
            //אחרי שהוא לקח את הטייפ של הטייטל הראשון הוא בודק ולוקח את כל האיננר טייפ שבעלי אותו טייפ הנל
            OleDbConnection Con2 = new OleDbConnection();
            Con2.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con2.Open();

            string sqlstring2 = "select MyTitle from MyInnerMenu WHERE Type='" + uptitle + "'";
            OleDbCommand Cmd2 = new OleDbCommand(sqlstring2, Con2);
            OleDbDataReader Dr2 = Cmd2.ExecuteReader();
            innerTypeBox.DataSource = Dr2;
            innerTypeBox.DataTextField = "MyTitle";
            innerTypeBox.DataBind();

            Con2.Close();
        }

        //לפי טייטל אני מקבל את הקטלוג
        //כלומר לפי זה שכתוב גיטרה חחשמלית זה יחזיר לי את הקטלוג 
        // guitar -> electric
        public string TitleToKatID(string title, string table)
        {

            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con.Open();

            string sqlstring = "select * from " + table + " WHERE MyTitle ='" + title + "'";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            Dr.Read();
            string KatID = Dr["KatID"].ToString();
            Con.Close();
            return KatID;
        }


        //לקיחת וחיסור כמות רצויה מהספק והסופה לטבלת הכלים עבור כלי מסוים
        public void AddSupplyBtn(object sender, EventArgs e)
        {
            int InstID = Convert.ToInt32(InstIDBox.Text);
            int amount = Convert.ToInt32(amountBox.Text);

            //לאחר שהמוצר נכנס לטבלה אני מוסיף לו אספקה
            string InstName = getInfoFromATbl(InstID, "InstName");
            int moreSupply = F.GetSapak(InstName, amount);
            addSupply(InstID, moreSupply);  //מוסיף לקובץ אקסס המקורי כמות של אספקה לכלי מסוים

            prompt.Text = "אספקה של " + moreSupply + " פריטים נוספה בהצלחה ל   " +InstName + " (" +InstID + ")";
            prompt.ForeColor = System.Drawing.Color.Green;//הודעה
        }



        //הפונקציה מסתנכרת עם הספק וכל כלי חדש הוא הוציא לאור נכנס לטבלה וירטואלית DT
        protected void SyncWithSapak()
        {
            DT = (DataTable)Session["SapakPrd"];
         
            DT.Clear();
            if (DT.Columns.Count == 0)
            {
                string[] cols = new[] {"InstName", "MySupply", "InstYear", "Company", "Type",
                    "InnerType","MyImage" };

                for (int i = 0; i < cols.Length; i++)
                    DT.Columns.Add(cols[i]);

            }

            //יוצר מערך ראשון של שמות מהטבלת הכלים המקומית
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
+ Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            string sqlstring = "select * from MyInstruments";
            OleDbCommand cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = cmd.ExecuteReader();

            localInstArr = new ArrayList();//הגדרתי אותה כבר למעלה כציבורית כדי שתעבור בין פונצקיות

            if (Dr.HasRows)
            {
                while (Dr.Read())
                {
                    localInstArr.Add((Dr["InstName"].ToString()));
                }
            }
            Con1.Close();

            //פרוקסי לספק
            object[] arr =F.GetNameArr();//מבקש מהספק לתת מערך של שמות של כל הכלים שלו
            ArrayList sapakInstArr = new ArrayList();// הוא חזיר את המערך כמערך רגיל ואני ממיר אותו לאררי ליסט כי יותר נוח
            sapakInstArr.AddRange(arr);//cast from object[] to arraylist




            foreach (string InstName in sapakInstArr)
            {
                if (!IsExistInLocal(InstName))// אם מוצא פריט חדש שיש רק לספק אז יוצר שורה בטבלה הוירטאולית
                {
                    DataRow InstRow = DT.NewRow();

                    //לוקחים מהספק את השורה של המוצר לפי השם שלו
                    // ואז ממלאים את השורה ב טבלה הוירטואלית לפי השורה באקסס

                    InstRow["InstName"] = InstName;
                    InstRow["MySupply"] = F.getInfoFromSapak(InstName,"MySupply");
                    InstRow["InstYear"] = F.getInfoFromSapak(InstName, "InstYear");
                    InstRow["Company"] = F.getInfoFromSapak(InstName, "Company");
                    InstRow["Type"] = F.getInfoFromSapak(InstName, "Type");
                    InstRow["InnerType"] = F.getInfoFromSapak(InstName, "InnerType");
                    InstRow["MyImage"] = imgSrc + F.getInfoFromSapak(InstName, "MyImage");

                    Con1.Close();

                    DT.Rows.Add(InstRow);//מחבר את השורה
                }
            }

            sapakCollection.DataSource = DT;///מחבר לדאטאליסט
            sapakCollection.DataBind();
            Session["SapakPrd"] = DT;//מכניס לסשן
        }

        //פונקציה שמקבלת שם מוצר ומחזירה אמת אם הוא נמצא בטבלת הכלים המקומית אחרת שקר
        public bool IsExistInLocal(string SapakInstName)
        {
            bool found = false;
            foreach (string localInstName in localInstArr)
            {
                if (localInstName == SapakInstName)
                {
                    found = true;
                }
            }
            return found;
        }

        protected void sapakCollection_ItemCommand(object source, DataListCommandEventArgs e)
        {
         
            TextBox PriceBoxSapak = e.Item.FindControl("PriceBoxSapak") as TextBox;
            TextBox discountBoxSapak = e.Item.FindControl("discountBoxSapak") as TextBox;
            TextBox desBoxSapak = e.Item.FindControl("desBoxSapak") as TextBox;
            TextBox supplyBoxSapak = e.Item.FindControl("supplyBoxSapak") as TextBox;

            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
+ Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();


            DT = (DataTable)Session["SapakPrd"];//מוציא מסשן

            string pic = DT.Rows[e.Item.ItemIndex]["MyImage"].ToString();
            int pos = pic.IndexOf("/", 2);
            string shortUrl = pic.Substring(pos + 1);

            int p = Convert.ToInt32(PriceBoxSapak.Text);

            //אני קורא לספק להביא לי כמות מסוימת לפי היכולות שלו והוא מחזיר למשתנה מורספליי כמות ומחסר ממנו את אותה הכמות
            //לכן את המורספליי אני אכניס לטבלה באינסרט אינטו
            int moreSupply = F.GetSapak(DT.Rows[e.Item.ItemIndex]["InstName"].ToString(),Convert.ToInt32(supplyBoxSapak.Text));

            //מכניס לטבלה של הכלים שלי קודם
            string sqlstring1 = "INSERT INTO MyInstruments(InstName,Price,Supply,Company,Discount," +
                "InstYear,Type,InnerType,Sold,Description,MyImage) " +
                "VALUES ('" + DT.Rows[e.Item.ItemIndex]["InstName"].ToString() + "' ," + p + ",'" + moreSupply + "','"
                + DT.Rows[e.Item.ItemIndex]["Company"].ToString() + "','" +
               discountBoxSapak.Text + "','" + DT.Rows[e.Item.ItemIndex]["InstYear"].ToString() + "','"
                + TitleToKatID(DT.Rows[e.Item.ItemIndex]["Type"].ToString(), "MyMenu")
                + "','" + TitleToKatID(DT.Rows[e.Item.ItemIndex]["InnerType"].ToString(), "MyInnerMenu") +
                "',0,'" + desBoxSapak.Text + "','" + shortUrl + "')";

            OleDbCommand cmd1 = new OleDbCommand(sqlstring1, Con1);
            cmd1.ExecuteNonQuery();
            prompt.Text = "כלי נוסף למחסן בהצלחה";
            prompt.ForeColor = System.Drawing.Color.Green;
            Con1.Close();

            DT.Rows[e.Item.ItemIndex].Delete();
            Session["SapakPrd"] = DT;//מכניס לסשן


        }
    }
}