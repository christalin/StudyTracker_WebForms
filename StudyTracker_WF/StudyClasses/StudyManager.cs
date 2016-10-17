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
            //string sql = "SELECT * FROM Study";
            string storedprocGetstudies = "GetStudies";
            da = new SqlDataAdapter(storedprocGetstudies,
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
        //    string sqls = "SELECT * FROM Study WHERE Id = "+Id;
            //string sql = "SELECT * FROM Study WHERE Id = @Id";
            string storedProc = "GetStudyById";
            cmd = new SqlCommand(storedProc,
              new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;
            //var param = new SqlParameter();
            //param.DbType=DbType.Int32;
            //param.Value = Id;
            //param.ParameterName = "@Id";
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
            //string sql = "Insert into Study(Title,PrincipalInvestigator,[Availability],[CreatedBy],[CreatedDate]," +
            //             "[UpdatedBy],[UpdatedDate])" +
            //             " Values(@Title, @PrincipalInvestigator, @Availability, @CreatedBy, getdate(), @UpdatedBy, getdate())";
            string storedProcInsert = "InsertStudy";

            SqlCommand objcmd = new SqlCommand(storedProcInsert,
                                               objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            objcmd.Parameters.Add(new SqlParameter("@Title", inStudy.Title));
            objcmd.Parameters.Add(new SqlParameter("@PrincipalInvestigator", inStudy.PrincipalInvestigator));
            objcmd.Parameters.Add(new SqlParameter("@Availability", inStudy.Availability));
            objcmd.Parameters.Add(new SqlParameter("@CreatedBy", "Christy"));
            objcmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedBy", "Christy"));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));

            objcmd.ExecuteNonQuery();
            return true;
        }

        public bool UpdateStudy(Study inStudy)
        {
            
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            //string updateSql =
            //    "UPDATE[Study] SET[Title] = @Title, [PrincipalInvestigator] = @PrincipalInvestigator," +
            //    "[Availability]= @Availability, [CreatedBy] = @CreatedBy, [CreatedDate] = getdate(), " +
            //    "[UpdatedBy] = @UpdatedBy, [UpdatedDate] = getDate() WHERE Id = @Id";
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

        public void DeleteStudy(Study inStudy)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            //string deleteSql = "DELETE From Study WHERE Id = @Id";
            string storedProcDelete = "DeleteStudy";
            SqlCommand objcmd = new SqlCommand(storedProcDelete, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.Parameters.Add(new SqlParameter("@Id", inStudy.Id));
            objcmd.ExecuteNonQuery();
            
        }

    }
}