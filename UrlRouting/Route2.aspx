<%@ Page Title="Article Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Route2.aspx.cs" Inherits="UrlRouting.Route2" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-lg-8 col-md-10">
            <div class="app-card">
                <div class="app-card-header d-flex justify-content-between align-items-center flex-wrap gap-2">
                    <div>
                        <span class="badge bg-secondary me-2">Routed Page</span>
                        <asp:Label ID="lblRouteInfo" runat="server" CssClass="small text-muted"></asp:Label>
                    </div>
                    <a href='<%= ResolveUrl("~/Articles/Search") %>' class="btn btn-outline-secondary btn-sm">
                        Back to Search
                    </a>
                </div>

                <div class="mb-3">
                    <h3 class="fw-bold text-dark mb-1">
                        <asp:Label ID="lblTitle" runat="server" Text="Article Title"></asp:Label>
                    </h3>
                    <div class="text-muted small">
                        <span>Author: </span>
                        <strong><asp:Label ID="lblAuthor" runat="server" Text="Author"></asp:Label></strong>
                    </div>
                </div>

                <div class="p-3 bg-light border rounded mb-4">
                    <p class="mb-0">
                        <asp:Label ID="lblDescription" runat="server" Text="Description will appear here..."></asp:Label>
                    </p>
                </div>

                <div class="d-flex justify-content-between align-items-center pt-2 border-top">
                    <small class="text-muted">
                        Loaded via route pattern: <code>Articles/{id}/{title}</code>
                    </small>
                    <a href='<%= ResolveUrl("~/") %>' class="btn btn-primary btn-sm">
                        Home
                    </a>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
