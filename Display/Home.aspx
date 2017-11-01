<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Display_Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
      
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="HomeDiv">  
            <br /><br />
            <table align="center" cellpadding="5" cellspacing="5" style="width: 1000px;">
                <tr>
                    <td class="auto-style2">
                        <table cellpadding="2" style="width:300px;" align="right">
                            <tr>
                                <td>
                                    <asp:Label ID="UserLB" runat="server" Text=""></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="LogoutLB" runat="server" Text=""></asp:Label>
                                </td>                                
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;"></td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:GridView ID="GridView1" runat="server" CellPadding="10" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" >
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>

        </div>
    
    </div>
    </form>
</body>
</html>
