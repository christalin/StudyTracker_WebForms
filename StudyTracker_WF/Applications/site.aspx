<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="site.aspx.cs" Inherits="StudyTracker_WF.Applications.site" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <h3>SITES</h3>
        </div>
    </div>
    
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <a href="#" data-toggle="modal" onclick="AddData()" class="btn btn-primary">Add New Site</a>
        </div>
    </div>

    <div class="table-responsive">
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1"
            CssClass="table table-bordered table-striped"
            PageSize="10"
            AllowPaging="True"
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="SiteId" HeaderText="SiteId" SortExpression="SiteId" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />

            </Columns>
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetSites" TypeName="StudyTracker_WF.SiteClasses.SiteManager"></asp:ObjectDataSource>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndOfPageContent" runat="server">
</asp:Content>
