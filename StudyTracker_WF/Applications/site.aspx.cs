﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyTracker_WF.SiteClasses;
using StudyTracker_WF.StudyClasses;
using StudyTracker_WF.StudysiteClasses;

namespace StudyTracker_WF.Applications
{
    public partial class site : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    var sk = Convert.ToString(Request.QueryString["id"]);
                    lblName.InnerText = "Update Site";
                    sbtnSave.Text = "Update Site";
                    shdnPK.Value = sk;
                    shdnAddMode.Value = "false";

                    LoadForSiteEdit(sk);
                    LoadEditSiteScript();

                }
            }
        }

        public void LoadForSiteEdit(string sk)
        {
            SiteManager skr = new SiteManager();
            var s = skr.GetSite(Convert.ToInt32(sk));
            TextName.Text = s.Name;
            TextLocation.Text = s.Location;
            shdnPK.Value = s.SiteId.ToString();
            shdnAddMode.Value = "false";
        }

        public void LoadEditSiteScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#siteDialog').modal();");
            sb.AppendLine("});");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "EditData", sb.ToString(), true);

        }

        protected void sbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                KeepModalOpenScript();
                SiteManager ss = new SiteManager();
                var site = new SiteClasses.Site();
                site.Name = TextName.Text;
                site.Location = TextLocation.Text;
                site.CreatedBy = "Christy";
                site.UpdatedBy = "Christy";
                


                if (Convert.ToBoolean(shdnAddMode.Value))
                {
                    lblsMessage.Text = "Inserting";
                    sbtnSave.Text = "Create Site";
                    ss.InsertSite(site);
                    lblsMessage.Text = "Site Inserted Successfully!";
                    divMessageArea.Visible = true;
                    GridView1.DataBind();
                }
                else
                {
                    lblsMessage.Text = "Updating";
                    site.SiteId = Convert.ToInt32(shdnPK.Value);
                    ss.UpdateSite(site);
                    lblsMessage.Text = "Site Updated Successfully!";
                    divMessageArea.Visible = true;
                    GridView1.DataBind();

                }
            }

            catch (Exception a)
            {
                lblsMessage.Text = "Error while " + lblsMessage.Text + " Site";
                divMessageArea.Visible = true;
            }
        }

        public void KeepModalOpenScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#siteDialog').modal({ show: true });");
            sb.AppendLine("$('#sbtnDelete').hide()");
            sb.AppendLine("$('#sbtnSave').hide()");
            sb.AppendLine("$('#shdnAddMode').val('true')");
            sb.AppendLine("});");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Pop", sb.ToString(), true);

        }

        protected void sbtnDelete_OnClickbtnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                KeepModalOpenScript();
                SiteManager sd = new SiteManager();
                var de = new SiteClasses.Site();
                de.SiteId = Convert.ToInt32(shdnPK.Value);
                sd.DeleteSite(de);
                lblsMessage.Text = "Site Deleted Successfully!!";
                divMessageArea.Visible = true;
                GridView1.DataBind();
            }
            catch (Exception d)
            {
                lblsMessage.Text = "Error while Deleting Site";
                divMessageArea.Visible = true;
            }

        }

       protected void sbtnAssign_OnClick(object sender, EventArgs e)
       {
           Button btn = (Button) sender;
           BindDropdown();
           hdnSiteId.Value = btn.CommandArgument.ToString();
           OpenStudyAssignment();
       }

        private void BindDropdown()
        {
            ddlStudy.DataSource = new StudyManager().GetStudies();
            ddlStudy.DataTextField = "StudyDropdownFormat";
            ddlStudy.DataValueField = "Id";
            ddlStudy.DataBind();

        }

       protected void btnAssignStudySave_OnClick(object sender, EventArgs e)
        {
           try
           {
               OpenStudyAssignment();
               StudysiteManager ssmgr = new StudysiteManager();
               var studysite = new Studysite();
               studysite.site_id = Convert.ToInt32(hdnSiteId.Value);
               studysite.study_id = int.Parse(ddlStudy.SelectedValue);
               ssmgr.InsertStudysite(studysite);
               slblAssignMsg.Text = "Study assigned Successfully!!";
               sdivAssignMsg.Visible = true;
               GridViewShowStudies.DataSource = new StudysiteManager().GetStudysitesForSite(studysite.site_id);
               AssignStudyGridRefresh();
            }
           catch (SqlException f)
           {
               if (f.Message.Contains("UNIQUE KEY constraint"))
               {
                   slblAssignMsg.Text = "Study has already been assigned!!";
               }
               else
               {
                   slblAssignMsg.Text = "Error while Assigning Study";
               }
               sdivAssignMsg.Visible = true;
           }
           catch (Exception f)
           {
               slblAssignMsg.Text = "Error while Assigning Study";
               sdivAssignMsg.Visible = true;
           }
           //finally
           //{

           //}

        }

        public void OpenStudyAssignment()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#assignStudyDialog').modal({ show: true });");
            sb.AppendLine("});");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopAssignStudy", sb.ToString(), true);
        }


        protected void ShowAssignStudies_OnClick(object sender, EventArgs e)
        {
            Button btnShowAssignStudies = (Button) sender;
            var assignSiteid = btnShowAssignStudies.CommandArgument.ToString();
            var studysite = new StudysiteClasses.Studysite();
            int id = Int32.Parse(assignSiteid);
            GridViewShowStudies.DataSource = new StudysiteManager().GetStudysitesForSite(id);
            gridshowstudies.Visible = true;
            AssignStudyGridRefresh();

        }

       protected void GridViewDeleteAssignedStudies_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
           if (e.CommandName == "DeleteAssignedStudy")
           {
               string[] arg = new string[2];
               arg = e.CommandArgument.ToString().Split(';');
               StudysiteManager ssd = new StudysiteManager();
               var studySite = new StudysiteClasses.Studysite();
               studySite.study_id = Convert.ToInt32(arg[0]);
               studySite.site_id = Convert.ToInt32(arg[1]);
               ssd.DeleteAssignedSite(studySite);
               GridViewShowStudies.DataSource = new StudysiteManager().GetStudysitesForSite(studySite.site_id);
               AssignStudyGridRefresh();
           }
        }

        protected void AssignStudyGridRefresh()
        {
            GridViewShowStudies.DataBind();

        }
         
    }
}