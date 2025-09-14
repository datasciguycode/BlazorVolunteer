using System;
using System.Collections.Generic;

namespace Volunteer.Models;

public partial class User
{
    public User() { }

    public User(User other)
    {
        this.UserId = other.UserId;
        this.FirstName = other.FirstName;
        this.LastName = other.LastName;
        this.Phone = other.Phone;
        this.Email = other.Email;
        this.Address1 = other.Address1;
    }
}
