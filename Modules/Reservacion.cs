using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class Reservacion
    {
        public int codigo { get; set; }
        public DateTime creacion { get; set; }
        public DateTime fecha_reservacion { get; set; }
        public double pago_total { get; set; }
        public string estado { get; set; }
        public string comentario { get; set; }
        public string extrellas { get; set; }

        //Lista de servicios 
        public List<Servicios> servicios = new List<Servicios>();

        //Lista de empleados
        public List<Empleado> empleados = new List<Empleado>();

        //Datos de la empresa
        public Empresa datos_empresa { get; set; }

        //Datos del cliente 
        public Cliente datos_cliente { get; set; }

        //Datos de contabilidad
        public Contabilidad datos_contabilidad { get; set; }

        public Reservacion()
        {
        }

        public Reservacion(int codigo, DateTime creacion, DateTime fecha_reservacion, double pago_total, string estado, string comentario, string extrellas, List<Servicios> servicios, List<Empleado> empleados, Empresa datos_empresa, Cliente datos_cliente, Contabilidad datos_contabilidad)
        {
            this.codigo = codigo;
            this.creacion = creacion;
            this.fecha_reservacion = fecha_reservacion;
            this.pago_total = pago_total;
            this.estado = estado;
            this.comentario = comentario;
            this.extrellas = extrellas;
            this.servicios = servicios;
            this.empleados = empleados;
            this.datos_empresa = datos_empresa;
            this.datos_cliente = datos_cliente;
            this.datos_contabilidad = datos_contabilidad;
        }
    }
}
