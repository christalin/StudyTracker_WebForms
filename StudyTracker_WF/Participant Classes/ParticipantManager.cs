using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudyTracker_WF.Participant_Classes
{
    public class ParticipantManager
    {
        #region Get All Participants
        public List<Participant> GetParticipants()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;
            string storedprocGetparts = "GetParticipants";
            cmd = new SqlCommand(storedprocGetparts, new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            var query = (from dr in dt.AsEnumerable()
                select new Participant
                {
                    ParticipantId = Convert.ToInt32(dr["ParticipantId"]),
                    ParticipantName = dr["ParticipantName"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    //Dob = Convert.ToDateTime(dr["Dob"]),
                    Address = dr["Address"].ToString()

                });

            return query.ToList();
        }
        #endregion

        #region - Get participant By Id

        public Participant GetParticipant(int id)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = null;
            SqlCommand cmd = null;
            string storedProc = "GetParticipantById";
            cmd = new SqlCommand(storedProc, new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ParticipantId", id));

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            Participant record =
                (from dr in dt.AsEnumerable()
                 select new Participant
                 {
                     ParticipantId = Convert.ToInt32(dr["ParticipantId"]),
                     ParticipantName = dr["ParticipantName"].ToString(),
                     Gender = dr["Gender"].ToString(),
                     //Dob = DateTime.Parse(dr["Dob"].ToString()),
                     Address = dr["Address"].ToString(),
                     CreatedBy = dr["CreatedBy"].ToString(),
                     CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                     UpdatedBy = dr["UpdatedBy"].ToString(),
                     UpdatedDate = DateTime.Parse(dr["UpdatedDate"].ToString())
                 }).FirstOrDefault();

            return record;
        }
        #endregion

        #region Insert Participant

        public bool InsertParticipant(Participant inParticipant)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcInsert = "InsertParticipant";
            SqlCommand objcmd = new SqlCommand(storedProcInsert, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            objcmd.Parameters.Add(new SqlParameter("@ParticipantName", inParticipant.ParticipantName));
            objcmd.Parameters.Add(new SqlParameter("@Gender", inParticipant.Gender));
            //objcmd.Parameters.Add(new SqlParameter("@Dob", inParticipant.Dob));
            objcmd.Parameters.Add(new SqlParameter("@Address", inParticipant.Address));
            objcmd.Parameters.Add(new SqlParameter("@CreatedBy", "Christy"));
            objcmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedBy", "Christy"));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));

            objcmd.ExecuteNonQuery();
            return true;
        }
        #endregion

        #region Update Participant

        public bool UpdateParticipant(Participant inParticipant)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcUpdate = "UpdateParticipant";
            SqlCommand objcmd = new SqlCommand(storedProcUpdate, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;

            objcmd.Parameters.Add(new SqlParameter("@ParticipantId", inParticipant.ParticipantId));
            objcmd.Parameters.Add(new SqlParameter("@ParticipantName", inParticipant.ParticipantName));
            objcmd.Parameters.Add(new SqlParameter("@Gender", inParticipant.Gender));
            //objcmd.Parameters.Add(new SqlParameter("@Dob", inParticipant.Dob));
            objcmd.Parameters.Add(new SqlParameter("@Address", inParticipant.Address));
            objcmd.Parameters.Add(new SqlParameter("@CreatedBy", "Christy"));
            objcmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedBy", "Christy"));
            objcmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now));

            objcmd.ExecuteNonQuery();
            return true;
        }
        #endregion

        #region Delete Participant

        public void DeleteParticipant(Participant inParticipant)
        {
            string conn = "";
            conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            SqlConnection objsqlconn = new SqlConnection(conn);
            objsqlconn.Open();
            string storedProcDelete = "DeleteParticipant";
            SqlCommand objcmd = new SqlCommand(storedProcDelete, objsqlconn);
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.Parameters.Add(new SqlParameter("@ParticipantId", inParticipant.ParticipantId));
            objcmd.ExecuteNonQuery();
        }
#endregion
    }
}