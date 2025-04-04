﻿using stockman.Enuns;
using System.ComponentModel.DataAnnotations;

namespace stockman.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        public required string Name { get; set; }

        [EmailAddress(ErrorMessage = "Email inválido!")]
        [Required(ErrorMessage = "O email é obrigatório!")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória!")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "A role é obrigatória!")]
        public required Roles Role { get; set; }
    }
}
