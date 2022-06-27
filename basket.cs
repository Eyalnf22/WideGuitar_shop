using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
namespace EyalProject
{
    public partial class basket
    {
        private ArrayList _basket = new ArrayList();
        public DataTable _dt = new DataTable();
        public double _sum = 0;//סכום כל הפריטים
        public int _itemSum = 0;//כמות הפריטים

        public basket()
        {
            string[] cols = new[] {"ProID", "ProName", "singleP", "Count", "originalP",
                    "discount", "comp", "finalP", "ImageColumn" };

            for (int i = 0; i < cols.Length; i++)
                dt.Columns.Add(cols[i]);

            dt.Columns.Add("IsMin", typeof(bool));
            dt.Columns.Add("IsMax", typeof(bool));
        }
        public void UpdateDT()
        {
            dt.Clear();
            itemSum = 0;
            sum = 0;
            foreach (item i in Basket)
            {
                myLibrary L = new myLibrary();
                DataRow inst = dt.NewRow();

                inst["ProID"] = L.getInfoFromATbl(i.ProductKey, "InstID");

                inst["ProName"] = L.getInfoFromATbl(i.ProductKey, "InstName");
                double singlePrice = double.Parse(L.getInfoFromATbl(i.ProductKey, "Price"));
                inst["singleP"] = "₪" + singlePrice.ToString();

                inst["Count"] = i.Count.ToString();
                double originalPrice = (i.Count * singlePrice);

                inst["originalP"] = "₪" + originalPrice.ToString();
                inst["discount"] = L.getInfoFromATbl(i.ProductKey, "Discount") + "%";
                inst["comp"] = L.getInfoFromATbl(i.ProductKey, "Company");

                //חישובי אוחזוי הנחה כמו אצל טניה בכיתה ט
                double sale = 100 - double.Parse(L.getInfoFromATbl(i.ProductKey, "Discount"));
                sale = sale * 0.01;
                double afterPrice = sale * originalPrice;

                inst["finalP"] = "₪" + afterPrice.ToString();
                Image img = new Image();
                img.ImageUrl = "intrumentsImagesDB/greenie.jpg";

                inst["ImageColumn"] = L.imgSrc + L.getInfoFromATbl(i.ProductKey, "MyImage");


                bool isMinCount = true;
                if (i.Count == 1)
                    isMinCount = false;

                bool isMaxCount = true;


                if (i.Count >= Int32.Parse(L.getInfoFromATbl(i.ProductKey, "Supply")))
                    isMaxCount = false;

                inst["IsMin"] = isMinCount;
                inst["IsMax"] = isMaxCount;

                dt.Rows.Add(inst);

                itemSum += i.Count;
                sum += afterPrice;
            }
        }
        public void AddItem(item i)
        {
            myLibrary L = new myLibrary();
           int curSup = Int32.Parse(L.getInfoFromATbl(i.ProductKey, "Supply"));
            if (i.Count <curSup || i.Count ==1)//רק אם יש מספיק מלאי
            _basket.Add(i);
            
            UpdateDT();
        }
        public void RemItem(int key)
        {
            item i;
            for (int k = 0; k < Basket.Count; k++)///חייב לולאת פור כדי לבל אינדס
            {
                i = (item)Basket[k];//לוקח מהאארי ליסט אובייקט במיקום קיי וממיר אותו לאייטם 

                if (i.ProductKey == key)
                {
                    Basket.RemoveAt(k);
                    //מוציא מהסל
                    UpdateDT();//בונה טבלה מחדש
                    break;//הברייק פה הוכרחי כי אם לא אז הוא ממשיך לחפש בתא הבא אבל הוא
                          //לא יהיה קיים עבור 3 מוצרים בסל
                }
            }
        }

        public void PlusMinus(int key, int num)
        {
            myLibrary L = new myLibrary();
          
                foreach (item i in Basket)
            {
                int curSup = Int32.Parse(L.getInfoFromATbl(i.ProductKey, "Supply"));
                    if (i.ProductKey == key)
                {
                    if ((i.Count < curSup && num == 1) || num == -1  )
                    {
                        //אם הנאמ שלילי אז תעשה רגיל כי זה  לא מפריע
                        // אם הכמות היא אחד, תריץ את הקוד אחרת זה לא יעבוד כי במקרה שיש אספקה אחת ומוצר אחד בסל הכמות לעולם לא תהיה קטנה מאספקה,סמוך עלי תשאיר ככה!
                        i.Count += num;
                        UpdateDT();
                        break;
                    }
                }
            }
        }

        public void ClearArrAndDT()
        {
            Basket.Clear();
            UpdateDT();
        }

        public void RemoveArrAndDT(int index)
        {
            Basket.RemoveAt(index);
            UpdateDT();
        }

        public override string ToString()
        {
            return Basket.ToString();
        }
        public ArrayList Basket
        {
            get { return _basket; }
            set { _basket = value; }
        }
        public DataTable dt
        {
            get { return _dt; }
            set { _dt = value; }
        }
        public double sum
        {
            get { return _sum; }
            set { _sum = value; }
        }
        public int itemSum
        {
            get { return _itemSum; }
            set { _itemSum = value; }

        }

    }
}