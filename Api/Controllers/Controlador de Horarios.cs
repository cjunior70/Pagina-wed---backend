using Microsoft.AspNetCore.Mvc;
using Modules;
using Newtonsoft.Json;
using Services;
using System.Data;

namespace Api.Controllers
{
    public class Controlador_de_Horarios : ControllerBase
    {
        //Instancia de la clase logica_de_los_usuarios globales
        Logica_de_los_Horarios logica_De_Los_Horarios = new Logica_de_los_Horarios();

        //Esctura post para registrar un usuario
        [HttpPost("Post_Registrar_Un_Horario_a_un_empleado/")]
        public ActionResult<Boolean> Post_Registrar_Un_Horario_a_un_empleadov([FromBody] Horario datos_del_horario_del_empleado)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Horario datos_horario = new Horario();

           datos_horario = datos_del_horario_del_empleado;

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confirmacion = logica_De_Los_Horarios.registro_de_un_horario(datos_horario, datos_de_conexion);

            return confirmacion;

        }

        ////Esctura get para traer todos los usuario
        //[HttpGet("Get_Traer_Todos_los_Usuario/")]
        //public ActionResult<string> Get_Traer_Todos_los_Usuario()
        //{
        //    DataTable lista_de_usuario = new DataTable();

        //    Datos_login datos_de_conexion = new Datos_login();

        //    datos_de_conexion.usuario = "admin";
        //    datos_de_conexion.constraseña = "admin";

        //    lista_de_usuario = logica_De_Los_Horarios.consultar_todo_los_usuarios(datos_de_conexion);

        //    string mensaje = JsonConvert.SerializeObject(lista_de_usuario);

        //    return (mensaje);

        //}

        //Esctura get para traer datos de un solo usuario
        [HttpGet("Get_Datos_de_los_horario_de_un_empleado/{codigo}")]
        public ActionResult<string> Get_Datos_de_un_Usuario([FromRoute] string codigo)
        {

            Horario datos_de_usuario = new Horario();

            datos_de_usuario.codigo = Convert.ToInt32(codigo);

            DataTable Datos_de_usuario = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Datos_de_usuario = logica_De_Los_Horarios.consultar_el_horario_De_un_empleado(datos_de_usuario, datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(Datos_de_usuario);

            return (mensaje);

        }

        //Esctura put para traer datos de un solo usuario
        [HttpPut("Put_Actualizar_Datos_de_un_horario/")]

        //El frombody es para recibir el odjeto
        public ActionResult<Boolean> Put_Actualizar_Datos_de_un_horario([FromBody] Horario datos_Del_usuario)
        {

            Boolean confimacion;

            Horario datos_de_usuario = new Horario();

            datos_de_usuario = datos_Del_usuario;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Horarios.actualizar_el_horario_De_un_empleado(datos_de_usuario, datos_de_conexion);

            return (confimacion);

        }

        //Esctura dalete para traer datos de un solo usuario
        [HttpDelete("Delete_horario_de_un_empleado/{codigo}")]
        public ActionResult<Boolean> Delete_horario_de_un_empleado([FromRoute] string codigo)
        {

            Boolean confimacion;

            Horario datos_del_horario = new Horario();

            datos_del_horario.codigo = Convert.ToInt32(codigo);

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Horarios.borrar_horario_de_un_empleado(datos_del_horario, datos_de_conexion);

            return (confimacion);

        }
    }
}
