<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <asp:Button ID="Button1" runat="server" Text="Edit" onclick="Button1_Click" CssClass="myButton"/><br /><br />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>

