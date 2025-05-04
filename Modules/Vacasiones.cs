using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Vacasiones
    {
        public Vacasiones()
        {
        }

        public int codigo { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }

        public Vacasiones(int codigo, DateTime fecha_inicio, DateTime fecha_fin)
        {
            this.codigo = codigo;
            this.fecha_inicio = fecha_inicio;
            this.fecha_fin = fecha_fin;
        }
    }
}
