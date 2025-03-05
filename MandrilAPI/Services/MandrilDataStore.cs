//para no crear una base de datos con las tablas y sus datos vamos a usar un servicio de almacenamiento de datos en memoria (In Memory Storage), para esto vamos a crear una clase llamada MandrilDataStore en la carpeta Services

using MandrilAPI.Models;

namespace MandrilAPI.Services
{
    public class MandrilDataStore
    {
        public List<Mandril> Mandriles { get; set; }

        public static MandrilDataStore Current { get; } = new MandrilDataStore();//creamos una instancia de la clase MandrilDataStore para poder acceder a los datos de los mandriles

        public MandrilDataStore()
        {
            Mandriles = new List<Mandril>() {
            
                new Mandril(){  
                    Id = 1,
                    Nombre= "Martin",
                    Apellido = "Rodrigeuz",
                    Habilidades = new List<Habilidad>(){

                       new Habilidad(){
                            Id = 1,
                            Nombre = "Correr",
                            Potencia = Habilidad.EPotencia.Suave
                        },

                    }
                }
            
            };
        }
    }
}
