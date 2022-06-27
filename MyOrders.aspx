<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.Master" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs" Inherits="EyalProject.MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>  <%=headOrderNum %>
</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="par">
        
        <div  style="font-size:50px;  padding:20px;">
            <asp:Button CssClass="send"  Width="270px"  
                                    Style="background-color:lightgreen; position:absolute; float:right;" ID="toDeal" 
                                    OnClick="toDeal_Click" runat="server" 
                                    Text="חזור לעסקאות שלי "  />
<center> 
    <asp:Label ID="orderNum" runat="server" Text="NUMBER"></asp:Label><br />
                      
 </center>
</div>
        <div class="tbl" style="margin-top: 5px; overflow-y:scroll; border: 2px solid black; height: 90%; width: 90%;">
            <center>
            <asp:DataList Style="font-size: 30px;"  DataKeyField="InstID"  OnItemCommand="allOrders_ItemCommand" ID="allOrders" runat="server">
                <ItemTemplate>
                    <table border="1" style="background-color: palevioletred; width: 100%;">


                        <tr>
                            <td style="font-size: 30px;">

                              <b>מק"ט מוצר: </b> <%# Eval("InstID") %><br />
                              <b>תשלום ליחידה: </b> <%# Eval("SinglePrice")  %> ₪<br />
                              <b>כמות: </b> <%# Eval("Amount") %><br />
                              <b>תשלום עבור כולם: </b> <%# Convert.ToDouble(Eval("Amount")) *Convert.ToDouble(Eval("SinglePrice"))%> ₪
                            </td>
                            <td style=" text-align:center; padding:0;"   rowspan="2">
                                <asp:ImageButton Width="20%" Height="150px" Style="position:relative;"  CssClass="productImg"  CommandName="toProduct" ImageUrl='<%#imgSrc+ getInfoFromATbl((int)Eval("InstID"),"MyImage")%>' runat="server" />
                            </td>
                        </tr>

                    </table>
                </ItemTemplate>
            </asp:DataList>
                </center>

        </div>
    </div>
</asp:Content>
