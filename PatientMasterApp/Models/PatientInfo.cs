using System;
using System.ComponentModel.DataAnnotations;
namespace PatientMasterApp.Model
{
    public class PatientInfo
    {
        [Key]
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int age { get; set; }
        public string Gender { get; set; }
        public Boolean vaccinestatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
