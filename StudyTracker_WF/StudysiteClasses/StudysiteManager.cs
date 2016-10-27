using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudyTracker_WF.StudysiteClasses
{
    public class StudysiteManager
    {
        #region - Get All Studysites

        public List<Studysite> GetAllStudysites()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;

            string storedprocGetAllstudysites = "GetAllStudysites";
            cmd = new SqlCommand(storedprocGetAllstudysites, new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;


            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            var query =
                (from dr in dt.AsEnumerable()
                 select new Studysite()
                 {
                     id = Convert.ToInt32(dr["id"]),
                     StudyTitle = dr["Title"].ToString(),
                     SiteName = dr["Name"].ToString()
                 });
            return query.ToList();
        }
#endregion

        #region - Get All the Sites assigned for a Study
        public List<Studysite> GetStudysites(int id)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;

            string storedprocGetstudysites = "GetStudysiteByStudyid";
            cmd = new SqlCommand(storedprocGetstudysites, new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", id));

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            var query =
                (from dr in dt.AsEnumerable()
                 select new Studysite()
                 {
                     //studyId = Convert.ToInt32(dr["Id"]),
                     study_id = Convert.ToInt32(dr["study_id"]),
                     site_id = Convert.ToInt32(dr["site_id"]),
                     StudyTitle = dr["Title"].ToString(),
                     SiteName = dr["Name"].ToString()
                 });
            return query.ToList();
        }
        #endregion

        #region - Get All the Studies assigned for a Site
        public List<Studysite> GetStudysitesForSite(int id)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;

            string storedprocGetstudysites = "GetStudysiteBySiteid";
            cmd = new SqlCommand(storedprocGetstudysites, new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SiteId", id));

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            var query =
                (from dr in dt.AsEnumerable()
                 select new Studysite()
                 {
                     //studyId = Convert.ToInt32(dr["Id"]),
                     study_id = Convert.ToInt32(dr["study_id"]),
                     site_id = Convert.ToInt32(dr["site_id"]),
                     StudyTitle = dr["Title"].ToString(),
                     SiteName = dr["Name"].ToString()
                 });
            return query.ToList();
        }
        #endregion

        #region - Insert a study site combination
        public bool InsertStudysite(Studysite inStudysite)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcInsert = "InsertStudysite";
            SqlCommand objcmd = new SqlCommand(storedProcInsert, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            objcmd.Parameters.Add(new SqlParameter("@study_id", inStudysite.study_id));
            objcmd.Parameters.Add(new SqlParameter("@site_id", inStudysite.site_id));

            objcmd.ExecuteNonQuery();

            return true;
        }
        #endregion

        #region - Delete a study site combination
        public void DeleteAssignedSite(Studysite inStudysite)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcInsert = "DeleteAssignedSite";
            SqlCommand objcmd = new SqlCommand(storedProcInsert, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            objcmd.Parameters.Add(new SqlParameter("@study_id", inStudysite.study_id));
            objcmd.Parameters.Add(new SqlParameter("@site_id", inStudysite.site_id));

            objcmd.ExecuteNonQuery();
        }
#endregion

    }
}