<%@ Page Title="" Language="C#" MasterPageFile="~/adminMaster.Master" AutoEventWireup="true" CodeBehind="Stat.aspx.cs" Inherits="EyalProject.Stat" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>סטטיסטיקות</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
    <asp:Button CssClass="send" ID="toSearch" runat="server" Style="width: 250px;" Text="דף חיפוש ומחיקה" OnClick="toSearchDel_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="toInsert" runat="server" Style="width: 230px;" Text="הוספת משתמש" OnClick="toInsert_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="Button2" runat="server" Style="width: 150px;" Text="הוספת עיר" OnClick="toCity_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="Button1" runat="server" Style="width: 130px;" Text="דף עדכון" OnClick="toUpdate_Click" CausesValidation="false" />
    <asp:Button CssClass="send" ID="Button3" runat="server" Style="width: 150px;" Text="הוספת כלי" OnClick="toInst_Click" CausesValidation="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="par">
        <div style="font-size: 50px; padding: 20px;">
            <center>
                סטטיסטיקות ומידע<br />

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

                    <asp:Chart ID="myChart" runat="server">
                        <Titles>
                            <asp:Title Font="Times New Roman, 20pt, style=Bold" Name="Title1"
                                Text="התפלגות גילים">
                            </asp:Title>
                        </Titles>
                        <Series>
                            <asp:Series ChartType="Doughnut" Name="Series1">
                            </asp:Series>

                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Area3DStyle-Enable3D="true" Name="ChartArea1">
                                <AxisX Title="גילים"></AxisX>
                                <AxisY Title="כמות"></AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
                <td>
                    <asp:Chart ID="ChartSex" runat="server">
                        <Titles>
                            <asp:Title Font="Times New Roman, 20pt, style=Bold" Name="Title1"
                                Text="התפלגות מינים">
                            </asp:Title>
                        </Titles>
                        <Series>
                            <asp:Series ChartType="Doughnut" Font="Times New Roman, 20pt, style=Bold" Name="Series3">
                            </asp:Series>

                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea2">
                                <AxisX TitleFont="Times New Roman, 20pt, style=Bold"></AxisX>
                                <AxisY TitleFont="Times New Roman, 20pt, style=Bold"></AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
            </tr>

            <tr>
                <td colspan="2">

                    <asp:Chart ID="Chart2" Width="800px" runat="server">
                        <Titles>
                            <asp:Title Font="Times New Roman, 20pt, style=Bold" Name="Title1"
                                Text=" הנמכרים ביותר">
                            </asp:Title>
                        </Titles>
                        <Series>
                            <asp:Series Font="Times New Roman, 20pt, style=Bold" Name="Series2">
                            </asp:Series>

                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea2">
                                <AxisX TitleFont="Times New Roman, 20pt, style=Bold" Title="שם מוצר"></AxisX>
                                <AxisY TitleFont="Times New Roman, 20pt, style=Bold" Title="מכירות"></AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>

                </td>
            </tr>
            <tr>
                <td>
 <asp:Chart ID="chartCity" runat="server">
                        <Titles>
                            <asp:Title Font="Times New Roman, 20pt, style=Bold" Name="Title1"
                                Text="התפלגות אזורים">
                            </asp:Title>
                        </Titles>
                        <Series>
                            <asp:Series ChartType="Doughnut" Font="Times New Roman, 20pt, style=Bold" Name="Series4">
                            </asp:Series>

                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea2">
                                <AxisX TitleFont="Times New Roman, 20pt, style=Bold"></AxisX>
                                <AxisY TitleFont="Times New Roman, 20pt, style=Bold"></AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>
                </td>
                <td>
                    <asp:Label ID="dealNum" runat="server" Text="DEALNUM"></asp:Label>
                    <asp:Label ID="instNum" runat="server" Text="INSTNUM"></asp:Label>
                </td>
            </tr>


        </table>

    </div>

</asp:Content>
