using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProto.Models;

public class User
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;

    public User()
    {

    }

    public User(string username, string password, string email, string fullName)
    {
        Username = username;
        Password = password;
        Email = email;
        FullName = fullName;
    }
}