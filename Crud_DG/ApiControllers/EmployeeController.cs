using Crud_DG.Common;
using Crud_DG.Models;
using Crud_DG.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_DG.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service= service;
        }

        [HttpGet]
        [Route("get-employees")]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            ResultSet<IEnumerable<Employee>> _listOfEmployees = new();
            try
            {
                _listOfEmployees = await _service.GetAllEmployeesAsync();
                if (!ReferenceEquals(_listOfEmployees.Data, null) && _listOfEmployees.IsSuccess == true)
                {
                    //_logger.LogInformation(ApiResult.GetRequest.GetDescription(), HttpContext.Request.Path.ToString());
                }
                else if (_listOfEmployees.IsSuccess == true)
                {
                    //_logger.LogInformation(ApiResult.NoDataFound.GetDescription(), HttpContext.Request.Path.ToString());
                    return NotFound(_listOfEmployees);
                }
                else
                {
                    var errorResponse = new ErrorResponse
                    {
                        Error = new ErrorDetails
                        {
                            Code = ApiResult.ServerError.GetDescription(),
                            Message = ApiResult.ServerErrorMessage.GetDescription(),
                            Details = new List<string> { _listOfEmployees.Message }
                        }
                    };

                    return StatusCode(500, errorResponse);

                }

                return Ok(_listOfEmployees);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ApiResult.GetRequestException.GetDescription(), HttpContext.Request.Path.ToString(), Helper.GetCurrentLineNumber(), ex);
                var errorResponse = new ErrorResponse
                {
                    Error = new ErrorDetails
                    {
                        Code = ApiResult.BadRequest.GetDescription(),
                        Message = ApiResult.BadRequestMessage.GetDescription(),
                        Details = new List<string> { ex.Message }
                    }
                };

                return BadRequest(errorResponse);
            }
        }
        [HttpPost]
        [Route("get-employee")]
        public async Task<IActionResult> GetEmployeeByIdAsync([FromBody] GetEmployeeModel empParam)
        {
            ResultSet<Employee> _employee = new();
            try
            {
                if (ReferenceEquals(empParam,null))
                {
                    var errorResponse = new ErrorResponse
                    {
                        Error = new ErrorDetails
                        {
                            Code = ApiResult.BadRequest.GetDescription(),
                            Message = ApiResult.BadRequestMessage.GetDescription(),
                            Details = new List<string> { "Invalid Id" }
                        }
                    };
                    return BadRequest(errorResponse);
                }
                _employee = await _service.GetEmployeeByIdAsync(empParam);
                if (!ReferenceEquals(_employee.Data, null) && _employee.IsSuccess == true)
                {
                    //_logger.LogInformation(ApiResult.GetRequest.GetDescription(), HttpContext.Request.Path.ToString());
                }
                else if (_employee.IsSuccess == true)
                {
                    //_logger.LogInformation(ApiResult.NoDataFound.GetDescription(), HttpContext.Request.Path.ToString());
                    return NotFound(_employee);
                }
                else
                {
                    var errorResponse = new ErrorResponse
                    {
                        Error = new ErrorDetails
                        {
                            Code = ApiResult.ServerError.GetDescription(),
                            Message = ApiResult.ServerErrorMessage.GetDescription(),
                            Details = new List<string> { _employee.Message }
                        }
                    };

                    return StatusCode(500, errorResponse);

                }

                return Ok(_employee);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ApiResult.GetRequestException.GetDescription(), HttpContext.Request.Path.ToString(), Helper.GetCurrentLineNumber(), ex);
                var errorResponse = new ErrorResponse
                {
                    Error = new ErrorDetails
                    {
                        Code = ApiResult.BadRequest.GetDescription(),
                        Message = ApiResult.BadRequestMessage.GetDescription(),
                        Details = new List<string> { ex.Message }
                    }
                };

                return BadRequest(errorResponse);
            }
        }

        [HttpPost]
        [Route("upload-employee")]
        public async Task<IActionResult> UploadEmployeeAsync([FromBody] UploadEmployeeModel uploadEmployee)
        {
            ResultSet<string> _getResponse = new ResultSet<string>();
            try
            {
                _getResponse = await _service.CreateOrUpdateEmployeeAsync(uploadEmployee);
                if (!ReferenceEquals(_getResponse.Data, null) && _getResponse.IsSuccess == true)
                {
                    //_logger.LogInformation(ApiResult.GetRequest.GetDescription(), HttpContext.Request.Path.ToString());
                }
                else if (_getResponse.IsSuccess == true)
                {
                    //_logger.LogInformation(ApiResult.NoDataFound.GetDescription(), HttpContext.Request.Path.ToString());
                    return NotFound(_getResponse);
                }
                else
                {
                    var errorResponse = new ErrorResponse
                    {
                        Error = new ErrorDetails
                        {
                            Code = ApiResult.ServerError.GetDescription(),
                            Message = ApiResult.ServerErrorMessage.GetDescription(),
                            Details = new List<string> { _getResponse.Message }
                        }
                    };

                    return StatusCode(500, errorResponse);

                }

                return Ok(_getResponse);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ApiResult.GetRequestException.GetDescription(), HttpContext.Request.Path.ToString(), Helper.GetCurrentLineNumber(), ex);
                var errorResponse = new ErrorResponse
                {
                    Error = new ErrorDetails
                    {
                        Code = ApiResult.BadRequest.GetDescription(),
                        Message = ApiResult.BadRequestMessage.GetDescription(),
                        Details = new List<string> { ex.Message }
                    }
                };

                return BadRequest(errorResponse);
            }
        }
        [HttpPost]
        [Route("delete-employee")]
        public async Task<IActionResult> DeleteEmployeeAsync([FromBody] DeleteEmployeeModel uploadEmployee)
        {
            ResultSet<string> _getResponse = new ResultSet<string>();
            try
            {
                _getResponse = await _service.DeleteEmployeeAsync(uploadEmployee);
                if (!ReferenceEquals(_getResponse.Data, null) && _getResponse.IsSuccess == true)
                {
                    //_logger.LogInformation(ApiResult.GetRequest.GetDescription(), HttpContext.Request.Path.ToString());
                }
                else if (_getResponse.IsSuccess == true)
                {
                    //_logger.LogInformation(ApiResult.NoDataFound.GetDescription(), HttpContext.Request.Path.ToString());
                    return NotFound(_getResponse);
                }
                else
                {
                    var errorResponse = new ErrorResponse
                    {
                        Error = new ErrorDetails
                        {
                            Code = ApiResult.ServerError.GetDescription(),
                            Message = ApiResult.ServerErrorMessage.GetDescription(),
                            Details = new List<string> { _getResponse.Message }
                        }
                    };

                    return StatusCode(500, errorResponse);

                }

                return Ok(_getResponse);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ApiResult.GetRequestException.GetDescription(), HttpContext.Request.Path.ToString(), Helper.GetCurrentLineNumber(), ex);
                var errorResponse = new ErrorResponse
                {
                    Error = new ErrorDetails
                    {
                        Code = ApiResult.BadRequest.GetDescription(),
                        Message = ApiResult.BadRequestMessage.GetDescription(),
                        Details = new List<string> { ex.Message }
                    }
                };

                return BadRequest(errorResponse);
            }
        }

    }
}
