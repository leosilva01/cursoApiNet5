using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Cep
{
    public class CepDtoUpdate
    {
        [Required(ErrorMessage = "Código é campo Obrigatório.")]
        public Guid Id { get; set; }

        [Required (ErrorMessage = "Cep é campo Obrigatório.")]
        [MaxLength(60, ErrorMessage = "Cep deve ter no maximo {1} caracteres.")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Número é campo Obrigatório.")]
        [MaxLength(10, ErrorMessage = "Número deve ter no maximo {1} caracteres.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Número é campo Obrigatório.")]
        [MaxLength(60, ErrorMessage = "Número deve ter no maximo {1} caracteres.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Logradouro do Município é campo Obrigatório.")]
        public Guid MunicipioId { get; set; }
    }
}