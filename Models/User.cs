using System;
using System.Collections.Generic;

namespace AspCrudApp.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string City { get; set; } = null!;

    public string Password { get; set; } = null!;
}
