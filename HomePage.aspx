<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="EyalProject.HomePage" %>
<%@ MasterType VirtualPath="~/adminMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>הכלים שלנו</title>
</asp:Content>

      <asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">
                             
                        <asp:Button CssClass="send" Visible="true" ID="toLogin" runat="server" Style="width:250px;" Text="התחבר" OnClick="toLogin_Click" CausesValidation="false"/> 
                        <asp:Button CssClass="send" Visible="true" ID="tosignUp" runat="server" Style="width:250px;" Text="הרשם" OnClick="toSignUp_Click" CausesValidation="false"/> 
                 
          </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
      <div class="par" style="width:80%;">
          <div class="tbl"  style="margin-top: 5px; background-color:mistyrose; overflow-y: scroll; border: 2px solid black; height: 90%; width: 90%;">
               
              <asp:Button ID="upPrice" Style="cursor:pointer;" CommandName="lowToHigh" CssClass="box" runat="server" OnClick="PriceOrder_Click" Text="מחיר מנמוך לגבוה" />
              <asp:Button ID="downPrice" Style="cursor:pointer;" CommandName="highToLow" CssClass="box" runat="server" OnClick="PriceOrder_Click" Text="מחיר גבוה לנמוך" />
            <center>
<asp:DataList ID="DataList"    RepeatDirection="Horizontal"  RepeatColumns="4" runat="server" OnItemCommand="DataList_ItemCommand" DataKeyField="InstID" >
    <ItemTemplate>
        <table border="1">
         <tr style="background-color:grey;">
            <td >
                <asp:LinkButton Font-Size="Large" ID="LinkButton1" Text='<%# Eval("InstName")%>'  CommandName="toProduct" ForeColor="#003366" runat="server"></asp:LinkButton><br />
                 ₪<asp:Label  style=" text-decoration: line-through;" runat="server" Text='<%#Convert.ToDouble(Eval("Price"))%>' ForeColor="Brown"></asp:Label><br />
                הנחה:  <asp:Label   runat="server" Text='<%# Eval("Discount")%>'></asp:Label>%<br />
                 ₪<asp:Label   runat="server" Text='<%#(100-Convert.ToDouble((Eval("Discount"))))*0.01*Convert.ToDouble((Eval("Price")))%>'></asp:Label><br />
                  חברה:  <asp:Label   runat="server" Text='<%# Eval("Company")%>'></asp:Label><br />
                

            <asp:Button ID="addTobasket" OnClientClick="return GeustPrompt()" runat="server" Text="הוספה לעגלה" CommandName="AddtoBasket" />
            </td>

            <td>
                <asp:ImageButton CommandName="toProduct"  CssClass="opacityCss" Width="120" Height="140" ImageUrl=<%# imgSrc+DataBinder.Eval(Container.DataItem,"MyImage")%> runat="server" />
           
  
        </td>

    
        
        </tr>
            <tr style="background-color:grey;">
                <td colspan="2">
                 <asp:Label   runat="server" Text='<%# Eval("Description")%>'></asp:Label><br />

                </td>
            </tr>
        
        
        
        </table>
           
        
    </ItemTemplate>        
</asp:DataList>
         </center>
              </div>

    </div>
</asp:Content>
