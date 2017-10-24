<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Forms_Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="RegisterDiv">
            <style type="text/css"> 
                .register-auto-style3 {
                    text-align: center;
                }
                .register-auto-style4 {
                    width: 185px;
                }
                .register-auto-style1 {
                    width: 400px;
                }
                .register-auto-style2 {
                    width: 140px;
                }
            </style>
            <table cellpadding="5" class="register-auto-style1">
                <tr>
                    <td class="register-auto-style2">Code</td>
                    <td>
                        <input id="CodeTxt" class="register-auto-style4" type="text" runat="server" /></td>
                </tr>
                <tr>
                    <td class="register-auto-style2">First Name:</td>
                    <td>
                        <input id="FirstNameTxt" class="register-auto-style4" type="text"  /></td>
                </tr>
                <tr>
                    <td class="register-auto-style2">Last Name: </td>
                    <td>
                        <input id="LastNameTxt" class="register-auto-style4" type="text"  /></td>
                </tr>
                <tr>
                    <td class="register-auto-style2">Email:</td>
                    <td>
                        <input id="EmailTxt" class="register-auto-style4" type="text"  /></td>
                </tr>
                <tr>
                    <td class="register-auto-style2">Username:</td>
                    <td>
                        <input id="UsernamedTxt" class="register-auto-style4" type="text"  /></td>
                </tr>
                <tr>
                    <td class="register-auto-style2">Password:</td>
                    <td>
                        <input id="PasswordTxt" class="register-auto-style4" type="password" /></td>
                </tr>
                <tr>
                    <td class="register-auto-style2">Re-Type Password:</td>
                    <td>
                        <input id="RePasswordTxt" class="register-auto-style4" type="password" /></td>
                </tr>
                <tr>
                    <td class="register-auto-style3" colspan="2">
                        <input id="RegisterBtn" type="button" value="Submit" />
                        <input id="CancelBtn" type="button" value="Cancel" onclick="Close();" /></td>
                </tr>               
            </table>
        </div>        
    </div>
    </form>
</body>
</html>
