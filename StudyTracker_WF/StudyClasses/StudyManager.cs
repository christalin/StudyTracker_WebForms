using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using StudyTracker_WF.SiteClasses;

namespace StudyTracker_WF.StudyClasses
{
    public class StudyManager
    {
        #region Get All Studies
        public List<Study> GetStudies()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;
            //string sql = "SELECT * FROM Study";
            string storedprocGetstudies = "GetStudies";
            cmd = new SqlCommand(storedprocGetstudies,
                                    new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            //cmd.Parameters.Add(new SqlParameter("@site_id", site_id));


            var query =
              (from dr in dt.AsEnumerable()
               select new Study
               {
                   Id = Convert.ToInt32(dr["Id"]),
                   Title = dr["Title"].ToString(),
                   PrincipalInvestigator = dr["PrincipalInvestigator"].ToString(),
                   Availability = bool.Parse(dr["Availability"].ToString())
               });

            return query.ToList();
        }
        #endregion

        #region Get All Active Studies
        public List<Study> GetActiveStudies()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;
            //string sql = "SELECT * FROM Study";
            string storedprocGetstudies = "GetActiveStudies";
            cmd = new SqlCommand(storedprocGetstudies,
                                    new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            

            var query =
              (from dr in dt.AsEnumerable()
               select new Study
               {
                   Id = Convert.ToInt32(dr["Id"]),
                   Title = dr["Title"].ToString(),
                   PrincipalInvestigator = dr["PrincipalInvestigator"].ToString(),
                   Availability = bool.Parse(dr["Availability"].ToString())
               });

            return query.ToList();
        }
        #endregion

        #region Get Study By Id
        public Study GetStudy(int Id)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;
            string storedProc = "GetStudyById";
            cmd = new SqlCommand(storedProc,
              new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", Id));

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            Study record =
              (from dr in dt.AsEnumerable()
               select new Study
               {
                   Id = Convert.ToInt32(dr["Id"]),
                   Title = dr["Title"].ToString(),
                   PrincipalInvestigator = dr["PrincipalInvestigator"].ToString(),
                   Availability = bool.Parse(dr["Availability"].ToString()),
                   CreatedBy = dr["CreatedBy"].ToString(),
                   CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                   UpdatedBy = dr["UpdatedBy"].ToString(),
                   UpdatedDate = DateTime.Parse(dr["UpdatedDate"].ToString())
               }).FirstOrDefault();

            return record;
        }
#endregion

        #region Insert Study
        public bool Insert(Study inStudy)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcInsert = "InsertStudy";
            SqlCommand objcmd = new SqlCommand(storedProcInsert,
                                               objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            objcmd.Parameters.Add(new SqlParameter("@Title", inStudy.Title));
            objcmd.Parameters.Add(new SqlParameter("@PrincipalInvestigator", inStudy.PrincipalInvestigator));
            objcmd.Parameters.Add(new SqlParameter("@Availability", inStudy.Availability));
            objcmd.Parameters.Add(new SqlParameter("@CreatedBy", inStudy.CreatedBy));
            objcmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedBy", inStudy.UpdatedBy));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));

            objcmd.ExecuteNonQuery();
            return true;
        }
#endregion

        #region Update Study
        public bool UpdateStudy(Study inStudy)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcUpdate = "UpdateStudy";
            SqlCommand objcmd = new SqlCommand(storedProcUpdate, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            objcmd.Parameters.Add(new SqlParameter("@Id", inStudy.Id));
            objcmd.Parameters.Add(new SqlParameter("@Title", inStudy.Title));
            objcmd.Parameters.Add(new SqlParameter("@PrincipalInvestigator", inStudy.PrincipalInvestigator));
            objcmd.Parameters.Add(new SqlParameter("@Availability", inStudy.Availability));
            objcmd.Parameters.Add(new SqlParameter("@CreatedBy", inStudy.CreatedBy));
            objcmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedBy", inStudy.UpdatedBy));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));

            objcmd.ExecuteNonQuery();

            return true;
        }
#endregion

        #region Delete Study
        public void DeleteStudy(Study inStudy)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcDelete = "DeleteStudy";
            SqlCommand objcmd = new SqlCommand(storedProcDelete, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.Parameters.Add(new SqlParameter("@Id", inStudy.Id));
            objcmd.ExecuteNonQuery();

        }
#endregion
    }
}