using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ChangePass
    {
        [Display(Name = "رمز عبور سابق ")]
        [Required(ErrorMessage = "{0}  نمیتواند خالی باشد")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0}  نمیتواند خالی باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "{0}  نمیتواند خالی باشد")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور  مطابقت ندارد")]
        public string RepPassword { get; set; }
    }
}
