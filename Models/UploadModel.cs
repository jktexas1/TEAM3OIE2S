using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEAM3OIE2S.Models
{
    public class UploadModel
    {
        [Required]
        [DisplayName("Patient Institutional ID")]
        public int originalID { get; set; }
        [Required]
        [DisplayName("Date of surgery")]
        public string dateOfSurgery { get; set; }
        [Required]
        [DisplayName("Endograph brand")]
        public string endographBrand { get; set; }
        [Required]
        public int endographDiameter { get; set; }
        [Required]
        public int endographLength { get; set; }
        [Required]
        public int unilateralDiameter { get; set; }
        [Required]
        public int unlateralLength { get; set; }
        [Required]
        public int contralateralDiameter { get; set; }
        [Required]
        public int contralateralLength { get; set; }

        public IEnumerable<SelectListItem> OriginalIDs { get; set; }
        public IEnumerable<SelectListItem> EndographBrands { get; set; }

    }
}