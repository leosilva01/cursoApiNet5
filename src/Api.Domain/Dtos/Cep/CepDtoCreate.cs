using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Cep
{
    public class CepDtoCreate
    {   
        [Required (ErrorMessage = "Cep é campo Obrigatório.")]
        [MaxLength(10, ErrorMessage = "Cep deve ter no maximo {1} caracteres.")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro é campo Obrigatório.")]
        [MaxLength(60, ErrorMessage = "Logradouro deve ter no maximo {1} caracteres.")]
        public string Logradouro { get; set; }

        [MaxLength(10, ErrorMessage = "Número deve ter no maximo {1} caracteres.")]
        public string Numero { get; set; }
        
        [Required(ErrorMessage = "Código do Município é campo Obrigatório.")]
        public Guid MunicipioId { get; set; }
    }
}