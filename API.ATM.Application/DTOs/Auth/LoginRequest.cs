using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Application.DTOs.Auth
{
    public class LoginRequest
    {
        [Required] 
        public string CardNumber { get; set; } = "";
        [Required] 
        public string Pin { get; set; } = "";
    }
}
