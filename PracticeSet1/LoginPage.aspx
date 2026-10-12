<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="PracticeSet1.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Label ID="lblUsername" runat="server" Text="User Name"></asp:Label>
        <asp:TextBox ID="txtUsernameInput" runat="server" OnTextChanged="txtUsernameInput_TextChanged" Width="195px"></asp:TextBox>
&nbsp;<p>
            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" OnTextChanged="txtPassword_TextChanged" TextMode="Password" Width="198px"></asp:TextBox>
        </p>
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Login" Width="99px" />
    </form>
</body>
</html>
