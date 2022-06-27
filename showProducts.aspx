<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.Master" AutoEventWireup="true" CodeBehind="showProducts.aspx.cs" Inherits="EyalProject.showProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%=headName %>
    </title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
      <asp:Button CssClass="send" Visible="true" ID="toLogin" runat="server" Style="width:250px;" Text="התחבר" OnClick="toLogin_Click" CausesValidation="false"/> 
                        <asp:Button CssClass="send" Visible="true" ID="tosignUp" runat="server" Style="width:250px;" Text="הרשם" OnClick="toSignUp_Click" CausesValidation="false"/> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="par" >

        <div class="tbl" style="margin-top: 5px; border: 2px solid black; height:90%; width: 90%;">

            <u>
                <h1>
                    <center>
                        <asp:Label BackColor="LawnGreen" ID="headLbl" runat="server"></asp:Label></center>
                </h1>
            </u>
            <table border="0" style="padding:10px;"  class="infoCell">
                <tr>
                    <td style=" text-align:center; padding:0; width:30%;" rowspan="2">
                        <asp:ImageButton ID="productImg" OnClientClick="return closeOpen('zoom')" Width="80%"  CssClass="productImg" runat="server" />
                    </td>
                    <td style=" width:30%;" >
                        <asp:DataList ID="MainDetalis" Font-Size="25px" runat="server">
                            <ItemTemplate>
                                <span style="font-size:20px;">
                                  <%#    Description %></span><br /><br />
                                <b>מק"ט:</b>  <%#  Eval("InstID") %><br />
                                <b>שם:</b> <%#  Eval("InstName") %><br />
                                <b>מחיר:</b> <%# Convert.ToDouble(Eval("Price"))%> ₪<br />
                                <b>אחוז הנחה:</b> <%#  Eval("Discount") %>%<br />
                                <b>לאחר הנחה:</b> <%# (100-Convert.ToDouble((Eval("Discount"))))*0.01*Convert.ToDouble((Eval("Price"))) %> ₪<br /> 
                              <b>חברה:</b> <%#  Eval("Company") %><br />
                                <b>שנת יצור:</b>  <%# Eval("InstYear") %><br />
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                    <td >
                        <asp:DataList ID="detailList" CssClass="secondryInfo" runat="server">
                            <ItemTemplate>

                                <h1>מפרט:</h1>
                                <ul>
                                    <li><%#  Eval("Field1") %> </li>
                                    <li><%#  Eval("Field2") %></li>
                                    <li><%#  Eval("Field3") %></li>
                                    <li><%#  Eval("Field4") %></li>
                                    <li><%#  Eval("Field5") %></li>
                                    <li><%#  Eval("Field6") %></li>
                                    <li><%#  Eval("Field7") %></li>
                                </ul>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td style=" width:28%;">
                        <asp:Button ID="addBtn" OnClientClick="return GeustPrompt()" CssClass="send" OnClick="AddProduct" runat="server" Text="הוסף לסל" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>

            <div id="zoom" style="display: none;">
                <div class="overlay" style="opacity: 0.5;" runat="server"></div>
                <!--שחור -->

                <div class="zoom">

                    <asp:ImageButton CssClass="exitInsImg" ID="imgExitPrmt" ImageUrl="pics/error.png" runat="server" OnClientClick="return closeOpen('zoom')" />
                    <asp:Image Width="260" Height="500"  ID="zoomImg" runat="server" />



                </div>
            </div>
        </div>
        </div>

</asp:Content>

