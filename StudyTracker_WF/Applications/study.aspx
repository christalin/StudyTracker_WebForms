<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="study.aspx.cs"
    Inherits="StudyTracker_WF.Study.study" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/PagerStyle.css" rel="stylesheet" type="text/css" />
    <script>
        function AddData() {
            $("#hdnPK").val("-1");
            $("#lblTitle").text("Add New Study");
            $("#TextTitle").val("");
            $("#TextPI").val("");
            $("#btnsave").val("Create Study");
            $("#btnDelete").hide();
            $("#hdnAddMode").val("true");
            $("#studyDialog").modal();

        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <h3>STUDY</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <%--            <a id="btnAdd" class="btn btn-primary" onclick="AddData();">Add New Study</a>--%>
            <a href="#" data-toggle="modal" onclick="AddData()" class="btn btn-primary">Add New Study</a>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-8">
            <div class="modal fade" id="studyDialog" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dimiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" runat="server" id="lblTitle" clientidmode="Static">Study</h4>
                        </div>
                        <div class="modal-body">
                            <input type="hidden" id="hdnPK" runat="server" />
                            <input type="hidden" id="hdnAddMode" runat="server" value="false" clientidmode="Static" />
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="form-group">
                                        <label for="TextTitle">Title</label>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                                <asp:TextBox ID="TextTitle" runat="server" CssClass="form-control"
                                                    autofocus="autofocus" required="required" placeholder="Study Name"
                                                    title="Study Name" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-gruop">
                                        <label for="TextPI">Principal Investigator</label>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                                <asp:TextBox ID="TextPI" runat="server" CssClass="form-control"
                                                    autofocus="autofocus" required="required" placeholder="Principal Investigator"
                                                    title="Principal Investigator" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="checkbox">
                                            <label>
                                                <asp:CheckBox ID="TextAvail" runat="server" ClientIDMode="Static" />Availability
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divMessageArea" runat="server" visible="false">
                                <div class="clearfix"></div>
                                <div class="row messageArea">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="well">
                                            <asp:Label ID="lblMessage" runat="server"
                                                CssClass="text-warning"
                                                Text="This is some text to show what a message would look like."></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                            CssClass="btn btn-default"
                            title="Cancel"
                            formnovalidate="formvalidate" UseSubmitBehavior="false"
                            data-dismiss="modal"
                            ClientIDMode="Static" />
                        <asp:Button ID="btnsave" runat="server"
                            Text="Save" CssClass="btn btn-primary"
                            title="Create Study"
                            ClientIDMode="Static"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server"
                            Text="Delete" CssClass="btn btn-danger"
                            title="Delete Study"
                            ClientIDMode="Static"
                            OnClick="btnDelete_OnClick" />
                    </div>

                </div>
            </div>
        </div>
    </div>
 


   <div class="table-responsive">

       <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1"
           CssClass="table table-striped table-bordered"
           PageSize="10"
           AllowPaging="True"
           DataKeyNames="Id" AutoGenerateColumns="False">
           <Columns>
               <asp:TemplateField HeaderText="Study Name">
                   <ItemTemplate>
                       <a href='study.aspx?id=<%#Eval("Id") %>'>
                           <asp:Label ID="lblTitle" runat="server" ClientIDMode="Static" Text='<%# Bind("Title") %>'></asp:Label>
                       </a>
                   </ItemTemplate>
               </asp:TemplateField>
               <%--                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />--%>
               <asp:BoundField DataField="PrincipalInvestigator" HeaderText="PrincipalInvestigator" SortExpression="PrincipalInvestigator" />
               <asp:CheckBoxField DataField="Availability" HeaderText="Availability" SortExpression="Availability" />
               <%--                    <asp:CommandField ShowSelectButton="True" />--%>
           </Columns>
       </asp:GridView>

       <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetStudies"
           TypeName="StudyTracker_WF.StudyClasses.StudyManager"></asp:ObjectDataSource>

   </div>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="EndOfPageContent" runat="server">
    <script>
        // Make DetailView 'Edit', 'Delete', 'New' links show as buttons
        $(document).ready(function () {
            $("td[colspan='2'] a").addClass('btn btn-primary');
        });
    </script>
</asp:Content>

<%--            <div id="productEdit" hidden="hidden">--%>
<%--            <div class="panel panel-primary">--%>

