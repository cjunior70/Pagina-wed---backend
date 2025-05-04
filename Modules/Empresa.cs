using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class Empresa
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public int extrellas { get; set; }
        public string correo { get; set; }
        public string Descripcion_de_la_Ubicacion { get; set; }
        public string whatsapp { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public byte[] imagen_en_miniatura { get; set; }
        public byte[] imagen_general { get; set; }
        public string comiezo_laboral { get; set; }
        public string finalizacion_laboral { get; set; }

        //Datos del usuario de la empresa
        public Usuario Usuario { get; set; }

        //Datos de la ubicacion de la empresa
        public Ubicacion Ubicacion { get; set; }

        //Lista de empleados de la empresa
        public List<Empleado> lista_de_empleados = new List<Empleado>();

        //Lista de servicios de la empresa
        public List<Servicios> lista_de_servicios = new List<Servicios>();

        //lista de contabilidad 
        public List<Contabilidad> lista_de_contabilidad = new List<Contabilidad>();

        //Datos de las vacaciones
        public Vacasiones vacaciones = new Vacasiones();

        //Lista reservaciones de la empresa
        public List<Reservacion> lista_de_reservaciones = new List<Reservacion>();

        //lista de fechas reservadas
        public List<Fechas> lista_de_Fechas = new List<Fechas>(); //Lista de fechas reservadas

        //Lista de reservaciones

        public Empresa()
        {
        }

        public Empresa(int codigo, string nombre, int extrellas, string correo, string descripcion_de_la_Ubicacion, string whatsapp, string facebook, string instagram, byte[] imagen_en_miniatura, byte[] imagen_general, string comiezo_laboral, string finalizacion_laboral, Usuario usuario, Ubicacion ubicacion, List<Empleado> lista_de_empleados, List<Servicios> lista_de_servicios, List<Contabilidad> lista_de_contabilidad, Vacasiones vacaciones, List<Reservacion> lista_de_reservaciones, List<Fechas> lista_de_Fechas)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.extrellas = extrellas;
            this.correo = correo;
            Descripcion_de_la_Ubicacion = descripcion_de_la_Ubicacion;
            this.whatsapp = whatsapp;
            this.facebook = facebook;
            this.instagram = instagram;
            this.imagen_en_miniatura = imagen_en_miniatura;
            this.imagen_general = imagen_general;
            this.comiezo_laboral = comiezo_laboral;
            this.finalizacion_laboral = finalizacion_laboral;
            Usuario = usuario;
            Ubicacion = ubicacion;
            this.lista_de_empleados = lista_de_empleados;
            this.lista_de_servicios = lista_de_servicios;
            this.lista_de_contabilidad = lista_de_contabilidad;
            this.vacaciones = vacaciones;
            this.lista_de_reservaciones = lista_de_reservaciones;
            this.lista_de_Fechas = lista_de_Fechas;
        }
    }
}
