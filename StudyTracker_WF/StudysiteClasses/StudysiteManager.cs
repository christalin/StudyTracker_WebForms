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
        public List<Studysite> GetStudysites()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;

            string storedprocGetstudysites = "GetStudysites";
            cmd = new SqlCommand(storedprocGetstudysites, new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            var query =
                (from dr in dt.AsEnumerable()
                 select new Studysite()
                 {
                     StudyTitle = dr["StudyTitle"].ToString(),
                     SiteName = dr["SiteName"].ToString()
                 });
            return query.ToList();
        }

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
    }
}