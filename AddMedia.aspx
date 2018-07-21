<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddMedia.aspx.cs" Inherits="AddMedia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<input id="filMyFile" type="file" runat="server"  />
    <asp:Button ID="upload" runat="server" Text="Upload" 
    onclick="upload_Click" />
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
Comment
    <asp:TextBox ID="desc" runat="server" TextMode="MultiLine"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Text="" ForeColor="Green"></asp:Label>
    <hr />


    <asp:Table ID="Table1" runat="server" Caption="Uploaded Files"  Width=100%>
    </asp:Table>


    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server"> 



</asp:Content>


