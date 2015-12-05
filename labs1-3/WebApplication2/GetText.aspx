<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetText.aspx.cs" Inherits="WebApplication2.GetText" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
<asp:TextBox runat="server" ID="FirstLine"></asp:TextBox>
<asp:TextBox runat="server" ID="LastLine"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Go" OnClick="Button1_Click" />
        Output:
        <asp:Label ID="Output" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
