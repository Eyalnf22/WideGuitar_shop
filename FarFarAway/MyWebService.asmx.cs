using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.OleDb;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace FarFarAway
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public String imgSrc = "intrumentsImagesDB/";
        //לוקח מהספק כמות מסוימת עבור פריט מסוים
        [WebMethod]
        public int GetSapak(string InstName, int amount)
        {
            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\SapakDataBase.accdb";
            Con.Open();

            string sqlstring = "select * from MyInstList WHERE InstName = '" + InstName + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();

            int backAmount = 0;
            //כמה מחזירים בסוף הדיפולט הוא אפס כאילו לא קיבלתי בחזרה כלום המספק

            if (Dr.Read())
            {
                int cursup = Convert.ToInt32(Dr["MySupply"]);
                int left = cursup; // כברירת מחדל אני רוצה שכמה שישארו זה אותו כמות כדי להתעלם משגיאות וכו
                if (cursup > amount)
                {
                    backAmount = amount;
                    left = cursup - amount;
                }
                else
                {
                    backAmount = cursup;
                    //אם יש יותר דריה מכמה שיש במחסן אז אני רוצה לרוקן את המחסן. כלומר הלפט יהיה 0 כי נשארו 0 במחסן
                    left = 0;
                }


                string sqlstring1 = "UPDATE MyInstList SET MySupply = '" + left + "' WHERE InstName = '" + InstName + "' ";
                OleDbCommand cmd1 = new OleDbCommand(sqlstring1, Con);
                cmd1.ExecuteNonQuery();
                Con.Close();
            }
            return backAmount;
        }

        //מחזיר מערך של שמות של הכלים המטלבה של הספק
        [WebMethod]
        public ArrayList GetNameArr()
        {
            ArrayList sapakInstArr = new ArrayList();
            OleDbConnection Con2 = new OleDbConnection();
            Con2.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\SapakDataBase.accdb";
            Con2.Open();

            string sqlstring2 = "select * from MyInstList";
            OleDbCommand cmd2 = new OleDbCommand(sqlstring2, Con2);
            OleDbDataReader Dr2 = cmd2.ExecuteReader();

            if (Dr2.HasRows)
            {
                while (Dr2.Read())
                {
                    sapakInstArr.Add(Dr2["InstName"].ToString());
                }
            }
            Con2.Close();
            return sapakInstArr;
        }

        //מקבל שם ובודק האם מופיע בטבלה של הספק
        [WebMethod]
        public bool NameDontExist(string InstName)
        {

            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\SapakDataBase.accdb";
            Con.Open();

            string sqlstring = "select * from MyInstList WHERE InstName ='" + InstName + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            if (Dr.HasRows)
            {
                Con.Close();
                return false;
            }
            else
            {
                Con.Close();
                return true;

            }
        }

        //מקבל משתנים לשורה בטבלה של הספק ומכניס לספק את השורה הזו
        [WebMethod]
        public void InsertSapak(string InstName, int amount, string image, string instYear, string company, string type, string innertype)
        {
            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\SapakDataBase.accdb";
            Con.Open();

            string sqlstring = "INSERT INTO MyInstList(InstName,MySupply,InstYear,Company,Type,InnerType,MyImage) " +
                "VALUES ('" + InstName + "' ," + amount + ", '" + instYear + "','"
                + company + "', '" + type + "','" + innertype + "','" + imgSrc + image + "')";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            Cmd.ExecuteNonQuery();
            Con.Close();

        }


        //מקבל משתנים לשורה בטבלה של הספק ומכניס לספק את השורה הזו
        [WebMethod]
        public string getInfoFromSapak(string InstName, string wantedField)
        {
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\SapakDataBase.accdb";
            Con1.Open();

            string sqlstring = "select " + wantedField + " from MyInstList WHERE InstName = '" + InstName + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con1);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            Dr.Read();

            string value = Dr[wantedField].ToString();
            Con1.Close();
            return value;

        }
    }
}