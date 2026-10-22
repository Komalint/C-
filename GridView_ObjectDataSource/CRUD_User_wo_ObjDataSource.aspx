<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRUD_User_wo_ObjDataSource.aspx.cs" Inherits="GridView_ObjectDataSource.CRUD_User_wo_ObjDataSource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>

    body {
        font-family: Arial, Helvetica, sans-serif;
        background-color: #f5f6fa;
        padding: 20px;
    }

    .container {
        width: 95%;
        margin: auto;
        background-color: white;
        padding: 25px;
        border-radius: 10px;
        box-shadow: 0px 3px 10px lightgray;
    }

    .title {
        text-align: center;
        color: #2c3e50;
        margin-bottom: 25px;
        font-size: 28px;
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
        font-size: 14px;
    }

    .grid td {
        padding: 10px;
        text-align: center;
        border: 1px solid #e0e0e0;
    }

    .grid tr:nth-child(even) {
        background-color: #f9f9f9;
    }

    .grid tr:hover {
        background-color: #e8f4ff;
    }

    .grid input[type=text] {
        width: 95%;
        padding: 6px;
        border: 1px solid #ccc;
        border-radius: 4px;
    }

    .grid select {
        width: 100%;
        padding: 6px;
        border: 1px solid #ccc;
        border-radius: 4px;
    }

    .grid input[type=submit] {
        background-color: #0078D7;
        color: white;
        border: none;
        padding: 7px 12px;
        border-radius: 4px;
        cursor: pointer;
    }

    .grid input[type=submit]:hover {
        background-color: #005fa3;
    }

    .footer-text {
        margin-top: 15px;
        text-align: center;
        color: gray;
        font-size: 13px;
    }

</style>
</head>
<body>
    <form id="form1" runat="server">

    <div class="container">

        <h2 class="title">
            Employee Information
        </h2>
        

       

       <asp:GridView
        ID="GridView1"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="grid"
        ShowFooter="True"
        DataKeyNames="EmployeeId"        
           OnRowCommand="GridView1_RowCommand"
           AllowPaging="true"
           OnPageIndexChanging="gvUsers_pageIndexChanging"
           PageSize="20">

           
        <Columns>

    <asp:TemplateField HeaderText="ID">

        <ItemTemplate>
            <%# Eval("EmployeeId") %>
        </ItemTemplate>

        <FooterTemplate>

            <asp:LinkButton
                ID="btnInsert"
                runat="server"
                Text="Insert"
                onClientClick="return confirm('Added Employee Successfully');"
                CommandName="Addnew" />

        </FooterTemplate>

    </asp:TemplateField>

    <asp:TemplateField HeaderText="First Name">

        <ItemTemplate>
            <%# Eval("FirstName") %>
        </ItemTemplate>

        <EditItemTemplate>
            <asp:TextBox ID="txtFirstName"
                runat="server"
                Text='<%# Bind("FirstName") %>' />
        </EditItemTemplate>

        <FooterTemplate>
            <asp:TextBox ID="txtNewFirstName"
                runat="server" />
        </FooterTemplate>

    </asp:TemplateField>

    <asp:TemplateField HeaderText="Last Name">

        <ItemTemplate>
            <%# Eval("LastName") %>
        </ItemTemplate>

        <EditItemTemplate>
            <asp:TextBox ID="txtLastName"
                runat="server"
                Text='<%# Bind("LastName") %>' />
        </EditItemTemplate>

        <FooterTemplate>
            <asp:TextBox ID="txtNewLastName"
                runat="server" />
        </FooterTemplate>

    </asp:TemplateField>

    <asp:TemplateField HeaderText="Date Of Birth">

        <ItemTemplate>
            <%# Eval("DateOfBirth","{0:dd-MMM-yyyy}") %>
        </ItemTemplate>

        <EditItemTemplate>
            <asp:TextBox ID="txtDOB"
                runat="server"
                Text='<%# Bind("DateOfBirth") %>' />
        </EditItemTemplate>

        <FooterTemplate>
            <asp:TextBox ID="txtNewDOB"
                runat="server" />
        </FooterTemplate>

    </asp:TemplateField>

    <asp:TemplateField HeaderText="Salary">

        <ItemTemplate>
            <%# Eval("Salary") %>
        </ItemTemplate>

        <EditItemTemplate>
            <asp:TextBox ID="txtSalary"
                runat="server"
                Text='<%# Bind("Salary") %>' />
        </EditItemTemplate>

        <FooterTemplate>
            <asp:TextBox ID="txtNewSalary"
                runat="server" />
        </FooterTemplate>

    </asp:TemplateField>

    <asp:TemplateField HeaderText="Gender">

        <ItemTemplate>
            <%# Eval("Gender") %>
        </ItemTemplate>

        <EditItemTemplate>

            <asp:DropDownList
                ID="ddlGender"
                runat="server"
                SelectedValue='<%# Bind("Gender") %>'>

                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>

            </asp:DropDownList>

        </EditItemTemplate>

        <FooterTemplate>

            <asp:DropDownList
                ID="ddlNewGender"
                runat="server">

                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>

            </asp:DropDownList>

        </FooterTemplate>

    </asp:TemplateField>

    <asp:TemplateField HeaderText="Department">

        <ItemTemplate>
            <%# Eval("DepartmentName") %>
        </ItemTemplate>

        <EditItemTemplate>
            <asp:TextBox ID="txtDepartment"
                runat="server"
                Text='<%# Bind("DepartmentName") %>' />
        </EditItemTemplate>

        <FooterTemplate>
            <asp:TextBox ID="txtNewDepartment"
                runat="server" />
        </FooterTemplate>

    </asp:TemplateField>

    <asp:TemplateField HeaderText="Country">

        <ItemTemplate>
            <%# Eval("Country") %>
        </ItemTemplate>

        <EditItemTemplate>
            <asp:TextBox ID="txtCountry"
                runat="server"
                Text='<%# Bind("Country") %>' />
        </EditItemTemplate>

        <FooterTemplate>
            <asp:TextBox ID="txtNewCountry"
                runat="server" />
        </FooterTemplate>

    </asp:TemplateField>

            <asp:TemplateField>
 <ItemTemplate>
 <asp:LinkButton ID="lbEdit" CommandArgument='<%# Eval("EmployeeId") %>' CommandName="EditRow"
 ForeColor="#8C4510" runat="server">Edit</asp:LinkButton>
 <asp:LinkButton ID="lbDelete" CommandArgument='<%# Eval("EmployeeId") %>' CommandName="DeleteRow"
 ForeColor="#8C4510" runat="server" CausesValidation="false" onClientClick="return confirm('Deleted Employee Successfully');">Delete</asp:LinkButton>
 </ItemTemplate>
 <EditItemTemplate>
 <asp:LinkButton ID="lbUpdate" CommandArgument='<%# Eval("EmployeeId") %>' CommandName="UpdateRow"
 ForeColor="#8C4510" runat="server" onClientClick="return confirm('Updated Employee Successfully');">Update</asp:LinkButton>
 <asp:LinkButton ID="lbCancel" CommandArgument='<%# Eval("EmployeeId") %>' CommandName="CancelUpdate"
 ForeColor="#8C4510" runat="server" CausesValidation="false">Cancel</asp:LinkButton>
 </EditItemTemplate>
 </asp:TemplateField>

</Columns>


        </asp:GridView>


        <div class="footer-text">
            ObjectDataSource CRUD Practice
        </div>

    </div>

</form>
</body>
</html>
