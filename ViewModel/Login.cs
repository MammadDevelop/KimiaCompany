using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class Login
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0}  نمیتواند خالی باشد")]
        public string UserName { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0}  نمیتواند خالی باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
