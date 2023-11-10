using System.ComponentModel;
using System.Reflection;

namespace Crud_DG.Common
{
    public class ResultSet<T> : IDisposable
    {
        public bool HasResponse { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ResultSet()
        {
            IsSuccess = false;
            Message = string.Empty;
            HasResponse = false;
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
    public class ErrorResponse
    {
        public ErrorDetails Error { get; set; } = new ErrorDetails();
    }

    public class ErrorDetails
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<string> Details { get; set; } = new List<string>();
    }
    public enum ApiResult
    {
        [Description("An unexpected server error occurred while processing the request.")]
        ServerErrorMessage,
        [Description("500")]
        ServerError,
        [Description("An error occurred while processing the request.")]
        BadRequestMessage,
        [Description("400")]
        BadRequest,
        [Description("Data Retrieved Successfully!")]
        Success,
        [Description("No Data Found!")]
        NoDataFound,
        [Description("GET request occured failure => API URL: {ApiUrl}")]
        GetRequestException,
        [Description("GET request processed successfully => API URL: {ApiUrl}")]
        GetRequest,
        [Description("Invalid or Missing fields")]
        MissingModel,
        [Description("Data Uploaded Successfully!")]
        UploadSuccess,
        [Description("Data Modified Successfully!")]
        UpdateSuccess,
        [Description("An error occured during insertion!")]
        InsertionFailed,
        [Description("An error occured during updation!")]
        UpdationFailed,
        [Description("Action Perform Successfully!")]
        DeleteSuccess,
        [Description("Action Failed due to an error!")]
        Failed,
    }


    public enum TableInfo
    {
        [Description("SP_GetAllEmployees")]
        SP_GetAllEmployees,
        [Description("SP_GetEmployeeById")]
        SP_GetEmployeeById, 
        [Description("SP_CreateOrUpdateEmployee")]
        SP_CreateOrUpdateEmployee, 
        [Description("SP_ActiveOrDeactivateEmployee")]
        SP_ActiveOrDeactivateEmployee,
    }
    public static class EnumHelper
    {
        private static Dictionary<Enum, DescriptionAttribute> _stringValues = new Dictionary<Enum, DescriptionAttribute>();
        public static string GetDescription(this Enum status)
        {
            string output = null;
            Type type = status.GetType();

            //Check first in our cached results...
            if (_stringValues.ContainsKey(status))
                output = (_stringValues[status] as DescriptionAttribute).Description;
            else
            {
                //Look for our 'StringValueAttribute' 
                //in the field's custom attributes
                FieldInfo fi = type.GetField(status.ToString());
                DescriptionAttribute[] attrs =
                   fi.GetCustomAttributes(typeof(DescriptionAttribute),
                                           false) as DescriptionAttribute[];
                if (attrs.Length > 0)
                {
                    _stringValues.Add(status, attrs[0]);
                    output = attrs[0].Description;
                }
                else
                {
                    output = attrs[0].ToString();
                }
            }

            return output;
        }
    }

}
