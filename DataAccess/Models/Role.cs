using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Role : IdentityRole<int>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name {  get; set; }
    }
}
