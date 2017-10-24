<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Forms_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="LoginDiv">
             <style type="text/css">
                .login-auto-style1 {
                    width: 300px;
                }
                .login-auto-style2 {
                    width: 90px;
                }
                .login-auto-style3 {
                    text-align: center;
                }
                .login-auto-style4 {
                    width: 185px;
                }
            </style>
            <table cellpadding="5" class="login-auto-style1" style="">
                <tr>
                    <td class="login-auto-style2">Username:</td>
                    <td>
                        <input id="UsernameTxt" class="login-auto-style4" type="text" /></td>
                </tr>
                <tr>
                    <td class="login-auto-style2">Password:</td>
                    <td>
                        <input id="PasswordTxt" class="login-auto-style4" type="password" /></td>
                </tr>
                <tr>
                    <td class="login-auto-style3" colspan="2">
                        <input id="LoginBtn" type="button" value="Login" />
                        <input id="CancelBtn" type="button" value="Cancel" onclick="Close();" /></td>
                </tr>
            </table>

        </div>
    </div>
    </form>
</body>
</html>
