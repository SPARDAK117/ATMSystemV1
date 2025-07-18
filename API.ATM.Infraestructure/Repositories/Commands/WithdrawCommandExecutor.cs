using API.ATM.Application.Commands;
using API.ATM.Domain;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace API.ATM.Infrastructure.Repositories.Commands
{
    public sealed class WithdrawCommandExecutor
    {
        private readonly string ConnectionString;

        public WithdrawCommandExecutor(IConfiguration Config)
        {
            ConnectionString = Config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<ApiResponse<Unit>> ExecuteAsync(WithdrawCommand Data, CancellationToken cancellationToken)
        {
                using SqlConnection DbConnection = new(ConnectionString);
                using SqlCommand Command = new("Bank.UspWithdraw", DbConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                Command.Parameters.AddWithValue("@CardNumber", Data.Request.CardNumber);
                Command.Parameters.AddWithValue("@Amount", Data.Request.Amount);

                SqlParameter ResultCodeParameter = new("@ResultCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                SqlParameter ResultMessageParameter = new("@ResultMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                Command.Parameters.Add(ResultCodeParameter);
                Command.Parameters.Add(ResultMessageParameter);

                await DbConnection.OpenAsync(cancellationToken);
                await Command.ExecuteNonQueryAsync(cancellationToken);

                int Code = (int)(ResultCodeParameter.Value ?? 900);
                string Message = ResultMessageParameter.Value?.ToString() ?? "Unknown";

                return Code == 0
                    ? ApiResponse<Unit>.Ok(Unit.Value)
                    : ApiResponse<Unit>.Fail(Code, Message);
        }
    }
}
