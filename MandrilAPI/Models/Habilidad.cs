using System.ComponentModel.DataAnnotations;

namespace MandrilAPI.Models
{
    public class Habilidad
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;  

        public EPotencia Potencia { get; set; } // Propiedad de potencia de habilidades
        public enum EPotencia // Enumeración de potencia de habilidades, se guardan en la base de datos como numeros enteros, pero con esta propiedad nos permite manipular con nombres mas facil
        {
            Suave, //Ej: Suave  = 0 en database
            Moderada,
            Intenso,
            Extremo
        }
    }
}
