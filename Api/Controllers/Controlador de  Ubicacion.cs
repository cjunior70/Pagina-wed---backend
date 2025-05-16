using Microsoft.AspNetCore.Mvc;
using Modules;
using Newtonsoft.Json;
using Services;
using System.Data;

namespace Api.Controllers
{
    public class Controlador_de__Ubicacion : ControllerBase
    {
        //Instancia de la clase logica_de_los_usuarios globales
        logica_de_las_ubicaciones logica_De_La_Ubicacion = new logica_de_las_ubicaciones();

        //Esctura post para registrar un usuario
        [HttpPost("Post_Registrar_Ubicacion/")]
        public ActionResult<Boolean> Post_Registrar_Ubicacion([FromBody] Ubicacion datos_de_la_ubicacion_a_registrar)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Ubicacion datos_ubicacion = new Ubicacion();

            // datos_ubicacion = datos_de_la_ubicacion_a_registrar;

            datos_ubicacion.latitud = "hola";
            datos_ubicacion.longitud = "hol2a";

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confirmacion = logica_De_La_Ubicacion.registrar_una_ubicacion(datos_ubicacion, datos_de_conexion);

            return confirmacion;

        }

        //Esctura get para traer datos de un solo usuario
        [HttpGet("Get_Datos_de_una_ubicacion/{codigo}")]
        public ActionResult<string> Get_Datos_de_una_ubicacion([FromRoute] string codigo)
        {

            Ubicacion datos_de_ubicacion = new Ubicacion();

            datos_de_ubicacion.codigo = Convert.ToInt32(codigo);

            DataTable Datos_de_usuario = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Datos_de_usuario = logica_De_La_Ubicacion.buscar_una_ubicacion_por_codigo(datos_de_ubicacion, datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(Datos_de_usuario);

            return (mensaje);

        }

        //Esctura get para traer todos los usuario
        [HttpGet("Get_Traer_Todos_las_ubicaciones/")]
        public ActionResult<string> Get_Traer_Todos_las_ubicaciones()
        {
            DataTable lista_de_usuario = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            lista_de_usuario = logica_De_La_Ubicacion.consultar_todas_las_ubicaciones(datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(lista_de_usuario);

            return (mensaje);

        }


        //Esctura put para traer datos de un solo usuario
        [HttpPut("Put_Actualizar_Datos_de_ubicacion/")]

        //El frombody es para recibir el odjeto
        public ActionResult<Boolean> Put_Actualizar_Datos_de_ubicacion([FromBody] Ubicacion datos_nuevos_de_la_ubicacion)
        {

            Boolean confimacion;

            Ubicacion datos_de_ubicacion = new Ubicacion();

            datos_de_ubicacion = datos_nuevos_de_la_ubicacion;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_La_Ubicacion.actualizar_ubicacion(datos_de_ubicacion, datos_de_conexion);

            return (confimacion);

        }

        //Esctura dalete para traer datos de un solo usuario
        [HttpDelete("Delete_datos_de_un_ubicacion/{codigo}")]
        public ActionResult<Boolean> Delete_datos_de_un_ubicacion([FromRoute] string codigo)
        {

            Boolean confimacion;


            Ubicacion datos_de_ubicacion = new Ubicacion();

            datos_de_ubicacion.codigo = Convert.ToInt32(codigo);

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_La_Ubicacion.borrar_una_ubicacion(datos_de_ubicacion, datos_de_conexion);

            return (confimacion);

        }
    }
}
