using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Empleado:Datos_Personales
    {

        public DateTime fecha_de_inicio { get; set; }
        public DateTime fecha_de_actual { get; set; }
        public string cargo { get; set; }
        public string estado { get; set; }
        public string estacion { get; set; }
        public double porcentaje_comision { get; set; }

        //Datos de la empresa
        public Empresa Empresa { get; set; }

        //Datos del horario
        public List<Horario> Horario = new List<Horario>();

        //Datos de las vaciones
        public Vacasiones Vacasiones { get; set; }

       //Lista de reservaciones 
        public List<Reservacion> lista_De_reservaciones = new List<Reservacion>();

        //Lista de servicios
        public List<Servicios> lista_De_servicios = new List<Servicios>();

        public Empleado()
        {
        }

        public Empleado(int codigo, string cedula, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, string telefono, string correo, char sexo, byte[] foto) : base(codigo, cedula, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, telefono, correo, sexo, foto)
        {
        }

        public Empleado(DateTime fecha_de_inicio, DateTime fecha_de_actual, string cargo, string estado, string estacion, double porcentaje_comision, Empresa empresa, List<Horario> horario, Vacasiones vacasiones, List<Reservacion> lista_De_reservaciones, List<Servicios> lista_De_servicios)
        {
            this.fecha_de_inicio = fecha_de_inicio;
            this.fecha_de_actual = fecha_de_actual;
            this.cargo = cargo;
            this.estado = estado;
            this.estacion = estacion;
            this.porcentaje_comision = porcentaje_comision;
            Empresa = empresa;
            Horario = horario;
            Vacasiones = vacasiones;
            this.lista_De_reservaciones = lista_De_reservaciones;
            this.lista_De_servicios = lista_De_servicios;
        }
    }
}
