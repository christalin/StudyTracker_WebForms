<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="participant.aspx.cs" Inherits="StudyTracker_WF.Applications.participant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/PagerStyle.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
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
            $("#TextDob").datepicker();
            $("#TextAddress").val("");
            $("#pbtnsave").val("Create Study");
            $("#pbtnDelete").hide();
            $("#phdnAddMode").val("true");
            $("#MainContent_pdivMessageArea").hide();
            $("#participantDialog").modal();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br/>
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <a href="#" data-toggle="modal" onclick="AddData()" class="btn btn-primary">Add New Participant</a>
        </div>
    </div>
    <br/>
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
                            <h4 class="modal-title" runat="server" id="plblTitle" ClientIDMode="Static">Participant</h4>
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
                                                <asp:TextBox ID="TextGender" runat="server" CssClass="form-control"
                                                    autofocus="autofocus" placeholder="Gender"
                                                    title="Gender" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-gruop">
                                        <label for="TextDob">Date Of Birth</label>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                                <asp:TextBox ID="TextDob" runat="server" CssClass="form-control"
                                                    autofocus="autofocus" placeholder="Date Of Birth"
                                                    title="Date Of Birth" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-gruop">
                                        <label for="TextAddress">Gender</label>
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
                            <div id="pdivMessageArea" runat="server" visible="false">
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

    <!---Main GridView -->   
     <asp:GridView ID="GridViewParticipant" runat="server" 
        CssClass="table table-bordered table-striped"
        PageSize="10"
        AllowPaging="True">
        <Columns>
            <asp:TemplateField HeaderText="Study Name">
                    <ItemTemplate>
                        <a href='participant.aspx?id=<%#Eval("ParticipantId") %>'>
                            <asp:Label ID="plblTitle" runat="server" ClientIDMode="Static" Text='<%# Bind("ParticipantName") %>'></asp:Label>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                <asp:BoundField DataField="Dob" HeaderText="Dob" SortExpression="Dob" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
        </Columns> 
        
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="EndOfPageContent" runat="server">
    
</asp:Content>

