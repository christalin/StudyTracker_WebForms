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
            //$("#TextAvail").val("");
            $("#btnsave").val("Create Study");
            $("#hdnAddMode").val("true");
            //$('#productEdit').show();
            $("#studyDialog").modal()
            //$("#btnAdd").addClass("hidden");
            //$("#gridArea").hide();
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
                            <h4 class="modal-title" runat="server" id="lblTitle">Study</h4>
                        </div>
                        <div class="modal-body">
                            <input type="hidden" id="hdnPK" runat="server" />
                            <input type="hidden" id="hdnAddMode" runat="server" value="false" />
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="form-gruop">
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
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                CssClass="btn btn-default"
                                title="Cancel"
                                formnovalidate="formvalidate" UseSubmitBehavior="false" 
                                data-dismiss="modal"/>
                            <asp:Button ID="btnsave" runat="server"
                                Text="Save" CssClass="btn btn-default"
                                title="Create Study"
                                OnClick="CreateStudy"/>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="table-responsive">
        <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-striped table-bordered"
            AutoGenerateDeleteButton="True" AutoGenerateEditButton="True"
            DataSourceID="ObjectDataSource2" AutoGenerateRows="False" OnItemUpdated="DetailsView1_ItemUpdated" DataKeyNames="Id" OnItemDeleted="DetailsView1_ItemDeleted">
            <Fields>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="PrincipalInvestigator" HeaderText="PrincipalInvestigator" SortExpression="PrincipalInvestigator" />
                <asp:CheckBoxField DataField="Availability" HeaderText="Availability" SortExpression="Availability" />
                <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" SortExpression="CreatedDate" />
                <asp:BoundField DataField="UpdatedBy" HeaderText="UpdatedBy" SortExpression="UpdatedBy" />
                <asp:BoundField DataField="UpdatedDate" HeaderText="UpdatedDate" SortExpression="UpdatedDate" />
            </Fields>
        </asp:DetailsView>
    </div>

    <div class="row" id="gridArea">
        <div class="col-xs-12 col-sm-12 col-md-8">
            <div class="table-responsive">

                <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" 
                    CssClass="table table-striped table-bordered"
                    PageSize="10"
                    AllowPaging="True"
                    DataKeyNames="Id" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="PrincipalInvestigator" HeaderText="PrincipalInvestigator" SortExpression="PrincipalInvestigator" />
                        <asp:CheckBoxField DataField="Availability" HeaderText="Availability" SortExpression="Availability" />
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>

                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetStudy"
                    TypeName="StudyTracker_WF.StudyClasses.StudyManager" DataObjectTypeName="StudyTracker_WF.StudyClasses.Study"
                    UpdateMethod="UpdateStudy" DeleteMethod="DeleteStudy">

                    <SelectParameters>
                        <asp:ControlParameter ControlID="GridView1" Name="Id" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetStudies"
                    TypeName="StudyTracker_WF.StudyClasses.StudyManager"></asp:ObjectDataSource>

            </div>


            <%-- <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server" for="txtTitle" class="col-sm-2 control-label">Title</asp:Label>
            <div class="col-sm-12">
                <asp:TextBox class="form-control" ID="txtTitle" placeholder="Title" runat="server" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" for="txtPI" class="col-sm-2 control-label">Principal Investigator</asp:Label>
            <div class="col-sm-12">
                <asp:TextBox class="form-control" ID="txtPI" placeholder="Principal Investigator" runat="server" />
            </div>
        </div>

        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-12">
                <div class="checkbox">
                    <asp:Label runat="server" for="txtavail">
                        <asp:CheckBox ID="txtavail" runat="server" />
                        Availability
                    </asp:Label>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-12">
                <asp:Button type="submit" class="btn btn-success" OnClick="CreateStudy" runat="server" Text="New Study"></asp:Button>
                <asp:Label runat="server" ID="lblInsertInfo"></asp:Label>
            </div>
        </div>
    </div>--%>
        </div>
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

