using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Models
{
    public class SiteUser
    {
        public int SiteUserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public bool IsAdmin { get; set; }
    }
}
