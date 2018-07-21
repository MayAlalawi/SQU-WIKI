<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Forms.aspx.cs" Inherits="Forms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
Form Name : 
    <asp:TextBox ID="Fname" runat="server"></asp:TextBox><br /><br />
Upload Form <input id="filMyFile" type="file" runat="server" />
    <asp:Button ID="upload" runat="server" Text="Upload" 
    onclick="upload_Click" />
    <br />
    
 <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
 <br />
 <hr />
    <asp:Table ID="Table1" runat="server">
    </asp:Table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
</asp:Content>

