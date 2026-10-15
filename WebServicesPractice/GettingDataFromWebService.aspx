<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GettingDataFromWebService.aspx.cs" Inherits="WebServicesPractice.GettingDataFromWebService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Getting Data From Web Service</title>

    <style type="text/css">
        body {
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f4f6f9;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 800px;
            margin: 50px auto;
            background-color: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px lightgray;
        }

        .heading {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 20px;
        }

        .textbox {
            padding: 6px;
            width: 220px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .btn {
            background-color: #0078D7;
            color: white;
            border: none;
            padding: 8px 18px;
            cursor: pointer;
            border-radius: 4px;
        }

        .btn:hover {
            background-color: #005ea6;
        }

        .grid {
            margin-top: 20px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div class="container">

            <h2 class="heading">Get User Details From Web Service</h2>

            <table>
                <tr>
                    <td>
                        <b>Enter User Name :</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName"
                            runat="server"
                            CssClass="textbox">
                        </asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit"
                            runat="server"
                            Text="Search"
                            CssClass="btn"
                            OnClick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>

            <div class="grid">

                <asp:GridView ID="gvUserDetails"
                    runat="server"
                    AutoGenerateColumns="true"
                    Width="100%"
                    EmptyDataText="No Record Found"
                    CellPadding="4"
                    GridLines="None">

                    <RowStyle BackColor="#EFF3FB" />

                    <FooterStyle BackColor="#507CD1"
                        Font-Bold="True"
                        ForeColor="White" />

                    <PagerStyle BackColor="#2461BF"
                        ForeColor="White"
                        HorizontalAlign="Center" />

                    <HeaderStyle BackColor="#507CD1"
                        Font-Bold="True"
                        ForeColor="White" />

                    <AlternatingRowStyle BackColor="White" />

                </asp:GridView>

            </div>

        </div>

    </form>
</body>
</html>