using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Horario
    {
        public Horario()
        {
        }

        public int codigo { get; set; }
        public string dia_de_la_semana { get; set; }
        public string turno_de_la_mañana { get; set; }
        public string turno_de_la_tarde { get; set; }
        public string turno_de_la_noche { get; set; }

        //Datos del empleado
        public Empleado empleado { get; set; }
        
        public Horario(int codigo, string dia_de_la_semana, string turno_de_la_mañana, string turno_de_la_tarde, string turno_de_la_noche, Empleado empleado)
        {
            this.codigo = codigo;
            this.dia_de_la_semana = dia_de_la_semana;
            this.turno_de_la_mañana = turno_de_la_mañana;
            this.turno_de_la_tarde = turno_de_la_tarde;
            this.turno_de_la_noche = turno_de_la_noche;
            this.empleado = empleado;
        }
    }
}
