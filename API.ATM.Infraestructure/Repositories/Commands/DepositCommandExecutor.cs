using API.ATM.Application.Commands;
using API.ATM.Domain;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace API.ATM.Infrastructure.Repositories.Commands
{
    public class DepositCommandExecutor
    {
        private readonly string ConnectionString;
        public DepositCommandExecutor(IConfiguration Config)
        {
            ConnectionString = Config.GetConnectionString("DefaultConnection")!;
        }
        public async Task<ApiResponse<Unit>> ExecuteAsync(DepositCommand Data, CancellationToken cancellationToken)
        {
            using SqlConnection DBConnection = new(ConnectionString);
            using SqlCommand Command = new("Bank.UspDeposit", DBConnection) { CommandType = CommandType.StoredProcedure };

            Command.Parameters.AddWithValue("@AccountOrCard", Data.Request.AccountOrCard);
            Command.Parameters.AddWithValue("@Amount", Data.Request.Amount);

            SqlParameter ResultCodeParameter = new("@ResultCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
            SqlParameter ResultMessageParameter = new("@ResultMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

            Command.Parameters.Add(ResultCodeParameter);
            Command.Parameters.Add(ResultMessageParameter);

            await DBConnection.OpenAsync(cancellationToken);
            await Command.ExecuteNonQueryAsync(cancellationToken);

            int Code = (int)(ResultCodeParameter.Value ?? 900);
            string Message = ResultMessageParameter.Value?.ToString() ?? "Unknown";

            return Code == 0
                ? ApiResponse<Unit>.Ok(Unit.Value)
                : ApiResponse<Unit>.Fail(Code, Message);
        }

    }
}
