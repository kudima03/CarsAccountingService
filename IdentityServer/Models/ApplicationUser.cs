﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models;

public class ApplicationUser : IdentityUser
{
    [Required] public string Name { get; set; }

    [Required] public string LastName { get; set; }

    [Required] public string Street { get; set; }

    [Required] public string City { get; set; }

    [Required] public string Country { get; set; }
}