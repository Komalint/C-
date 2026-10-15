<%@ Page Title="Product Entry and Validation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductEntry.aspx.cs" Inherits="UrlRouting.ProductEntry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center">
        <div class="col-lg-8 col-md-10">
            <div class="app-card">
                <div class="app-card-header text-center">
                    <h3 class="fw-bold mb-1">Product Entry</h3>
                    <p class="text-muted small mb-0">Input Validation and Database Insertion</p>
                </div>

                <asp:ValidationSummary ID="valSummary" runat="server" CssClass="alert alert-danger py-2 small fw-semibold" HeaderText="Please correct the following errors:" />

                <div class="row g-3">
                    <div class="col-12">
                        <asp:Label ID="lblProductName" runat="server" CssClass="form-label fw-semibold" Text="Product Name:"></asp:Label>
                        <asp:TextBox ID="txtPname" runat="server" CssClass="form-control" placeholder="Enter product name"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="rfvProductName" runat="server"
                            ControlToValidate="txtPname" Display="Dynamic" ForeColor="Red" CssClass="small fw-semibold d-block mt-1"
                            ErrorMessage="Product name is required.">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="revProductName" runat="server"
                            ControlToValidate="txtPname" Display="Dynamic" ForeColor="Red" CssClass="small fw-semibold d-block mt-1"
                            ValidationExpression="^[A-Za-z ][A-Za-z0-9 ]*$"
                            ErrorMessage="Invalid Product Name: Must start with a letter and contain only alphanumeric characters or spaces.">
                        </asp:RegularExpressionValidator>
                    </div>

                    <div class="col-md-6">
                        <asp:Label ID="lblPrice" runat="server" CssClass="form-label fw-semibold" Text="Price:"></asp:Label>
                        <asp:TextBox ID="txtPprice" runat="server" CssClass="form-control" placeholder="e.g. 19.99"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="rfvPrice" runat="server"
                            ControlToValidate="txtPprice" Display="Dynamic" ForeColor="Red" CssClass="small fw-semibold d-block mt-1"
                            ErrorMessage="Price is required.">
                        </asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="revPrice" runat="server"
                            ControlToValidate="txtPprice" Display="Dynamic" ForeColor="Red" CssClass="small fw-semibold d-block mt-1"
                            ValidationExpression="^\d+(\.\d{1,2})?$"
                            ErrorMessage="Price must be a valid numeric amount.">
                        </asp:RegularExpressionValidator>
                    </div>

                    <div class="col-md-6">
                        <asp:Label ID="lblStocks" runat="server" CssClass="form-label fw-semibold" Text="Stock Available:"></asp:Label>
                        <asp:TextBox ID="txtPstocks" runat="server" CssClass="form-control" placeholder="e.g. 50"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="rfvStock" runat="server"
                            ControlToValidate="txtPstocks" Display="Dynamic" ForeColor="Red" CssClass="small fw-semibold d-block mt-1"
                            ErrorMessage="Stock is required.">
                        </asp:RequiredFieldValidator>

                        <asp:RangeValidator ID="rngStock" runat="server"
                            ControlToValidate="txtPstocks" Display="Dynamic" ForeColor="Red" CssClass="small fw-semibold d-block mt-1"
                            Type="Integer" MinimumValue="1" MaximumValue="10000"
                            ErrorMessage="Stock must be between 1 and 10,000.">
                        </asp:RangeValidator>
                    </div>

                    <div class="col-12 mt-3">
                        <asp:Button ID="btnAddProductDetails" runat="server"
                            Text="Add Product Details"
                            CssClass="btn btn-primary w-100"
                            OnClick="btnAddProductDetails_Click" />
                    </div>
                </div>

                <div class="mt-3 text-center">
                    <asp:Label ID="lbldisplay" runat="server" CssClass="d-block fw-bold small"></asp:Label>
                </div>

                <div class="mt-4 pt-3 border-top">
                    <h6 class="fw-bold mb-2">Recently Added Products</h6>
                    <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-sm table-bordered align-middle small"
                        EmptyDataText="No products added yet.">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Product Name" HeaderStyle-CssClass="table-light" />
                            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C2}" HeaderStyle-CssClass="table-light" />
                            <asp:BoundField DataField="Stock" HeaderText="Stock" HeaderStyle-CssClass="table-light" />
                            <asp:BoundField DataField="AddedAt" HeaderText="Time" HeaderStyle-CssClass="table-light" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
