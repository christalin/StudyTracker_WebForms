<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="participant.aspx.cs" Inherits="StudyTracker_WF.Applications.participant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/PagerStyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <%--  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <a href="#" data-toggle="modal" onclick="AddData()" class="btn btn-primary">Add New Participant</a>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <h4>LIST OF PARTICIPANTS</h4>
        </div>
    </div>

    <!--Popup Add Participant-->
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-8">
            <div class="modal fade" id="participantDialog" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" runat="server" id="plblTitle" clientidmode="Static">Participant</h4>
                        </div>
                        <div class="modal-body">
                            <input type="hidden" id="phdnPK" runat="server" />
                            <input type="hidden" id="phdnAddMode" runat="server" value="false" clientidmode="Static" />
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                    <div class="form-group">
                                        <label for="TextPName">Participant Name</label>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                                <asp:TextBox ID="TextPName" runat="server" CssClass="form-control"
                                                    autofocus="autofocus" placeholder="Participant Name"
                                                    title="Participant Name" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-gruop">
                                        <label for="TextGender">Gender</label>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                               
                                                <asp:RadioButtonList ID="rgender" runat="server">

                                                    <asp:ListItem Text="M" Value="M" />
                                                    <asp:ListItem Text="F" Value="F" />
                                                    
                                                    </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="GenderRequired" runat="server"
                                                     ErrorMessage="Please select a gender"
                                                    ControlToValidate="rgender"
                                                    Display="Dynamic">
                                                    
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Date Of Birth</label>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                              <input type="text" id="datepicker" runat="server" ClientIDMode="Static"/>
                                            </div>
                                        </div>
                                    </div>
                                   
                                    <div class="form-gruop">
                                        <label for="TextAddress">Address</label>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                                <asp:TextBox ID="TextAddress" runat="server" CssClass="form-control"
                                                    autofocus="autofocus" placeholder="Address"
                                                    title="Address" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div id="pdivMessageArea" runat="server" visible="False" clientidmode="Static">
                                <div class="clearfix"></div>
                                <div class="row messageArea">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="well">
                                            <asp:Label ID="plblMessage" runat="server"
                                                ClientIDMode="Static"
                                                CssClass="text-warning"
                                                Text="This is some text to show what a message would look like."></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="pbtnCancel" runat="server" Text="Close"
                                CssClass="btn btn-default"
                                title="Cancel"
                                formnovalidate="formvalidate" UseSubmitBehavior="false"
                                data-dismiss="modal"
                                ClientIDMode="Static" />
                            <asp:Button ID="pbtnsave" runat="server"
                                Text="Save" CssClass="btn btn-primary"
                                title="Create Participant"
                                ClientIDMode="Static"
                                OnClick="btnSaveParticipant_Click" />
                            <asp:Button ID="pbtnDelete" runat="server"
                                Text="Delete" CssClass="btn btn-danger"
                                title="Delete Participant"
                                ClientIDMode="Static"
                                OnClick="pbtnDeleteParticipant_OnClick" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--Popup Assign Studysite for a Participant-->
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-8">
            <div class="modal fade" id="assignStudysiteDialog" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" runat="server" id="lbl2" clientidmode="Static">Assign Study</h4>
                        </div>
                        <div class="modal-body">
                            <input type="hidden" id="hdnStudysiteId" runat="server" />
                            <div class="row">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                                    <asp:DropDownList ID="ddlStudysite" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divEnrollMsg" runat="server" visible="False" clientidmode="Static">
                                    <div class="clearfix"></div>
                                    <div class="row messageArea">
                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <div class="well">
                                                <asp:Label ID="lblEnrollMsg" runat="server"
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
                                <asp:Button ID="EnrollSave" runat="server"
                                    Text="Save" CssClass="btn btn-primary"
                                    Visible="True"
                                    title="Save"
                                    ClientIDMode="Static"
                                    OnClick="btnEnrollSave_OnClick" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!---Main GridView -->
    <div class="table-responsive width-75">
        <asp:GridView ID="GridViewParticipant" runat="server"
            CssClass="table table-bordered table-striped"
            PageSize="5"
            OnPageIndexChanging="GridViewParticipant_OnPageIndexChanging"
            OnSorting="GridViewParticipant_OnSorting"
            AllowPaging="True"
            DataKeyNames="ParticipantId"
            AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="Participant Name">
                    <ItemTemplate>
                        <a href='participant.aspx?id=<%#Eval("ParticipantId") %>'>
                            <asp:Label ID="plblTitle" runat="server" ClientIDMode="Static" Text='<%# Bind("ParticipantName") %>'></asp:Label>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                <%-- <asp:BoundField DataField="Dob" HeaderText="Dob" SortExpression="Dob" />--%>
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="EnrollStudy" runat="server" CommandName="Enroll"
                            CommandArgument='<%#Eval("ParticipantId") %>'
                            CausesValidation="False"
                            Text="Enroll" CssClass="btn btn-info"
                            UseSubmitBehavior="True"
                            OnClick="EnrollStudy_OnClick" />

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="ShowEnrolledStudies"
                            CommandName="Show Enrolled Studies"
                            CommandArgument='<%#Eval("ParticipantId") %>'
                            CausesValidation="False"
                            CssClass="btn btn-info"
                            runat="server" Text="Show Enrolled Studies"
                            UseSubmitBehavior="True"
                            OnClick="ShowEnrolledStudies_OnClick" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!--Show Enrollments for participants Gridview-->
    <div class="table-responsive width-75" id="gridshowenrollments" runat="server" visible="False">
        <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-8">
                <h4>ENROLLMENTS</h4>
            </div>
        </div>
        <asp:GridView ID="GridViewShowEnrolledStudy" runat="server"
            CssClass="table table-bordered table-striped"
            ClientIDMode="Static"
            PageSize="10"
            AutoGenerateColumns="False"
            AllowPaging="True"
            EmptyDataText="Not Enrolled to any Study!"
            OnRowCommand="GridViewDelete_OnRowCommand">
            <Columns>
                <asp:BoundField DataField="participant_name" HeaderText="Participant Name" SortExpression="participant_name" />
                <asp:BoundField DataField="study_name" HeaderText="Study Name" SortExpression="study_name" />
                <asp:BoundField DataField="site_name" HeaderText="Site Name" SortExpression="site_name" />
                <asp:BoundField DataField="study_PI" HeaderText="Principal Investigator" SortExpression="study_PI" />
                <asp:BoundField DataField="site_location" HeaderText="Location" SortExpression="site_location" />

                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button runat="server"
                            CommandName="DeleteEnrollment"
                            CommandArgument='<%#Eval("id") + ";" + Eval("participant_id") %>'
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
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        function AddData() {
            $("#phdnPK").val("-1");
            $("#plblTitle").text("Add New Participant");
            $("#TextPName").prop('required', true);
            $("#TextGender").prop('required', true);
            $("#TextDob").prop('required', true);
            $("#TextAddress").prop('required', true);
            $("#TextPName").val("");
            $("#TextGender").val("");
            $("#TextDob").val("");
            $("#TextAddress").val("");
            $("#pbtnsave").val("Create Participant");
            $("#pbtnDelete").hide();
            $("#phdnAddMode").val("true");
            $("#pdivMessageArea").hide();
            $("#participantDialog").modal();
            $('#divEnrollMsg').hide();
            $("#datepicker").datepicker();
            $("#rgender").val('');

        }
        //  $(document).ready(function () { $('#datepicker').datepicker(); });

    </script>
</asp:Content>

