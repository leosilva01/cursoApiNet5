using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public class UfEntity : BaseEntity
    {
        [Required]
        [MaxLength(45)]
        public string Nome { get; set; }
        
        [Required]
        [MaxLength(2)]
        public string Sigla { get; set; }
        
        public IEnumerable<MunicipioEntity> municipios { get; set; }

    }
}