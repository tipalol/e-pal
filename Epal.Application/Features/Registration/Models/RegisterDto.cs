﻿using System.ComponentModel.DataAnnotations;

namespace Epal.Application.Features.Registration.Models;

public class RegisterDto(string Username, string Email, string Password, string PasswordConfirm)
{
    [Required]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string PasswordConfirm { get; set; }
}