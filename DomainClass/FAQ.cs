using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass
{
    public class FAQ
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "سوال")]
        
        public string Question { get; set; }
        [Display(Name = "جواب")]
        public string Answer { get; set; }

        [Display(Name = "الویت")]
        public int Level { get; set; }
    }
}
