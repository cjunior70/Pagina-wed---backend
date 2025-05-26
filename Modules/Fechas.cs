using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class Fechas
    {
        public Fechas()
        {
        }

        public int codigo { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
        public Empresa Empresa { get; set; }

        public Fechas(int codigo, DateTime fecha, string estado, Empresa empresa)
        {
            this.codigo = codigo;
            this.fecha = fecha;
            this.estado = estado;
            Empresa = empresa;
        }
    }
}
