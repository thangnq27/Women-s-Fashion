using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnLineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Input username")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Input password")]
        public string Password { get; set; }
        public bool Remember { get; set; }

    }
}