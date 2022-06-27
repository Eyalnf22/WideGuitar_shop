<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.Master" AutoEventWireup="true" CodeBehind="Pay.aspx.cs" Inherits="EyalProject.Pay" %>

<%@ MasterType VirtualPath="~/adminMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>תשלום סופי</title>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="par">

        <div style="font-size: 50px; padding: 20px;">
            <center>
                תשלום<br />

            </center>
        </div>
        <table class="tbl" border="1" style="width: 80%; font-size: 25px;">
            <tr>
                <th>
                    <br />
                    <asp:Label ID="noItem" runat="server"></asp:Label>
                    <div style="overflow-y: scroll; height:400px;">

                        <asp:GridView CellPadding="40" RowStyle-VerticalAlign="Middle"
                            RowStyle-HorizontalAlign="Center" AutoGenerateColumns="false" HeaderStyle-Font-Size="Large"
                            RowStyle-Font-Size="Large" ID="shopGrid" Caption="פירוט קנייה" BackColor="#CD5C5C"
                            AlternatingRowStyle-BackColor="LightGray" HeaderStyle-BackColor="LightBlue"
                            runat="server">
                            <Columns>

                                <asp:TemplateField HeaderText="תצוגה">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton CommandName='<%# Eval("ProID") %>' OnClick="ImgView_Click" ControlStyle-CssClass="viewImg" ControlStyle-Width="50" ImageUrl='<%# Eval("ImageColumn") %>' runat="server" />
                                                </td>
                                                <th>
                                                    <asp:ImageButton OnClientClick="return poop()" OnClick="trash_Click" CommandName='<%# Eval("ProID") %>' ID="trash" Width="30" CssClass="trash" ImageUrl="/pics/trash.png" runat="server" />
                                                </th>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="שם המוצר">
                                    <ItemTemplate>
                                        <asp:LinkButton CommandName='<%# Eval("ProID") %>' CssClass="cssLink" OnClick="proName_Click" ID="link" Text='<%# Eval("ProName") %>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:BoundField DataField="singleP" HeaderText="מחיר ליחידה" />

                                <asp:TemplateField HeaderText="כמות">
                                    <ItemTemplate>
                                        <asp:Button ID="plus" runat="server" Enabled='<%#Eval("IsMax")%>' OnClick="plusMinus_Click" CommandName='<%# Eval("ProID") %>' Text="+" />
                                        <asp:Label ID="countLbl" Text='<%# Eval("Count") %>' runat="server"></asp:Label>
                                        <asp:Button ID="minus" runat="server" OnClick="plusMinus_Click" Enabled='<%#Eval("IsMin")%>' CommandName='<%# Eval("ProID") %>' Text="-" />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:BoundField DataField="originalP" HeaderText="מחיר מקורי" />
                                <asp:BoundField DataField="discount" HeaderText="הנחה" />
                                <asp:BoundField DataField="comp" HeaderText="חברה" />
                                <asp:BoundField DataField="finalP" HeaderText="מחיר סופי" />


                            </Columns>
                        </asp:GridView>
                    </div>

                </th>
            </tr>
            <tr>
                <th>


                    <br />
                    <asp:Label ID="Receipt" runat="server" ForeColor="Green"></asp:Label>
                </th>
            </tr>
            <tr>
                <td style="background-color: turquoise; text-align: center">סכום לתשלום:

₪<asp:Label ID="sumLbl" ForeColor="#ff6600" runat="server"> </asp:Label>
                    <br />
                    כמות פריטים:
                        <asp:Label ID="itemCount" ForeColor="#ff6600" runat="server"> </asp:Label>
                    <br />
                    <br />
                    הכנס כרטיס אשראי<br />
                    <br />
                    <asp:TextBox ID="credit"  CssClass="box" runat="server"></asp:TextBox>
<asp:CompareValidator ID="cv" runat="server" ControlToValidate="credit" Type="Integer"
   Operator="DataTypeCheck" ValidationGroup="pay" ForeColor="Red" ErrorMessage="הכנס מספר תקין" />

                    <br />
                    <asp:Button ValidationGroup="pay" ID="PayB" runat="server" Text="שלם" Style="background-image: radial-gradient(circle, Chartreuse, LawnGreen, Green);" CssClass="send" OnClick="PayB_Click" />

                </td>
            </tr>

        </table>
    </div>
</asp:Content>
