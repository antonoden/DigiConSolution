using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigiCon.Web.UI.Models.Account
{
    public class Login
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; internal set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; internal set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; internal set; }
    }
}