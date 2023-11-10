using Crud_DG.Common;
using Crud_DG.DbManager;
using Crud_DG.Models;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;

namespace Crud_DG.Services
{
    public class EmployeeService : IEmployeeService
    {
        private DbOperations _dbHelper;
        public EmployeeService()
        {
            _dbHelper = new DbOperations();

        }
        public async Task<ResultSet<string>> CreateOrUpdateEmployeeAsync(UploadEmployeeModel uploadEmployee)
        {
            long? _rowsAffected = 0;
            ResultSet<string> _response = new ResultSet<string>();
            try
            {
                if (ReferenceEquals(uploadEmployee, null))
                {
                    _response.IsSuccess = true;
                    _response.Data = string.Empty;
                    _response.Message = ApiResult.MissingModel.GetDescription();
                    return _response;
                }

                #region Creating Sql Prams
                List<SqlParameter> _params = new List<SqlParameter> {
                        new SqlParameter("EmployeeId",uploadEmployee.EmployeeId),
                        new SqlParameter("FirstName",uploadEmployee.FirstName),
                        new SqlParameter("LastName",uploadEmployee.LastName),
                        new SqlParameter("Gender",uploadEmployee.Gender),
                };
               
                #endregion

                _rowsAffected = await _dbHelper._ExecuteNonQueryAsync(TableInfo.SP_CreateOrUpdateEmployee.GetDescription(), CommandType.StoredProcedure, _params);
                if (_rowsAffected > 0)
                {
                    _response.IsSuccess = true;
                    _response.Data = string.Empty;
                    _response.Message = uploadEmployee.EmployeeId == 0 ? ApiResult.UploadSuccess.GetDescription() : ApiResult.UpdateSuccess.GetDescription();
                    //_logger.LogInformation($" Service CreateOrUpdateEmployeeAsync :,  {_response.Message} ");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Data = string.Empty;
                    _response.Message = uploadEmployee.EmployeeId == 0 ? ApiResult.InsertionFailed.GetDescription() : ApiResult.UpdationFailed.GetDescription();
                    //_logger.LogInformation($" Service CreateOrUpdateEmployeeAsync :,  {_response.Message} ");
                }


            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Data = string.Empty;
                _response.Message = $"{ApiResult.GetRequestException.GetDescription()} => " + ex;
                //_logger.LogError(_response.Message, Helper.GetCallingMethodName(), Helper.GetCurrentLineNumber(), ex);

            }
            return _response;
        }

        public async Task<ResultSet<string>> DeleteEmployeeAsync(DeleteEmployeeModel deleteEmployee)
        {
            long? _rowsAffected = 0;
            ResultSet<string> _response = new ResultSet<string>();
            try
            {
                if (!ReferenceEquals(deleteEmployee, null))
                {
                    List<SqlParameter> _params = new List<SqlParameter> {
                        new SqlParameter("EmployeeId",deleteEmployee.EmployeeId),
                        new SqlParameter("IsActive",deleteEmployee.IsActive),
                    };
                    _rowsAffected = await _dbHelper._ExecuteNonQueryAsync(TableInfo.SP_ActiveOrDeactivateEmployee.GetDescription(), CommandType.StoredProcedure, _params);
                    if (_rowsAffected > 0)
                    {
                        _response.IsSuccess = true;
                        _response.Data = string.Empty;
                        _response.Message = ApiResult.DeleteSuccess.GetDescription();
                        //_logger.LogInformation($" Service DeleteEmployeeAsync :,  {_response.Message} ");
                    }
                    else
                    {
                        _response.IsSuccess = true;
                        _response.Data = string.Empty;
                        _response.Message = ApiResult.Failed.GetDescription();
                        //_logger.LogInformation($" Service DeleteEmployeeAsync :,  {_response.Message} ");
                    }
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Data = string.Empty;
                    _response.Message = ApiResult.MissingModel.GetDescription();
                    //_logger.LogInformation($" Service DeleteEmployeeAsync,  {_response.Message} ");
                }
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Data = string.Empty;
                _response.Message = $"{ApiResult.GetRequestException.GetDescription()} => " + ex;
                //_logger.LogError(_response.Message, Helper.GetCallingMethodName(), Helper.GetCurrentLineNumber(), ex);

            }
            return _response;
        }

        public async Task<ResultSet<IEnumerable<Employee>>> GetAllEmployeesAsync()
        {
            List<Employee> _listOfEmployees = new();
            ResultSet<IEnumerable<Employee>>? _response = new();
            try
            {
                using (DataTable? sqlData = await _dbHelper._ExecuteReader(TableInfo.SP_GetAllEmployees.GetDescription(), CommandType.StoredProcedure, new List<SqlParameter>()))
                {
                    if (!ReferenceEquals(sqlData, null))
                    {
                        _listOfEmployees = ModelMapper.MapEmployeeListDataTableToModelObj(sqlData);
                        _response.HasResponse = _listOfEmployees.Count > 0 ? true : false;
                        _response.IsSuccess = true;
                        _response.Data = _listOfEmployees.AsEnumerable();
                        _response.Message = _listOfEmployees.Count > 0 ? ApiResult.Success.GetDescription() : ApiResult.NoDataFound.GetDescription();
                        //_logger.LogInformation($" Service GetAllEmployeesAsync ,  {_response.Message} ");
                    }
                    else
                    {
                        _response.IsSuccess = true;
                        _response.Data = new List<Employee>();
                        _response.Message = ApiResult.NoDataFound.GetDescription();
                        //_logger.LogInformation($" Service GetAllEmployeesAsync : Null SqlReader,  {_response.Message} ");
                    }
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Data = new List<Employee>();
                _response.Message = $"{ApiResult.GetRequestException.GetDescription()} => " + ex;
                //_logger.LogError(_response.Message, Helper.GetCallingMethodName(), Helper.GetCurrentLineNumber(), ex);

            }
            return _response;
        }

        public async Task<ResultSet<Employee>> GetEmployeeByIdAsync(GetEmployeeModel getEmployeeModel)
        {
            Employee _employee = new();
            ResultSet<Employee>? _response = new();
            try
            {
                if (getEmployeeModel.EmployeeId < 1)
                {
                    _response.HasResponse = false;
                    _response.IsSuccess = false;
                    _response.Data = _employee;
                    _response.Message = ApiResult.BadRequest.GetDescription();

                    return _response;
                }
                List<SqlParameter> _params = new List<SqlParameter>
                {
                    new SqlParameter("EmployeeId",getEmployeeModel.EmployeeId)
                };
                using (DataTable? sqlData = await _dbHelper._ExecuteReader(TableInfo.SP_GetEmployeeById.GetDescription(), CommandType.StoredProcedure, _params))
                {
                    if (!ReferenceEquals(sqlData, null))
                    {
                        _employee = ModelMapper.MapEmployeeDataTableToModelObj(sqlData);
                        _response.HasResponse = ReferenceEquals(_employee, null) ? false : true;
                        _response.IsSuccess = true;
                        _response.Data = _employee;
                        _response.Message = ReferenceEquals(_employee, null) ? ApiResult.NoDataFound.GetDescription() : ApiResult.Success.GetDescription();
                        //_logger.LogInformation($" Service GetEmployeeByIdAsync ,  {_response.Message} ");
                    }
                    else
                    {
                        _response.IsSuccess = true;
                        _response.Data = new Employee();
                        _response.Message = ApiResult.NoDataFound.GetDescription();
                        //_logger.LogInformation($" Service GetEmployeeByIdAsync : Null SqlReader,  {_response.Message} ");
                    }
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Data = new Employee();
                _response.Message = $"{ApiResult.GetRequestException.GetDescription()} => " + ex;
                //_logger.LogError(_response.Message, Helper.GetCallingMethodName(), Helper.GetCurrentLineNumber(), ex);

            }
            return _response;
        }
    }
}
