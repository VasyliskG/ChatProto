using System;
using System.Collections.Generic;
using ChatProto.Models;

namespace ChatProto.Services;

public class AuthService
{

    private List<User> _users;
    private User _currentUser;

    public User CurrentUser => _currentUser;

    public AuthService()
    {
        _users = new List<User>()
        {
            new User("d", "d", "d@a.com", "d"),
        };
    }

    //верифікація через хешування
    public bool Login(string username, string password)
    {
        foreach (var user in _users)
        {
            if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password)
            {
                _currentUser = user;
                return true;
            }
        }

        return false;
    }

    //верифікація через хешування для пошти
    public bool Register(User newUser)
    {
        foreach (var user in _users)
        {
            if (user.Username.Equals(newUser.Username, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }
        _users.Add(newUser);
        _currentUser = newUser;
        return true;
    }

    public void Logout()
    {
        _currentUser = null!;
    }
}
