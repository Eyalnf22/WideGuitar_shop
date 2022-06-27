<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="searchDel.aspx.cs" MasterPageFile="~/adminMaster.Master" Inherits="EyalProject.searchDel" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <title>חיפוש ומחיקה</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">

    <asp:Button CssClass="send" ID="Button1" runat="server" Style="width: 130px;" Text="דף עדכון" OnClick="toUpdate_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="toCity" runat="server" Style="width: 230px;" Text="הוספת עיר" OnClick="toCity_Click" CausesValidation="false" />
<asp:Button CssClass="send" ID="toInst" runat="server" Style="width:230px;" Text="הוספת כלי" OnClick="toInst_Click" CausesValidation="false"/>
    <asp:Button CssClass="send" ID="toInsert" runat="server" Style="width: 230px;" Text="הוספת משתמש" OnClick="toInsert_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="Button3" runat="server" Style="width: 180px;" Text="סטטיסטיקות" OnClick="toStat_Click" CausesValidation="false" />


</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="par" style="width: 95%;">

        <div style="font-size: 50px; padding: 20px;">
            <center>
                מידע צד מנהל
            </center>
        </div>



        <table class="tbl" border="1" style="overflow: hidden;">
            <tr>
                <td>הזן שם משתמש לחיפוש:<asp:TextBox ID="usernameBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ValidationGroup="searchGr" ID="ReqName" runat="server" ErrorMessage="שדה חובה" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Button OnClick="searchB_Click" ValidationGroup="searchGr" ID="searchB" runat="server" Text="חפש" Style="background-image: radial-gradient(circle, Chartreuse, LawnGreen, Green);" CssClass="send" />
                    <asp:Button OnClick="searchAll_Click" ID="searchAll" runat="server" Text="רענן את כל המשתמשים" CausesValidation="false" Style="width: 300px; font-size: 20px; background-image: radial-gradient(circle, Chartreuse, LawnGreen, Green);" CssClass="send" />
                    <asp:Label ID="isFoundLbl" ForeColor="dodgerblue" runat="server"></asp:Label>
                </td>

                <td>:הזן שם משתמש למחיקה
                    <asp:TextBox ID="deleteBox" runat="server" CssClass="box"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator CssClass="req" ValidationGroup="delGr" ID="RequiredFieldValidator1" runat="server" ErrorMessage="שדה חובה" ControlToValidate="deleteBox"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Button ID="deleteB" ValidationGroup="delGr" runat="server" Text="מחק" Style="background-image: radial-gradient(circle, red, tomato, firebrick);" CssClass="send" OnClick="delete_Click" />
                    <asp:Button ID="deleteAllB" runat="server" Text="מחק את כל המשתמשים" Style="width: 300px; font-size: 20px; background-image: radial-gradient(circle, red, tomato, firebrick);" CssClass="send" OnClick="deleteAll_Click" />

                    <asp:Label ID="isDeletedLbl" ForeColor="red" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    סננים:
                    <asp:Button CommandName="descF"  OnClick="usernameFilter_Click" ID="descFilter" CssClass="box" Style="cursor:pointer;" runat="server" Text=" לפי שם יורד" />
                    <asp:Button  CommandName="ascF" OnClick="usernameFilter_Click" ID="ascFilter" CssClass="box" Style="cursor:pointer;" runat="server" Text=" לפי שם עולה" />
                   <br />
                   <br />
                   מין: <asp:DropDownList ID="genderbox"  runat="server">
                        <asp:ListItem ></asp:ListItem>
                        <asp:ListItem Value="male">זכר</asp:ListItem>
                        <asp:ListItem Value="female">נקבה</asp:ListItem>
                        <asp:ListItem Value="other">אחר</asp:ListItem>
                    </asp:DropDownList>
                  עיר:  <asp:DropDownList ID="cityBox"  runat="server">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button OnClick="usernameFilter_Click" ID="sanan" CssClass="box" Style="cursor:pointer;" runat="server" Text=" החל סנן" />
                    </td>
            </tr>
            <tr>
                <th colspan="2">




                    <div style="border: 30px; height: 350px; width: 1300px; background-color: aqua;">
                        <div style="height: 100%; width: 100%; overflow: scroll; background-color: seagreen;">

                            <asp:GridView OnDataBound="GridView1_DataBound" CellPadding="15" ID="GridView1" Style="direction: ltr" CellSpacing="20"
                                BackColor="#ff3333" AlternatingRowStyle-BackColor="#ffcc99" HeaderStyle-BackColor="Orange"
                                Font-Size="15px" runat="server">
                            </asp:GridView>
                        </div>
                    </div>

                </th>
            </tr>
        </table>




    </div>


</asp:Content>
