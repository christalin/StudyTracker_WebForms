using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyTracker_WF.StudysiteClasses
{
    public class Studysite
    {
        public int id { get; set; }
        public int study_id { get; set; }
        public int site_id { get; set; }
        public string StudyTitle { get; set; }
        public string SiteName { get; set; }
    }
}