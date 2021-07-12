using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoUpdate
    {
        [Required(ErrorMessage = "Código é campo Obrigatório.")]
        public Guid Id { get; set; }

        [Required (ErrorMessage = "Nome é campo Obrigatório.")]
        [MaxLength(60, ErrorMessage = "Nome de município deve ter no maximo {1} caracteres.")]
        public string Nome { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Código IBGE Inválido.")]
        public int CodIBGE { get; set; }

        [Required(ErrorMessage = "Código de Uf é campo Obrigatório.")]
        public Guid UfId { get; set; }
    }
}