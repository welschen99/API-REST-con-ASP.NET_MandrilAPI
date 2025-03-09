//AGREGAMOS ESTE MODEL PARA NO ESPECIFICAR EL ID AL HACER EL POST, PUT SOLO CON LOS CAMPOS QUE NECESTIAMOS A MODO DE PRUEBA
using System.ComponentModel.DataAnnotations;
using static MandrilAPI.Models.Habilidad;

namespace MandrilAPI.Models
{
    public class HabilidadSimple
    {
        public string Nombre { get; set; } = string.Empty;

        public EPotencia Potencia { get; set; } 
    }
}
