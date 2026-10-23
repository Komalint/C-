<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="EmployeeListFormerDetails.aspx.cs"
    Inherits="GridView_ObjectDataSource.EmployeeListFormerDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee List</title>

    <style>
        body {
            font-family: Segoe UI, Arial, sans-serif;
            background-color: #f4f6f9;
            margin: 0;
            padding: 30px;
        }

        .container {
            width: 85%;
            margin: auto;
            background: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0px 4px 12px rgba(0,0,0,0.1);
        }

        .title {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 25px;
            font-size: 30px;
            font-weight: bold;
        }

        .grid {
            width: 100%;
            border-collapse: collapse;
        }

        .grid th {
            background-color: #0078D7;
            color: white;
            padding: 12px;
            text-align: center;
        }

        .grid td {
            padding: 12px;
            text-align: center;
            border: 1px solid #ddd;
        }

        .grid tr:nth-child(even) {
            background-color: #f9f9f9;
        }

        .grid tr:hover {
            background-color: #eaf4ff;
        }

        .employee-link {
            color: #0078D7;
            font-weight: bold;
            text-decoration: none;
        }

        .employee-link:hover {
            color: #0056b3;
            text-decoration: underline;
        }
        .paging-section {
                margin-top: 25px;
                text-align: center;
            }

            .page-info {
                font-size: 15px;
                font-weight: 600;
                color: #2c3e50;
            }

            .pager-btn {
                display: inline-block;
                padding: 10px 20px;
                margin: 0 10px;
                background-color: #0078D7;
                color: white;
                text-decoration: none;
                border-radius: 5px;
                font-weight: 600;
                transition: 0.3s;
            }

            .pager-btn:hover {
                background-color: #005fa3;
            }

            .pager-btn[disabled] {
                background-color: gray;
                cursor: not-allowed;
            }

        .footer {
            text-align: center;
            margin-top: 20px;
            color: gray;
            font-size: 13px;
        }
    </style>

</head>
<body>
<form id="form1" runat="server">

    <div class="container">

        <div class="title">
            Employee Master Records
        </div>

        <asp:GridView ID="gvEmployees"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="grid" >

            <Columns>

                <asp:TemplateField HeaderText="Employee ID">
                    <ItemTemplate>
                        <asp:LinkButton
                            ID="lnkEmployeeId"
                            runat="server"
                            CssClass="employee-link"
                            onClick ="btnClick_getEmployeeDetails"
                            Text='<%# Eval("EmployeeId") %>'
                            CommandArgument='<%# Eval("EmployeeId") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField
                    HeaderText="Full Name"
                    DataField="FullName" />

                <asp:BoundField
                    HeaderText="Department"
                    DataField="DepartmentName" />

            </Columns>

        </asp:GridView>

        <div class="paging-section">

                <asp:Label
                    ID="lblPageInfo"
                    runat="server"
                    CssClass="page-info">
                </asp:Label>

                <br /><br />

                <asp:HyperLink
                    ID="lnkPrevious"
                    runat="server"
                    CssClass="pager-btn">
                    ← Previous
                </asp:HyperLink>

                <asp:HyperLink
                    ID="lnkNext"
                    runat="server"
                    CssClass="pager-btn">
                    Next →
                </asp:HyperLink>

            </div>

        <div class="footer">
            Click Employee ID to view complete employee details
        </div>

    </div>

</form>
</body>
</html>
