using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.DataAccess.Domain.Identity
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        //[Required(ErrorMessage = "User Name is required")]
        //public string Username { get; set; }

        //[EmailAddress]
        //[Required(ErrorMessage = "Email is required")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //public string Password { get; set; }
    }
}
