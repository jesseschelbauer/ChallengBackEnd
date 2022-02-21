﻿namespace SimpleWebAplication.Models
{
    public class AccountInfoResponse 
    {
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public decimal Balance { get; set; } 
    }
}