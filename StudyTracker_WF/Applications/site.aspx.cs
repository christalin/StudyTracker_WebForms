using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyTracker_WF.SiteClasses;

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
           SiteManager ss = new SiteManager();
           var site = new SiteClasses.Site();
            site.Name = TextName.Text;
            site.Location = TextLocation.Text;
            site.CreatedBy = "Christy";
            site.UpdatedBy = "Christy";

            if (Convert.ToBoolean(shdnAddMode.Value))
            {
                ss.InsertSite(site);
                GridView1.DataBind();
            }
            else
            {
                site.SiteId = Convert.ToInt32(shdnPK.Value);
                ss.UpdateSite(site);
                GridView1.DataBind();

            }
        }

        protected void sbtnDelete_OnClickbtnDelete_OnClick(object sender, EventArgs e)
        {
            SiteManager sd = new SiteManager();
            var de = new SiteClasses.Site();
            de.SiteId = Convert.ToInt32(shdnPK.Value);
            sd.DeleteSite(de);
            GridView1.DataBind();
        }
    }
}