<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebDasboard.aspx.cs" Inherits="QueryString.WebDasboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Dashboard</title>

    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Arial, sans-serif;
            background: #f4f7fc;
        }

        .header {
            background: #2563eb;
            color: white;
            padding: 20px 40px;
            font-size: 28px;
            font-weight: bold;
            box-shadow: 0 2px 12px rgba(0,0,0,0.15);
        }

        .dashboard-container {
            width: 700px;
            margin: 40px auto;
        }

        .card {
            background-color: white;
            border-radius: 15px;
            padding: 30px;
            box-shadow: 0px 4px 15px rgba(0,0,0,0.12);
        }

        .profile-section {
            text-align: center;
            margin-bottom: 25px;
        }

        .profile-pic {
            width: 110px;
            height: 90px;
            border-radius: 50%;
            background-color: #2563eb;
            color: white;
            line-height: 90px;
            font-size: 36px;
            font-weight: bold;
            margin: auto;
        }

        .welcome {
            margin-top: 15px;
            font-size: 24px;
            color: #333;
            font-weight: 600;
        }

        .auto-style1 {
            width: 100%;
        }

        .auto-style1 td {
            padding: 12px;
        }

        .label-text {
            font-weight: 600;
            color: #374151;
            font-size: 15px;
        }

        .textbox {
            width: 95%;
            padding: 10px;
            border: 1px solid #d1d5db;
            border-radius: 8px;
            font-size: 14px;
            background-color: #f9fafb;
        }

        .status-card {
            margin-top: 25px;
            padding: 15px;
            background: #eff6ff;
            border-left: 5px solid #2563eb;
            border-radius: 8px;
        }

        .status-card h3 {
            margin: 0;
            color: #2563eb;
        }

        .status-card p {
            margin-top: 8px;
            color: #555;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div class="header">
            User Dashboard
        </div>

        <div class="dashboard-container">

            <div class="card">

                <div class="profile-section">
                    <div class="profile-pic">
                        &nbsp;<asp:Label ID="lblinitial" runat="server"></asp:Label>
                    </div>

                    <div class="welcome">
                        Welcome Back!
                    </div>
                </div>

                <table class="auto-style1" cellpadding="4" cellspacing="5">
                    <tr>
                        <td>
                            <asp:Label ID="lblname" runat="server"
                                CssClass="label-text"
                                Text="User Name :"></asp:Label>
                        </td>

                        <td>
                            <asp:TextBox ID="txtname" runat="server"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblmail" runat="server"
                                CssClass="label-text"
                                Text="Email Address :"></asp:Label>
                        </td>

                        <td>
                            <asp:TextBox ID="txtmail" runat="server"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                </table>

                <div class="status-card">
                    <h3>Account Status</h3>
                    <p>Your account is active and all services are available.</p>
                </div>

            </div>

        </div>

    </form>
</body>
</html>