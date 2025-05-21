using Microsoft.AspNetCore.Mvc;
using Modules;
using Newtonsoft.Json;
using Services;
using System.Data;

namespace Api.Controllers
{
    public class Controlador_de_Empleados : ControllerBase
    {
        //Instancia de la clase logica_de_los_usuarios globales
        logica_de_ logica_De_Los_Empleados = new logica_de_();

        //Esctura post para registrar un usuario
        [HttpPost("Post_Registrar_Empleado/")]
        public ActionResult<Boolean> Post_Registrar_Empleado([FromBody] Empleado datos_del_empleado_a_ingresar)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Empleado datos_del_empleado = new Empleado();

            datos_del_empleado = datos_del_empleado_a_ingresar;

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confirmacion = logica_De_Los_Empleados.ingresar_un_empleado(datos_del_empleado, datos_de_conexion);

            return confirmacion;

        }

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

        //Esctura get para traer datos de un solo usuario
        [HttpGet("Get_Traer_datos_de_un_empleado/{codigo_del_empleado}")]
        public ActionResult<string> Get_Datos_de_un_Usuario( int codigo_del_empleado)
        {

            Empleado datos_de_empleado = new Empleado();
            datos_de_empleado.codigo = codigo_del_empleado;

            DataTable Datos_del_empleado = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Datos_del_empleado = logica_De_Los_Empleados.consultar_datos_de_un_empleado(datos_de_conexion, datos_de_empleado);

            string mensaje = JsonConvert.SerializeObject(Datos_del_empleado);

            return (mensaje);

        }

        //Esctura put para traer datos de un solo usuario
        [HttpPut("Put_Actualizar_Datos_Del_Empleado/")]
        //El frombody es para recibir el odjeto
        public ActionResult<Boolean> Put_Actualizar_Datos_Del_Empleado([FromBody] Empleado datos_Del_empleado)
        {

            Boolean confimacion;

            Empleado datos_de_usuario = new Empleado();

            datos_de_usuario = datos_Del_empleado;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Empleados.actualizar_datos_de_un_empleado(datos_de_usuario, datos_de_conexion);

            return (confimacion);

        }

        //Esctura dalete para traer datos de un solo usuario
        [HttpDelete("Delete_datos_del_empleado/{codigo}")]
        public ActionResult<Boolean> Delete_datos_del_usuario([FromRoute] string codigo)
        {

            Boolean confimacion;


            Empleado datos_de_usuario = new Empleado();

            datos_de_usuario.codigo = Convert.ToInt32(codigo);

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Empleados.borrar_un_empleado(datos_de_usuario, datos_de_conexion);

            return (confimacion);

        }
    }
}
