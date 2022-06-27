<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/adminMaster.Master" CodeBehind="InsertPage.aspx.cs" Inherits="EyalProject.InsertPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>הוספת לקוח</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">

    <asp:Button CssClass="send" ID="toSearchDel" runat="server" Style="width: 250px;" Text="דף חיפוש ומחיקה" OnClick="toSearchDel_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="toUpdate" runat="server" Style="width: 130px;" Text="דף עדכון" OnClick="toUpdate_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="toCity" runat="server" Style="width: 230px;" Text="הוספת עיר" OnClick="toCity_Click" CausesValidation="false" />
<asp:Button CssClass="send" ID="toInst" runat="server" Style="width:230px;" Text="הוספת כלי" OnClick="toInst_Click" CausesValidation="false"/>
    <asp:Button CssClass="send" ID="Button3" runat="server" Style="width: 180px;" Text="סטטיסטיקות" OnClick="toStat_Click" CausesValidation="false" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="par">

        <div style="font-size: 50px; padding: 20px;">
            <center>
                צור משתמש חדש
            </center>
        </div>
        <center>
            <div style="background-color: white;">
                <asp:Label ForeColor="red" ID="IsUserExistPrmt" runat="server"></asp:Label>
            </div>
        </center>
        <table class="tbl" border="1">
            <tr>
                <td>שם פרטי:<asp:TextBox ID="nameBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ID="ReqName" runat="server" ErrorMessage="שדה חובה" ValidationGroup="gr" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                </td>

                <td>עיר:<asp:DropDownList ID="cityBox" runat="server" CssClass="box"></asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ID="Reqcity" runat="server" ErrorMessage="שדה חובה" ValidationGroup="gr" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>שם משפחה:<asp:TextBox ID="familyBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ID="Reqfamily" runat="server" ErrorMessage="שדה חובה" ValidationGroup="gr" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                </td>

                <td>כתובת<asp:TextBox ID="AddressBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ID="ReqAddress" runat="server" ErrorMessage="שדה חובה" ValidationGroup="gr" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                </td>
            </tr>


            <tr>
                <td>כתובת אימייל:<asp:TextBox ID="EmailBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ID="ReqEmail" runat="server" ErrorMessage="שדה חובה" ValidationGroup="gr" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                </td>

                <td>שם משתמש:<asp:TextBox ID="usernameBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ID="Requsername" runat="server" ErrorMessage="שדה חובה" ValidationGroup="gr" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>טלפון:<asp:TextBox ID="phoneBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ID="Reqphone" runat="server" ErrorMessage="שדה חובה" ValidationGroup="gr" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                </td>

                <td>סיסמה:<asp:TextBox ID="passwordBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ID="Reqpassword" runat="server" ErrorMessage="שדה חובה" ValidationGroup="gr" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>ת.ז:<asp:TextBox ID="IdBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ID="ReqId" runat="server" ErrorMessage="שדה חובה" ValidationGroup="gr" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                </td>

                <td>אימות סיסמה:<asp:TextBox ID="PassmoreBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ID="ReqPassmore" runat="server" ErrorMessage="שדה חובה" ValidationGroup="gr" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <b>בחר מין:</b><br />
                    <asp:RadioButtonList ID="genderBox" runat="server">
                        <asp:ListItem Text="זכר" Value="male"></asp:ListItem>
                        <asp:ListItem Text="נקבה" Value="female"></asp:ListItem>
                        <asp:ListItem Text="אחר" Value="other"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:RequiredFieldValidator CssClass="req" ValidationGroup="gr" ID="Reqgender" runat="server" ErrorMessage="שדה חובה" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>

                    <asp:FileUpload ID="FileUploaded" runat="server" />
                    <asp:Image ID="ProfilePic" Width="100" runat="server" />
                    <asp:Button ID="upload" runat="server" Text="העלה תמונה" OnClick="upload_Click" />
                </td>

                <td style="width: 400px;">
                    <b>תאריך לידה:</b>

                    <br />
                    <br />

                    חודש:
                    <asp:DropDownList ID="monthBox" ForeColor="black" BackColor="Azure" Font-Size="20px" Height="30px" runat="server" CssClass="dateBox">
                        <asp:ListItem Value="ינואר">ינואר</asp:ListItem>
                        <asp:ListItem Value="פברואר">פברואר</asp:ListItem>
                        <asp:ListItem Value="מרץ">מרץ</asp:ListItem>
                        <asp:ListItem Value="אפריל">אפריל</asp:ListItem>
                        <asp:ListItem Value="מאי">מאי</asp:ListItem>
                        <asp:ListItem Value="יוני">יוני</asp:ListItem>
                        <asp:ListItem Value="יולי">יולי</asp:ListItem>
                        <asp:ListItem Value="אוגוסט">אוגוסט</asp:ListItem>
                        <asp:ListItem Value="ספטמבר">ספטמבר</asp:ListItem>
                        <asp:ListItem Value="אוטקובר">אוטקובר</asp:ListItem>
                        <asp:ListItem Value="נובמבר">נובמבר</asp:ListItem>
                        <asp:ListItem Value="דצמבר">דצמבר</asp:ListItem>
                    </asp:DropDownList>

                    <br />
                    <br />
                    יום: 
                    <asp:DropDownList ID="dayBox" runat="server" ForeColor="black" BackColor="Azure" Font-Size="20px" Height="30px" CssClass="dateBox">
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                        <asp:ListItem Value="13">13</asp:ListItem>
                        <asp:ListItem Value="14">14</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="16">16</asp:ListItem>
                        <asp:ListItem Value="17">17</asp:ListItem>
                        <asp:ListItem Value="18">18</asp:ListItem>
                        <asp:ListItem Value="19">19</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="21">21</asp:ListItem>
                        <asp:ListItem Value="22">22</asp:ListItem>
                        <asp:ListItem Value="23">23</asp:ListItem>
                        <asp:ListItem Value="24">24</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="26">26</asp:ListItem>
                        <asp:ListItem Value="27">27</asp:ListItem>
                        <asp:ListItem Value="28">28</asp:ListItem>
                        <asp:ListItem Value="29">29</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="31">31</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    שנה:  
                    <asp:DropDownList ID="yearBox" runat="server" ForeColor="black" BackColor="Azure" Font-Size="20px" Height="30px" CssClass="dateBox">
                        <asp:ListItem Value="1930">1930</asp:ListItem>
                        <asp:ListItem Value="1931">1931</asp:ListItem>
                        <asp:ListItem Value="1932">1932</asp:ListItem>
                        <asp:ListItem Value="1933">1933</asp:ListItem>
                        <asp:ListItem Value="1934">1934</asp:ListItem>
                        <asp:ListItem Value="1935">1935</asp:ListItem>
                        <asp:ListItem Value="1936">1936</asp:ListItem>
                        <asp:ListItem Value="1937">1937</asp:ListItem>
                        <asp:ListItem Value="1938">1938</asp:ListItem>
                        <asp:ListItem Value="1939">1939</asp:ListItem>
                        <asp:ListItem Value="1940">1940</asp:ListItem>
                        <asp:ListItem Value="1941">1941</asp:ListItem>
                        <asp:ListItem Value="1942">1942</asp:ListItem>
                        <asp:ListItem Value="1943">1943</asp:ListItem>
                        <asp:ListItem Value="1944">1944</asp:ListItem>
                        <asp:ListItem Value="1945">1945</asp:ListItem>
                        <asp:ListItem Value="1946">1946</asp:ListItem>
                        <asp:ListItem Value="1947">1947</asp:ListItem>
                        <asp:ListItem Value="1948">1948</asp:ListItem>
                        <asp:ListItem Value="1949">1949</asp:ListItem>
                        <asp:ListItem Value="1950">1950</asp:ListItem>
                        <asp:ListItem Value="1951">1951</asp:ListItem>
                        <asp:ListItem Value="1952">1952</asp:ListItem>
                        <asp:ListItem Value="1953">1953</asp:ListItem>
                        <asp:ListItem Value="1954">1954</asp:ListItem>
                        <asp:ListItem Value="1955">1955</asp:ListItem>
                        <asp:ListItem Value="1956">1956</asp:ListItem>
                        <asp:ListItem Value="1957">1957</asp:ListItem>
                        <asp:ListItem Value="1958">1958</asp:ListItem>
                        <asp:ListItem Value="1959">1959</asp:ListItem>
                        <asp:ListItem Value="1960">1960</asp:ListItem>
                        <asp:ListItem Value="1961">1961</asp:ListItem>
                        <asp:ListItem Value="1962">1962</asp:ListItem>
                        <asp:ListItem Value="1963">1963</asp:ListItem>
                        <asp:ListItem Value="1964">1964</asp:ListItem>
                        <asp:ListItem Value="1965">1965</asp:ListItem>
                        <asp:ListItem Value="1966">1966</asp:ListItem>
                        <asp:ListItem Value="1967">1967</asp:ListItem>
                        <asp:ListItem Value="1968">1968</asp:ListItem>
                        <asp:ListItem Value="1969">1969</asp:ListItem>
                        <asp:ListItem Value="1970">1970</asp:ListItem>
                        <asp:ListItem Value="1971">1971</asp:ListItem>
                        <asp:ListItem Value="1972">1972</asp:ListItem>
                        <asp:ListItem Value="1973">1973</asp:ListItem>
                        <asp:ListItem Value="1974">1974</asp:ListItem>
                        <asp:ListItem Value="1975">1975</asp:ListItem>
                        <asp:ListItem Value="1976">1976</asp:ListItem>
                        <asp:ListItem Value="1977">1977</asp:ListItem>
                        <asp:ListItem Value="1978">1978</asp:ListItem>
                        <asp:ListItem Value="1979">1979</asp:ListItem>
                        <asp:ListItem Value="1980">1980</asp:ListItem>
                        <asp:ListItem Value="1981">1981</asp:ListItem>
                        <asp:ListItem Value="1982">1982</asp:ListItem>
                        <asp:ListItem Value="1983">1983</asp:ListItem>
                        <asp:ListItem Value="1984">1984</asp:ListItem>
                        <asp:ListItem Value="1985">1985</asp:ListItem>
                        <asp:ListItem Value="1986">1986</asp:ListItem>
                        <asp:ListItem Value="1987">1987</asp:ListItem>
                        <asp:ListItem Value="1988">1988</asp:ListItem>
                        <asp:ListItem Value="1989">1989</asp:ListItem>
                        <asp:ListItem Value="1990">1990</asp:ListItem>
                        <asp:ListItem Value="1991">1991</asp:ListItem>
                        <asp:ListItem Value="1992">1992</asp:ListItem>
                        <asp:ListItem Value="1993">1993</asp:ListItem>
                        <asp:ListItem Value="1994">1994</asp:ListItem>
                        <asp:ListItem Value="1995">1995</asp:ListItem>
                        <asp:ListItem Value="1996">1996</asp:ListItem>
                        <asp:ListItem Value="1997">1997</asp:ListItem>
                        <asp:ListItem Value="1998">1998</asp:ListItem>
                        <asp:ListItem Value="1999">1999</asp:ListItem>
                        <asp:ListItem Value="2000">2000</asp:ListItem>
                        <asp:ListItem Value="2001">2001</asp:ListItem>
                        <asp:ListItem Value="2002">2002</asp:ListItem>
                        <asp:ListItem Value="2003">2003</asp:ListItem>
                        <asp:ListItem Value="2004">2004</asp:ListItem>
                        <asp:ListItem Value="2005">2005</asp:ListItem>
                        <asp:ListItem Value="2006">2006</asp:ListItem>
                        <asp:ListItem Value="2007">2007</asp:ListItem>
                        <asp:ListItem Value="2008">2008</asp:ListItem>
                        <asp:ListItem Value="2009">2009</asp:ListItem>
                        <asp:ListItem Value="2010">2010</asp:ListItem>
                        <asp:ListItem Value="2011">2011</asp:ListItem>
                        <asp:ListItem Value="2012">2012</asp:ListItem>
                        <asp:ListItem Value="2013">2013</asp:ListItem>
                        <asp:ListItem Value="2014">2014</asp:ListItem>
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                        <asp:ListItem Value="2019">2019</asp:ListItem>
                        <asp:ListItem Value="2020">2020</asp:ListItem>
                    </asp:DropDownList>

                </td>



            </tr>



            <tr>
                <th colspan="3">



                    <asp:Button OnClick="loginB_Click" ValidationGroup="gr" ID="loginB" runat="server" Text="שלח" Style="background-image: radial-gradient(circle, Chartreuse, LawnGreen, Green);" CssClass="send" />
                    <input type="reset" class="send" value="נקה" />


                </th>
            </tr>
        </table>

    </div>
</asp:Content>
