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

        public Site GetSite()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;

            string storedProc = "GetSiteById";
            cmd = new SqlCommand(storedProc, new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;
            return null;
        }
    }
}