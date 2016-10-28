using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudyTracker_WF.StudysiteParticipant_Classes
{
    public class StudysiteparticipantManager
    {
        #region-Get all enrollments for a participant

        public List<StudysiteParticipant> GetEnrollments(int id)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;

            string storedproc = "GetEnrollments";
            cmd = new SqlCommand(storedproc, new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ParticipantId", id));

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            var query =
                (from dr in dt.AsEnumerable()
                    select new StudysiteParticipant()
                    {
                       id = Convert.ToInt32(dr["id"]),
                       study_name = dr["Title"].ToString(),
                       site_name = dr["Name"].ToString(),
                       study_PI = dr["PrincipalInvestigator"].ToString(),
                       site_location = dr["Location"].ToString(),
                       participant_name = dr["ParticipantName"].ToString(),
                       participant_id = Convert.ToInt32(dr["participant_id"])

                    });

            return query.ToList();
        }
#endregion

        #region - Insert a enrollment in the table
        public bool InsertEnrolledparticipant(StudysiteParticipant inEnrolledparticipant)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcInsert = "InsertEnrollment";
            SqlCommand objcmd = new SqlCommand(storedProcInsert, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            objcmd.Parameters.Add(new SqlParameter("@studysite_id", inEnrolledparticipant.studysite_id));
            objcmd.Parameters.Add(new SqlParameter("@participant_id", inEnrolledparticipant.participant_id));

            objcmd.ExecuteNonQuery();

            return true;
        }
        #endregion

        #region-Delete Enrollment

        public void DeleteEnrollment(StudysiteParticipant inStudysiteParticipant)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcInsert = "DeleteEnrollment";
            SqlCommand objcmd = new SqlCommand(storedProcInsert, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            //objcmd.Parameters.Add(new SqlParameter("@studysite_id", inStudysiteParticipant.studysite_id));
            objcmd.Parameters.Add(new SqlParameter("@id", inStudysiteParticipant.id));

            objcmd.ExecuteNonQuery();
        }
#endregion
    }
}