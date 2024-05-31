using System.ComponentModel.DataAnnotations;

namespace Estudiantes.Backend
{
    public class Estudiantes
    {
        //Entity - Tabla Db
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Edad { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Curso { get; set; }
    }
    public class CrearActualizar
    {
        //Actualizar - Update

        //Anotaciones de datos / validar campos
        [Required(ErrorMessage = "El campo {0} no es correcto")]
        [MinLength(5,ErrorMessage ="El campo {0} debe tener al menos {1} letras")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} no es correcto")]
        [MinLength(5,ErrorMessage ="El campo {0} debe tener al menos {1} letras")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El campo {0} no es correcto")]
        [MinLength(2,ErrorMessage ="El campo {0} debe tener al menos {1} letras")]
        public string Edad { get; set; }

        [Required(ErrorMessage = "El campo {0} no es correcto")]
        [MinLength(3,ErrorMessage ="El campo {0} debe tener al menos {1} letras")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "El campo {0} no es correcto")]
        [MinLength(5,ErrorMessage ="El campo {0} debe tener al menos {1} letras")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0} no es correcto")]
        [MinLength(5,ErrorMessage ="El campo {0} debe tener al menos {1} letras")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} no es correcto")]
        [MinLength(2,ErrorMessage ="El campo {0} debe tener al menos {1} letras")]
        public string Curso { get; set; }
    }
}
