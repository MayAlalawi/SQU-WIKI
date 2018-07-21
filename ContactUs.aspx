<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
    <div>
    <h2>Contact us: </h2>
    </div>

     <div style="text-align :center ; background-color :Green" >
        <asp:Label ID="ResultLabel" runat="server" Text=""></asp:Label>

    </div>

     <div>
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="msubtxt" ForeColor="Maroon" ValidationGroup="c1">*</asp:RequiredFieldValidator>
                    &nbsp;Message Subject :
                </td>
                <td>
                    &nbsp;<asp:TextBox ID="msubtxt" runat="server" TabIndex="1"
                        ValidationGroup="c1" Width="222px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" valign="top">
                    &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                        ControlToValidate="bdytxt" ForeColor="Maroon" ValidationGroup="c1">*</asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Body:</td>
                <td>
                    &nbsp;<asp:TextBox ID="bdytxt" runat="server" Height="87px"
                         TabIndex="2" TextMode="MultiLine"
                        ValidationGroup="c1" Width="223px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" valign="top">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="rplynmetxt" ForeColor="Maroon" ValidationGroup="c1">*</asp:RequiredFieldValidator>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Reply name:</td>
                <td>
                    <asp:TextBox ID="rplynmetxt" runat="server" TabIndex="3" ValidationGroup="c1"
                        Width="222px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" valign="top">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="rplyemltxt" ForeColor="Maroon" ValidationGroup="c1">*</asp:RequiredFieldValidator>
&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="rplyemltxt" ForeColor="Maroon"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="c1">*</asp:RegularExpressionValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Reply Email:</td>
                <td>
                    <asp:TextBox ID="rplyemltxt" runat="server" TabIndex="4" ValidationGroup="c1"
                        Width="222px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" valign="top">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Send" ValidationGroup="c1"
                       onclick="Button1_Click"  />
&nbsp;<asp:Button ID="Button2" runat="server" Text="Cancel" ValidationGroup="c1" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
   
   

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" Runat="Server">
 

</asp:Content>

