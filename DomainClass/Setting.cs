using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DomainClass
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "عنوان صفحه اصلی")]
        public string HeroTitle { get; set; }
        [Display(Name = "متن صفحه اصلی")]
        public string HeroText { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "ایدی اینستاگرام")]
        public string InstaId { get; set; }
        [Display(Name = "آیدی تلگرام")]
        public string TelegramId { get; set; }
        [Display(Name = "شماره واتس آپ")]
        public string WhatsApp { get; set; }
        [Display(Name = "متن درباره ما")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string About { get; set; }
        [Display(Name = "لینک گوگل مپ")]
        [Url]
        public string MapLink { get; set; }
        [Display(Name ="تعداد شاگردان")]
        public int StudentCount { get; set; }
        [Display(Name = "تعداد اساتید")]
        public int TeacherCount { get; set; }
        [Display(Name = "تعداد دوره ها")]
        public int CourseCount { get; set; }
        [Display(Name = "تعداد جوایز")]
        public int RewardCount { get; set; }
        [Display(Name = "توضیحات")]
        public string Summery { get; set; }
        [Display(Name = "لینک نرم افزار")]
        public string appLink { get; set; }
    }
}
