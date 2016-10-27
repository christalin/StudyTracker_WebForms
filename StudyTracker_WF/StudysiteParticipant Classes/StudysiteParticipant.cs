using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyTracker_WF.StudysiteParticipant_Classes
{
    public class StudysiteParticipant
    {
        public int id { get; set; }
        public int studysite_id { get; set; }
        public int participant_id { get; set; }
        public string study_name { get; set; }
        public string site_name { get; set; }
        public string participant_name { get; set; }
        public string study_PI { get; set; }
        public string site_location { get; set; }
    }
}