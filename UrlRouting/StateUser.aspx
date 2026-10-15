<%@ Page Title="State Transfer Demo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StateUser.aspx.cs" Inherits="UrlRouting.StateUser" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-lg-6 col-md-8">
            <div class="app-card">
                <div class="app-card-header text-center">
                    <h3 class="fw-bold mb-1">State Transfer Demo</h3>
                    <p class="text-muted small mb-0">QueryString and HttpCookies Demonstration</p>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblName" runat="server" CssClass="form-label fw-semibold" Text="Full Name:"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="Name is required" ForeColor="Red" Display="Dynamic" CssClass="small fw-semibold mt-1">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="mb-4">
                    <asp:Label ID="lblEmail" runat="server" CssClass="form-label fw-semibold" Text="Email Address:"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Email is required" ForeColor="Red" Display="Dynamic" CssClass="small fw-semibold mt-1">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="row g-2 mb-3">
                    <div class="col-sm-6">
                        <asp:Button ID="btnQueryString" runat="server"
                            Text="Send via QueryString"
                            CssClass="btn btn-outline-primary w-100"
                            OnClick="btnQueryString_Click" />
                    </div>
                    <div class="col-sm-6">
                        <asp:Button ID="btnCookie" runat="server"
                            Text="Store in Cookie"
                            CssClass="btn btn-primary w-100"
                            OnClick="btnCookie_Click" />
                    </div>
                </div>

                <asp:Label ID="lblStateMessage" runat="server" CssClass="d-block text-center fw-semibold mb-3"></asp:Label>

                <div class="row g-2 mt-2">
                    <div class="col-md-6">
                        <div class="p-3 bg-light border rounded small">
                            <strong>QueryString:</strong> Appends parameters directly to the URL (e.g. ?username=...&amp;useremail=...).
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="p-3 bg-light border rounded small">
                            <strong>HttpCookie:</strong> Stores values on the client browser with an expiration time.
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
