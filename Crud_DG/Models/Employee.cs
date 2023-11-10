using System.ComponentModel;

namespace Crud_DG.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
    }
    public class UploadEmployeeModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
    }
    public class GetEmployeeModel
    {
        public int EmployeeId { get; set; }
    }
    public class DeleteEmployeeModel
    {
        public int EmployeeId { get; set; }
        public bool IsActive { get; set; }
    }
    public class EmployeeVM
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
    }
    public enum Gender
    {
        Male = 1,
        Female =2
    }
    
}
