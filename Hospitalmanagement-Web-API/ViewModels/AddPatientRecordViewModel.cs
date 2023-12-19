using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitalmanagement_Web_API.ViewModels
{
    public class AddPatientRecordViewModel
    {
        [Required(ErrorMessage = " Name is mandatory")]
        public string PatientName { get; set; }
        [Required(ErrorMessage = " Age is mandatory")]
        public int PatientAge { get; set; }
        [Required(ErrorMessage = " Gender is mandatory")]
        public string PatientGender { get; set; }

        [Required(ErrorMessage = " Department is mandatory")]
        public int DepartmentId { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        [Required(ErrorMessage = " Doctor Name is mandatory")]
        public string DoctorName { get; set; }
        [Required(ErrorMessage = " Fee is mandatory")]
        public double DoctorFee { get; set; }
    }
}
