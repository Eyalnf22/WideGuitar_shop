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
using System.Web.UI.DataVisualization.Charting;

namespace EyalProject
{
    public partial class Stat : myLibrary
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userStat"] = "admin";
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            /// לוקח את הגילאים
            FindAges();
            /// לוקח מינים
            FindSexAndArea();
            /// לוקח  הכי נמכרים
             FindBestSellers();
            //כמות דילים
            FindDealNum();
            //כמות כלים
            FindInstNum();

        }
        public void FindInstNum()
        {
            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con.Open();

            string sqlstring = "select COUNT(InstID) as total from MyInstruments";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            Dr.Read();
            int total = Convert.ToInt32(Dr["total"]);
            instNum.Text ="כמות המוצרם בחנות: "+ total.ToString();
            Con.Close();
        }
        public void FindDealNum()
        {
            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con.Open();

            string sqlstring = "select COUNT(DealID) as total from MyDeals";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            Dr.Read();
            int total = Convert.ToInt32(Dr["total"]);
            dealNum.Text ="כמות העסקאות שבוצעו: "+ total.ToString();
            Con.Close();
        }
        public void FindSexAndArea()
        {
            Series ser3 = ChartSex.Series["Series3"];
            Series ser4 = chartCity.Series["Series4"];
            OleDbConnection Con3 = new OleDbConnection();
            Con3.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con3.Open();

            string sqlstring3 = "select * from MyusersList";
            OleDbCommand Cmd3 = new OleDbCommand(sqlstring3, Con3);
            OleDbDataReader Dr3 = Cmd3.ExecuteReader();

            int maleSum = 0;
            int femaleSum = 0;
            int otherSum = 0;

            int centerSum = 0;
            int northSum = 0;
            int southSum = 0;
            while (Dr3.Read())
            {
                if (Dr3["MyGender"].ToString() == "other")
                    otherSum++;
                else if (Dr3["MyGender"].ToString() == "male")
                    maleSum++;
                else if (Dr3["MyGender"].ToString() == "female")
                    femaleSum++;

                string area = getAreaCity(Dr3["MyCity"].ToString());
                if (area == "דרום")
                    southSum++;
                else if (area == "צפון")
                    northSum++;
                else if (area == "מרכז")
                    centerSum++;
            }
            ser3.Points.AddXY("זכר", maleSum);
            ser3.Points.AddXY("נקבה", femaleSum);
            ser3.Points.AddXY("אחר", otherSum);

            ser4.Points.AddXY("זרום", southSum);
            ser4.Points.AddXY("צפון", northSum);
            ser4.Points.AddXY("מרכז", centerSum);

            Con3.Close();

            foreach (DataPoint point in ser3.Points)
            {
                point.Label = "#PERCENT\n#VALX";
            }
        }
        public void FindBestSellers()
        {
            Series ser2 = Chart2.Series["Series2"];
            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();

            string sqlstring1 = "select top 5 * from MyInstruments ORDER BY Sold DESC";
            OleDbCommand Cmd1 = new OleDbCommand(sqlstring1, Con1);
            OleDbDataReader Dr1 = Cmd1.ExecuteReader();

            while (Dr1.Read())
            {
                ser2.Points.AddXY(Dr1["InstName"].ToString() + "\n" + Convert.ToInt32(Dr1["Sold"]), Convert.ToInt32(Dr1["Sold"]));
            }
            Con1.Close();
        }
        public void FindAges()
        {
            Series ser = myChart.Series["Series1"];
            // ser.ChartType = SeriesChartType.Area;
            string[] titles = { "groupName", "min", "max", "sum" };
            string[] AgeGroups = { "kids", "teens", "preAdults", "adults", "parents", "old" };
            int[] AgeGaps = { -1, 12, 18, 30, 40, 60, 100 };
            DataTable dt = new DataTable();

            foreach (string title in titles)
            {
                dt.Columns.Add(title);
            }
            for (int i = 0; i < AgeGroups.Length; i++)
            {
                DataRow group = dt.NewRow();
                group["groupName"] = AgeGroups[i];
                group["min"] = AgeGaps[i] + 1;
                group["max"] = AgeGaps[i + 1];
                group["sum"] = 0;

                dt.Rows.Add(group);
            }


            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con.Open();

            string sqlstring = "select * from MyUsersList";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            int age;

           // print(dt.Rows.Count);
            while (Dr.Read())
            {
                age = DateTime.Now.Year - Convert.ToInt32(Dr["MyYear"]);
                foreach (DataRow row in dt.Rows)
                {
                    int minAge = Convert.ToInt32(row["min"]);
                    int maxAge = Convert.ToInt32(row["max"]);
                    if (age >= minAge && age <= maxAge)
                    {
                        int curSum = Convert.ToInt32(row["sum"]);
                        curSum++;
                        row["sum"] = curSum;
                        break;
                    }
                }
            }
            Con.Close();
            foreach (DataRow row in dt.Rows)
            {
                string str = " גיל" + row["min"].ToString() + "-" + row["max"].ToString();
                ser.Points.AddXY(str, Convert.ToInt32(row["sum"]));
            }

            foreach (DataPoint point in ser.Points)
            {
                point.Label = "#PERCENT\n#VALX";
            }
        }
        public string getAreaCity(string CityName)
        {
            OleDbConnection Con = new OleDbConnection();
            Con.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con.Open();

            string sqlstring = "select MyLocation from MyCities WHERE CityName = '" + CityName + "' ";
            OleDbCommand Cmd = new OleDbCommand(sqlstring, Con);
            OleDbDataReader Dr = Cmd.ExecuteReader();
            string value = "";
            if (Dr.HasRows)
            {
                Dr.Read();

                 value = Dr["MyLocation"].ToString();
            }
                Con.Close();
                return value;

        }
    }
}