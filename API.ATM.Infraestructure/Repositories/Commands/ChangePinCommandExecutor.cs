using API.ATM.Application.Commands;
using API.ATM.Domain;
using API.ATM.Domain.Helpers;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace API.ATM.Infrastructure.Repositories.Commands
{
    public class ChangePinCommandExecutor
    {
        private readonly string ConnectionString;

        public ChangePinCommandExecutor(IConfiguration Config)
        {
            ConnectionString = Config.GetConnectionString("DefaultConnection")!;
        }

        public async Task<ApiResponse<Unit>> ExecuteAsync(ChangePinCommand Data, CancellationToken cancellationToken)
        {
            var (salt, hash) = PinHasher.HashPin(Data.Request.NewPin);

            using SqlConnection DbConnection = new(ConnectionString);
            using SqlCommand Command = new("Bank.UspChangePin", DbConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            Command.Parameters.AddWithValue("@CardNumber", Data.Request.CardNumber);
            Command.Parameters.AddWithValue("@NewSalt", salt);
            Command.Parameters.AddWithValue("@NewHash", hash);

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
