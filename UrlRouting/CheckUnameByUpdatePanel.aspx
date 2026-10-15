<%@ Page Title="Username Availability Checker" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckUnameByUpdatePanel.aspx.cs" Inherits="UrlRouting.CheckUnameByUpdatePanel" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-lg-6 col-md-8">
            <div class="app-card">
                <div class="app-card-header text-center">
                    <h3 class="fw-bold mb-1">Username Availability Checker</h3>
                    <p class="text-muted small mb-0">Asynchronous check using ASP.NET UpdatePanel</p>
                </div>

                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div class="alert alert-info py-2 mb-3 small d-flex align-items-center" role="alert">
                            <span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                            <span>Checking username availability...</span>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="mb-3">
                            <asp:Label ID="lblUsernameLabel" runat="server" CssClass="form-label fw-semibold" Text="Username:"></asp:Label>
                            <asp:TextBox ID="txtun" runat="server" CssClass="form-control"
                                AutoPostBack="true" OnTextChanged="CheckUserNameAvailability"
                                placeholder="Type username and press Tab">
                            </asp:TextBox>
                            
                            <div id="checkun" runat="server" visible="false" class="mt-2">
                                <asp:Label ID="lblmsg" runat="server" CssClass="fw-semibold d-block small"></asp:Label>
                            </div>
                        </div>

                       

                        <div class="d-grid mb-3">
                            <asp:Button ID="txtbtn" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="txtbtn_Click" />
                        </div>

                        <asp:Label ID="lblFormFeedback" runat="server" CssClass="d-block text-center fw-semibold small"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="p-3 bg-light border rounded small mt-3">
                    <strong>Note:</strong> When you leave the Username box, an asynchronous postback is fired without reloading the page.
                </div>
            </div>
        </div>
    </div>

</asp:Content>