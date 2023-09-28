using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerUICore.Models.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "An email address is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; }
    }
}
