using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DtoModels
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public int Mobile { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "")]
        public string ConfirmPassword { get; set; }
    }
}
