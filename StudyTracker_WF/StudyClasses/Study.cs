﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyTracker_WF.StudyClasses
{
    public class Study
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PrincipalInvestigator { get; set; }
        public bool Availability { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}