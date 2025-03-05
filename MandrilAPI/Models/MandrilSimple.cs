//creamos este model para probar el insert sencillo, sin insert join de tablas como habilidades, en este caso
using System.ComponentModel.DataAnnotations;

namespace MandrilAPI.Models
{
    public class MandrilSimple

    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; } = string.Empty;
    }
}
