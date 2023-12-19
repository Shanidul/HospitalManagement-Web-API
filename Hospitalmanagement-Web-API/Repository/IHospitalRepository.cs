using Hospitalmanagement_Web_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitalmanagement_Web_API.Repository
{
    public interface IHospitalRepository
    {
        Hospital AddPatientRecords(Hospital hospital);
        List<Hospital> GetAllPatientRecords(string username);
        List<Hospital> GetFullPatientDetailsById(int id, string username);
        List<Hospital> GetFullPatientDetailsByName(string name, string username);
        bool UpdatePatientDetails(int id, Hospital hospital);
        bool DeletePatientDetails(int id);
    }
}
