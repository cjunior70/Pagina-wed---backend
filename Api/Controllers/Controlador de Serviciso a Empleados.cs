using Microsoft.AspNetCore.Mvc;
using Modules;
using Newtonsoft.Json;
using Services;
using System.Data;

namespace Api.Controllers
{
    public class Controlador_de_Serviciso_a_Empleados : ControllerBase
    {
        //Instancia de la clase logica_de_los_usuarios globales
        logica_de_los_servicios_de_un_empleado logica_De_Los_Empleados = new logica_de_los_servicios_de_un_empleado();

        //Esctura post para registrar un usuario
        [HttpPost("Post_Registrar_Servicios_a_un_empleado/")]
        public ActionResult<Boolean> Post_Registrar_Servicios_a_un_empleado([FromBody] Empleado datos_del_empleado_a_ingresar)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Empleado datos_del_empleado = new Empleado();

            datos_del_empleado = datos_del_empleado_a_ingresar;

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confirmacion = logica_De_Los_Empleados.registrar_servicio_a_un_empleado(datos_del_empleado, datos_de_conexion);

            return confirmacion;

        }

        //Esctura get para traer datos de un solo usuario
        [HttpGet("Get_Traer_servicios_de_un_empleado/{codigo_del_empleado}")]
        public ActionResult<string> Get_Traer_servicios_de_un_empleado(int codigo_del_empleado)
        {

            Empleado datos_de_empleado = new Empleado();
            datos_de_empleado.codigo = codigo_del_empleado;

            DataTable Datos_del_empleado = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Datos_del_empleado = logica_De_Los_Empleados.traer_todos_los_servicios_de_un_empleados(datos_de_conexion, datos_de_empleado);

            string mensaje = JsonConvert.SerializeObject(Datos_del_empleado);

            return (mensaje);

        }

        //Esctura put para actualizar los servicios de un empleado
        [HttpPut("Put_Actualizar_DServicios_de_un_Empleado/")]
        //El frombody es para recibir el odjeto
        public ActionResult<Boolean> Put_Actualizar_DServicios_de_un_Empleado([FromBody] Empleado datos_Del_empleado)
        {

            Boolean confimacion;

            Empleado datos_de_usuario = new Empleado();

            datos_de_usuario = datos_Del_empleado;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Empleados.actualizacion_de_datos_de_un_servicio_De_un_empleado(datos_de_conexion, datos_de_usuario);

            return (confimacion);

        }

        // Estructura DELETE para eliminar un servicio de un empleado
        [HttpDelete("eliminar_servicio_empleado/{codigo_del_servicio}/{codigo_del_empleado}")]
        public ActionResult<bool> Eliminar_Servicio_De_Empleado([FromRoute] int codigo_del_servicio, [FromRoute] int codigo_del_empleado)
        {
            Datos_login datos_de_conexion = new Datos_login
            {
                usuario = "admin",
                constraseña = "admin"
            };

            Boolean confirmacion = logica_De_Los_Empleados.eliminar_un_servicio_De_un_empleado(codigo_del_empleado, codigo_del_servicio, datos_de_conexion);

            return confirmacion;
        }



        /*
        //Esctura get para traer todos los usuario
        [HttpGet("Get_Traer_Todos_los_Empleados_de_una_empresa/{codigo_De_la_empresa}/")]
        public ActionResult<string> Get_Traer_Todos_los_Empleados_de_la_empresa(int codigo_De_la_empresa)
        {
            DataTable lista_de_empleados = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Empresa datos_de_empresa = new Empresa();
            datos_de_empresa.codigo = codigo_De_la_empresa;

            lista_de_empleados = logica_De_Los_Empleados.consultar_todos_los_empleados_de_una_empresa(datos_de_conexion, datos_de_empresa);

            string mensaje = JsonConvert.SerializeObject(lista_de_empleados);

            return (mensaje);

        }

        */
    }
}
