using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Modules;

namespace Services
{
    public class logica_de_los_servicios_de_una_empresa
    {

        Funciones_para_agregar_un_servicio_a_una_empresa funcios_para_los_servicios_De_una_empresa = new Funciones_para_agregar_un_servicio_a_una_empresa();

        //Funcion para poder registraar un servicio a una empresa
        public Boolean registrar_servicio_a_una_empresa(Empresa datos_del_servicio, Datos_login datos_de_conexion)
        {
            Boolean confirmacion;

            confirmacion = funcios_para_los_servicios_De_una_empresa.Ingresar_Un_Servicio_a_una_Empresa(datos_del_servicio,datos_de_conexion);

            return confirmacion;
        }

        //Funcion para traer todos los servicios de una empresa
        public DataTable traer_todos_los_servicios_de_las_empresas(Datos_login datos_de_conexion, Empresa datos_de_la_empresa)
        {
            DataTable datos;

            datos = funcios_para_los_servicios_De_una_empresa.Consultar_servicios_y_empresas_relacionados(datos_de_conexion, datos_de_la_empresa);

            return datos;
        }


        //Funcion para actualizar datos de un servicio de una empresa
        public Boolean actualizacion_de_datos_de_un_servicio_De_una_empresa (Empresa datos_De_la_empresa, Datos_login datos_de_conexion, string codigo_del_servicio)
        {
            Boolean confirmacion=new Boolean();

            confirmacion = funcios_para_los_servicios_De_una_empresa.modificar_datos_de_un_servicio_de_una_empresa(datos_de_conexion, datos_De_la_empresa, codigo_del_servicio);

            return confirmacion;

        }

        //Funcion para eliminar un servicio de una empresa
        public Boolean eliminar_una_reservacion(string codigo, Datos_login datos_de_conexion)
        {
            Boolean confirmacion;

            confirmacion = funcios_para_los_servicios_De_una_empresa.borrar_un_servicio_de_una_empresa(datos_de_conexion, codigo);

            return confirmacion;

        }

        //    //Funcion para traer los servicios de una empresa
        //    public DataTable traer_los_servicios_de_una_empresa(Datos_login datos_de_conexion, Servicio_de_una_Empresa datos_de_la_reservacion)
        //    {
        //        DataTable datos;

        //        datos = funcios_para_los_servicios_De_una_empresa.Consultar_Un_Servicio_de_una_empresa(datos_de_conexion, datos_de_la_reservacion);

        //        return datos;
        //    }



    }
}
