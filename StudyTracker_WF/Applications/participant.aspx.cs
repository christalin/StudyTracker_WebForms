using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyTracker_WF.Participant_Classes;

namespace StudyTracker_WF.Applications
{
    public partial class participant : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                TextDob.Text = DateTime.Today.AddDays(-7).ToString("yyyy/MM/dd");
                GridRefresh();

                if (Request.QueryString["id"] != null)
                {
                    var pk = Convert.ToString(Request.QueryString["id"]);
                    plblTitle.InnerText = "Update/Delete Study";
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
            TextGender.Text = s.Gender;
            TextDob.Text = s.Dob.ToString();
            TextAddress.Text = s.Address;
            phdnPK.Value = s.ParticipantId.ToString();
            phdnAddMode.Value = "false";
        }

        public void LoadEditScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#participantDialog').modal();");
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
                participant.Gender = TextGender.Text;
                participant.Dob = DateTime.Parse(TextDob.Text);
                participant.CreatedBy = "Christy";
                participant.UpdatedBy = "Christy";

                if (Convert.ToBoolean(phdnAddMode.Value))
                {
                    plblMessage.Text = "Inserting";
                    pbtnsave.Text = "Create Participant";
                    pm.InsertParticipant(participant);
                    plblMessage.Text = "Participant Inserted Successfully!";
                    pdivMessageArea.Visible = true;
                    GridRefresh();

                }
                else
                {
                    plblMessage.Text = "Updating";
                    participant.ParticipantId = Convert.ToInt32(phdnPK.Value);
                    pm.UpdateParticipant(participant);
                    plblMessage.Text = "Participant Updated Successfully!";
                    pdivMessageArea.Visible = true;
                    GridRefresh();
                }

            }
            catch (Exception p)
            {
                plblMessage.Text = "Error while " + plblMessage.Text + " a Participant Record!!";
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
                plblMessage.Text = "Participant deleted Successfully!!";
                pdivMessageArea.Visible = true;
                GridRefresh();
            }
            catch (Exception q)
            {
                plblMessage.Text = "Error while Deleting Participant";
                pdivMessageArea.Visible = true;
            }
        }

        public void KeepModalOpenScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#participantDialog').modal({ show: true });");
            sb.AppendLine("$('#sbtnDelete').hide()");
            sb.AppendLine("$('#sbtnsave').hide()");
            sb.AppendLine("$('#shdnAddMode').val('true')");
            sb.AppendLine("});");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Pop", sb.ToString(), true);

        }


        private void GridRefresh()
        {
            GridViewParticipant.DataSource = new ParticipantManager().GetParticipants();
            GridViewParticipant.DataBind();
        }

    }
}