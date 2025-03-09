using MandrilAPI.Models;
using MandrilAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MandrilAPI.Controllers;

[ApiController]
[Route("api/mandril/{mandrilId}/[controller]")]
public class HabilidadController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Habilidad>> GetHabilidades(int mandrilId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(c => c.Id == mandrilId);//buscamos el mandril con el id especificado
        if (mandril == null)
        {
            return NotFound("El mandril solicitado no existe");//si no se encuentra el mandril retornamos un error 404
        }
        return Ok(mandril.Habilidades);
    }

    [HttpGet("{habilidadId}")]
    public ActionResult<Habilidad> GetHabilidad([FromRoute] int mandrilId, [FromRoute] int habilidadId)
    {
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(c => c.Id == mandrilId);//buscamos el mandril con el id especificado
        if (mandril == null)
        {
            return NotFound("El mandril solicitado no existe");//si no se encuentra el mandril retornamos un error 404
        }

        var habilidad = mandril?.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);//con el signo de pregunta nos asegurameos que lo que estamos buscando no sea nulo para ejecutarse
        if (habilidad == null)
        {
            return NotFound("La habilidad solicitada no existe");//si no se encuentra la habilidad retornamos un error 404
        }

        return Ok(habilidad);
    }

    [HttpPost]
    public ActionResult<Habilidad> PostHabilidad(int mandrilId, HabilidadSimple habilidadInsert)
    {
        //verificamos si el mandril existe
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(c => c.Id == mandrilId);//buscamos el mandril con el id especificado
        if (mandril == null)
        {
            return NotFound("El mandril solicitado no existe");//si no se encuentra el mandril retornamos un error 404
        }

        var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Nombre == habilidadInsert.Nombre);// esto es para ver si existe una habilidad con el mismo nombre
        if ( habilidadExistente != null)
        {
            return BadRequest("Ya existe esa habilidad con el mismo nombre");
        }

        var maxHabilidad = mandril.Habilidades.Max(h => h.Id);
        var HabilidadNueva = new Habilidad()
        {
            Id = maxHabilidad+1,
            Nombre = habilidadInsert.Nombre,
            Potencia = habilidadInsert.Potencia
        };// este codigo crea una nueva habilidad con el id maximo + 1, el nombre y la potencia que se le pasa por parametro

        mandril.Habilidades.Add(HabilidadNueva);//agregamos la habilidad a la lista de habilidades del mandril

        return CreatedAtAction(nameof(GetHabilidad),
            new { mandrilId = mandrilId, habilidadId = HabilidadNueva.Id },
            HabilidadNueva);// este codigo completo lo que hace es retornar un 201 Created, con la url de donde se puede obtener la habilidad creada
    }

    [HttpPut("{habilidadId}")]
    public ActionResult<Habilidad> PutHabilidad(int mandrilId, int habilidadId, HabilidadSimple habilidadInsert)//recibe los tres parametros, el id del mandril, el id de la habilidad y la habilidad que se va a modificar
    {
        // Validaciones
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);

        if (mandril == null)
            return NotFound("El mandril solicitado no existe");

        var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);
        if (habilidadExistente == null)
            return NotFound("La habilidad solicitada no existe");

        var habilidadMismoNombre = mandril.Habilidades?.FirstOrDefault(h => h.Id != habilidadId && h.Nombre == habilidadInsert.Nombre);// aca verificamos que exista una habilidad con el mismo nombre con otro id
        if (habilidadMismoNombre != null)
            return BadRequest("Ya existe otra habilidad con el mismo nombre");

        // Asignacion
        habilidadExistente.Nombre = habilidadInsert.Nombre;
        habilidadExistente.Potencia = habilidadInsert.Potencia;

        return NoContent();//el noContent es para indicar que se ha modificado la habilidad
    }

    [HttpDelete("{habilidadId}")]
    public ActionResult<Habilidad> DeleteHabilidad(int mandrilId, int habilidadId)
    {
        // Validamos que exista el mandril y la habilidad
        var mandril = MandrilDataStore.Current.Mandriles.FirstOrDefault(x => x.Id == mandrilId);
        if (mandril == null)
            return NotFound("El mandril solicitado no existe");

        var habilidadExistente = mandril.Habilidades?.FirstOrDefault(h => h.Id == habilidadId);
        if (habilidadExistente == null)
            return NotFound("La habilidad solicitada no existe");

        // Eliminacion
        mandril.Habilidades?.Remove(habilidadExistente);

        return NoContent();
    }
}