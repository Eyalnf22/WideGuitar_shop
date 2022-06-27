<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.Master" AutoEventWireup="true" CodeBehind="InsertCity.aspx.cs" Inherits="EyalProject.InsertCity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>צור עיר חדשה</title>
</asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
                       
<asp:Button CssClass="send" ID="toSearch" runat="server" Style="width:250px;" Text="דף חיפוש ומחיקה" OnClick="toSearchDel_Click" CausesValidation="false"/> 
<asp:Button CssClass="send" ID="toInst" runat="server" Style="width:230px;" Text="הוספת כלי" OnClick="toInst_Click" CausesValidation="false"/>
<asp:Button CssClass="send" ID="toInsert" runat="server" Style="width:230px;" Text="הוספת משתמש" OnClick="toInsert_Click" CausesValidation="false"/>
<asp:Button CssClass="send" ID="Button1" runat="server" Style="width: 130px;" Text="דף עדכון" OnClick="toUpdate_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="Button3" runat="server" Style="width: 180px;" Text="סטטיסטיקות" OnClick="toStat_Click" CausesValidation="false" />

        </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="par">
                  <div style="font-size:50px; padding:20px;">
                  <center> 
       צור עיר חדשה<br />
                      
                  </center>
                 </div>
              <center>
            <div style="background-color: white;">
         <asp:Label ID="promptCity" runat="server"></asp:Label>
            </div>
        </center>
            <table class="tbl" border="1" style="width:60%; font-size:25px;"   >
                <tr>
                    <td><center>
                         שם עיר:<asp:TextBox  ID="cityNameBox" runat="server" CssClass="box" ></asp:TextBox>
                       <br /> <asp:RequiredFieldValidator ValidationGroup="gr" CssClass="req" runat="server" ErrorMessage="שדה חובה" ControlToValidate="cityNameBox"></asp:RequiredFieldValidator>
                    </center> </td>
                </tr>
                <tr>
                    <td><center>
                         מיקום:<asp:TextBox ID="locationBox" runat="server" CssClass="box" ></asp:TextBox>
                       <br /> <asp:RequiredFieldValidator ValidationGroup="gr" CssClass="req" runat="server" ErrorMessage="שדה חובה" ControlToValidate="locationBox"></asp:RequiredFieldValidator>
                    </center> </td>
                </tr>


                 <tr>
<th  colspan="1">
        <asp:Button OnClick="InsertCity_Click"  Width="160" ValidationGroup="gr" ID="InsertCityBtn" runat="server" Text="הוסף עיר" style="background-image: radial-gradient(circle, Chartreuse, LawnGreen, Green);" CssClass="send"/>
        <input type="reset" class="send"  value="נקה"/> 
                  
</th>
                </tr>
</table>
  
                </div>
</asp:Content>
