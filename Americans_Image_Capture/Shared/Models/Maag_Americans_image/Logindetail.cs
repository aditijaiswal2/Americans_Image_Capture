using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Americans_Image_Capture.Shared.Models.Maag_Americans_image
{
    public class Logindetail
    {
        public int Id { get; set; } // Auto-incremented ID
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
