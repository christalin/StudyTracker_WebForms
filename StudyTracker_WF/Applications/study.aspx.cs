using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyTracker_WF.SiteClasses;
using StudyTracker_WF.StudyClasses;
using StudyTracker_WF.StudysiteClasses;

namespace StudyTracker_WF.Study
{
    public partial class study : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridRefresh();
                if (Request.QueryString["id"] != null)
                {
                    var pk = Convert.ToString(Request.QueryString["id"]);
                    lblTitle.InnerText = "Update/Delete Study";
                    btnsave.Text = "Update Study";
                    btnDelete.Text = "Delete Study";
                    hdnPK.Value = pk;
                    hdnAddMode.Value = "false";

                    LoadForEdit(pk);
                    LoadEditScript();

                }
            }
        }

        //#region WebMethods

        //[System.Web.Services.WebMethod]
        //public static List<Site> SitesForList()
        //{
        //    SiteManager sitm = new SiteManager();
        //    return sitm.GetSites();
        //}
        //#endregion

     
        public void LoadForEdit(string pk)
        {
            StudyManager smr = new StudyManager();
            var s = smr.GetStudy(Convert.ToInt32(pk));
            TextTitle.Text = s.Title;
            TextPI.Text = s.PrincipalInvestigator;
            TextAvail.Checked = s.Availability;
            hdnPK.Value = s.Id.ToString();
            hdnAddMode.Value = "false";
        }

        public void LoadEditScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#studyDialog').modal();");
            sb.AppendLine("});");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "EditData", sb.ToString(), true);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                KeepModalOpenScript();
                StudyManager sm = new StudyManager();
                var study = new StudyClasses.Study();
                study.Title = TextTitle.Text;
                study.PrincipalInvestigator = TextPI.Text;
                study.Availability = TextAvail.Checked;
                study.CreatedBy = "Christy";
                study.UpdatedBy = "Christy";

                if (Convert.ToBoolean(hdnAddMode.Value))
                {
                    lblMessage.Text = "Inserting";
                    btnsave.Text = "Create Study";
                    sm.Insert(study);
                    lblMessage.Text = "Study inserted Successfully!!!!";
                    divMessageArea.Visible = true;
                    GridRefresh();
                }
                else
                {
                    lblMessage.Text = "Updating";
                    study.Id = Convert.ToInt32(hdnPK.Value);
                    sm.UpdateStudy(study);
                    lblMessage.Text = "Study updated Successfully!!!!";
                    divMessageArea.Visible = true;
                    GridRefresh();

                }

            }
            catch (Exception a)
            {
                lblMessage.Text = "Error while " + lblMessage.Text + " a Study Record!!";
                divMessageArea.Visible = true;
            }

        }
        public void KeepModalOpenScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#studyDialog').modal({ show: true });");
            sb.AppendLine("$('#btnDelete').hide()");
            sb.AppendLine("$('#btnsave').hide()");
            sb.AppendLine("$('#hdnAddMode').val('true')");
            sb.AppendLine("});");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Pop", sb.ToString(), true);

        }

        
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                KeepModalOpenScript();
                StudyManager sd = new StudyManager();
                var d = new StudyClasses.Study();
                d.Id = Convert.ToInt32(hdnPK.Value);
                sd.DeleteStudy(d);
                lblMessage.Text = "Study deleted Successfully!!";
                divMessageArea.Visible = true;
                GridRefresh();
            }
            catch (Exception b)
            {
                lblMessage.Text = "Error while Deleting Study";
                divMessageArea.Visible = true;
            }

        }

        protected void Button1_OnClick(object sender, EventArgs e)
        {
            Button btnAssign = (Button) sender;
            BindDropdown();
            hdnStudyId.Value = btnAssign.CommandArgument.ToString();
            OpenSiteAssignment();

        }

        public void BindDropdown()
        {
            ddlSite.DataSource = new SiteManager().GetSites();
            ddlSite.DataTextField = "DropdownFormat";
            ddlSite.DataValueField = "SiteId";
            ddlSite.DataBind();

        }

        public void OpenSiteAssignment()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#assignSiteDialog').modal({ show: true });");
            sb.AppendLine("});");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopAssignSite", sb.ToString(), true);

        }

        private void GridRefresh()
        {
            GridView1.DataSource = new StudyManager().GetStudies();
            GridView1.DataBind();
        }

        protected void btnAssignSiteSave_OnClick(object sender, EventArgs e)
        {
            try
            {
                OpenSiteAssignment();
                StudysiteManager ssmgr = new StudysiteManager();
                var studysite = new Studysite();
                studysite.study_id = Convert.ToInt32(hdnStudyId.Value);
                var idsite = ddlSite.SelectedValue;
                studysite.site_id = int.Parse(ddlSite.SelectedValue);
                ssmgr.InsertStudysite(studysite);
                lblAssignMsg.Text = "Site assigned Successfully!!";
                divAssignMsg.Visible = true;

            }

            catch (Exception r)
            {
                lblAssignMsg.Text = "Error while Assigning Site";
                divAssignMsg.Visible = true;
            }


        }

        protected void ShowAssignSites_OnClick(object sender, EventArgs e)
        {
            Button btnShowAssignSites = (Button) sender;
            var assignStudyid = btnShowAssignSites.CommandArgument.ToString();
            var studysite = new StudysiteClasses.Studysite();
            int id = Int32.Parse(assignStudyid);
            GridViewShowSites.DataSource = new StudysiteManager().GetStudysites(id);
            gridshowsites.Visible = true;
            AssignSiteGridRefresh();
        }

        protected void GridViewShowSites_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAssignSite")
            {
                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(';');
                StudysiteManager ssd = new StudysiteManager();
                var da = new StudysiteClasses.Studysite();
                da.study_id = Convert.ToInt32(arg[0]);
                da.site_id = Convert.ToInt32(arg[1]);
                ssd.DeleteAssignedSite(da);
                GridViewShowSites.DataSource = new StudysiteManager().GetStudysites(da.study_id);
                AssignSiteGridRefresh();
            }

        }

        protected void AssignSiteGridRefresh()
        {
            GridViewShowSites.DataBind();

        }
    }
}