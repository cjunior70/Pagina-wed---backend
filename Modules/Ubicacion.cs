using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class Ubicacion
    {

        public int codigo { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }

        public Ubicacion()
        {
        }

        public Ubicacion(int codigo, string latitud, string longitud)
        {
            this.codigo = codigo;
            this.latitud = latitud;
            this.longitud = longitud;
        }
    }
}
