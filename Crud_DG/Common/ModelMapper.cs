using Crud_DG.Models;
using System.Data;

namespace Crud_DG.Common
{
    public class ModelMapper
    {
        internal static List<Employee> MapEmployeeListDataTableToModelObj(DataTable dataTable)
        {
            List<Employee> _listOfEmployees = new List<Employee>();
            try
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    Employee obj = new Employee();
                    obj.Id = Convert.ToInt32(row["Id"]);
                    obj.IsActive = Convert.ToBoolean(row["IsActive"]);
                    obj.FirstName = row["FirstName"].ToString();
                    obj.LastName = row["LastName"].ToString();
                    if (Enum.TryParse(row["Gender"].ToString(), out Gender gender))
                    {
                        obj.Gender = gender;
                    }
                    else
                    {
                        obj.Gender = Gender.Male;
                    }


                    _listOfEmployees.Add(obj);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
            return _listOfEmployees;
        }
        internal static Employee MapEmployeeDataTableToModelObj(DataTable dataTable)
        {
            Employee obj = new Employee();

            try
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    obj.FirstName = row["FirstName"].ToString();
                    obj.LastName = row["LastName"].ToString();
                    if (Enum.TryParse(row["Gender"].ToString(), out Gender gender))
                    {
                        obj.Gender = gender;
                    }
                    else
                    {
                        obj.Gender = Gender.Male;
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return obj;
        }

    }
}
