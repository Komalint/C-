<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryUser.aspx.cs" Inherits="QueryString.QueryUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Login</title>

    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Arial, sans-serif;
            background: linear-gradient(135deg,#2563eb,#4f46e5);
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .login-card {
            width: 420px;
            background: white;
            border-radius: 18px;
            padding: 35px;
            box-shadow: 0 10px 25px rgba(0,0,0,0.2);
        }

        .title {
            text-align: center;
            font-size: 28px;
            font-weight: bold;
            color: #2563eb;
            margin-bottom: 8px;
        }

        .subtitle {
            text-align: center;
            color: #666;
            margin-bottom: 25px;
        }

        .auto-style1 {
            width: 100%;
        }

        .auto-style1 td {
            padding: 10px;
        }

        .label {
            font-weight: 600;
            color: #374151;
        }

        .textbox {
            width: 95%;
            padding: 10px;
            border: 1px solid #d1d5db;
            border-radius: 8px;
            font-size: 14px;
        }

        .textbox:focus {
            outline: none;
            border-color: #2563eb;
            box-shadow: 0 0 6px rgba(37,99,235,0.3);
        }

        .btn {
            width: 100%;
            padding: 12px;
            background: #2563eb;
            color: white;
            border: none;
            border-radius: 8px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
        }

        .btn:hover {
            background: #1d4ed8;
        }

        .icon {
            text-align: center;
            font-size: 55px;
            margin-bottom: 10px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div class="login-card">

            <div class="icon">👤</div>

            <div class="title">
                Welcome
            </div>

            <div class="subtitle">
                Enter your details to continue
            </div>

            <table class="auto-style1">

                <tr>
                    <td>
                        <asp:Label ID="lblUsername"
                            runat="server"
                            CssClass="label"
                            Text="User Name">
                        </asp:Label>
                    </td>

                    <td>
                        <asp:TextBox ID="txtName"
                            runat="server"
                            CssClass="textbox">
                        </asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblEmailId"
                            runat="server"
                            CssClass="label"
                            Text="Email">
                        </asp:Label>
                    </td>

                    <td>
                        <asp:TextBox ID="txtEmail"
                            runat="server"
                            CssClass="textbox"
                            TextMode="Email">
                        </asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="padding-top:20px;">
                        <asp:Button ID="btnClickRedrctDash"
                            runat="server"
                            Text="Go To Dashboard"
                            CssClass="btn"
                            OnClick="btnClickRedrctDash_Click" />
                    </td>
                </tr>
                <tr>
                     <td colspan="2" style="padding-top:20px;">
                        <asp:Button ID="btnstoreInCookie"
                            runat="server"
                            Text="Store data in cookie"
                            CssClass="btn"
                            OnClick="btnClickSendData_Click" />
                     </td>
                </tr>

            </table>

        </div>

    </form>
</body>
</html>