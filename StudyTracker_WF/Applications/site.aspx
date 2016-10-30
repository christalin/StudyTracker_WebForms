<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="site.aspx.cs" Inherits="StudyTracker_WF.Applications.site" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/PagerStyle.css" rel="stylesheet" type="text/css" />
    <script>
        function AddSdata() {
            $("#shdn_PK").val("-1");
            $("#lblName").text("Add New Site");
            $("#TextName").val("");
            $("#TextName").prop('required', true);
            $("#TextLocation").val("");
            $("#TextLocation").prop('required', true);
            $("#sbtnSave").val("Create Site");
            $("#sbtnDelete").hide();
            $("#shdnAddMode").val("true");
            $("#divMessageArea").hide();
            $("#siteDialog").modal();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <a href="#" data-toggle="modal" onclick="AddSdata()" class="btn btn-primary">Add New Site</a>
        </div>
    </div>
    <br/>
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <h4>LIST OF SITES</h4>
        </div>
    </div>


    <!--Popup Add Site-->
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-8">
            <div class="modal fade" id="siteDialog" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dimiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" runat="server" id="lblName" clientidmode="Static">Site</h4>
                        </div>
                        <div class="modal-body">
                            <input type="hidden" id="shdnPK" clientidmode="Static" runat="server" />
                            <input type="hidden" id="shdnAddMode" runat="server" value="false" clientidmode="Static" />
                            <div class="row">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="form-group">
                                            <label for="TextName">Name</label>
                                            <div class="row">
                                                <div class="col=xs-12 col-sm-8 col-md-8 col-lg-8">
                                                    <asp:TextBox ID="TextName" runat="server" CssClass="form-control"
                                                        autofocus="autofocus" placeholder="Site Name"
                                                        title="Site Name" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="TextLocation">Location</label>
                                            <div class="row">
                                                <div class="col=xs-12 col-sm-8 col-md-8 col-lg-8">
                                                    <asp:TextBox ID="TextLocation" runat="server" CssClass="form-control"
                                                        autofocus="autofocus" placeholder="Location"
                                                        title="Location" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divMessageArea" runat="server" Visible="False" ClientIDMode="Static">
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
                                    runat="server" />
                                <asp:Button ID="sbtnSave" CssClass="btn btn-primary"
                                    Text="Update Site"
                                    title="Update Site"
                                    OnClick="sbtnSave_Click"
                                    ClientIDMode="Static"
                                    runat="server" />
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

    <!--Popup Assign Study for a Site-->
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-8">
            <div class="modal fade" id="assignStudyDialog" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" runat="server" id="lbl2" clientidmode="Static">Assign Site</h4>
                        </div>
                        <div class="modal-body">
                            <input type="hidden" id="hdnSiteId" runat="server" />
                            <div class="row">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                                    <asp:DropDownList ID="ddlStudy" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="sdivAssignMsg" runat="server" Visible="False" ClientIDMode="Static">
                                    <div class="clearfix"></div>
                                    <div class="row messageArea">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <div class="well">
                                                <asp:Label ID="slblAssignMsg" runat="server"
                                                    ClientIDMode="Static"
                                                    CssClass="text-warning"
                                                    Text="This is some text to show what a message would look like."></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="modal-footer">
                                <asp:Button ID="Button2" runat="server" Text="Close"
                                    CssClass="btn btn-default"
                                    title="Close"
                                    formnovalidate="formvalidate" UseSubmitBehavior="false"
                                    data-dismiss="modal"
                                    ClientIDMode="Static" />
                                <asp:Button ID="sAssignstudysave" runat="server" Visible="True"
                                    Text="Save" CssClass="btn btn-primary"
                                    title="Save"
                                    ClientIDMode="Static"
                                    OnClick="btnAssignStudySave_OnClick" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
<!--Main GridView-->
    <div class="table-responsive width-75">
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1"
            CssClass="table table-bordered table-striped"
            PageSize="5"
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

                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="sbtnAssign" runat="server"
                            ClientIDMode="Static"
                            CssClass="btn btn-info"
                            CommandName="Assign Study"
                            CommandArgument='<%#Eval("SiteId") %>'
                            CausesValidation="False"
                            UseSubmitBehavior="True"
                            OnClick="sbtnAssign_OnClick"
                            Text="Assign Study" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                          <asp:Button ID="ShowAssignStudies" 
                              CommandName="Show Assigned Studies"
                              CommandArgument=<%#Eval("SiteId") %>
                              CausesValidation="False"
                              CssClass="btn btn-info" 
                              runat="server" Text="Show Assigned Studies"
                              UseSubmitBehavior="True"
                              OnClick="ShowAssignStudies_OnClick"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetSites"
            TypeName="StudyTracker_WF.SiteClasses.SiteManager"></asp:ObjectDataSource>
    </div>
    
<!--Show Assigned Studies GridView-->
    <div class="table-responsive width-75" id="gridshowstudies" runat="server" Visible="False">
       <div class="row">
           <div class="col-xs-12 col-sm-6 col-md-8">
             <h4>ASSIGNED STUDIES</h4>
           </div>
       </div>
       <asp:GridView ID="GridViewShowStudies" runat="server" 
           CssClass="table table-bordered table-striped"
           ClientIDMode="Static"
           PageSize="10" 
           AutoGenerateColumns="False"
           AllowPaging="True"
           EmptyDataText="No Studies Assigned!"
           OnRowCommand="GridViewDeleteAssignedStudies_OnRowCommand">
           
            <Columns>
               <asp:BoundField DataField="StudyTitle" HeaderText="Study Name" SortExpression="Title" />
                <asp:BoundField DataField="SiteName" HeaderText="Site Name" SortExpression="Name" />
               <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                           <asp:Button runat="server" 
                               CommandName="DeleteAssignedStudy" 
                               CommandArgument='<%#Eval("study_id") + ";" + Eval("site_id") %>'
                               CausesValidation="False"
                               CssClass="btn btn-danger"
                               Text="Delete" />
                     </ItemTemplate>
                </asp:TemplateField>
           </Columns>
           
           
       </asp:GridView>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndOfPageContent" runat="server">
</asp:Content>
