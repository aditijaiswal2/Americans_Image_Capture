using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Americans_Image_Capture.Shared.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string UserCode { get; set; }
       public int isDeleted { get; set; }
        //public string PageNames { get; set; }
        //public string Routes { get; set; }
    }
}
