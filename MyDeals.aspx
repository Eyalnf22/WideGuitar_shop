<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.Master" AutoEventWireup="true" CodeBehind="MyDeals.aspx.cs" Inherits="EyalProject.MyDeals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>העסקאות שלי
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="par">

        <div style="font-size: 50px; padding: 20px;">
            <asp:Button CssClass="send" Width="270px"
                Style="position: absolute; float: right;" ID="toAccount"
                OnClick="toAcc_Click" runat="server"
                Text="חזור לחשבון שלי " />
            <center>
                העסקאות שלי<br />

            </center>
        </div>
        <div class="tbl" style="margin-top: 5px; overflow-y: scroll; border: 2px solid black; height: 90%; width: 90%;">

            <center>
                <asp:DataList ID="allDeals" OnItemDataBound="allDeals_ItemDataBound" DataKeyField="DealID" OnItemCommand="allDeals_ItemCommand" runat="server">

                    <ItemTemplate>

                        <table border="1" style="background-color: lightsalmon; width: 100%;">


                            <tr>

                                <td style="font-size: 25px;">
                                    <asp:Button CssClass="send" CommandName="showOrder" Style="float: left; left: 10px;" Width="200" ID="Button1" runat="server" Text="הצג פרטים" />

                                    <b>מספר עסקה:</b>  <%#  Eval("DealID") %><br />
                                    <b>מספר מוצרים:</b> <%#  Eval("ItemNum") %><br />
                                    <b>סכום:</b> <%#  Eval("SumPrice") %> ₪<br />
                                    <b>נקנה בתאריך:</b> <%#  Eval("MyDate") %><br />

                                    <asp:Image ID="dealImg" runat="server" />
                                </td>

                                <td>
                                    <div style="border: 30px; height: 250px; width: 100%; background-color: aqua;">
                                        <div style="height: 100%; width: 100%; overflow-y: scroll; background-color: seagreen;">
                                            <asp:DataList RepeatColumns="2" DataKeyField="InstID" OnItemCommand="allDeals_ItemCommand" ID="chidList" runat="server">
                                                <ItemTemplate>
                                                    <div style="background-color: aqua; height: 100%;">
                                                        <asp:ImageButton Width="30%" Height="100%" Style="float: right;" CssClass="productImg" CommandName="toProduct" ImageUrl='<%#imgSrc+ getInfoFromATbl((int)Eval("InstID"),"MyImage")%>' runat="server" />
                                                        <b>שם:</b>  <span style=""><%# getInfoFromATbl((int)Eval("InstID"),"InstName") %></span><br />
                                                        <b>כמות:</b> <%#  Eval("Amount") %><br />
                                                        <b>שולם:</b> <%#   Convert.ToDouble(Eval("Amount")) *Convert.ToDouble(Eval("SinglePrice"))  %>₪<br />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <hr />
                    </ItemTemplate>



                </asp:DataList>
            </center>

        </div>
    </div>
</asp:Content>
