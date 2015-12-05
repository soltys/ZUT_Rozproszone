<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoSthWithFiles.aspx.cs" Inherits="WebApplication2.DoSthWithFiles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="One" runat="server" />
        <asp:FileUpload ID="Two" runat="server" />
        <asp:button runat="server" text="Go" OnClick="Go_Click" />
        <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>
    </div>
    </form>
</body>
</html>

