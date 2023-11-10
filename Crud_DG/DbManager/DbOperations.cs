using System.Data.SqlClient;
using System.Data;

namespace Crud_DG.DbManager
{
    public class DbOperations
    {
       
        public async Task<DataTable?> _ExecuteReader(string commandText, CommandType commandType, List<SqlParameter> param)
        {
            SqlCommand comm = null;
            SqlDataReader? reader = null;
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection _connection = new SqlConnection("Data Source=.;Initial Catalog=DG;Persist Security Info=False;TrustServerCertificate=True;Trusted_Connection=true;"))
                {
                    _connection.Open();
                    comm = new SqlCommand(commandText, _connection);

                    comm.CommandType = commandType;
                    comm.CommandTimeout = 90;
                    for (int i = 0; i < param.Count; i++)
                    {
                        comm.Parameters.Add(param[i]);
                    }
                    reader = await comm.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        dataTable.Load(reader);
                        return dataTable;
                    }
                    else
                    {
                        //_logger.LogInformation("_ExecuteReader Message : No Data Found!");
                    }


                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("_ExecuteReader", ex);

            }
            return dataTable;

        }
        public async Task<long?> _ExecuteNonQueryAsync(string commandText, CommandType commandType, List<SqlParameter> param)
        {
            SqlCommand comm = null;
            int rowsAffected = 0;
            try
            {
                using (SqlConnection _connection = new SqlConnection("Data Source=.;Initial Catalog=DG;Persist Security Info=False;TrustServerCertificate=True;Trusted_Connection=true;"))
                {
                    SqlParameter rowsAffectedParam = new SqlParameter("@RowsAffected", SqlDbType.Int);
                    rowsAffectedParam.Direction = ParameterDirection.Output;
                    param.Add(rowsAffectedParam);
                    _connection.Open();
                    comm = new SqlCommand(commandText, _connection);

                    comm.CommandType = commandType;
                    comm.CommandTimeout = 90;
                    for (int i = 0; i < param.Count; i++)
                    {
                        comm.Parameters.Add(param[i]);
                    }

                    // Execute the query
                    await comm.ExecuteNonQueryAsync();
                    rowsAffected = (int)rowsAffectedParam.Value;

                }

                //_logger.LogInformation("_ExecuteReader Message : No Data Found!");
            }

            catch (Exception ex)
            {
                //_logger.LogError("_ExecuteReader", ex);

            }
            return rowsAffected;

        }
        public async Task<long?> _ExecuteNonQueryWithNoOutParamAsync(string commandText, CommandType commandType, List<SqlParameter> param)
        {
            SqlCommand comm = null;
            int rowsAffected = 0;
            try
            {
                using (SqlConnection _connection = new SqlConnection("Data Source=.;Initial Catalog=DG;Persist Security Info=False;TrustServerCertificate=True;Trusted_Connection=true;"))
                {
                    
                    _connection.Open();
                    comm = new SqlCommand(commandText, _connection);

                    comm.CommandType = commandType;
                    comm.CommandTimeout = 90;
                    for (int i = 0; i < param.Count; i++)
                    {
                        comm.Parameters.Add(param[i]);
                    }

                    // Execute the query
                    rowsAffected = await comm.ExecuteNonQueryAsync();

                }

                //_logger.LogInformation("_ExecuteReader Message : No Data Found!");
            }

            catch (Exception ex)
            {
                //_logger.LogError("_ExecuteReader", ex);

            }
            return rowsAffected;

        }


    }
}
