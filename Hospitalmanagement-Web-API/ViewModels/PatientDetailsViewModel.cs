using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitalmanagement_Web_API.ViewModels
{
    public class PatientDetailsViewModel
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientGender { get; set; }
        public string Department { get; set; }
        public string DoctorName { get; set; }
        public double DoctorFee { get; set; }
        public DateTime RegisterDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
