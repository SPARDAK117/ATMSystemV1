using API.ATM.Application.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Infrastructure
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SqlConnection Connection;

        public LoginRepository(IConfiguration Config)
        {
            Connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
        }

        public async Task<string?> GetAccountNumberByCardAsync(string CardNumber)
        {
            return await GetAccountNumberByCardMethod(CardNumber);
        }
        public async Task<bool> ValidateCardAndPinAsync(string CardNumber, string Pin)
        {
            return await ValidateCardAndPinMethod(CardNumber, Pin);
        }
        private async Task<string?> GetAccountNumberByCardMethod(string CardNumber)
        {
            using SqlCommand Command = new("Bank.UspGetAccountNumberByCard", Connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            Command.Parameters.AddWithValue("@CardNumber", CardNumber);

            SqlParameter ResultParam = new SqlParameter("@AccountNumber", SqlDbType.VarChar, 20) { Direction = ParameterDirection.Output };
            Command.Parameters.Add(ResultParam);

            await Connection.OpenAsync();
            await Command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();

            return ResultParam.Value?.ToString();
        }
        private async Task<bool> ValidateCardAndPinMethod(string CardNumber, string Pin)
        {
            using SqlCommand Command = new SqlCommand("Bank.UspGetCardHashInfo", Connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            Command.Parameters.AddWithValue("@CardNumber", CardNumber);

            await Connection.OpenAsync();
            using SqlDataReader reader = await Command.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                await Connection.CloseAsync();
                return false;
            }

            await reader.ReadAsync();

            Guid salt = reader.GetGuid(reader.GetOrdinal("PinSalt"));
            var dbHash = (byte[])reader["PinHash"];
            bool isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));

            await Connection.CloseAsync();

            if (!isActive) return false;

            using SHA512 Sha512 = SHA512.Create();
            var combined = Encoding.UTF8.GetBytes(Pin + salt.ToString());
            var inputHash = Sha512.ComputeHash(combined);

            return dbHash.SequenceEqual(inputHash);
        }

    }

}
