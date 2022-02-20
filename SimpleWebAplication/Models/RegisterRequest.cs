﻿namespace SimpleWebAplication.Models;

public class RegisterRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
}
