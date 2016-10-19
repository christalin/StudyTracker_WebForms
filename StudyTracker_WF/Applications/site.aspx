<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="site.aspx.cs" Inherits="StudyTracker_WF.Applications.site" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/PagerStyle.css" rel="stylesheet" type="text/css" />
    <script>
        function AddSdata() {
            $("#shdn_PK").val("-1");
            $("#lblName").text("Add New Site");
            $("#TextName").val("");
            $("#TextLocation").val("");
            $("#sbtnSave").val("Create Site");
            $("#sbtnDelete").hide();
            $("#shdnAddMode").val("true");
            $("#divMessageArea").hide();
            $("#siteDialog").modal();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <h3>SITES</h3>
        </div>
    </div>
    
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <a href="#" data-toggle="modal" onclick="AddSdata()" class="btn btn-primary">Add New Site</a>
        </div>
    </div>
    
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-8">
            <div class="modal fade" id="siteDialog" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dimiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" runat="server" id="lblName" ClientIDMode="Static">Site</h4>
                        </div>
                        <div class="modal-body">
                            <input type="hidden" id="shdnPK" ClientIDMode="Static" runat="server" />
                            <input type="hidden" id="shdnAddMode" runat="server" value="false" ClientIDMode="Static"/>
                            <div class="row">
                                 <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="form-group">
                                        <label for="TextName">Name</label>
                                        <div class="row">
                                            <div class="col=xs-12 col-sm-8 col-md-8 col-lg-8">
                                                <asp:TextBox ID="TextName" runat="server"  CssClass="form-control"
                                                    autofocus="autofocus" required="required" placeholder="Site Name"
                                                    title="Site Name" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="TextLocation">Location</label>
                                        <div class="row">
                                            <div class="col=xs-12 col-sm-8 col-md-8 col-lg-8">
                                                <asp:TextBox ID="TextLocation" runat="server" CssClass="form-control"
                                                    autofocus="autofocus" required="required" placeholder="Location"
                                                    title="Location" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divMessageArea" runat="server" visible="false">
                                <div class="clearfix"></div>
                                <div class="row messageArea">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="well">
                                            <asp:Label ID="lblsMessage" runat="server"
                                                CssClass="text-warning"
                                                Text="This is some text to show what a message would look like."></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
<%--                        </div>--%>
                        <div class="modal-footer">
                            <asp:Button ID="sbtnCancel" CssClass="btn btn-default"
                                title="Cancel" formvalidate="formvalidate"
                                Text="Close"
                                UseSubmitBehavior="false"
                                data-dismiss="modal"
                                ClientIDMode="Static" 
                                runat="server"/>
                            <asp:Button ID="sbtnSave" CssClass="btn btn-primary"
                                Text="Update Site"
                                title="Update Site"
                                OnClick="sbtnSave_Click"
                                ClientIDMode="Static"
                                runat="server"/>
                            <asp:Button ID="sbtnDelete" runat="server"
                                Text="Delete" CssClass="btn btn-danger"
                                title="Delete Site"
                                ClientIDMode="Static"
                                OnClick="sbtnDelete_OnClickbtnDelete_OnClick" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1"
            CssClass="table table-bordered table-striped"
            PageSize="10"
            AllowPaging="True"
            AutoGenerateColumns="False" DataKeyNames="SiteId">
            <Columns>
                <asp:TemplateField HeaderText="Site Name">
                    <ItemTemplate>
                        <a href='site.aspx?id=<%# Eval("SiteId") %>'>
                            <asp:Label ID="lblName" ClientIDMode="Static" Text='<%# Bind("Name") %>' runat="server"></asp:Label>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />

            </Columns>
        </asp:GridView>
  
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetSites"
             TypeName="StudyTracker_WF.SiteClasses.SiteManager"></asp:ObjectDataSource>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndOfPageContent" runat="server">
</asp:Content>
