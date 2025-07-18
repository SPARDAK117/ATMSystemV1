using API.ATM.Application.DTOs;
using API.ATM.Application.Queries;
using API.ATM.Domain;
using API.ATM.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace API.ATM.Infrastructure.Repositories.Commands
{
    public class BalanceQueryExecutor
    {
        private readonly string ConnectionString;

        public BalanceQueryExecutor(IConfiguration Config)
        {
            ConnectionString = Config.GetConnectionString("DefaultConnection")!;
        }
        public async Task<ApiResponse<BalanceResponse>> ExecuteAsync(BalanceQuery Query, CancellationToken cancellationToken)
        {
            using SqlConnection DBConnection = new(ConnectionString);
            using SqlCommand Command = new("Bank.UspGetBalance", DBConnection) { CommandType = CommandType.StoredProcedure };

            Command.Parameters.AddWithValue("@CardNumber", Query.Request.CardNumber);

            SqlParameter ResultCodeParameter = new("@ResultCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            SqlParameter ResultMessageParameter = new("@ResultMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

            Command.Parameters.Add(ResultCodeParameter);
            Command.Parameters.Add(ResultMessageParameter);

            await DBConnection.OpenAsync(cancellationToken);
            using SqlDataReader reader = await Command.ExecuteReaderAsync(cancellationToken);

            if (await reader.ReadAsync(cancellationToken))
            {
                BalanceResponse response = new()
                {
                    Balance = reader.GetDecimal(0)
                };
                return ApiResponse<BalanceResponse>.Ok(response);
            }

            int Code = (int)(ResultCodeParameter.Value ?? 900);
            string Message = ResultMessageParameter.Value?.ToString() ?? "Unknown";


            return ApiResponse<BalanceResponse>.Fail(Code, Message);
        }
    }
}
