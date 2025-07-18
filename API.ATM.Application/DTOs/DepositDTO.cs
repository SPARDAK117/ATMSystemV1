using API.ATM.Shared.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Application.DTOs
{
    public class DepositDto
    {
        public string AccountOrCard { get; set; } = "";
        public decimal Amount { get; set; }
    }
}
