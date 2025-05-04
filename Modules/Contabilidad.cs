using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Contabilidad
    {
        public int codigo { get; set; }
        public double total_efectivo { get; set; }
        public DateTime fecha{ get; set; }

        //lista de reservaciones
        public List<Reservacion> reservaciones = new List<Reservacion>();

        //Datos de la empresa
        public Empresa datos_empresa { get; set; }

        public Contabilidad()
        {
        }
        public Contabilidad(int codigo, double total_efectivo, DateTime fecha, List<Reservacion> reservaciones, Empresa datos_empresa)
        {
            this.codigo = codigo;
            this.total_efectivo = total_efectivo;
            this.fecha = fecha;
            this.reservaciones = reservaciones;
            this.datos_empresa = datos_empresa;
        }
    }
}
