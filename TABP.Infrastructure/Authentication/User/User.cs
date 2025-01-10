﻿using Domain.Enums;

namespace TABP.Infrastructure.Authentication.User;

public record User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string Salt { get; set; }
}

