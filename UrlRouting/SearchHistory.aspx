<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchHistory.aspx.cs" Inherits="UrlRouting.SearchHistory" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="container mt-4">

        <h2>Search History</h2>
        <hr />

        <asp:GridView
            ID="gvSearchHistory"
            runat="server"
            AutoGenerateColumns="false"
            CssClass="table table-bordered table-striped">

            <Columns>

                <asp:BoundField
                    DataField="Id"
                    HeaderText="Sr No" />

                <asp:BoundField
                    DataField="SearchTerm"
                    HeaderText="Search Text" />

                <asp:BoundField
                    DataField="SearchTime"
                    HeaderText="Search Time" />

            </Columns>

        </asp:GridView>

    </div>



</asp:Content>