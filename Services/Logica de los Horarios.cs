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
    public class Logica_de_los_Horarios
    {

        //Llamada de la funcion del dal para ingresar a este los datos a la base
        funciones_del_horario funciones_Del_horarios = new funciones_del_horario();

        //Funcion para el ingreso de datos de un usuario
        public Boolean registro_de_un_horario(Horario datos_de_usuario, Datos_login datos_de_conexion)
        {

            //Variabale para la confirmacion de ninguno error ajeno
            Boolean confirmacion;

            confirmacion = funciones_Del_horarios.Ingresar_Un_Horario(datos_de_conexion, datos_de_usuario);

            return confirmacion;

        }

        //Funcion para poder consultar todos los horarios de un empleado por codigo
        public DataTable consultar_el_horario_De_un_empleado(Horario datos_de_usuario, Datos_login datos_de_conexion)
        {

            //Variable para saber la datos_del_usuario de alguien ya registrado
            DataTable datos_del_usuario = new DataTable();

            datos_del_usuario = funciones_Del_horarios.Consultar_Un_horario_de_un_empleado(datos_de_conexion, datos_de_usuario);

            return datos_del_usuario;
        }

        //Funcion para poder actualizar datos del horario de un empleado por codigo
        public Boolean actualizar_el_horario_De_un_empleado(Horario datos_de_usuario, Datos_login datos_de_conexion)
        {

            //Variable para saber la datos_del_usuario de alguien ya registrado
            Boolean datos_del_usuario = new Boolean();

            datos_del_usuario = funciones_Del_horarios.Modificar_horario_de_un_empleado(datos_de_conexion, datos_de_usuario);

            return datos_del_usuario;
        }

        //Funcion para poder borrar un horario de un empleado por codigo
        public Boolean borrar_horario_de_un_empleado(Horario datos_de_usuario, Datos_login datos_de_conexion)
        {

            //Variable para saber la datos_del_usuario de alguien ya registrado
            Boolean datos_del_usuario = new Boolean();

            datos_del_usuario = funciones_Del_horarios.borrar_un_horario(datos_de_conexion, datos_de_usuario);

            return datos_del_usuario;
        }

    }
}
