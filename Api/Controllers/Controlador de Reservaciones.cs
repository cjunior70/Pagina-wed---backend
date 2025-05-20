using Microsoft.AspNetCore.Mvc;
using Modules;
using Newtonsoft.Json;
using Services;
using System.Data;

namespace Api.Controllers
{
    public class Controlador_de_Reservaciones : ControllerBase
    {
        //Instancia de la clase logica_de_los_usuarios globales
        logica_de_las_reservaciones logica_De_Los_Usuarios = new logica_de_las_reservaciones();

        //Esctura post para registrar un usuario
        [HttpPost("Post_Registrar_Una_Reservacion/")]
        public ActionResult<Boolean> Post_Registrar_Una_Reservacion([FromBody] Reservacion datos_de_la_reservacion)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Reservacion datos_da_la_reservacion = new Reservacion();

            datos_da_la_reservacion = datos_de_la_reservacion;

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confirmacion = logica_De_Los_Usuarios.registrar_una_reservacion(datos_de_conexion, datos_da_la_reservacion);

            return confirmacion;

        }

        //Esctura get para traer datos de un solo usuario
        [HttpGet("Get_Reservaciones_de_un_cliente/{codigo}")]
        public ActionResult<string> Get_Datos_de_un_Usuario([FromRoute] string codigo)
        {

            Reservacion datos_de_la_reservacion = new Reservacion();

            Cliente datos = new Cliente();
            datos.codigo = Convert.ToInt32(codigo);

            datos_de_la_reservacion.datos_cliente = datos;

            DataTable Lista_de_reservaciones = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Lista_de_reservaciones = logica_De_Los_Usuarios.todas_las_reservaciones(datos_de_conexion, datos_de_la_reservacion);

            string mensaje = JsonConvert.SerializeObject(Lista_de_reservaciones);

            return (mensaje);

        }

        //Esctura get para traer datos de un solo usuario
        [HttpGet("Get_Reservaciones_de_una_empresa/{codigo}")]
        public ActionResult<string> Get_Reservaciones_de_una_empresa([FromRoute] string codigo)
        {

            Reservacion datos_de_la_reservacion = new Reservacion();

            Empresa datos = new Empresa();
            datos.codigo = Convert.ToInt32(codigo);

            datos_de_la_reservacion.datos_empresa = datos;

            DataTable Lista_de_reservaciones = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Lista_de_reservaciones = logica_De_Los_Usuarios.todas_las_reservaciones_de_una_empresa(datos_de_conexion, datos_de_la_reservacion);

            string mensaje = JsonConvert.SerializeObject(Lista_de_reservaciones);

            return (mensaje);

        }

        //Esctura put para traer datos de un solo usuario
        [HttpPut("Put_Actualizar_Datos_de_una_reservacion/")]

        //El frombody es para recibir el odjeto
        public ActionResult<Boolean> Put_Actualizar_Datos_de_una_reservacion([FromBody] Reservacion datos_De_la_reservacion)
        {

            Boolean confimacion;

            Reservacion datos_de_la_reservacion = new Reservacion();

            datos_de_la_reservacion = datos_De_la_reservacion;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Usuarios.actualizar_reservacion(datos_de_conexion, datos_de_la_reservacion);

            return (confimacion);

        }


        //Esctura dalete para traer datos de un solo usuario
        [HttpDelete("Delete_Una_reservacion/{codigo}")]
        public ActionResult<Boolean> Delete_Una_reservacion([FromRoute] string codigo)
        {

            Boolean confimacion;

            Reservacion datos_de_la_reservacion = new Reservacion();

            datos_de_la_reservacion.codigo = Convert.ToInt32(codigo);

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Usuarios.eliminar_reservacion(datos_de_conexion, datos_de_la_reservacion);

            return (confimacion);


            /*
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

            */

        }
    }
}
