<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="EyalProject.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>תפריט</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
    <asp:Button CssClass="send" Visible="true" ID="toLogin" runat="server" Style="width: 250px;" Text="התחבר" OnClick="toLogin_Click" CausesValidation="false" />
    <asp:Button CssClass="send" Visible="true" ID="tosignUp" runat="server" Style="width: 250px;" Text="הרשם" OnClick="toSignUp_Click" CausesValidation="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="par"  style="width: 80%; height:100%;">
        <div style="font-size: 50px; padding: 20px;">
            <center>
                <asp:Image ID="menuImg" ImageAlign="Right" Width="150" runat="server" />
                <asp:Label ID="menuLbl" runat="server"></asp:Label>

                <br />

            </center>
        </div>
        <asp:Button ID="AllInst" CssClass="send" Width="200" OnClick="AllInst_Click" runat="server" />
        <asp:DataList RepeatColumns="4" DataKeyField="KatID" OnItemCommand="MyMenu_ItemCommand" ID="MyMenu" runat="server">
            <ItemTemplate>
                <table border="1">
                    <tr style="background-color: grey;">
                        <td style="width: 10px;">
                            <center>
                                <b>
                                    <asp:LinkButton Font-Size="30px" ID="Title" Text='<%# Eval("MyTitle") %>' CssClass="cssLink" runat="server">LinkButton</asp:LinkButton></b></center>
                            <br />
                            <asp:ImageButton ID="ImageButton1" Width="200" ImageUrl='<%#  imgSrc + Eval("MyImage") %>' runat="server" />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>

        <br />
        <hr />
        <div style="font-size: 25px; background-color: white;">
            <b>הכי נמכרים:</b>
        </div>
        <asp:DataList ID="BestSellers" RepeatColumns="4" OnItemCommand="DataList_ItemCommand" DataKeyField="InstID" runat="server">
            <ItemTemplate>
                <table border="1">
                    <tr style="background-color: grey;">
                        <td>
                            <asp:LinkButton Font-Size="Large" ID="LinkButton1" Text='<%# Eval("InstName")%>' CommandName="toProduct" ForeColor="#003366" runat="server"></asp:LinkButton><br />
                            ₪<asp:Label Style="text-decoration: line-through;" runat="server" Text='<%# Convert.ToDouble(Eval("Price"))%>' ForeColor="Brown"></asp:Label><br />
                            הנחה: 
                            <asp:Label runat="server" Text='<%# Eval("Discount")%>'></asp:Label>%<br />
                            ₪<asp:Label runat="server" Text='<%#(100-Convert.ToDouble((Eval("Discount"))))*0.01*Convert.ToDouble((Eval("Price")))%>'></asp:Label><br />
                            חברה: 
                            <asp:Label runat="server" Text='<%# Eval("Company")%>'></asp:Label><br />
                            <asp:Button ID="addTobasket" OnClientClick="return GeustPrompt()" runat="server" Text="הוספה לעגלה" CommandName="AddtoBasket" />
                        </td>
                        <td>
                            <asp:ImageButton CommandName="toProduct" CssClass="opacityCss" Width="120" Height="140" ImageUrl='<%# imgSrc+DataBinder.Eval(Container.DataItem,"MyImage")%>' runat="server" />
                        </td>
                    </tr>
                    <tr style="background-color: grey;">
                        <td colspan="2">
                            <asp:Label runat="server" Text='<%# Eval("Description")%>'></asp:Label><br />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>



          <br />
        <hr />
        <div style="font-size: 25px; background-color: white;">
            <asp:Label ID="forUlbl" runat="server" ></asp:Label> 
        </div>
        <asp:DataList ID="forU" RepeatColumns="4" OnItemCommand="DataList_ItemCommand" DataKeyField="InstID" runat="server">
            <ItemTemplate>
                <table border="1">
                    <tr style="background-color: grey;">
                        <td>
                            <asp:LinkButton Font-Size="Large" ID="LinkButton1" Text='<%# Eval("InstName")%>' CommandName="toProduct" ForeColor="#003366" runat="server"></asp:LinkButton><br />
                            ₪<asp:Label Style="text-decoration: line-through;" runat="server" Text='<%# Convert.ToDouble(Eval("Price"))%>' ForeColor="Brown"></asp:Label><br />
                            הנחה: 
                            <asp:Label runat="server" Text='<%# Eval("Discount")%>'></asp:Label>%<br />
                            ₪<asp:Label runat="server" Text='<%#(100-Convert.ToDouble((Eval("Discount"))))*0.01*Convert.ToDouble((Eval("Price")))%>'></asp:Label><br />
                            חברה: 
                            <asp:Label runat="server" Text='<%# Eval("Company")%>'></asp:Label><br />
                            <asp:Button ID="addTobasket" OnClientClick="return GeustPrompt()" runat="server" Text="הוספה לעגלה" CommandName="AddtoBasket" />
                        </td>
                        <td>
                            <asp:ImageButton CommandName="toProduct" CssClass="opacityCss" Width="120" Height="140" ImageUrl='<%# imgSrc+DataBinder.Eval(Container.DataItem,"MyImage")%>' runat="server" />
                        </td>
                    </tr>
                    <tr style="background-color: grey;">
                        <td colspan="2">
                            <asp:Label runat="server" Text='<%# Eval("Description")%>'></asp:Label><br />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>


    </div>
</asp:Content>
