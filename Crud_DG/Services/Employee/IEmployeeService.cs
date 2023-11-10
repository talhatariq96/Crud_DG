using Crud_DG.Common;
using Crud_DG.Models;

namespace Crud_DG.Services
{
    public interface IEmployeeService
    {
        Task<ResultSet<IEnumerable<Employee>>> GetAllEmployeesAsync();
        Task<ResultSet<Employee>> GetEmployeeByIdAsync(GetEmployeeModel getEmployeeModel);
        Task<ResultSet<string>> CreateOrUpdateEmployeeAsync(UploadEmployeeModel uploadEmployee);
        Task<ResultSet<string>> DeleteEmployeeAsync(DeleteEmployeeModel deleteEmployee);

    }
}
