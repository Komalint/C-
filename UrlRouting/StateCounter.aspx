<%@ Page Title="ViewState and Control Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StateCounter.aspx.cs" Inherits="UrlRouting.StateCounter" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-lg-8 col-md-10">
            <div class="app-card">
                <div class="app-card-header text-center">
                    <h3 class="fw-bold mb-1">ViewState and Control Events</h3>
                    <p class="text-muted small mb-0">State Management and DropDownList Postback Events</p>
                </div>

                <div class="row g-3">
                    <!-- ViewState Counter -->
                    <div class="col-md-6">
                        <div class="card h-100 p-3">
                            <h6 class="fw-bold mb-2">1. Page ViewState Counter</h6>
                            <p class="small text-muted mb-2">Maintains value across postbacks on this page only.</p>
                            
                            <div class="mb-3">
                                <label class="form-label small fw-semibold">Current Count:</label>
                                <asp:TextBox ID="txtViewStateCount" runat="server" CssClass="form-control text-center fw-bold" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="d-flex gap-2">
                                <asp:Button ID="btnIncrementViewState" runat="server" Text="Increment ViewState" CssClass="btn btn-primary btn-sm flex-grow-1" OnClick="btnIncrementViewState_Click" />
                                <asp:Button ID="btnResetViewState" runat="server" Text="Reset" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnResetViewState_Click" />
                            </div>
                        </div>
                    </div>

                    <!-- Session Counter -->
                    <div class="col-md-6">
                        <div class="card h-100 p-3">
                            <h6 class="fw-bold mb-2">2. Global Session Counter</h6>
                            <p class="small text-muted mb-2">Maintains value across all pages for the current session.</p>

                            <div class="mb-3">
                                <label class="form-label small fw-semibold">Current Count:</label>
                                <asp:TextBox ID="txtSessionCount" runat="server" CssClass="form-control text-center fw-bold" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="d-flex gap-2">
                                <asp:Button ID="btnIncrementSession" runat="server" Text="Increment Session" CssClass="btn btn-primary btn-sm flex-grow-1" OnClick="btnIncrementSession_Click" />
                                <asp:Button ID="btnResetSession" runat="server" Text="Reset" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnResetSession_Click" />
                            </div>
                        </div>
                    </div>

                    <!-- DropDownList -->
                    <div class="col-12">
                        <div class="card p-3 bg-light">
                            <h6 class="fw-bold mb-2">3. DropDownList Event (AutoPostBack)</h6>
                            <div class="row g-3 align-items-center">
                                <div class="col-md-6">
                                    <label class="form-label small fw-semibold">Select Location:</label>
                                    <asp:DropDownList ID="DrpListCategory" runat="server" CssClass="form-select"
                                        AutoPostBack="true" OnSelectedIndexChanged="DrpListCategory_SelectedIndexChanged">
                                        <asp:ListItem Value="">-- Select Location --</asp:ListItem>
                                        <asp:ListItem Value="Kolkata">Kolkata</asp:ListItem>
                                        <asp:ListItem Value="Ludhiana">Ludhiana</asp:ListItem>
                                        <asp:ListItem Value="Gujarat">Gujarat</asp:ListItem>
                                        <asp:ListItem Value="Bihar">Bihar</asp:ListItem>
                                        <asp:ListItem Value="Rajasthan">Rajasthan</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label small fw-semibold">Selection Result:</label>
                                    <div class="p-2 bg-white rounded border small">
                                        <asp:Label ID="lblSelectionResult" runat="server" CssClass="fw-bold text-primary" Text="No location selected"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="mt-3 pt-2 border-top d-flex gap-2 align-items-center flex-wrap">
                                <asp:Button ID="btnGreeting" runat="server" Text="Click For Message" CssClass="btn btn-outline-primary btn-sm" OnClick="btnGreeting_Click" />
                                <asp:Label ID="lblGreetingMsg" runat="server" CssClass="small text-muted"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
