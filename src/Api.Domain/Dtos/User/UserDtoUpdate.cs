using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDtoUpdate
    {
        [Required (ErrorMessage = "Id é campo obrigatório.")]
        public Guid Id { get; set; }
        [StringLength (60, ErrorMessage = "Nome deve ter no Máximo {1} caracteres.")]
        public string Name { get; set; }
        [EmailAddress (ErrorMessage = "E-mail em formato inválido.")]
        [StringLength (100, ErrorMessage = "E-mail deve ter no Máximo {1} caracteres.")]
        public string Email { get; set; }
    }
}