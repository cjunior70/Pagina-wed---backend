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
        logica_de_los_servicios_de_una_empresa logica_De_Los_Servicios_de_una_empresa = new logica_de_los_servicios_de_una_empresa();

        //Esctura Post_Registrar_Un_Servicio_a_una_Empresa
        [HttpPost("Post_Registrar_Un_Servicio_a_una_Empresa/")]
        public ActionResult<Boolean> Post_Registrar_Un_Servicio_a_una_Empresa([FromBody] Empresa datos_de_los_servicios_de_una_empresa)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Empresa datos_empresa = new Empresa();

            datos_empresa = datos_de_los_servicios_de_una_empresa;

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confirmacion = logica_De_Los_Servicios_de_una_empresa.registrar_servicio_a_una_empresa(datos_empresa, datos_de_conexion);

            return confirmacion;

        }

        //Esctura get para traer todos los usuario
        [HttpGet("Get_Traer_Todos_servicios_de_una_empresa/{codigo}")]
        public ActionResult<string> Get_Traer_Todos_los_Usuario([FromRoute] string codigo)
        {
            Empresa datos_de_la_empresa = new Empresa();
           
            datos_de_la_empresa.codigo = Convert.ToInt32(codigo);

            DataTable lista_de_servicios_De_una_empresa = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            lista_de_servicios_De_una_empresa = logica_De_Los_Servicios_de_una_empresa.traer_todos_los_servicios_de_las_empresas(datos_de_conexion, datos_de_la_empresa);

            string mensaje = JsonConvert.SerializeObject(lista_de_servicios_De_una_empresa);

            return (mensaje);

        }

        //Esctura put para traer datos de un solo usuario
        [HttpPut("Put_Actualizar_Un_Servicio_de_una_empresa/{codigo_del_servicio}")]

        //El frombody es para recibir el odjeto
        public ActionResult<Boolean> Put_Actualizar_Un_Servicio_de_una_empresa([FromBody] Empresa datos_De_la_empresa, [FromRoute] string codigo_del_servicio)
        {

            Boolean confimacion;

            Empresa datos_de_usuario = new Empresa();

            datos_de_usuario = datos_De_la_empresa;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Servicios_de_una_empresa.actualizacion_de_datos_de_un_servicio_De_una_empresa(datos_de_usuario, datos_de_conexion, codigo_del_servicio);

            return (confimacion);

        }

        //Esctura dalete para traer datos de un solo usuario
        [HttpDelete("Delete_Un_Servicio_de_una_empresa/{codigo}")]
        public ActionResult<Boolean> Delete_datos_del_usuario([FromRoute] string codigo)
        {

            Boolean confimacion=new Boolean();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Servicios_de_una_empresa.eliminar_una_reservacion(codigo, datos_de_conexion);

            return (confimacion);

        }

        /*

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

            Datos_de_usuario = logica_De_Los_Servicios_de_una_empresa.consulta_De_datos_personales_por_codigo(datos_de_usuario, datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(Datos_de_usuario);

            return (mensaje);

        }

        */
    }
}
