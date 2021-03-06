﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMG_Portal.API.Models.SystemParameters
{
    public class AboutAll
    { 
        public string AboutTitle { get; set; }
        public string AboutDesc { get; set; }
        public string AboutVideoUrl { get; set; }
        public string VisionTitle { get; set; }
        public string VisionDesc { get; set; }
        public string MissionTitle { get; set; }
        public string MissionDesc { get; set; }
        public List<CoreValuesModel> CoreValueses { get; set; }

    }
}