<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="OneTach.vi_vn.Register" %>

<%@ Register src="../UIs/Register.ascx" tagname="Register" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Register ID="Register1" runat="server" />
</asp:Content>
