using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Servicios
    {

        public int codigo { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public TimeSpan tiempo_promedio {get; set; }

        //Lista de empresa relacionada con los servicios
        public List<Empresa> lista_de_empresas_con_servicios =new List<Empresa>();

        //Lista de servicios de un empleado
        public List<Empleado> lista_de_empleados_con_servicios = new List<Empleado>();

        //Lista de reservaciones relacionadas con los servicios
        public List<Reservacion> lista_de_reservaciones_con_servicios = new List<Reservacion>();

        public Servicios()
        {
        }

        public Servicios(int codigo, string nombre, double precio, TimeSpan tiempo_promedio, List<Empresa> lista_de_empresas_con_servicios, List<Empleado> lista_de_empleados_con_servicios, List<Reservacion> lista_de_reservaciones_con_servicios)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.precio = precio;
            this.tiempo_promedio = tiempo_promedio;
            this.lista_de_empresas_con_servicios = lista_de_empresas_con_servicios;
            this.lista_de_empleados_con_servicios = lista_de_empleados_con_servicios;
            this.lista_de_reservaciones_con_servicios = lista_de_reservaciones_con_servicios;
        }
    }
}
