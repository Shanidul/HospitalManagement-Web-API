using Hospitalmanagement_Web_API.Model;
using Hospitalmanagement_Web_API.Repository;
using Hospitalmanagement_Web_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitalmanagement_Web_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        IHospitalRepository _hospitalRepository;
        IDepartmentRepository _departmentRepository;
        public HospitalController(IHospitalRepository hospitalRepository, IDepartmentRepository departmentRepository)
        {
            _hospitalRepository = hospitalRepository;
            _departmentRepository = departmentRepository;

        }
        [Route("AddPatient")]
        [HttpPost]
        public IActionResult AddPatientRecords([FromBody] AddPatientRecordViewModel addPatientRecordViewModel)
        {
            var existingPatientRecords = _hospitalRepository.GetAllPatientRecords(User.Identity.Name);
            var patientRecord = existingPatientRecords?.FirstOrDefault(i => i.PatientName.ToLower() == addPatientRecordViewModel.PatientName.ToLower() && i.DoctorName.ToLower() == addPatientRecordViewModel.DoctorName.ToLower());
            if (patientRecord != null)
            {
                return Conflict("Patient record already exists");
            }
            var hospital = new Hospital
            {
                PatientName = addPatientRecordViewModel.PatientName,
                PatientAge = addPatientRecordViewModel.PatientAge,
                PatientGender = addPatientRecordViewModel.PatientGender,
                DepartmentId = addPatientRecordViewModel.DepartmentId,
                DoctorName = addPatientRecordViewModel.DoctorName,
                DoctorFee = addPatientRecordViewModel.DoctorFee,
                CreatedBy=User.Identity.Name,
                RegisterDate = DateTime.Now
            };
            var addedPatientRecords = _hospitalRepository.AddPatientRecords(hospital);
            return Ok($"Patient details with name {hospital.PatientName} is added successfully.");
        }
        [HttpGet]
        public IActionResult GetPatientRecords()
        {
            var patients = _hospitalRepository.GetAllPatientRecords(User.Identity.Name);
            List<PatientDetailsViewModel> patientDetailsListViewModel = new List<PatientDetailsViewModel>();
            foreach (var patient in patients)
            {
                PatientDetailsViewModel patientDetailsViewModel = new PatientDetailsViewModel
                {
                    Id = patient.Id,
                    PatientName = patient.PatientName,
                    PatientAge = patient.PatientAge,
                    PatientGender = patient.PatientGender,
                    Department = patient.Department.DepartmentName,
                    DoctorName = patient.DoctorName,
                    DoctorFee = patient.DoctorFee,
                    CreatedBy = User.Identity.Name,
                    RegisterDate = DateTime.Now
                    
                };
                patientDetailsListViewModel.Add(patientDetailsViewModel);
            }
            if (patientDetailsListViewModel.Count == 0)
            {
                return NotFound("No Records Found");
            }
            return Ok(patientDetailsListViewModel);
        }
        [HttpGet("{id}")]
        public IActionResult GetPatientById(int id)
        {
            var allPatientById = _hospitalRepository.GetFullPatientDetailsById(id, User.Identity.Name);

            List<PatientDetailsViewModel> patientDetailsListViewModel = new List<PatientDetailsViewModel>();

            foreach (var patient in allPatientById)
            {
                PatientDetailsViewModel patientDetailsViewModel = new PatientDetailsViewModel
                {
                    Id = patient.Id,
                    PatientName = patient.PatientName,
                    PatientAge = patient.PatientAge,
                    PatientGender = patient.PatientGender,
                    Department = patient.Department.DepartmentName,
                    DoctorName = patient.DoctorName,
                    DoctorFee = patient.DoctorFee,
                    CreatedBy = User.Identity.Name,
                    RegisterDate = DateTime.Now
                };
                patientDetailsListViewModel.Add(patientDetailsViewModel);
            }
            if (patientDetailsListViewModel.Count == 0)
            {
                return NotFound("No record found");
            }

            return Ok(patientDetailsListViewModel);
        }
        [HttpGet("patient/{name}")]
        public IActionResult GetPatientByName(string name)
        {
            var allPatientByName = _hospitalRepository.GetFullPatientDetailsByName(name, User.Identity.Name);

            List<PatientDetailsViewModel> patientDetailsListViewModel = new List<PatientDetailsViewModel>();

            foreach (var patient in allPatientByName)
            {
                PatientDetailsViewModel patientDetailsViewModel = new PatientDetailsViewModel
                {
                    Id = patient.Id,
                    PatientName = patient.PatientName,
                    PatientAge = patient.PatientAge,
                    PatientGender = patient.PatientGender,
                    Department = patient.Department.DepartmentName,
                    DoctorName = patient.DoctorName,
                    DoctorFee = patient.DoctorFee,
                    CreatedBy = User.Identity.Name,
                    RegisterDate = DateTime.Now
                };
                patientDetailsListViewModel.Add(patientDetailsViewModel);
            }
            if (patientDetailsListViewModel.Count == 0)
            {
                return NotFound("No Records Found");
            }

            return Ok(patientDetailsListViewModel);
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePatientRecord(int id, [FromBody] UpdatePatientRecordViewModel updatePatientRecordViewModel)
        {
            Hospital hospital = new Hospital
            {
                PatientName = updatePatientRecordViewModel.PatientName,
                PatientAge = updatePatientRecordViewModel.PatientAge,
                PatientGender = updatePatientRecordViewModel.PatientGender,
                DepartmentId = updatePatientRecordViewModel.DepartmentId,
                DoctorName = updatePatientRecordViewModel.DoctorName,
                DoctorFee = updatePatientRecordViewModel.DoctorFee,
                CreatedBy = User.Identity.Name,
                RegisterDate = DateTime.Now
            };

            bool isPatientRecordUpdated = _hospitalRepository.UpdatePatientDetails(id, hospital);

            if (!isPatientRecordUpdated)
            {
                return NotFound($"Patient with id = {id} is not found.");
            }
            return Ok($"Patient with id = {id} is updated successfully.");

        }
        [HttpDelete("{id}")]
        public IActionResult DeletePatientRecord(int id)
        {
            bool isPatientRecordRemoved = _hospitalRepository.DeletePatientDetails(id);
            if (!isPatientRecordRemoved)
            {
                return NotFound($"Patient with id = {id} is not found.");
            }
            return Ok($"Patient with id = {id} is removed successfully.");
        }
    }
}
