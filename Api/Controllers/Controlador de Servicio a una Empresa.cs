using Microsoft.AspNetCore.Mvc;
using Modules;
using Newtonsoft.Json;
using Services;
using System.Data;

namespace Api.Controllers
{
    public class Controlador_de_Servicio_a_una_Empresa : ControllerBase
    {
        //Instancia de la clase logica_de_los_usuarios globales
        logica_de_los_usuarios logica_De_Los_Usuarios = new logica_de_los_usuarios();

        //Esctura post para registrar un usuario
        [HttpPost("Post_Registrar_Usuario/")]
        public ActionResult<Boolean> Post_Registrar_Usuario([FromBody] Usuario datos_del_usuario_a_ingresar)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Usuario datos_usuario = new Usuario();

            datos_usuario = datos_del_usuario_a_ingresar;

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confirmacion = logica_De_Los_Usuarios.registro_de_un_usuario(datos_usuario, datos_de_conexion);

            return confirmacion;

        }

        //Esctura get para traer todos los usuario
        [HttpGet("Get_Traer_Todos_los_Usuario/")]
        public ActionResult<string> Get_Traer_Todos_los_Usuario()
        {
            DataTable lista_de_usuario = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            lista_de_usuario = logica_De_Los_Usuarios.consultar_todo_los_usuarios(datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(lista_de_usuario);

            return (mensaje);

        }

        //Esctura get para traer datos de un solo usuario
        [HttpGet("Get_Datos_de_un_Usuario/{codigo}")]
        public ActionResult<string> Get_Datos_de_un_Usuario([FromRoute] string codigo)
        {

            Usuario datos_de_usuario = new Usuario();

            datos_de_usuario.codigo = Convert.ToInt32(codigo);

            DataTable Datos_de_usuario = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Datos_de_usuario = logica_De_Los_Usuarios.consulta_De_datos_personales_por_codigo(datos_de_usuario, datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(Datos_de_usuario);

            return (mensaje);

        }

        //Esctura put para traer datos de un solo usuario
        [HttpPut("Put_Actualizar_Datos_Usuario/")]

        //El frombody es para recibir el odjeto
        public ActionResult<Boolean> Put_Actualizar_Datos_Usuario([FromBody] Usuario datos_Del_usuario)
        {

            Boolean confimacion;

            Usuario datos_de_usuario = new Usuario();


            datos_de_usuario = datos_Del_usuario;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Usuarios.actualizar_datos_de_un_usuario(datos_de_usuario, datos_de_conexion);

            return (confimacion);

        }

        //Esctura dalete para traer datos de un solo usuario
        [HttpDelete("Delete_datos_del_usuario/{codigo}")]
        public ActionResult<Boolean> Delete_datos_del_usuario([FromRoute] string codigo)
        {

            Boolean confimacion;


            Usuario datos_de_usuario = new Usuario();

            datos_de_usuario.codigo = Convert.ToInt32(codigo);

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Usuarios.borrar_datos_de_un_usuario(datos_de_usuario, datos_de_conexion);

            return (confimacion);

        }
    }
}
