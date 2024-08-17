using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass
{
    public class User
    {

        [Key]
        public int Id { get; set; }
        [Display(Name = "نام ونام خانوادگی")]
        [Required(ErrorMessage = "{0}  نمیتواند خالی باشد")]
        public string FullName { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0}  نمیتواند خالی باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0}  نمیتواند خالی باشد")]
        public string UserName { get; set; }
        [Display(Name = "پست الکرونیکی")]
        [Required(ErrorMessage = "{0}  نمیتواند خالی باشد")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "{0}  نمیتواند خالی باشد")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public virtual Role Role { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        public int? StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }

    }
}
