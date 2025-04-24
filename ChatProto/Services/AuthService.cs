using System;
using System.Collections.Generic;
using System.Text.Json;
using ChatProto.Models;
using System.IO;

namespace ChatProto.Services;

public class AuthService
{
    private static readonly string UsersFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ChatProto", "users.json");
    private List<User> _users;
    private User _currentUser;

    public User CurrentUser => _currentUser;

    public AuthService()
    {
        _users = LoadUsers();
        if (_users.Count == 0)
        {
            _users = new List<User>()
        {
            new User("admin1", "admin1", "admin1@e.com", "admin1"),
        };
        }
    }

    /// <summary>
    /// Авторизація користувача.
    /// </summary>
    /// <param name="username">Логін користувача.</param>
    /// <param name="password">Пароль користувача.</param>
    /// <returns>true якщо користувач існує, false якщо ні.</returns>
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

    /// <summary>
    /// Реєстрація нового користувача.
    /// </summary>
    /// <param name="newUser">Новий користувач, який реєструється.</param>
    /// <returns>true якщо реєстрація успішна, false якщо користувач з таким логіном вже існує.</returns>
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

    /// <summary>
    /// Вихід з системи.
    /// </summary>
    public void Logout()
    {
        _currentUser = null!;
    }

    /// <summary>
    /// Завантаження користувачів з файлу.
    /// </summary>
    /// <returns>Список користувачів, завантажених з файлу, або порожній список, якщо файл не існує.</returns>
    private List<User> LoadUsers()
    {
        if (File.Exists(UsersFilePath))
        {
            string json = File.ReadAllText(UsersFilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        return new List<User>();
    }

    /// <summary>
    /// Збереження користувачів у файл.
    /// </summary>
    public void SaveUsers()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(UsersFilePath));
        string json = JsonSerializer.Serialize(_users);
        File.WriteAllText(UsersFilePath, json);
    }
}
