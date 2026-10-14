<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckUnameByUpdatePanel.aspx.cs" Inherits="UrlRouting.CheckUnameByUpdatePanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Username Availability Checker</title>

    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Segoe UI;
            background: #f4f6f9;
        }

        .header {
            background: #0d6efd;
            color: white;
            text-align: center;
            padding: 20px;
            font-size: 28px;
            font-weight: bold;
        }

        .container {
            width: 500px;
            margin: 50px auto;
        }

        .card {
            background: white;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0px 4px 15px rgba(0,0,0,0.15);
        }

        .title {
            text-align: center;
            font-size: 22px;
            margin-bottom: 25px;
            color: #333;
        }

        table {
            width: 100%;
        }

        td {
            padding: 10px;
        }

        .txt {
            width: 95%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 6px;
        }

        .btn {
            background: #0d6efd;
            color: white;
            border: none;
            padding: 12px 20px;
            border-radius: 6px;
            width: 100%;
            cursor: pointer;
            font-size: 15px;
        }

        .btn:hover {
            background: #0b5ed7;
        }

        .status {
            margin-top: 5px;
            font-weight: bold;
        }

        .availability {
            display: flex;
            align-items: center;
            gap: 8px;
        }

        .loading {
            text-align: center;
            padding: 10px;
            color: #0d6efd;
            font-weight: bold;
        }
    </style>

</head>
<body>

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="header">
            Username Availability Checker
        </div>

        <div class="container">

            <div class="card">

                <div class="title">
                    Registration Form
                </div>

                <asp:UpdateProgress ID="UpdateProgress1"
                    runat="server"
                    AssociatedUpdatePanelID="UpdatePanel1">

                    <ProgressTemplate>

                        <div class="loading">

                            <img src="https://upload.wikimedia.org/wikipedia/commons/c/c7/Loading_2.gif"
                                width="30"
                                height="30"
                                alt="Loading" />

                            <br />

                            Checking Username...

                        </div>

                    </ProgressTemplate>

                </asp:UpdateProgress>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                    <ContentTemplate>

                        <table>

                            <tr>
                                <td>Username</td>

                                <td>:</td>

                                <td>
                                    <asp:TextBox ID="txtun"
                                        runat="server"
                                        CssClass="txt"
                                        AutoPostBack="true"
                                        OnTextChanged="CheckUserNameAvailability">
                                    </asp:TextBox>
                                </td>
                            </tr>

                            <tr>

                                <td></td>
                                <td></td>

                                <td>

                                    <div id="checkun"
                                        runat="server"
                                        visible="false"
                                        class="availability">

                                        <asp:Label ID="lblmsg"
                                            runat="server"
                                            CssClass="status">
                                        </asp:Label>

                                    </div>

                                </td>

                            </tr>

                            <tr>

                                <td>Password</td>

                                <td>:</td>

                                <td>
                                    <asp:TextBox ID="txtpwd"
                                        runat="server"
                                        CssClass="txt"
                                        TextMode="Password">
                                    </asp:TextBox>
                                </td>

                            </tr>

                            <tr>

                                <td>Confirm Password</td>

                                <td>:</td>

                                <td>
                                    <asp:TextBox ID="txtconpwd"
                                        runat="server"
                                        CssClass="txt"
                                        TextMode="Password">
                                    </asp:TextBox>
                                </td>

                            </tr>

                            <tr>

                                <td colspan="3">

                                    <asp:Button ID="txtbtn"
                                        runat="server"
                                        Text="SUBMIT"
                                        CssClass="btn" />

                                </td>

                            </tr>

                        </table>

                    </ContentTemplate>

                </asp:UpdatePanel>

            </div>

        </div>

    </form>

</body>
</html>