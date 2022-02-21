﻿namespace SimpleWebAplication.Models;

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
