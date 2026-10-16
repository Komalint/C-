<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LargeDataSetToPaging.aspx.cs" Inherits="WebServicesPractice.LargeDataSetToPaging" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Large Dataset Paging</title>

    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f4f6f9;
            margin: 20px;
        }

        .container {
            width: 95%;
            margin: auto;
            background-color: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0px 2px 8px rgba(0,0,0,0.1);
        }

        h2 {
            color: #2c3e50;
            margin-bottom: 15px;
        }

        .btnLoad {
            background-color: #0078D7;
            color: white;
            border: none;
            padding: 10px 18px;
            border-radius: 4px;
            cursor: pointer;
            font-size: 14px;
        }

        .btnLoad:hover {
            background-color: #005fa3;
        }

        .lblInfo {
            margin-left: 15px;
            font-weight: bold;
            color: #555;
        }

        .grid {
            margin-top: 20px;
            width: 100%;
        }

        .grid th {
            background-color: #0078D7;
            color: white;
            padding: 8px;
        }

        .grid td {
            padding: 8px;
        }

        .grid tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .grid tr:hover {
            background-color: #e8f4ff;
        }

        .gridPager {
            text-align: center;
            padding: 10px;
            background-color: #fafafa;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="container">

            <h2>Large Dataset Paging Example</h2>

            <asp:Button ID="btnLoadData"
                runat="server"
                Text="Load Data"
                CssClass="btnLoad"
                OnClick="btnLoadData_Click" />

            <asp:Label ID="lblRecordCount"
                runat="server"
                CssClass="lblInfo"
                Text="Total Records: 0">
            </asp:Label>

            <br />
            <br />

            <asp:GridView ID="gridViewLong"
                runat="server"
                CssClass="grid"
                AutoGenerateColumns="true"
                AllowPaging="true"
                PageSize="100"
                OnPageIndexChanging ="gvPageIndexChanging"
                GridLines="Horizontal">

                <PagerStyle CssClass="gridPager" />

            </asp:GridView>

        </div>

    </form>
</body>
</html>
`