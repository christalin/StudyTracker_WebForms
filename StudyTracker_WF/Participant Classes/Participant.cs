using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyTracker_WF.Participant_Classes
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        public string ParticipantName { get; set; }
        public string Gender { get; set; }
        //public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}