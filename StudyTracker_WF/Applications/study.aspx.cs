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
using Microsoft.AspNet.Identity;
using StudyTracker_WF.SiteClasses;
using StudyTracker_WF.StudyClasses;
using StudyTracker_WF.StudysiteClasses;

namespace StudyTracker_WF.Study
{
    public partial class study : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var username = HttpContext.Current.User.Identity.GetUserName();
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
                study.CreatedBy = HttpContext.Current.User.Identity.GetUserName();
                study.UpdatedBy = HttpContext.Current.User.Identity.GetUserName();

                if (Convert.ToBoolean(hdnAddMode.Value))
                {
                    lblMessage.Text = "creating";
                    btnsave.Text = "Create Study";
                    sm.Insert(study);
                    lblMessage.Text = "Study has been created!!!!";
                    divMessageArea.Visible = true;
                    GridRefresh();
                }
                else
                {
                    lblMessage.Text = "updating";
                    study.Id = Convert.ToInt32(hdnPK.Value);
                    sm.UpdateStudy(study);
                    lblMessage.Text = "Study has been updated!!!!";
                    divMessageArea.Visible = true;
                    GridRefresh();

                }
                btnsave.Visible = false;

            }
            catch (SqlException a)
            {
                if (a.Message.Contains("UNIQUE KEY constraint"))
                {
                    lblMessage.Text = "Study already Exists!!";
                    btnsave.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Error while " + lblMessage.Text + " Study!!";
                }
                divMessageArea.Visible = true;
            }
            catch (Exception a)
            {
                lblMessage.Text = "Error while " + lblMessage.Text + " Study!!";
                divMessageArea.Visible = true;
            }

        }
        public void KeepModalOpenScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("$(document).ready(function() {");
            sb.AppendLine("$('#studyDialog').modal({ show: true });");
            sb.AppendLine("$('#btnDelete').hide()");
            //sb.AppendLine("$('#btnsave').hide()");
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
                lblMessage.Text = "Study has been deleted!!";
                divMessageArea.Visible = true;
                GridRefresh();
            }
            catch (SqlException b)
            {
                if (b.Message.Contains("REFERENCE constraint"))
                {
                    lblMessage.Text = "Study has assigned Sites!!";
                    btnsave.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Error while deleting Study!!";
                }
                divMessageArea.Visible = true;
            }
            catch (Exception b)
            {
                lblMessage.Text = "Error while Deleting Study";
                divMessageArea.Visible = true;
            }
            btnsave.Visible = false;
        }

        protected void Button1_OnClick(object sender, EventArgs e)
        {
            Button btnAssign = (Button) sender;
            string[] arg = new string[2];
            arg = btnAssign.CommandArgument.Split(';');
            hdnStudyId.Value = arg[0];
            bool status = Convert.ToBoolean(arg[1]);
            if (status)
            {
                BindDropdown();
                //hdnStudyId.Value = btnAssign.CommandArgument.ToString();
                divAssignMsg.Visible = false;
                AssignSave.Visible = true;
                OpenSiteAssignment();
                //noAssign.Visible = false;
            }
            else
            {
                AlertErrorMessage();
            }
            

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

        public void AlertErrorMessage()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("BootstrapDialog.show({");
            sb.AppendLine("type: BootstrapDialog.TYPE_INFO,");
            sb.AppendLine("title: 'Alert ',");
            sb.AppendLine("message: 'Study is not Active!',");
            sb.AppendLine("});");


            Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertError", sb.ToString(), true);
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
                AssignSave.Visible = false;
                GridViewShowSites.DataSource = new StudysiteManager().GetStudysites(studysite.study_id);
                AssignSiteGridRefresh();

            }
            catch (SqlException r)
            {
                if (r.Message.Contains("UNIQUE KEY constraint"))
                {
                    lblAssignMsg.Text = "Site has already been assigned!!";
                    AssignSave.Visible = true;
                }
                else
                {
                    lblAssignMsg.Text = "Error while Assigning Site";
                }
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

        protected void GridViewShowDelete_OnRowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void GridView1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = new StudyManager().GetStudies();
            GridView1.DataBind();

            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        protected void GridView1_OnSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = GridView1.DataSource as DataTable;

            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                GridView1.DataSource = dataView;
                GridView1.DataBind();
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