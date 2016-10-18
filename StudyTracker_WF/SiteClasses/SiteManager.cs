using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Web;

namespace StudyTracker_WF.SiteClasses
{
    public class SiteManager
    {
        public List<Site> GetSites()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            string storedprocGetsites = "GetSites";
            da = new SqlDataAdapter(storedprocGetsites,
                ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);

            da.Fill(dt);

            var sites =
                (from dr in dt.AsEnumerable()
                 select new Site
                 {
                     SiteId = Convert.ToInt32(dr["SiteId"]),
                     Name = dr["Name"].ToString(),
                     Location = dr["Location"].ToString(),
                     CreatedBy = dr["CreatedBy"].ToString(),
                     CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                     UpdatedBy = dr["UpdatedBy"].ToString(),
                     UpdatedDate = DateTime.Parse(dr["UpdatedDate"].ToString())

                 });
            return sites.ToList();
        }

        public Site GetSite(int Id)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;

            string storedProc = "GetSiteById";
            cmd = new SqlCommand(storedProc, new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SiteId", Id));

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            Site rec =
              (from dr in dt.AsEnumerable()
               select new SiteClasses.Site
               {
                   SiteId = Convert.ToInt32(dr["SiteId"]),
                   Name = dr["Name"].ToString(),
                   Location = dr["Location"].ToString(),
                   CreatedBy = dr["CreatedBy"].ToString(),
                   CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                   UpdatedBy = dr["UpdatedBy"].ToString(),
                   UpdatedDate = DateTime.Parse(dr["UpdatedDate"].ToString())
               }).FirstOrDefault();

            return rec;
        }

        public bool InsertSite(Site inSite)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcInsert = "InsertSite";
            SqlCommand objcmd = new SqlCommand(storedProcInsert, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            objcmd.Parameters.Add(new SqlParameter("@Name", inSite.Name));
            objcmd.Parameters.Add(new SqlParameter("@Location", inSite.Location));
            objcmd.Parameters.Add(new SqlParameter("@CreatedBy", "Christy"));
            objcmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedBy", "Christy"));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));

            objcmd.ExecuteNonQuery();

            return true;
        }

        public bool UpdateSite(Site inSite)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcUpdate = "UpdateSite";
            SqlCommand objcmd = new SqlCommand(storedProcUpdate, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            objcmd.Parameters.Add(new SqlParameter("@SiteId", inSite.SiteId));
            objcmd.Parameters.Add(new SqlParameter("@Name", inSite.Name));
            objcmd.Parameters.Add(new SqlParameter("@Location", inSite.Location));
            objcmd.Parameters.Add(new SqlParameter("@CreatedBy", "Christy"));
            objcmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedBy", "Christy"));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));

            objcmd.ExecuteNonQuery();

            return true;
        }
    }
}