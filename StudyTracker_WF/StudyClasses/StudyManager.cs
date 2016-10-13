using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudyTracker_WF.StudyClasses
{
    public class StudyManager
    {
        public List<Study> GetStudies()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            string sql = "SELECT * FROM Study";
            da = new SqlDataAdapter(sql,
                                    ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

            da.Fill(dt);

            var query =
              (from dr in dt.AsEnumerable()
               select new Study
               {
                   Id = Convert.ToInt32(dr["Id"]),
                   Title = dr["Title"].ToString(),
                   PrincipalInvestigator                  = dr["PrincipalInvestigator"].ToString(),
                   Availability = bool.Parse(dr["Availability"].ToString()),
                   CreatedBy = dr["CreatedBy"].ToString(),
                   CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                   UpdatedBy = dr["UpdatedBy"].ToString(),
                   UpdatedDate = DateTime.Parse(dr["UpdatedDate"].ToString())
               });

            return query.ToList();
        }

        public Study GetStudy(int Id)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;

            string sql = "SELECT * FROM Study WHERE Id = @Id";
            cmd = new SqlCommand(sql,
              new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));

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

        public bool Insert(Study inStudy)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string sql = "Insert into Study(Title,PrincipalInvestigator,[Availability],[CreatedBy],[CreatedDate]," +
                         "[UpdatedBy],[UpdatedDate])" +
                         " Values('" + inStudy.Title + "','" + inStudy.PrincipalInvestigator + "','"
                         + inStudy.Availability + "', 'christy',getdate(),'christy',getdate())";


            SqlCommand objcmd = new SqlCommand(sql,
                                               objsqlconn);
            objcmd.ExecuteNonQuery();
            return true;
        }

        public bool UpdateStudy(Study inStudy)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string updateSql = "UPDATE [dbo].[Study] SET[Title] = '" + inStudy.Title + "'," +
                               "[PrincipalInvestigator] ='"+inStudy.PrincipalInvestigator+"'," +
                               "[Availability] = '"+inStudy.Availability+"',[CreatedBy] = '"+inStudy.CreatedBy+"'," +
                               "[CreatedDate] = getdate(),[UpdatedBy] = '"+inStudy.UpdatedBy+ "',[UpdatedDate] = getdate() " +
                               "WHERE Id = '"+ inStudy.Id + "'";

            SqlCommand objcmd = new SqlCommand(updateSql,objsqlconn);
            objcmd.ExecuteNonQuery();
            
            return true;
        }

        public bool DeleteStudy(int Id)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string deleteSql = "DELETE From Study WHERE Id = '" + Id + "'";
            SqlCommand objcmd = new SqlCommand(deleteSql, objsqlconn);
            objcmd.ExecuteNonQuery();
            return true;
        }

    }
}