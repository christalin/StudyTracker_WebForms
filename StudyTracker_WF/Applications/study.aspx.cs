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
using StudyTracker_WF.StudyClasses;

namespace StudyTracker_WF.Study
{
    public partial class study : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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

        public void LoadForEdit(string pk)
        {
            StudyManager smr = new StudyManager();
            //StudyClasses.Study e = new StudyClasses.Study();
            var e = smr.GetStudy(Convert.ToInt32(pk));
            TextTitle.Text = e.Title;
            TextPI.Text = e.PrincipalInvestigator;
            TextAvail.Checked = e.Availability;
            hdnPK.Value = e.Id.ToString();
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
            StudyManager sm = new StudyManager();
            var study = new StudyClasses.Study();
            study.Title = TextTitle.Text;
            study.PrincipalInvestigator = TextPI.Text;
            study.Availability = TextAvail.Checked;
            study.CreatedBy = "Christy";
            study.UpdatedBy = "Christy";

            if (Convert.ToBoolean(hdnAddMode.Value))
            {
                sm.Insert(study);
                GridRefresh();
            }
            else
            {
                study.Id = Convert.ToInt32(hdnPK.Value);
                sm.UpdateStudy(study);
                GridRefresh();


            }
        }

       private void GridRefresh()
        {
            GridView1.DataBind();
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            StudyManager sd = new StudyManager();
            var d = new StudyClasses.Study();
            d.Id = Convert.ToInt32(hdnPK.Value);
            sd.DeleteStudy(d);
            GridRefresh();
        }
    }
}