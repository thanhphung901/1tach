<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Orderchart.ascx.cs" Inherits="vpro.eshop.cpanel.UIs.Orderchart" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Chart ID="Chartorder" runat="server">
    <Series>
        <asp:Series Name="Series1" ChartType="Line">
        </asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1">
        </asp:ChartArea>
    </ChartAreas>
</asp:Chart>
