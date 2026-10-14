<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Route1.aspx.cs" Inherits="UrlRouting.Route1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Article Search</title>

    <style>
        body {
            font-family: Segoe UI;
            background-color: #f4f6f9;
            margin: 0;
            padding: 0;
        }

        .header {
            background-color: #0d6efd;
            color: white;
            padding: 20px;
            text-align: center;
            font-size: 28px;
            font-weight: bold;
        }

        .card {
            width: 500px;
            margin: 40px auto;
            background-color: white;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0px 4px 12px rgba(0,0,0,0.15);
        }

        .title {
            text-align: center;
            font-size: 22px;
            color: #333;
            margin-bottom: 25px;
        }

        .lbl {
            font-weight: 600;
            display: block;
            margin-bottom: 8px;
        }

        .txtbox {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 6px;
            margin-bottom: 20px;
        }

        .btn {
            width: 100%;
            padding: 12px;
            background-color: #0d6efd;
            color: white;
            border: none;
            border-radius: 6px;
            font-size: 16px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #0b5ed7;
        }

        .info {
            margin-top: 20px;
            background-color: #eef5ff;
            padding: 12px;
            border-radius: 6px;
            color: #444;
        }
    </style>

</head>
<body>

    <form id="form1" runat="server">

        <div class="header">
            Article Portal
        </div>

        <div class="card">

            <div class="title">
                Search Article By ID
            </div>

            <asp:Label ID="lblArticleId"
                runat="server"
                CssClass="lbl"
                Text="Article ID">
            </asp:Label>

            <asp:TextBox ID="txtArticleId"
                runat="server"
                CssClass="txtbox"
                placeholder="Enter Article ID">
            </asp:TextBox>

            <asp:Button ID="btnView"
                runat="server"
                Text="View Article"
                CssClass="btn"
                OnClick="btnView_Click" />

            <div class="info">
                Available Article IDs:
                <br />
                1 - Dropdown Validation
                <br />
                2 - .NET Assemblies
                <br />
                3 - BinaryReader/BinaryWriter
                <br />
                4 - StreamWriter
            </div>

        </div>

    </form>

</body>
</html>
``