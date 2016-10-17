using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyTracker_WF.SiteClasses
{
    public class Site
    {
        public int SiteId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool Availability { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}