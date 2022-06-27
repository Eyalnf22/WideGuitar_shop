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
    
        public partial class update : myLibrary
    {
        public string msg;
        public string msgcolor;
        string newName, newCity, newUsername, newPhone, newEmail, newPassword, newAddres, newId,
            newFamilyName, newGender, newMonth, newDay, newYear;

       
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userStat"] = "admin";

        }

        protected void updateB_Click(object sender, EventArgs e)
        {
            OleDbConnection Con2 = new OleDbConnection();
            Con2.ConnectionString = @"provider=Microsoft.ACE.OLEDB.12.0; Data source="
                + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con2.Open();

            string sqlstring2 = "select * from MyUsersList WHERE MyUser ='" + usernameBox.Text + "'";
            OleDbCommand Cmd = new OleDbCommand(sqlstring2, Con2);
            try
            {
                OleDbDataReader Dr2 = Cmd.ExecuteReader();
                Dr2.Read();
                //הקריאה היא במטרה ליצור אפשרות ל Dr["קקי"]
                // ואם עושים בלי ריד אז השורה לא נמחקת ואפשר לייצג אותה בגריד


                if (Dr2.HasRows)
                {
                    msg = "עודכן בהצלחה";
                    msgcolor = "blue";
                    newName = Dr2["MyName"].ToString();
                    newUsername = Dr2["MyUser"].ToString();
                    newCity = Dr2["MyCity"].ToString();
                    newPhone = Dr2["MyPhone"].ToString();
                    newEmail = Dr2["MyEmail"].ToString();
                    newPassword = Dr2["MyPassword"].ToString();
                    newAddres = Dr2["MyAddres"].ToString();
                    newId = Dr2["MyID"].ToString();
                    newFamilyName = Dr2["MyFamilyName"].ToString();
                    newGender = Dr2["MyGender"].ToString();
                    newMonth = Dr2["MyMonth"].ToString();
                    newDay = Dr2["MyDay"].ToString();
                    newYear = Dr2["MyYear"].ToString();

                    if (nameBox.Text.Trim().Length != 0) newName = nameBox.Text;
                    if (usernameBox.Text.Trim().Length != 0) newUsername = usernameBox.Text;
                    if (cityBox.Text.Trim().Length != 0) newCity = cityBox.Text;
                    if (phoneBox.Text.Trim().Length != 0) newPhone = phoneBox.Text;
                    if (EmailBox.Text.Trim().Length != 0) newEmail = EmailBox.Text;
                    if (passwordBox.Text.Trim().Length != 0) newPassword = passwordBox.Text;
                    if (AddressBox.Text.Trim().Length != 0) newAddres = AddressBox.Text;
                    if (IdBox.Text.Trim().Length != 0) newId = IdBox.Text;
                    if (familyBox.Text.Trim().Length != 0) newFamilyName = familyBox.Text;
                    if (genderBox.Text.Trim().Length != 0) newGender = genderBox.Text;


                }
                else
                {
                    msg = "משתמש לא קיים";
                    msgcolor = "red";
                }
            }
            catch (Exception ex)
            {
                //a.Catchz();
            }

            Con2.Close();

            OleDbConnection Con1 = new OleDbConnection();
            Con1.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data source="
    + Server.MapPath("") + "\\eyalDataBase.accdb";
            Con1.Open();
            string sqlstring = "UPDATE MyUsersList SET MyName = '" + newName + "',MyID ='" + newId + "',MyPassword ='"
            + newPassword + "',MyFamilyName ='" + newFamilyName + "',MyCity ='" + newCity + "',MyAddres ='" + newAddres +
            "',MyEmail ='" + newEmail + "',MyGender ='" + newGender + "',MyPhone ='" + newPhone + "',MyMonth ='"
            + monthBox.SelectedValue + "',MyDay ='"
            + dayBox.SelectedValue + "',MyYear ='" + yearBox.SelectedValue + "' WHERE MyUser ='" +
            newUsername + "' ";
            OleDbCommand cmd = new OleDbCommand(sqlstring, Con1);
            int y = 0;
            y = cmd.ExecuteNonQuery();
            Con1.Close();
        }

      

    }

}