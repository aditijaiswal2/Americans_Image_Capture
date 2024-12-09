using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Americans_Image_Capture.Shared.DTOs
{
    public class UserAddDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Usercode { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter valid Email address")]
        public string Email { get; set; }
    
        //public string PageNames { get; set; }
        //public string Routes { get; set; }


    }
}
