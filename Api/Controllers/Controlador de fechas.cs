using Microsoft.AspNetCore.Mvc;
using Modules;
using Newtonsoft.Json;
using Services;
using System.Data;

namespace Api.Controllers
{
    public class Controlador_de_fechas : ControllerBase
    {

        //Instancia de la clase logica_de_los_usuarios globales
        Logica_de_las_fechas logica_De_Los_Usuarios = new Logica_de_las_fechas();

        //Esctura post para registrar un usuario
        [HttpPost("Post_Registrar_Una_Fecha/")]
        public ActionResult<Boolean> Post_Registrar_Usuario([FromBody] Fechas datos_de_la_fecha_a_guardar)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Fechas datos_usuario = new Fechas();

            datos_usuario = datos_de_la_fecha_a_guardar;

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confirmacion = logica_De_Los_Usuarios.registro_de_una_Fecha(datos_usuario, datos_de_conexion);

            return confirmacion;

        }

        //Esctura get para traer datos de una empresa
        [HttpGet("Get_fehcas_de_una_empresa/{codigo}")]
        public ActionResult<string> Get_Datos_de_una_empresa([FromRoute] string codigo)
        {

            Empresa datos_de_usuario = new Empresa();

            datos_de_usuario.codigo = Convert.ToInt32(codigo);

            DataTable Datos_de_usuario = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Datos_de_usuario = logica_De_Los_Usuarios.consultar_fechas_de_una_empresa(datos_de_usuario, datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(Datos_de_usuario);

            return (mensaje);

        }

        //Esctura put para traer datos de un solo usuario
        [HttpPut("Put_Actualizar_Fecha_de_una_empresa/")]

        //El frombody es para recibir el odjeto
        public ActionResult<Boolean> Put_Actualizar_Datos_Usuario([FromBody] Fechas datos_nuevo_de_la_fecha)
        {

            Boolean confimacion;

            Fechas datos_de_usuario = new Fechas();

            datos_de_usuario = datos_nuevo_de_la_fecha;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Usuarios.actualizar_fecha_de_un_empresa(datos_de_usuario, datos_de_conexion);

            return (confimacion);

        }

        //Esctura dalete para traer datos de un solo usuario
        [HttpDelete("Delete_una_fecha_de_la_empresa/{codigo}")]
        public ActionResult<Boolean> Delete_una_fecha_de_la_empresa([FromRoute] string codigo)
        {

            Boolean confimacion;

            Fechas datos_de_usuario = new Fechas();

            datos_de_usuario.codigo = Convert.ToInt32(codigo);

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Usuarios.borrar_fecha_de_una_empresa(datos_de_usuario, datos_de_conexion);

            return (confimacion);

        }

        /*
        //Esctura get para traer todos los usuario
        [HttpGet("Get_Traer_Todos_los_Usuario/")]
        public ActionResult<string> Get_Traer_Todos_los_Usuario()
        {
            DataTable lista_de_usuario = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            lista_de_usuario = logica_De_Las_reservaciones.consultar_todo_los_usuarios(datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(lista_de_usuario);

            return (mensaje);

        }

         */
    }
}
