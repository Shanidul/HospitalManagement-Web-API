using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitalmanagement_Web_API.Model
{
    public class Hospital
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientGender { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]

        public Department Department { get; set; }
        public string DoctorName { get; set; }
        public double DoctorFee { get; set; }
        public DateTime RegisterDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
