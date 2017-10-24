<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default1.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True">
         
        </asp:GridView>
        
        <br />
        <br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Username</td>
                <td>
                    <asp:TextBox ID="UserTxt" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Password</td>
                <td>
                    <asp:TextBox ID="PassTxt" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="SubmitBtn" runat="server" Text="Submit" OnClick="SubmitBtn_Click" />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="SendmailBtn" runat="server" Text="Send Mail" OnClick="SendmailBtn_Click"/>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
