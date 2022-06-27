<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.Master" AutoEventWireup="true" CodeBehind="InsertInst.aspx.cs" Inherits="EyalProject.InsertInst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>צור כלי נגינה חדש</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
    <asp:Button CssClass="send" ID="toSearch" runat="server" Style="width: 250px;" Text="דף חיפוש ומחיקה" OnClick="toSearchDel_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="toInsert" runat="server" Style="width: 230px;" Text="הוספת משתמש" OnClick="toInsert_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="Button2" runat="server" Style="width: 150px;" Text="הוספת עיר" OnClick="toCity_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="Button1" runat="server" Style="width: 130px;" Text="דף עדכון" OnClick="toUpdate_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="Button3" runat="server" Style="width: 180px;" Text="סטטיסטיקות" OnClick="toStat_Click" CausesValidation="false" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="par">
        <div style="font-size: 50px; padding: 20px;">
            <center>
                צור כלי נגינה חדש<br />

            </center>
        </div>
        <center>
            <div style="background-color: white;">
                <asp:Label ID="prompt" runat="server"></asp:Label>
            </div>
        </center>
        <table class="tbl" border="1" style="width: 60%; font-size: 25px;">
            <tr>
                <td>
                    <center>
                        שם:<asp:TextBox ID="instNameBox" runat="server" Text="" CssClass="box"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ValidationGroup="gr" CssClass="req" runat="server" ErrorMessage="שדה חובה" ControlToValidate="instNameBox"></asp:RequiredFieldValidator>
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    <center>
                        מחיר:<asp:TextBox ID="priceBox" runat="server" Text="21000" CssClass="box"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ValidationGroup="gr" CssClass="req" runat="server" ErrorMessage="שדה חובה" ControlToValidate="priceBox"></asp:RequiredFieldValidator>
                    </center>
                </td>
            </tr>

            <tr>
                <td>
                    <center>
                        אחוזי הנחה:<asp:TextBox ID="discountBox" runat="server" Text="2" CssClass="box"></asp:TextBox>%
                       <br />
                        <asp:RequiredFieldValidator ControlToValidate="discountBox" ValidationGroup="gr" CssClass="req" runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                    </center>
                </td>
            </tr>


            <tr>
                <td>
                    <center>
                        אספקה:<asp:TextBox ID="supplyBox" runat="server" Text="666" CssClass="box"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ControlToValidate="supplyBox" ValidationGroup="gr" CssClass="req" runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                    </center>
                </td>
            </tr>


            <tr>
                <td>
                    <center>
                        שם חברה:<asp:TextBox ID="companyBox" runat="server" Text="2000" CssClass="box"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ControlToValidate="companyBox" ValidationGroup="gr" CssClass="req" runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    <center>
                        שנת יצור:<asp:TextBox ID="instYear" runat="server" Text="2000" CssClass="box"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ControlToValidate="instYear" ValidationGroup="gr" CssClass="req" runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                    </center>
                </td>
            </tr>

            <tr>
                <td>
                    <center>
                        סוג:<asp:DropDownList AutoPostBack="true" OnTextChanged="typeBox_TextChanged" ID="typeBox" CssClass="box" runat="server"></asp:DropDownList>
                        <br />
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    <center>
                        תת סוג:<asp:DropDownList ID="innerTypeBox" CssClass="box" runat="server"></asp:DropDownList>
                        <br />
                    </center>
                </td>
            </tr>

            <tr>
                <td>
                    <center>
                        תמונה:<asp:FileUpload ID="FileUploaded" runat="server" />
                        <asp:Image ID="ProfilePic" Width="100" runat="server" />
                        <asp:Button ID="upload" runat="server" Text="העלה תמונה" OnClick="upload_Click" />
                    </center>
                </td>
            </tr>

            <tr>
                <td>
                    <center>
                        תיאור:<asp:TextBox ID="description" runat="server" CssClass="box"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ControlToValidate="description" ValidationGroup="gr" CssClass="req" runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                    </center>
                </td>
            </tr>




            <tr>
                <th colspan="1">
                    <asp:Button OnClick="InsertInst_Click" Width="160" ValidationGroup="gr"
                        ID="InsertInstBtn" runat="server" Text="הוסף כלי"
                        Style="background-image: radial-gradient(circle, Chartreuse, LawnGreen, Green);" CssClass="send" />
                    <input type="reset" class="send" value="נקה" />

                </th>
            </tr>

        </table>
        <table class="tbl" border="1" style="width: 60%; background-color: gray; font-size: 25px;">
            <tr>
                <td>
                    <hr />
                    <center>
                        מ"קט כלי:<asp:TextBox ID="InstIDBox" runat="server" CssClass="box"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ControlToValidate="InstIDBox" ValidationGroup="gr2" CssClass="req" runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                    </center>
                </td>
            </tr>


            <tr>
                <td>
                    <center>
                        כמות:<asp:TextBox ID="amountBox" runat="server" CssClass="box"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ControlToValidate="amountBox" ValidationGroup="gr2" CssClass="req" runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    <center>
                        <asp:Button OnClick="AddSupplyBtn" Width="160" ValidationGroup="gr2"
                            ID="Button4" runat="server" Text="הוסף אספקה"
                            Style="background-image: radial-gradient(circle, Chartreuse, LawnGreen, Green);" CssClass="send" />
                    </center>
                </td>
            </tr>
        </table>

        <div style="background-color:antiquewhite; ">
                <div class="tbl" style="margin-top: 5px; overflow-y: scroll; border: 2px solid black;  background-color:antiquewhite; height: 90%; width: 90%;">
          <center> <h1><span style="padding:20px; background-color:aquamarine;"> מוצרים מהספק שטרם במחסן</span> </h1></center> 
            <center>
        <asp:DataList ID="sapakCollection"  RepeatColumns="2" RepeatDirection="Horizontal" DataKeyField="InstName" runat="server"  OnItemCommand="sapakCollection_ItemCommand">

            <ItemTemplate>
                <table border="1">
                    <tr style="background-color: grey;">
                        <td style="width:220px;">
                            <asp:Label Font-Size="XX-Large" Font-Bold="true" Text='<%# Eval("InstName")%>' ForeColor="#003366" runat="server"></asp:Label><br />
                            אספקה:
                            <asp:Label runat="server" Text='<%# Eval("MySupply")%>'></asp:Label><br />
                            שנת יצור:<asp:Label runat="server" Text='<%#Eval("InstYear")%>'></asp:Label><br />
                            חברה: 
                            <asp:Label runat="server" Text='<%# Eval("Company")%>'></asp:Label><br />
                           

                        <td>
                            <asp:Image Width="120" Height="140" ImageUrl='<%# Eval("MyImage") %>' runat="server" />
                        </td>
                        <td style="width: 100px;" rowspan="2">
                               כמות:
                           <asp:RangeValidator ID="RangeValidator3" Type="Integer" runat="server" ValidationGroup='<%# Eval("InstName") %>' MinimumValue="0" MaximumValue='<%# Eval("MySupply") %>' ControlToValidate="supplyBoxSapak" ForeColor="Brown"  ErrorMessage="כמות לא תקינה"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ControlToValidate="supplyBoxSapak" ValidationGroup='<%# Eval("InstName") %>'  ID="RequiredFieldValidator3" ForeColor="Brown" 
                                runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                            <asp:TextBox  ID="supplyBoxSapak"  Style="border-radius:10px;" 
                                runat="server" CssClass="box2"></asp:TextBox>
                            <br />

                            מחיר:
                           <asp:RangeValidator ID="RangeValidator2" Type="Integer" runat="server" ValidationGroup='<%# Eval("InstName") %>' MinimumValue="0" MaximumValue="10000000" ControlToValidate="PriceBoxSapak" ForeColor="Brown"  ErrorMessage="מספר לא תקין"></asp:RangeValidator>
                            <asp:RequiredFieldValidator ControlToValidate="PriceBoxSapak" ValidationGroup='<%# Eval("InstName") %>'  ID="req" ForeColor="Brown" 
                                runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                            <asp:TextBox   ID="PriceBoxSapak"  Style="border-radius:10px;" 
                                runat="server" CssClass="box2"></asp:TextBox>
                            <br />


                            אחוז הנחה:
                            <asp:RangeValidator ID="RangeValidator1" Type="Integer" runat="server" ValidationGroup='<%# Eval("InstName") %>' MinimumValue="0" MaximumValue="99" ControlToValidate="discountBoxSapak" ForeColor="Brown"  ErrorMessage="אחוז לא תקין"></asp:RangeValidator>
                            <asp:RequiredFieldValidator  ControlToValidate="discountBoxSapak" ValidationGroup='<%# Eval("InstName") %>'  ID="RequiredFieldValidator1" ForeColor="Brown" 
                                runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                            <asp:TextBox  ID="discountBoxSapak" runat="server" Style="border-radius:10px;" CssClass="box2"></asp:TextBox><br />

                            תיאור:
                            <asp:RequiredFieldValidator  ControlToValidate="desBoxSapak" ValidationGroup='<%# Eval("InstName") %>'  ID="RequiredFieldValidator2" ForeColor="Brown" 
                                runat="server" ErrorMessage="שדה חובה"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="desBoxSapak" runat="server"  Style="border-radius:10px;" CssClass="box2"></asp:TextBox><br />

                            <br />
                          <asp:Button ID="send" ValidationGroup='<%# Eval("InstName") %>'   style="cursor:pointer;"  CssClass="box" runat="server" Text="שלח למחסן" />

                        </td>
                    </tr>
                    <tr style="background-color: grey;">
                        <td>
                            <center><b>סוג:</b><br /><asp:Label runat="server" Font-Size="X-Large" Text='<%# Eval("Type")%>'></asp:Label></center>
                        </td>
                        <td>
                            <center><b>תת-סוג :</b><br /><asp:Label runat="server" Font-Size="X-Large" Text='<%# Eval("InnerType")%>'></asp:Label></center>
                        </td>
                    </tr>
                </table>


            </ItemTemplate>

        </asp:DataList>
                </center>
            </div>
            </div>
    </div>
</asp:Content>
