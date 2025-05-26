using Data;
using Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Logica_de_las_fechas
    {

        //Llamada de la funcion del dal para ingresar a este los datos a la base
        Funciones_de_las_fechas funciones_De_la_fecha = new Funciones_de_las_fechas();

        //Funcion para el ingreso de datos de un usuario
        public Boolean registro_de_una_Fecha(Fechas datos_de_usuario, Datos_login datos_de_conexion)
        {

            //Variabale para la confirmacion de ninguno error ajeno
            Boolean confirmacion;

            confirmacion = funciones_De_la_fecha.Ingresar_Una_fecha(datos_de_conexion, datos_de_usuario);

            return confirmacion;

        }

        //Funcion para poder consultar datos personales por codigo
        public DataTable consultar_fechas_de_una_empresa(Empresa datos_de_la_empresa, Datos_login datos_de_conexion)
        {
            //Variable para saber la datos_del_usuario de alguien ya registrado
            DataTable datos_del_usuario = new DataTable();

            datos_del_usuario = funciones_De_la_fecha.Consultar_Un_usuario_por_su_codigo(datos_de_conexion, datos_de_la_empresa);

            return datos_del_usuario;
        }

        //Funcion para consultar todos los usuarios
        public DataTable consultar_todo_los_usuarios(Datos_login datos_de_conexion)
        {

            //Variable para saber la datos_del_usuario de alguien ya registrado
            DataTable existencia = new DataTable();

            existencia = funciones_De_la_fecha.Consultar_Usuarios(datos_de_conexion);

            return existencia;
        }

        //Funcion para actualizar datos de un usuario
        public Boolean actualizar_fecha_de_un_empresa(Fechas datos_nuevo_De_la_fecha, Datos_login datos_de_conexion)
        {

            //Variable para saber la datos_del_usuario de alguien ya registrado
            Boolean existencia;

            existencia = funciones_De_la_fecha.Modificar_datos_del_usuario(datos_de_conexion, datos_nuevo_De_la_fecha);

            return existencia;
        }

        //Funcion para borrar datos de un usuario
        public Boolean borrar_fecha_de_una_empresa(Fechas datos_nuevo_Del_Usuario, Datos_login datos_de_conexion)
        {

            //Variable para saber la datos_del_usuario de alguien ya registrado
            Boolean existencia;

            existencia = funciones_De_la_fecha.borrar_una_fechas(datos_de_conexion, datos_nuevo_Del_Usuario);


            return existencia;
        }

        //    //Funcion para poder consultar datos personales por cedula
        //    public DataTable consulta_De_datos_personales(Usuario datos_de_la_empresa, Datos_login datos_de_conexion)
        //    {
        //        //Llamada de la funcion del dal para ingresar a este los datos a la base
        //        Funciones_del_Horario funciones_Del_horarios = new Funciones_del_Horario();

        //        //Variable para saber la datos_del_usuario de alguien ya registrado
        //        DataTable datos_del_usuario=new DataTable();

        //        datos_del_usuario = funciones_Del_horarios.Consultar_Un_usuario(datos_de_conexion, datos_de_la_empresa);

        //        return datos_del_usuario;
        //    }



    }
}
