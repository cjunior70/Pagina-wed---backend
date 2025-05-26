using Microsoft.AspNetCore.Mvc;
using Modules;
using Newtonsoft.Json;
using Services;
using System.Data;

namespace Api.Controllers
{
    public class Controlador_de_Servicios : ControllerBase
    {
        //Instancia de la clase logica_de_los_usuarios globales
        logica_de_los_servicios logica_De_Los_servicios = new logica_de_los_servicios();

        //Esctura post para registrar un usuario
        [HttpPost("Post_Registrar_Un_Servicio/")]
        public ActionResult<Boolean> Post_Registrar_Un_Servicio([FromBody] Servicios datos_del_servicio)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Servicios datos_usuario = new Servicios();

            datos_usuario = datos_del_servicio;

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confirmacion = logica_De_Los_servicios.registrar_un_servicio(datos_usuario, datos_de_conexion);

            return confirmacion;

        }

        //Esctura get para traer todos los usuario
        [HttpGet("Get_Traer_Todos_los_Nombres_de_los_servicios/")]
        public ActionResult<string> Get_Traer_Todos_los_Nombres_de_los_servicios()
        {
            DataTable lista_de_servicios = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            lista_de_servicios = logica_De_Los_servicios.funcion_para_consultar_todos_los_servicios(datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(lista_de_servicios);

            return (mensaje);

        }

        //no veo necesario traer solo un servicio ya que este solo tiene codigo y nombre en la base de datos

        ////Esctura get para traer datos de un solo usuario
        //[HttpGet("Get_Datos_de_un_servicio/{codigo}")]
        //public ActionResult<string> Get_Datos_de_un_Usuario([FromRoute] string codigo)
        //{

        //    Usuario datos_de_usuario = new Usuario();

        //    datos_de_usuario.codigo = Convert.ToInt32(codigo);

        //    DataTable Datos_de_usuario = new DataTable();

        //    Datos_login datos_de_conexion = new Datos_login();

        //    datos_de_conexion.usuario = "admin";
        //    datos_de_conexion.constraseña = "admin";

        //    Datos_de_usuario = logica_De_Los_servicios.consultar_fechas_de_una_empresa(datos_de_usuario, datos_de_conexion);

        //    string mensaje = JsonConvert.SerializeObject(Datos_de_usuario);

        //    return (mensaje);

        //}

        //Esctura put para actualizar datos de un solo usuario
        [HttpPut("Put_Actualizar_Un_Servicio/")]

        //El frombody es para recibir el odjeto
        public ActionResult<Boolean> Put_Actualizar_Un_Servicio([FromBody] Servicios datos_del_servicio)
        {

            Boolean confimacion;

            Servicios datos_de_usuario = new Servicios();


            datos_de_usuario = datos_del_servicio;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_servicios.actualizar_un_servicio(datos_de_usuario, datos_de_conexion);

            return (confimacion);

        }

        //Esctura dalete para eliminar datos de un servicio
        [HttpDelete("Delete_un_servicio/{codigo}")]
        public ActionResult<Boolean> Delete_un_servicio([FromRoute] string codigo)
        {

            Boolean confimacion;

            Servicios datos_de_usuario = new Servicios();

            datos_de_usuario.codigo = Convert.ToInt32(codigo);

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_servicios.eliminar_un_servicio(datos_de_usuario, datos_de_conexion);

            return (confimacion);


        }
    }
}
