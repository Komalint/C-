<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeanStateManagement.aspx.cs" Inherits="PracticeSet2.LeanStateManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtCount" runat="server" Height="82px" Width="252px"></asp:TextBox>
        </div>
        <asp:Button ID="btnOnSession" runat="server" Height="29px" OnClick="btnOnSession_Click" Text="Click Hear" Width="103px" />
    </form>
</body>
</html>
