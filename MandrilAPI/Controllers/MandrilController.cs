using MandrilAPI.Models;
using MandrilAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MandrilAPI.Controllers
{
    [ApiController]// hacemos que sea un controlador de api, como por ejemplo validar datos
    [Route("api/[controller]")]//este controller en especifico va a tener la url,+ routes especificado. EL controller hereda el nombre del Controller que seria Mandril, nada mas que la nomenclatura que se debe usar al programar es MAdrilController
    public class MandrilController : ControllerBase // hacemos que herede de controllerBase

    {
        //GET
        [HttpGet]//especificamos que es un metodo get
        public ActionResult<IEnumerable<Mandril>> GetMandriles()//metodo que se va a ejecutar cuando se haga una peticion get
        {
            return Ok(MandrilDataStore.Current.Mandriles);//retornamos la lista de mandriles que se encuentra en el servicio de almacenamiento de datos
        }


        //GET BY ID
        [HttpGet("{mandrilId}")]
        public ActionResult<Mandril> GetMandril(int mandrilId)//metodo que se va a ejecutar cuando se haga una peticion get con un id
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(c => c.Id == mandrilId);//buscamos el mandril con el id especificado
            if (mandril == null)
            {
                return NotFound("El mandril solicitado no existe");//si no se encuentra el mandril retornamos un error 404
            }
            return Ok(mandril);//retornamos el mandril encontrado
        }

        //POST
        [HttpPost]
        public ActionResult<Mandril> PostMandril(MandrilSimple mandrilInsert)//metodo que se va a ejecutar cuando se haga una peticion post
        {
            // Obtener el máximo ID actual y asignar un nuevo ID único
            var maxId = MandrilDataStore.Current.Mandriles.Max(m => m.Id);

            var mandrilNuevo = new Mandril()//creamos un nuevo mandril
            {
                Id = ++maxId,//le asignamos un id unico
                Nombre = mandrilInsert.Nombre,//le asignamos un nombre
                Apellido = mandrilInsert.Apellido//le asignamos un apellido
            };

            MandrilDataStore.Current.Mandriles.Add(mandrilNuevo);//agregamos el mandril a la lista de mandriles, el current hace que se guarde en la lista de mandriles que se encuentra en el servicio de almacenamiento de datos

            return CreatedAtAction(nameof(GetMandril), 
                new { mandrilId = mandrilNuevo.Id },
                mandrilNuevo);//esta linea de codigo lo que hace es retornar un 201 Created, con la url de donde se puede obtener el mandril creado
        }

        //PUT - modificar
        [HttpPut("{mandrilId}")]
        public ActionResult<Mandril> PutMandril([FromRoute]int mandrilId, [FromBody]MandrilSimple mandrilInsert) //la diferencia enttre FromRoute y FromBody es que FromRoute obtiene el valor de la ruta y FromBody obtiene el valor del cuerpo de la peticion
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(c => c.Id == mandrilId);//buscamos el mandril con el id especificado
           
            if (mandril == null)
            {
                return NotFound("El mandril solicitado no existe");//si no se encuentra el mandril retornamos un error 404
            }

            mandril.Nombre = mandrilInsert.Nombre;//modificamos el nombre del mandril
            mandril.Apellido = mandrilInsert.Apellido;//modificamos el apellido del mandril

            return NoContent();//retornamos un 204 No Content para indicar que se ha modificado el mandril
        }

        //DELETE
        [HttpDelete("{mandrilId}")]
        public ActionResult DeleteMandril(int mandrilId)//metodo que se va a ejecutar cuando se haga una peticion delete
        {
            var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(c => c.Id == mandrilId);//buscamos el mandril con el id especificado
            if (mandril == null)
            {
                return NotFound("El mandril solicitado no existe");//si no se encuentra el mandril retornamos un error 404
            }
            MandrilDataStore.Current.Mandriles.Remove(mandril);//eliminamos el mandril de la lista de mandriles
            return NoContent();//retornamos un 204 No Content para indicar que se ha eliminado el mandril
        }
    }
}
