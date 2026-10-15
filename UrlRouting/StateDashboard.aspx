<%@ Page Title="User Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StateDashboard.aspx.cs" Inherits="UrlRouting.StateDashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-lg-7 col-md-9">

            <asp:Panel ID="pnlContent" runat="server">
                <div class="app-card">
                    <div class="app-card-header d-flex justify-content-between align-items-center flex-wrap gap-2">
                        <div>
                            <span class="fw-bold fs-5">User Dashboard</span>
                            <asp:Label ID="lblSourceBadge" runat="server" CssClass="badge bg-secondary ms-2"></asp:Label>
                        </div>
                        <div class="d-flex gap-2">
                            <asp:Button ID="btnClearCookie" runat="server" Text="Clear Cookie" CssClass="btn btn-outline-danger btn-sm" OnClick="btnClearCookie_Click" />
                            <a href='<%= ResolveUrl("~/State/User") %>' class="btn btn-outline-secondary btn-sm">Back to Form</a>
                        </div>
                    </div>

                    <div class="my-3">
                        <h4>Welcome, <asp:Label ID="lblDisplayName" runat="server"></asp:Label></h4>
                        <p class="text-muted small">User details received below:</p>
                    </div>

                    <div class="table-responsive my-3">
                        <table class="table table-bordered align-middle mb-0">
                            <tbody>
                                <tr>
                                    <th style="width: 35%;" class="bg-light">User Name</th>
                                    <td>
                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="bg-light">Email Address</th>
                                    <td>
                                        <asp:TextBox ID="txtmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="bg-light">Data Source</th>
                                    <td>
                                        <asp:Label ID="lblTransportDetails" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <div class="alert alert-success py-2 px-3 small mb-0">
                        Data was successfully retrieved.
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlNoState" runat="server" Visible="false">
                <div class="app-card text-center py-4">
                    <h4 class="fw-bold text-dark">No State Data Found</h4>
                    <p class="text-muted mb-3">No user details were found in QueryString or Cookie.</p>
                    <a href='<%= ResolveUrl("~/State/User") %>' class="btn btn-primary">
                        Go to State Transfer Page
                    </a>
                </div>
            </asp:Panel>

        </div>
    </div>

</asp:Content>
