using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                //hdnAddMode.Value = "false";
                // GridView1.DataBind();
                // Make sure the GridView writes a 'thead' tag
                // This helps with the responsiveness
                // GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        //public void BindGridview()
        //{
        //    string connString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        //    DataSet dsPubs = new DataSet();
        //    using (var connection = new SqlConnection(connString))
        //    {
        //        SqlDataAdapter daAuthors
        //            = new SqlDataAdapter("Select * From Study", connection);

        //        //daAuthors.FillSchema(dsPubs, SchemaType.Source, "Authors");
        //        connection.Open();
        //        daAuthors.Fill(dsPubs);

        //        //DataTable tblAuthors;
        //        //tblAuthors = dsPubs.Tables["Authors"];
        //    }
        //    StudyManager sm = new StudyManager();
        //    GridView1.DataSource = sm.GetStudies();
        //    GridView1.DataBind();
        //    //GridView1.DataSource = dsPubs;
        //    //GridView1.DataBind();
        //}

        //protected void btnClick_OnClick(object sender, EventArgs e)
        //{
        //    BindGridview();
        //}

        protected void CreateStudy(object sender, EventArgs e)
        {
           var p=new StudyClasses.Study();
            p.Title = TextTitle.Text;
            p.PrincipalInvestigator = TextPI.Text;
            p.Availability = TextAvail.Checked;
            StudyManager sm = new StudyManager();
            bool rt = sm.Insert(p);
            if (rt == true)
            {
                //GridView1.DataSource = sm.GetStudies();
                GridView1.DataBind();
             //   lblInsertInfo.Text = "Success!!!!!!!!!!"+p.Title+" is saved.";
            } 
            
            //sm.Insert(txtTitle.Text, txtPI.Text, txtavail.Checked);
        }

        protected void DetailsView1_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
        {
            GridView1.DataBind();
        }

        protected void DetailsView1_ItemDeleted(object sender, DetailsViewDeletedEventArgs e)
        {
            GridView1.DataBind();
        }
    }
}