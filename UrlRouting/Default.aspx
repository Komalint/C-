<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UrlRouting._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="mb-4">
        <h2>Home</h2>
        <p class="text-muted">Welcome to the ASP.NET WebForms application. Choose an option below to explore each feature:</p>
    </div>

    <div class="row g-3">
        <!-- URL Routing -->
        <div class="col-md-6 col-lg-4">
            <div class="card h-100 shadow-sm">
                <div class="card-body">
                    <h5 class="card-title fw-bold">URL Routing</h5>
                    <p class="card-text text-muted small">Search articles by ID and route to friendly URLs (Articles/{id}/{title}).</p>
                    <a href='<%= ResolveUrl("~/Articles/Search") %>' class="btn btn-outline-primary btn-sm">Open Routing</a>
                </div>
            </div>
        </div>

        <!-- AJAX Username Checker -->
        <div class="col-md-6 col-lg-4">
            <div class="card h-100 shadow-sm">
                <div class="card-body">
                    <h5 class="card-title fw-bold">AJAX Username Checker</h5>
                    <p class="card-text text-muted small">Check username availability asynchronously using ASP.NET UpdatePanel.</p>
                    <a href='<%= ResolveUrl("~/Ajax/CheckUsername") %>' class="btn btn-outline-primary btn-sm">Open Checker</a>
                </div>
            </div>
        </div>

        <!-- Session State -->
        <div class="col-md-6 col-lg-4">
            <div class="card h-100 shadow-sm">
                <div class="card-body">
                    <h5 class="card-title fw-bold">Session State</h5>
                    <p class="card-text text-muted small">Login form and protected user dashboard using Session state.</p>
                    <a href='<%= ResolveUrl("~/Session/Login") %>' class="btn btn-outline-primary btn-sm">Open Session Login</a>
                </div>
            </div>
        </div>

        <!-- QueryString & Cookies -->
        <div class="col-md-6 col-lg-4">
            <div class="card h-100 shadow-sm">
                <div class="card-body">
                    <h5 class="card-title fw-bold">QueryString and Cookies</h5>
                    <p class="card-text text-muted small">Pass data to dashboard via URL QueryString or browser HttpCookie.</p>
                    <a href='<%= ResolveUrl("~/State/User") %>' class="btn btn-outline-primary btn-sm">Open State Transfer</a>
                </div>
            </div>
        </div>

        <!-- Product Entry -->
        <div class="col-md-6 col-lg-4">
            <div class="card h-100 shadow-sm">
                <div class="card-body">
                    <h5 class="card-title fw-bold">Product Entry and Validation</h5>
                    <p class="card-text text-muted small">Form validation with RequiredField, RegularExpression, and Range validators.</p>
                    <a href='<%= ResolveUrl("~/Products/Add") %>' class="btn btn-outline-primary btn-sm">Open Product Form</a>
                </div>
            </div>
        </div>

        <!-- ViewState and Counter -->
        <div class="col-md-6 col-lg-4">
            <div class="card h-100 shadow-sm">
                <div class="card-body">
                    <h5 class="card-title fw-bold">ViewState and Controls</h5>
                    <p class="card-text text-muted small">Compare Page ViewState with Session counter and DropDownList events.</p>
                    <a href='<%= ResolveUrl("~/State/Counter") %>' class="btn btn-outline-primary btn-sm">Open Counter Demo</a>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
