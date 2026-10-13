<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductForm.aspx.cs" Inherits="PracticeSet2.ProductForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            border-collapse: collapse;
        }
        .auto-style2 {
            width: 222px;
        }
    </style>
</head>
<body style="height: 239px">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="5" cellspacing="3" class="auto-style1">
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblProductName" runat="server" ForeColor="#660033" Text="Enter Product Name :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPname" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="txtPname" ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="Product name is required"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPname" ValidationExpression="^[A-Za-z ][A-Za-z0-9 ]*$" ForeColor="Red" ID="revProductName" runat="server" ErrorMessage="Invalid Product Name"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblPrice" runat="server" ForeColor="#660033" Text="Enter Product Price :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPprice" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="txtPprice" ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="Price is required"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtPprice" ValidationExpression="^\d+(\.\d{1,2})?$" ForeColor="Red" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Price must be numeric"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblStocks" runat="server" ForeColor="#660033" Text="Enter Stock Available :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPstocks" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="txtPstocks" ID="RequiredFieldValidator3" runat="server" ForeColor="Red" ErrorMessage="Stock is required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator Display="Dynamic" ControlToValidate="txtPstocks" Type="Integer" MinimumValue="1" MaximumValue="10000" ForeColor="Red" ID="RangeValidator1" runat="server" ErrorMessage="Stock must be between 1 and 10000"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Button ID="btnAddProductDetails" runat="server" OnClick="btnAddProductDetails_Click" Text="Add Details" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <asp:Label ID="lbldisplay" runat="server" Height="60px" Width="481px"></asp:Label>
        </div>
    </form>
</body>
</html>
