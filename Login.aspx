<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" MasterPageFile="~/adminMaster.Master"  Inherits="EyalProject.Login" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>התחברות לאתר</title>
</asp:Content>
 

      <asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">
                     
                        <asp:Button CssClass="send" ID="toSignUp" runat="server" Style="width:250px;" OnClick="toSignUp_Click"  Text="הרשם"  CausesValidation="false"/> 
             
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            

        <div class="par">
              
                  <div style="font-size:50px; padding:20px;">
                  <center> 
       התחברות<br />
                      <span style="color:red; font-size:20px;"><%=isLogin %></span>
                      
                  </center>
                 </div>
              
            <table class="tbl" border="1" style="width:60%; font-size:25px;"   >
                <tr>
                    <td><center>
                         שם משתמש:<asp:TextBox  ID="usernameBox" runat="server" CssClass="box" ></asp:TextBox>
                       <br /> <asp:RequiredFieldValidator ValidationGroup="gr" CssClass="req" ID="Requsername" runat="server" ErrorMessage="שדה חובה" ControlToValidate="usernameBox"></asp:RequiredFieldValidator>
                    </center> </td>
                </tr>
               <tr>
                   <td><center>
                        סיסמה:<asp:TextBox  ID="passwordBox" runat="server" CssClass="box" ></asp:TextBox>
                       <br /> <asp:RequiredFieldValidator ValidationGroup="gr" CssClass="req" ID="Reqpassword" runat="server" ErrorMessage="שדה חובה" ControlToValidate="passwordBox"></asp:RequiredFieldValidator>
                     </center></td>
                </tr>
                 <tr>
<th  colspan="1">
        <asp:Button OnClick="loginB_Click" ValidationGroup="gr" ID="loginB" runat="server" Text="שלח" style="background-image: radial-gradient(circle, Chartreuse, LawnGreen, Green);" CssClass="send"/>
        <input type="reset" class="send"  value="נקה"/> 
                  
</th>
                </tr>
</table>
  
                </div>


</asp:Content>

