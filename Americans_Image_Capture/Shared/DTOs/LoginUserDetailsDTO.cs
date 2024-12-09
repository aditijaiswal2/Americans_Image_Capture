using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Americans_Image_Capture.Shared.DTOs
{
    public class LoginUserDetailsDTO
    {
        public string UserName { get; set; }
        public string EMail { get; set; }
        public DateTime LoginDateAndTime { get; set; }
    }
}
