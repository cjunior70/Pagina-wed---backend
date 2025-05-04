using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Datos_Personales
    {
        public Datos_Personales()
        {
        }

        public int codigo { get; set; }
        public string cedula { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public char sexo { get; set; }
        public byte[] foto { get; set; }

        public Datos_Personales(int codigo, string cedula, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, string telefono, string correo, char sexo, byte[] foto)
        {
            this.codigo = codigo;
            this.cedula = cedula;
            this.primer_nombre = primer_nombre;
            this.segundo_nombre = segundo_nombre;
            this.primer_apellido = primer_apellido;
            this.segundo_apellido = segundo_apellido;
            this.telefono = telefono;
            this.correo = correo;
            this.sexo = sexo;
            this.foto = foto;
        }
    }
}
