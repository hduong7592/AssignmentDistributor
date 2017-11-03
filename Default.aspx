<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="Scripts/control.js?<%=currentDate%>"></script>
    <div id="myhome">
        <table align="center" cellpadding="2" style="height: 800px; width: 800px;">
            <tr>
                <td style="text-align: center; vertical-align: middle; height: 800px;">
                    <input id="LoginBtn" style="width: 100px; height: 50px" type="button" value="Login" runat="server" />
                    <input id="RegisterBtn" style="width: 100px; height: 50px" type="button" value="Register" runat="server" />                
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="ScriptLB" runat="server" Text=""></asp:Label>
</asp:Content>

