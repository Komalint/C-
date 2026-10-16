<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridViewType2.aspx.cs" Inherits="WebServicesPractice.GridViewType2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Details</title>

    <style>
        body {
            font-family: Arial;
            background-color: #f5f6fa;
            margin: 20px;
        }

        .container {
            width: 85%;
            margin: auto;
            background-color: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0px 2px 6px lightgray;
        }

        .heading {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 20px;
        }

        .grid {
            width: 100%;
        }

        .grid th {
            background-color: #0078D7;
            color: white;
            padding: 10px;
            text-align: left;
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
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <div class="container">

            <h2 class="heading">Employee Information</h2>

            <asp:SqlDataSource
                ID="SqlDataSource1"
                runat="server"
                ConnectionString="<%$ ConnectionStrings:DbConn %>"
                SelectCommand="SELECT * FROM tblEmployee">
            </asp:SqlDataSource>

            <asp:GridView
                ID="GridView1"
                runat="server"
                CssClass="grid"
                AutoGenerateColumns="False"
                DataKeyNames="EmployeeId"
                DataSourceID="SqlDataSource1"
                OnRowDataBound="GridView1_RowDataBound">

                <Columns>

                    <asp:BoundField
                        DataField="EmployeeId"
                        HeaderText="Employee ID"
                        ReadOnly="True" />

                    <asp:BoundField
                        DataField="FirstName"
                        HeaderText="First Name" />

                    <asp:BoundField
                        DataField="Salary"
                        HeaderText="Salary" />

                    <asp:BoundField
                        DataField="Country"
                        HeaderText="Country" />

                </Columns>

            </asp:GridView>

        </div>

    </form>
</body>
</html>
`