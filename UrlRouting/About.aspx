<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="UrlRouting.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center">
        <div class="col-lg-8 col-md-10">
            <div class="app-card">
                <div class="app-card-header">
                    <h3 class="fw-bold mb-1">About This Application</h3>
                    <p class="text-muted small mb-0">ASP.NET WebForms Project</p>
                </div>

                <p class="lead small text-secondary">
                    This project integrates multiple ASP.NET WebForms modules under a unified Master Page architecture:
                </p>

                <ul class="list-group list-group-flush small">
                    <li class="list-group-item"><strong>URL Routing:</strong> Parameterized routes for articles.</li>
                    <li class="list-group-item"><strong>AJAX:</strong> Partial updates with UpdatePanel and UpdateProgress.</li>
                    <li class="list-group-item"><strong>Session State:</strong> Login form and session-protected dashboard.</li>
                    <li class="list-group-item"><strong>QueryString and Cookies:</strong> State transfer and dashboard display.</li>
                    <li class="list-group-item"><strong>Validation:</strong> Input validators with SQL Server database entry.</li>
                    <li class="list-group-item"><strong>ViewState and Controls:</strong> Counter comparisons and DropDownList events.</li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
