using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "نام و نام خانوادگی را وارد کنید ")]
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "شماره تماس را وارد کنید ")]
        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "پست الکترونیکی را وارد کنید ")]
        [Display(Name = "پست الکترونیکی")]
        public string Email { get; set; }

        [Required(ErrorMessage = "عنوان را وارد کنید ")]
        [Display(Name = "عنوان")]
        public string Subject { get; set; }
        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "متن پیام را وارد کنید ")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public bool IsRead { get; set; }
        public string IpAddress { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
