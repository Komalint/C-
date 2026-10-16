<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridViewType3.aspx.cs" Inherits="WebServicesPractice.GridViewType3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Details</title>

    <style>
        body {
            font-family: Arial;
            background-color: #f5f6fa;
            padding: 20px;
        }

        .container {
            width: 85%;
            margin: auto;
            background: white;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0px 2px 8px lightgray;
        }

        .title {
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

            <h2 class="title">Employee Information</h2>

           <asp:SqlDataSource
                ID="SqlDataSource1"
                runat="server"
                ConnectionString="<%$ ConnectionStrings:DbConn %>"

                SelectCommand="
                SELECT Employeeld,
                       FirstName,
                       Gender,
                       Salary,
                       Country
                FROM tblEmployee"

                UpdateCommand="
                UPDATE tblEmployee
                SET FirstName=@FirstName,
                    Gender=@Gender,
                    Salary=@Salary,
                    Country=@Country
                WHERE Employeeld=@Employeeld"
               
               InsertCommand="
                INSERT INTO tblEmployee
                (
                    FirstName,
                    Gender,
                    Salary,
                    Country
                )
                VALUES
                (
                    @FirstName,
                    @Gender,
                    @Salary,
                    @Country
                )">
            </asp:SqlDataSource>

            <asp:GridView ID="GridView1"
                runat="server"
                AutoGenerateColumns="False"
                AutoGenerateEditButton ="True"
                CssClass="grid"
                ShowFooter="True"
                DataSourceID="SqlDataSource1"
                OnRowDataBound ="GridView1_RowDataBound"
                DataKeyNames="Employeeld" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">

                <Columns>

                    <asp:TemplateField HeaderText="Action">

                        <ItemTemplate>
                            <%# Eval("Employeeld") %>
                        </ItemTemplate>

                        <FooterTemplate>

                            <asp:Button
                                ID="btnInsert"
                                runat="server"
                                Text="Insert"
                                OnClick="btnInsert_Click" />

                        </FooterTemplate>

                    </asp:TemplateField>

                   <asp:TemplateField HeaderText="First Name">

                        <ItemTemplate>
                            <%# Eval("FirstName") %>
                        </ItemTemplate>

                        <EditItemTemplate>

                            <asp:TextBox ID="txtFirstName"
                                runat="server"
                                Text='<%# Bind("FirstName") %>'>
                            </asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="rfvFirstName"
                                runat="server"
                                ControlToValidate="txtFirstName"
                                ErrorMessage="First Name is required"
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>

                        </EditItemTemplate>

                        <FooterTemplate>

                            <asp:TextBox ID="txtNewFirstName"
                                runat="server">
                            </asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="rfvNewFirstName"
                                runat="server"
                                ControlToValidate="txtNewFirstName"
                                ErrorMessage="First Name is required"
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>

                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gender">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("Gender") %>'>
                                <asp:ListItem Value="">Select gender</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                id ="rfvGender"
                                runat="server"
                                ControlToValidate="DropDownList1"
                                InitialValue=""
                                ErrorMessage="Gender is required"
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Gender") %>'></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>

                            <asp:DropDownList ID="ddlNewGender"
                                runat="server">

                                <asp:ListItem Value="">Select Gender</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>

                            </asp:DropDownList>

                            <asp:RequiredFieldValidator
                                ID="rfvNewGender"
                                runat="server"
                                ControlToValidate="ddlNewGender"
                                InitialValue=""
                                ErrorMessage="Gender is required"
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>

                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Salary">

                        <ItemTemplate>
                            <%# Eval("Salary") %>
                        </ItemTemplate>

                        <EditItemTemplate>

                            <asp:TextBox ID="txtSalary"
                                runat="server"
                                Text='<%# Bind("Salary") %>'>
                            </asp:TextBox>

                        </EditItemTemplate>

                        <FooterTemplate>

                            <asp:TextBox ID="txtNewSalary"
                                runat="server">
                            </asp:TextBox>

                            <asp:RequiredFieldValidator
                                ID="rfvSalary"
                                runat="server"
                                ControlToValidate="txtNewSalary"
                                ErrorMessage="Salary is required"
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>

                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Country">

                    <ItemTemplate>
                        <%# Eval("Country") %>
                    </ItemTemplate>

                        <EditItemTemplate>

                            <asp:TextBox ID="txtCountry"
                                runat="server"
                                Text='<%# Bind("Country") %>'>
                            </asp:TextBox>

                        </EditItemTemplate>

                    <FooterTemplate>

                        <asp:TextBox ID="txtNewCountry"
                            runat="server">
                        </asp:TextBox>

                        <asp:RequiredFieldValidator
                            ID="rfvCountry"
                            runat="server"
                            ControlToValidate="txtNewCountry"
                            ErrorMessage="Country is required"
                            ForeColor="Red">
                        </asp:RequiredFieldValidator>

                    </FooterTemplate>

                </asp:TemplateField>

                </Columns>


            </asp:GridView>
            <asp:ValidationSummary
                ID="ValidationSummary1"
                runat="server"
                ForeColor="Red" />

        </div>

    </form>
</body>
</html>