<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="EmployeeFullDrtails.aspx.cs"
    Inherits="GridView_ObjectDataSource.EmployeeFullDrtails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Details</title>

    <style>

        body {
            font-family: Segoe UI, Arial;
            background: #f4f6f9;
            padding: 30px;
        }

        .container {
            width: 700px;
            margin: auto;
            background: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0px 2px 10px lightgray;
        }

        .title {
            text-align: center;
            font-size: 28px;
            font-weight: bold;
            color: #2c3e50;
            margin-bottom: 20px;
        }

        .details {
            width: 100%;
            border-collapse: collapse;
        }

        .details td {
            padding: 12px;
            border: 1px solid #ddd;
        }

        .details td:first-child {
            width: 35%;
            font-weight: bold;
            background-color: #0078D7;
            color: white;
        }

        .back-btn {
            margin-top: 20px;
            text-align: center;
        }

        .back-btn a {
            text-decoration: none;
            background-color: #0078D7;
            color: white;
            padding: 10px 18px;
            border-radius: 5px;
        }

        .back-btn a:hover {
            background-color: #005fa3;
        }

    </style>

</head>
<body>

<form id="form1" runat="server">

    <div class="container">

        <div class="title">
            Employee Details
        </div>

        <asp:DetailsView
            ID="dvEmployee"
            runat="server"
            AutoGenerateRows="False"
            CssClass="details">

            <Fields>

                <asp:BoundField DataField="EmployeeId"
                    HeaderText="Employee ID" />

                <asp:BoundField DataField="FirstName"
                    HeaderText="First Name" />

                <asp:BoundField DataField="LastName"
                    HeaderText="Last Name" />

                <asp:BoundField DataField="DepartmentName"
                    HeaderText="Department" />

                <asp:BoundField DataField="DateOfBirth"
                    HeaderText="Date Of Birth"
                    DataFormatString="{0:dd-MMM-yyyy}" />

                <asp:BoundField DataField="Salary"
                    HeaderText="Salary" />

                <asp:BoundField DataField="Gender"
                    HeaderText="Gender" />

                <asp:BoundField DataField="Country"
                    HeaderText="Country" />

            </Fields>

        </asp:DetailsView>

        <div class="back-btn">
            <asp:HyperLink
                ID="hlBack"
                runat="server"
                NavigateUrl="~/EmployeeListFormerDetails.aspx">
                Back to Employee List
            </asp:HyperLink>
        </div>

    </div>

</form>

</body>
</html>