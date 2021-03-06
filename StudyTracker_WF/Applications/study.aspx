﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="study.aspx.cs"
    Inherits="StudyTracker_WF.Study.study" EnableViewState="true" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/PagerStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function AddData() {
            $("#hdnPK").val("-1");
            $("#lblTitle").text("Add New Study");
            $("#TextTitle").prop('required', true);
            $("#TextPI").prop('required', true);
            $("#TextTitle").val("");
            $("#TextPI").val("");
            $("#btnsave").val("Create Study");
            $("#btnDelete").hide();
            $("#hdnAddMode").val("true");
            $("#divMessageArea").hide();
            $("#studyDialog").modal();

        }

        function Assign() {
            //$('#Button4').click();
            //alert('clcik');
            alert($(this).closest('tr').find('.current-study-id').text());
            $('#hdnpk1').val('-1');
            $('#hdnAddMode1').val('true');
            $('#lbl2').text('Assign Site to a Study');
            $('#assignSiteDialog').modal();
            //    $.ajax({
            //        type: "POST",
            //        url: "study.aspx/SitesForList",
            //        data: '{ }',
            //        contentType: "application/json; charset=utf-8",
            //        dataType: "json",
            //        success: function(response) {
            //            console.log(response.d);
            //            var ddlSites = $("[id*=ddlSite]");
            //            ddlSites.empty().append('<option selected="selected" value="0">Please select</option>');
            //            $.each(response.d, function () {
            //                ddlSites.append($("<option></option>").val(this['SiteId']).html(this['Name']+'--'+this['Location']));
            //            });
            //        }
            //});
        }

        function FireConfirm() {
            if (confirm('Do you want to delete?'))
                return true;
            else
                return false;
        }

    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <br/>
    <div class="row">
        <div class="col-xs-12 col-sm-6 col-md-8">
            <a href="#" data-toggle="modal" onclick="AddData()" class="btn btn-primary">Add New Study</a>
           
        </div>
    </div>
    <br/>
    <div class="row">
        <div class="col-md-6 col-md-offset-3">
            <h4><b>LIST OF STUDIES</b></h4>
        </div>
    </div>
    
    <!--Popup Add Study-->
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-8">
            <div class="modal fade" id="studyDialog" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
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
                                                    autofocus="autofocus" placeholder="Study Name"
                                                    title="Study Name" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-gruop">
                                        <label for="TextPI">Principal Investigator</label>
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                                <asp:TextBox ID="TextPI" runat="server" CssClass="form-control"
                                                    autofocus="autofocus" placeholder="Principal Investigator"
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
                            <div id="divMessageArea" runat="server" Visible="False" ClientIDMode="Static">
                                <div class="clearfix"></div>
                                <div class="row messageArea">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="well">
                                            <asp:Label ID="lblMessage" runat="server"
                                                ClientIDMode="Static"
                                                CssClass="text-warning"
                                                Text="This is some text to show what a message would look like."></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnCancel" runat="server" Text="Close"
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
    </div>


    <!--Popup Assign Site for a Study-->
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-8">
            <div class="modal fade" id="assignSiteDialog" tabindex="-1" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" runat="server" id="lbl2" clientidmode="Static">Assign Site</h4>
                        </div>
                        <div class="modal-body">
                            <input type="hidden" id="hdnStudyId" runat="server" />
                            <input type="hidden" id="hdnAddMode1" runat="server" value="false" clientidmode="Static" />
                             <div class="row">
                             <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-8 col-md-8 col-lg-8">
                                                <asp:DropDownList ID="ddlSite" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divAssignMsg" runat="server" Visible="False" ClientIDMode="Static">
                                <div class="clearfix"></div>
                                <div class="row messageArea">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="well">
                                            <asp:Label ID="lblAssignMsg" runat="server"
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
                            <asp:Button ID="AssignSave" runat="server"
                                Text="Save" CssClass="btn btn-primary"
                                title="Save"
                                ClientIDMode="Static"
                                OnClick="btnAssignSiteSave_OnClick" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>

    <!--Main GridView-->
    <div class="table-responsive width-75" >
       
        <asp:GridView ID="GridView1" runat="server" 
            CssClass="table table-striped table-bordered"
            PageSize="5"
            OnPageIndexChanging="GridView1_OnPageIndexChanging"
            OnSorting="GridView1_OnSorting"
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
                <asp:BoundField DataField="PrincipalInvestigator" HeaderText="PrincipalInvestigator" SortExpression="PrincipalInvestigator" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CommandName="Assign" 
                            CommandArgument=<%#Eval("Id") + ";" + Eval("Availability") %> 
                            CausesValidation="False" 
                            Text="Assign Site" CssClass="btn btn-info"
                             UseSubmitBehavior="True" OnClick="Button1_OnClick" />
                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                          <asp:Button ID="ShowAssignSites" 
                              CommandName="Show Assigned Sites"
                              CommandArgument=<%#Eval("Id") %>
                              CausesValidation="False"
                              CssClass="btn btn-info" 
                              runat="server" Text="Show Assigned Sites"
                              UseSubmitBehavior="True"
                              OnClick="ShowAssignSites_OnClick"/>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
</div>
    
<!--Show Assigned Sites Gridview-->
 <div class="table-responsive width-75" id="gridshowsites" runat="server" Visible="False">
       <div class="row">
           <div class="col-xs-12 col-sm-6 col-md-8">
             <h4>ASSIGNED SITES</h4>
           </div>
       </div>
       <asp:GridView ID="GridViewShowSites" runat="server" 
           CssClass="table table-bordered table-striped"
           ClientIDMode="Static"
           PageSize="10" 
           AutoGenerateColumns="False"
           AllowPaging="True"
           EmptyDataText="No Sites Assigned!"
           OnRowCommand="GridViewShowDelete_OnRowCommand">
            <Columns>
               <asp:BoundField DataField="StudyTitle" HeaderText="Study Name" SortExpression="Title" />
                <asp:BoundField DataField="SiteName" HeaderText="Site Name" SortExpression="Name" />
               <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                           <asp:Button runat="server" 
                               CommandName="DeleteAssignSite" 
                               CommandArgument='<%#Eval("study_id") + ";" + Eval("site_id") %>'
                               CausesValidation="False"
                               CssClass="btn btn-danger"
                               OnClientClick="return FireConfirm()"
                               Text="Delete" />
                     </ItemTemplate>
                </asp:TemplateField>
           </Columns>
           
           
       </asp:GridView>
</div>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="EndOfPageContent" runat="server">
       <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap3-dialog/1.34.7/js/bootstrap-dialog.min.js"></script>
       <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap3-dialog/1.34.7/css/bootstrap-dialog.min.css">
</asp:Content>
