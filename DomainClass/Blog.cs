using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DomainClass
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "متن مقاله")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Descreption { get; set; }
        [Display(Name = "تاریخ انتشار")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "تگ ها")]
        public string Tags { get; set; }
        [Display(Name = "عکس")]
        public string Image { get; set; }
        [Display(Name = "تعداد بازدید")]
        public int Seen { get; set; }
        [Display(Name = "انتشار دهنده")]
        public int PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public virtual User Publisher { get; set; }
    }
}
