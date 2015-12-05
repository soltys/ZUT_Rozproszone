<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:BulletedList ID="BulletedList1" runat="server" DisplayMode="HyperLink">
                <asp:ListItem Value="GetText.aspx">Get Text</asp:ListItem>
                <asp:ListItem Value="DoSthWithFiles.aspx">Do STH with files</asp:ListItem>
                <asp:ListItem Value="matrix.html">Generate matrix with given size and name</asp:ListItem>
                <asp:ListItem Value="matrixId.html">Get matrix with given ID</asp:ListItem>
                

            </asp:BulletedList>

        </div>
    </form>
</body>
</html>

