using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using StudyTracker_WF.Participant_Classes;
using StudyTracker_WF.StudysiteClasses;
using StudyTracker_WF.StudysiteParticipant_Classes;

namespace StudyTracker_WF.Applications
{
    public partial class participant : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!Page.IsPostBack)
            {
                GridRefresh();

                if (Request.QueryString["id"] != null)
                {
                    var pk = Convert.ToString(Request.QueryString["id"]);
                    plblTitle.InnerText = "Update/Delete Participant";
                    pbtnsave.Text = "Update Participant";
                    pbtnDelete.Text = "Delete Participant";
                    phdnPK.Value = pk;
                    phdnAddMode.Value = "false";

                    LoadForEdit(pk);
                    LoadEditScript();

                }
            }

        }

        public void LoadForEdit(string pk)
        {
            ParticipantManager pm = new ParticipantManager();
            var s = pm.GetParticipant(Convert.ToInt32(pk));
            TextPName.Text = s.ParticipantName;
            if (s.Gender == "M")
            {
                rgender.SelectedValue = "M";
                
            }
            else
            {
                rgender.SelectedValue = "F";
                
            }

            datepicker.Value = s.Dob.ToShortDateString();
            TextAddress.Text = s.Address;
            phdnPK.Value = s.ParticipantId.ToString();
            phdnAddMode.Value = "false";
        }

        public void LoadEditScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#participantDialog').modal();");
            sb.AppendLine("$('#datepicker').datepicker();");
            sb.AppendLine("});");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "EditParticipantData", sb.ToString(), true);

        }

        protected void btnSaveParticipant_Click(object sender, EventArgs e)
        {
            try
            {
                KeepModalOpenScript();
                ParticipantManager pm = new ParticipantManager();
                var participant = new Participant();
                participant.ParticipantName = TextPName.Text;
                participant.Address = TextAddress.Text;
                participant.Gender = rgender.SelectedValue;
                //participant.Dob = DateTime.Parse(TextDob.Text);
                //string dateValue = Page.Request.Form["ctl00$MainContent$datepicker"].ToString();
                participant.Dob = Convert.ToDateTime(datepicker.Value);
                participant.CreatedBy = HttpContext.Current.User.Identity.GetUserName();
                participant.UpdatedBy = HttpContext.Current.User.Identity.GetUserName();

                if (Convert.ToBoolean(phdnAddMode.Value))
                {
                    plblMessage.Text = "creating";
                    pbtnsave.Text = "Create Participant";
                    pm.InsertParticipant(participant);
                    plblMessage.Text = "Participant has been created!";
                    pdivMessageArea.Visible = true;
                    GridRefresh();

                }
                else
                {
                    plblMessage.Text = "updating";
                    participant.ParticipantId = Convert.ToInt32(phdnPK.Value);
                    pm.UpdateParticipant(participant);
                    plblMessage.Text = "Participant has been updated!";
                    pdivMessageArea.Visible = true;
                    GridRefresh();
                }
                pbtnsave.Visible = false;

            }
            catch (SqlException p)
            {
                if (p.Message.Contains("UNIQUE KEY constraint"))
                {
                    plblMessage.Text = "Participant already Exists!!";
                    pbtnsave.Visible = true;
                }
                else
                {
                    plblMessage.Text = "Error while " + plblMessage.Text + " Participant!!";
                }
                pdivMessageArea.Visible = true;
            }
            catch (Exception p)
            {
                plblMessage.Text = "Error while " + plblMessage.Text + " a Participant!!";
                pdivMessageArea.Visible = true;
            }

        }

        protected void pbtnDeleteParticipant_OnClick(object sender, EventArgs e)
        {
            try
            {
                KeepModalOpenScript();
                ParticipantManager pm = new ParticipantManager();
                var participant = new Participant();
                participant.ParticipantId = Convert.ToInt32(phdnPK.Value);
                pm.DeleteParticipant(participant);
                plblMessage.Text = "Participant has been deleted!!";
                pdivMessageArea.Visible = true;
                GridRefresh();
            }
            catch (SqlException q)
            {
                if (q.Message.Contains("REFERENCE constraint"))
                {
                    plblMessage.Text = "Participant has Enrollments.";
                    pbtnsave.Visible = true;
                }
                else
                {
                    plblMessage.Text = "Error while deleting Participant!!";
                }
                pdivMessageArea.Visible = true;
            }
            catch (Exception q)
            {
                plblMessage.Text = "Error while Deleting Participant";
                pdivMessageArea.Visible = true;
            }
            pbtnsave.Visible = false;
        }

        public void KeepModalOpenScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#participantDialog').modal({ show: true });");
            sb.AppendLine("$('#pbtnDelete').hide();");
            sb.AppendLine("$('#phdnAddMode').val('true');");
            sb.AppendLine("});");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Pop", sb.ToString(), true);

        }

        protected void EnrollStudy_OnClick(object sender, EventArgs e)
        {
            Button b = (Button) sender;
            BindDropdown();
            hdnStudysiteId.Value = b.CommandArgument.ToString();
            divEnrollMsg.Visible = false;
            EnrollSave.Visible = true;
            OpenStudySites();
        }

        public void BindDropdown()
        {
            ddlStudysite.DataSource = new StudysiteManager().GetAllStudysites();
            ddlStudysite.DataTextField = "DropdownForParticipant";
            ddlStudysite.DataValueField = "id";
            ddlStudysite.DataBind();

        }

        public void OpenStudySites()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#assignStudysiteDialog').modal({ show: true });");
            sb.AppendLine("});");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopStudySiteForEnrollment", sb.ToString(), true);

        }

        protected void btnEnrollSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                OpenStudySites();
                StudysiteparticipantManager spm = new StudysiteparticipantManager();
                var enrolledparticipant = new StudysiteParticipant();
                enrolledparticipant.studysite_id = Int32.Parse(ddlStudysite.SelectedValue);
                enrolledparticipant.participant_id = Int32.Parse(hdnStudysiteId.Value);
                spm.InsertEnrolledparticipant(enrolledparticipant);
                lblEnrollMsg.Text = "Participant enrolled successfully!";
                divEnrollMsg.Visible = true;
                EnrollSave.Visible = false;
                GridViewShowEnrolledStudy.DataSource = new StudysiteparticipantManager().GetEnrollments(enrolledparticipant.participant_id);
                EnrollRefresh();
            }
            catch (SqlException r)
            {
                if (r.Message.Contains("UNIQUE KEY constraint"))
                {
                    lblEnrollMsg.Text = "Participant has already been Enrolled!!";
                    EnrollSave.Visible = true;
                }
                else
                {
                    lblEnrollMsg.Text = "";
                }
                divEnrollMsg.Visible = true;
            }

            catch (Exception r)
            {
                lblEnrollMsg.Text = "Error while Enrolling participant!!";
                divEnrollMsg.Visible = true;
            }

        }

        private void GridRefresh()
        {
            GridViewParticipant.DataSource = new ParticipantManager().GetParticipants();
            GridViewParticipant.DataBind();
        }

        protected void ShowEnrolledStudies_OnClick(object sender, EventArgs e)
        {
            Button btnEnroll = (Button) sender;
            var showenrollid = btnEnroll.CommandArgument.ToString();
            var participantenrolled = new StudysiteparticipantManager();
            int id = Int32.Parse(showenrollid);
            GridViewShowEnrolledStudy.DataSource = new StudysiteparticipantManager().GetEnrollments(id);
            EnrollRefresh();
            gridshowenrollments.Visible = true;
        }

        private void EnrollRefresh()
        {
            GridViewShowEnrolledStudy.DataBind();
        }


        protected void GridViewDelete_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteEnrollment")
            {
                string [] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(';');
                //var id = e.CommandArgument.ToString();
                StudysiteparticipantManager spm = new StudysiteparticipantManager();
                var deleterecord = new StudysiteParticipant();
                //deleterecord.id = Convert.ToInt32(id);
                deleterecord.id = Convert.ToInt32(arg[0]);
                deleterecord.participant_id = Convert.ToInt32(arg[1]);
                spm.DeleteEnrollment(deleterecord);
                GridViewShowEnrolledStudy.DataSource = new StudysiteparticipantManager().GetEnrollments(deleterecord.participant_id);
                EnrollRefresh();

            }
        }

        protected void GridViewParticipant_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewParticipant.DataSource = new ParticipantManager().GetParticipants();
            GridViewParticipant.DataBind();

            GridViewParticipant.PageIndex = e.NewPageIndex;
            GridViewParticipant.DataBind();
        }

        protected void GridViewParticipant_OnSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = GridViewParticipant.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                GridViewParticipant.DataSource = dataView;
                GridViewParticipant.DataBind();
            }
        }

        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }
    }
}