<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="EyalProject.account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>החשבון שלי
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="par">

        <div style="font-size: 50px; padding: 20px;">
            <center>
                החשבון שלי<br />

            </center>
        </div>
        <div class="tbl" style="margin-top: 5px; border: 2px solid black; height: 90%; width: 90%;">

            <asp:DataList Style="font-size: 30px;" ID="userInfo" runat="server">
                <ItemTemplate>
                    <table border="1" style="background-color: palevioletred; width: 100%;">


                        <tr>
                            <td style="font-size: 30px;">


                                <b>שם משתמש: </b>
                                <asp:Label ID="usernameLbl" runat="server" Text="USERNAME"></asp:Label><br />
                                <asp:Label ID="takenUserLbl" ForeColor="Brown" Font-Size="25px" runat="server"></asp:Label>

                                <b>דואר אלקטרוני: </b>
                                <asp:Label ID="emailLbl" runat="server" Text='<%# Eval("MyEmail") %>'></asp:Label><br />
                                <asp:TextBox ID="emailBox" CssClass="box" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator  ValidationGroup="gr" ControlToValidate="emailBox" ID="RequiredFieldValidator1" runat="server" ForeColor="Brown" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                                <asp:Button ID="updateEmail" ValidationGroup="gr" OnClick="updateEmail_Click" Font-Size="25px" runat="server" Text="עדכן דואר אלקטרוני" />

                                <br />
                                <b>מספר טלפון: </b><%#  Eval("MyPhone") %><br />
                                <hr />

                                <b>
                                    <asp:Label ID="dealNumLbl" runat="server" Text="DEALNUM"></asp:Label></b> עסקאות בוצעו
                                <br />
                                <b>
                                    <asp:Label ID="ItemSumLbl" runat="server" Text="ITEMNUM"></asp:Label></b> מוצרים נקנו
             <br />
                                על סך <b>
                                    <asp:Label ID="dealSumLbl" runat="server" Text="DEALSUM"></asp:Label></b> ₪
                            </td>
                            <td style="float: left;">
                                <asp:Image ID="profileImg" Width="200" Height="200" ImageUrl='<%#  Eval("MyImage") %>' runat="server" /><br />
                                <b><%#  Eval("MyName") %>  <%#  Eval("MyFamilyName") %></b>

                                <center>
                                    <div style="background-color: white;">
                                        <asp:Label ForeColor="red" ID="IsUserExistPrmt" runat="server"></asp:Label>
                                    </div>
                                </center>
                                <asp:FileUpload ID="FileUploaded" runat="server" />
                                <asp:Image ID="ProfilePic" Width="100" runat="server" />
                                <asp:Button ID="upload" runat="server" Text="עדכן תמונה" OnClick="upload_Click" />
                            </td>
                        </tr>

                    </table>
                </ItemTemplate>
            </asp:DataList>

            <asp:DataList ID="lastDeal" runat="server">
                <ItemTemplate>

                    <table border="1" style="background-color: lightsalmon; width: 100%;">


                        <tr>
                            <td style="font-size: 25px;">
                                <u>
                                    <h1>עסקתך האחרונה</h1>
                                </u>
                                <b>מספר עסקה:</b>  <%#  Eval("DealID") %><br />
                                <b>מספר מוצרים:</b> <%#  Eval("ItemNum") %><br />
                                <b>סכום:</b> <%#  Eval("SumPrice") %> ₪<br />
                                <b>נקנה בתאריך:</b> <%#  Eval("MyDate") %><br />

                                <asp:Image ID="dealImg" runat="server" />
                                <asp:Button CssClass="send" Width="300px"
                                    Style="background-color: lightgreen;" ID="toDeal"
                                    OnClick="toDeal_Click" runat="server"
                                    Text="הצג עסקאות " />
                            </td>

                        </tr>

                    </table>
                    <hr />
                </ItemTemplate>



            </asp:DataList>





        </div>
    </div>
</asp:Content>
