using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estudiantes.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController(EstudiantesDbContext context) : ControllerBase
    {
        private readonly EstudiantesDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            if (estudiantes.Any())
            {
                return Ok(new Response<IEnumerable<Estudiantes>>
                {
                    IsSuccess = true,
                    Result = estudiantes,
                    Message = "Listado de estudiantes"
                });
            }
            return Ok(new Response<IEnumerable<Estudiantes>>
            {
                IsSuccess = false,
                Message = "No hay registros para mostrar",
                Result = []
            });
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearActualizar model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<CrearActualizar>
                {
                    IsSuccess = false,
                    Result = model,
                    Message = "Los campos no son correctos"
                });
            }
            var estudianteNuevo = new Estudiantes
            {
                Nombre = model.Nombre,
                Edad = model.Edad,
                Telefono = model.Telefono,
                Correo = model.Correo,
                Documento = model.Documento,
                Curso = model.Curso,
                Genero = model.Genero,
            };
            await _context.Estudiantes.AddAsync(estudianteNuevo);
            await _context.SaveChangesAsync();

            return Ok(new Response<Estudiantes>
            {
                IsSuccess = true,
                Result = estudianteNuevo,
                Message = "Estudiante creado correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new Response<Estudiantes>
                {
                    IsSuccess = false,
                    Message = "El id es necesario",
                    Result = null
                });
            }

            var estudiante = await GetEstudiante(id);
            if (estudiante !=null)
            {
                return Ok(new Response<Estudiantes>
                {
                    IsSuccess = true,
                    Message = "Se encontró un estudiante",
                    Result = estudiante,
                });
            }
            return NotFound(new Response<Estudiantes>
            {
                IsSuccess = false,
                Message = "No hay coincidencias",
                Result = null
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new Response<Estudiantes>
                {
                    IsSuccess = false,
                    Message = "El id no es correcto",
                    Result = null
                });
            }
            var estudiante = await GetEstudiante(id);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();

                return Ok(new Response<Estudiantes>
                {
                    Result = estudiante,
                    IsSuccess = true,
                    Message = $"Se ha eliminado el estudiante {estudiante.Nombre}"
                });
            }
            return NotFound(new Response<Estudiantes>
            {
                IsSuccess = false,
                Message = "No hay coincidencias",
                Result = estudiante
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CrearActualizar model)
        {

            //El id siempre debe estar
            if (id ==Guid.Empty)
            {
                return BadRequest(new Response<CrearActualizar>
                {
                    IsSuccess = false,
                    Message = "El id es necesario",
                    Result = model
                });
            }
            if (ModelState.IsValid)
            {
                var estudiante = await GetEstudiante(id);
                if (estudiante != null)
                {
                    estudiante.Nombre = model.Nombre;
                    estudiante.Edad = model.Edad;
                    estudiante.Telefono = model.Telefono;
                    estudiante.Correo = model.Correo;
                    estudiante.Documento = model.Documento;
                    estudiante.Curso = model.Curso;
                    estudiante.Genero = model.Genero;

                    _context.Estudiantes.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                return Ok(new Response<CrearActualizar>
                {
                    IsSuccess = true,
                    Message = "Estudiante actualizado",
                    Result = model
                });
            }
            return BadRequest(new Response<CrearActualizar>
            {
                IsSuccess = false,
                Message = "NO se pudo actualizar el registro",
                Result = model
            });
        }

        private async Task<Estudiantes> GetEstudiante(Guid id)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Id == id);
            return estudiante;
        }
    }
}
