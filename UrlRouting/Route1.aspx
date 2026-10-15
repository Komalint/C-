<%@ Page Title="Article Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Route1.aspx.cs" Inherits="UrlRouting.Route1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-lg-6 col-md-8">
            <div class="app-card">
                <div class="app-card-header text-center">
                    <h3 class="fw-bold mb-1">Search Article By ID</h3>
                    <p class="text-muted small mb-0">URL Routing Demonstration</p>
                </div>

                <div class="mb-3">
                    <asp:Label ID="lblArticleId" runat="server" CssClass="form-label fw-semibold" Text="Article ID:"></asp:Label>
                    <asp:TextBox ID="txtArticleId" runat="server" CssClass="form-control" placeholder="Enter ID (e.g. 1, 2, 3, 4)"></asp:TextBox>
                </div>

                <div class="d-grid mb-3">
                    <asp:Button ID="btnView" runat="server" Text="View Article" CssClass="btn btn-primary" OnClick="btnView_Click" />
                </div>

                <asp:Label ID="lblMessage" runat="server" CssClass="d-block mb-3 fw-semibold text-center small"></asp:Label>

                <div class="p-3 bg-light border rounded small mt-3">
                    <strong>Sample Article IDs:</strong>
                    <ul class="mb-0 mt-1 ps-3">
                        <li>ID 1: Dropdown Validation (Articles/1/Dropdown-Validation)</li>
                        <li>ID 2: .NET Assemblies (Articles/2/.NET-Assemblies)</li>
                        <li>ID 3: BinaryReader/BinaryWriter (Articles/3/BinaryReader-BinaryWriter)</li>
                        <li>ID 4: StreamWriter (Articles/4/StreamWriter)</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

</asp:Content>