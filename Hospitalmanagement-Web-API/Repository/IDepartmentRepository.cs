using Hospitalmanagement_Web_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospitalmanagement_Web_API.Repository
{
    public interface IDepartmentRepository
    {
        List<Department> GetAllDepartments();
    }
}
