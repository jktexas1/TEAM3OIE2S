using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEAM3OIE2S.Models
{
    public class AnalysisModel
    {
        [Required]
        [DisplayName("Patient Institutional ID")]
        public int originalID { get; set; }

        
        [DisplayName("Study / Series")]
        public string studyAndSeries { get; set; }


        
        [DisplayName("ROI Begin")]
        public int ROI_begin { get; set; }

        
        [DisplayName("Iliac bifurcation")]
        public int iliacBifurcation { get; set; }

        [DisplayName("Comment: ")]
        public string comment { get; set; }
        public IEnumerable<SelectListItem> OriginalIDs { get; set; }
        public IEnumerable<SelectListItem> StudySeries { get; set; }

    }
}