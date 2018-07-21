<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="signUp.aspx.cs" Inherits="signUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
       
   
      
        .style3
        {
            width: 130px;
        }
        .style7
        {
            width: 300px;
        }
        .style8
        {
            width: 50px;
        }
    </style>

 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <h1>Sign Up Form</h1>
    <table class="style1" style="width: 100%">
            <tr >
                <td class="style8">
                    User Name:</td>
                <td class="style7">
                    <asp:TextBox ID="TextBoxUN" runat="server" Width="180px" AutoPostBack="true" OnTextChanged="ValidateUser"></asp:TextBox>
                &nbsp;<span class="style9">*</span>
                    <asp:Label ID="ValidUser" runat="server" Text=""></asp:Label>
                </td>
                
            </tr>
           
             <tr>
                <td class="style8">
                    access level</td>
                <td class="style7">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="accessLevel">
                   
                    <asp:ListItem Text="Student">
                    
                    </asp:ListItem>
                    <asp:ListItem Text="Staff">
                    
                    </asp:ListItem>
                    
                    </asp:RadioButtonList>
                </td>
                
            </tr>
            <tr>
                <td class="style8">
                    E-mail:</td>
                <td class="style7">
                    <asp:TextBox ID="TextBoxEmail" runat="server" Width="180px"></asp:TextBox>
                &nbsp;<span class="style9"><asp:Label ID="mail" runat="server" Text=""></asp:Label>*</span></td>
                
            </tr>
            <tr>
                <td class="style8">
                    Password:</td>
                <td class="style7">
                    <asp:TextBox ID="TextBoxPass" runat="server" TextMode="Password" Width="180px"></asp:TextBox>
                    <span class="style9">&nbsp;*</span></td>
                
            </tr>
            <tr>
                <td class="style8">
                    Confirm Password:</td>
                <td class="style7">
                    <asp:TextBox ID="TextBoxConPass" runat="server" TextMode="Password" 
                        Width="180px"></asp:TextBox>
                &nbsp;<span class="style9">*</span></td>
                
            </tr>
            <tr>
                <td class="style8">
                    Gender</td>
                <td class="style7">
&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Width="180px">
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
               
            </tr>
            <tr>
                <td class="style8">
                    &nbsp;</td>
                <td class="style7">
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Submit" />
&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Reset1" type="reset" value="reset" onclick="return Reset1_onclick()" onclick="return Reset1_onclick()" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                
            </tr>
            
        </table>
        <br /><br /><br /><br />
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>

