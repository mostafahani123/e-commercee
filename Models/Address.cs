﻿using Microsoft.AspNetCore.Identity;

namespace e_commercee.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public string AddressLine { get; set; }
        public string City { get; set; }
        public string state { get; set; }
        public string PinCode { get; set; }

    }
}
