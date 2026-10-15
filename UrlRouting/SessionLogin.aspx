<%@ Page Title="Session Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SessionLogin.aspx.cs" Inherits="UrlRouting.SessionLogin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-lg-5 col-md-7">
            <div class="app-card">
                <div class="app-card-header text-center">
                    <h3 class="fw-bold mb-1">Session Login</h3>
                    <p class="text-muted small mb-0">Session State Demonstration</p>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblUsername" runat="server" CssClass="form-label fw-semibold" Text="User Name:"></asp:Label>
                    <asp:TextBox ID="txtUsernameInput" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsernameInput"
                        ErrorMessage="Username is required" ForeColor="Red" Display="Dynamic" CssClass="small fw-semibold mt-1">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="mb-4">
                    <asp:Label ID="lblPassword" runat="server" CssClass="form-label fw-semibold" Text="Password:"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="Password is required" ForeColor="Red" Display="Dynamic" CssClass="small fw-semibold mt-1">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="d-grid mb-3">
                    <asp:Button ID="btnSubmit" runat="server" Text="Log In" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                </div>

                <asp:Label ID="lblLoginMessage" runat="server" CssClass="d-block text-center fw-semibold mb-3 small"></asp:Label>

                <div class="p-3 bg-light border rounded small mt-2">
                    Stores username in <code>Session["username"]</code> and redirects to Session Dashboard.
                </div>
            </div>
        </div>
    </div>

</asp:Content>
