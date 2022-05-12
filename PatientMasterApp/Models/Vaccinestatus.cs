using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientMasterApp.Model
{
    public class Vaccinestatus
    {
    [Key]
        public int VaccineStatusId { get; set; }

        [Display(Name = "PatientId")]
        public virtual int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual PatientInfo PatientInfo { get; set; }     
        public string Dose { get; set; }         
        public DateTime createdby { get; set; }
        public DateTime updatedby { get; set; }

    }
}
