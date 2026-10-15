<%@ Page Title="Session Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SessionDashboard.aspx.cs" Inherits="UrlRouting.SessionDashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-lg-7 col-md-9">
            
            <asp:Panel ID="pnlLoggedIn" runat="server">
                <div class="app-card">
                    <div class="app-card-header d-flex justify-content-between align-items-center flex-wrap gap-2">
                        <div>
                            <span class="fw-bold fs-5">Session Dashboard</span>
                            <span class="badge bg-success ms-2">Authenticated</span>
                        </div>
                        <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="btn btn-outline-danger btn-sm" OnClick="btnLogout_Click" />
                    </div>

                    <div class="alert alert-light border p-3 mb-4">
                        <h4 class="mb-1">
                            Welcome, <asp:Label ID="lblUserN" runat="server" CssClass="fw-bold text-primary"></asp:Label>
                        </h4>
                        <p class="mb-0 text-muted small">
                            Login Time: <asp:Label ID="lblLoginTime" runat="server"></asp:Label> | Session ID: <code><asp:Label ID="lblSessionId" runat="server"></asp:Label></code>
                        </p>
                    </div>

                    <div class="p-3 bg-light border rounded small">
                        <strong>Session Information:</strong> Data is saved on the web server and persists while your session is active.
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlNotLoggedIn" runat="server" Visible="false">
                <div class="app-card text-center py-4">
                    <h4 class="fw-bold text-dark">No Active Session</h4>
                    <p class="text-muted mb-3">You are not logged in or your session has expired.</p>
                    <a href='<%= ResolveUrl("~/Session/Login") %>' class="btn btn-primary">
                        Go to Session Login
                    </a>
                </div>
            </asp:Panel>

        </div>
    </div>

</asp:Content>
