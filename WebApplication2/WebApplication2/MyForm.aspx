<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyForm.aspx.cs" Inherits="WebApplication2.MyForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Click Hear" />
        </div>
        <p>
            <asp:Label ID="lblMsg" runat="server" Text="Hello World"></asp:Label>
        </p>
        <asp:DropDownList ID="DrpListCategory" runat="server" OnSelectedIndexChanged="DrpListCategory_SelectedIndexChanged">
            <asp:ListItem>Kolkata</asp:ListItem>
            <asp:ListItem>Ludhiyana</asp:ListItem>
            <asp:ListItem>Gujrat</asp:ListItem>
            <asp:ListItem>Bihar</asp:ListItem>
            <asp:ListItem>Rajasthan</asp:ListItem>
        </asp:DropDownList>
        <p>
            <asp:Button ID="BtnUpdate" runat="server" Text="Button" OnClick="BtnUpdate_Click" />
        </p>
    </form>
</body>
</html>
