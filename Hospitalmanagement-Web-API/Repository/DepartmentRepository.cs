using Hospitalmanagement_Web_API.HDbContext;
using Hospitalmanagement_Web_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitalmanagement_Web_API.Repository
{
    public class DepartmentRepository: IDepartmentRepository
    {
        HospitalDbContext _hospitalDbContext;
        public DepartmentRepository(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }

        public List<Department> GetAllDepartments()
        {
            return _hospitalDbContext.Departments.ToList();
        }
    }
}
