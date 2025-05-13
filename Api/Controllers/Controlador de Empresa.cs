using Microsoft.AspNetCore.Mvc;
using Modules;
using Newtonsoft.Json;
using Services;
using System.Data;

namespace Api.Controllers
{
    public class Controlador_de_Empresa : ControllerBase
    {

        //Instancia de la clase logica_de_los_usuarios globales
        logica_de_las_empresas logica_De_Las_Empresas = new logica_de_las_empresas();

        //Esctura post para registrar un usuario
        [HttpPost("Post_Registrar_Empresa/")]
        public ActionResult<Boolean> Post_Registrar_Empresa([FromBody] Empresa registrar_empresa)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Empresa datos_de_la_Empresa = new Empresa();

            datos_de_la_Empresa = registrar_empresa;

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            //Importante, se necesita crear o traer el odjeto, o mejor dicho la instancia de las clases en para poder guardar la ubicacion y el usuario 
            confirmacion = logica_De_Las_Empresas.registro_de_una_empresa(datos_de_la_Empresa, datos_de_conexion);

            return confirmacion;

        }

        //Esctura get para traer todos los usuario
        [HttpGet("Get_Traer_Todos_las_Empresas/")]
        public ActionResult<string> Get_Traer_Todos_las_Empresas()
        {
            DataTable Lista_de_empresas = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Lista_de_empresas = logica_De_Las_Empresas.consultar_todas_las_empresas(datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(Lista_de_empresas);

            return (mensaje);

        }

        //Esctura get para traer datos de un solo usuario
        [HttpGet("Get_Datos_de_una_empresa/{codigo}")]
        public ActionResult<string> Get_Datos_de_una_empresa([FromRoute] string codigo)
        {

            Empresa Datos_de_empresa = new Empresa();

            Datos_de_empresa.codigo = Convert.ToInt32(codigo);

            DataTable Datos_de_la_empresa = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Datos_de_la_empresa = logica_De_Las_Empresas.consultar_datos_de_una_empresa(Datos_de_empresa, datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(Datos_de_la_empresa);

            return (mensaje);

        }

        //Esctura put para traer datos de un solo usuario
        [HttpPut("Put_Actualizar_Datos_de_Empresas/")]
        public ActionResult<Boolean> Put_Actualizar_Datos_de_Empresas([FromRoute] Empresa Datos_de_empresa)
        {

            Boolean confimacion;

            Empresa Datos_de_la_empresa = new Empresa();

            Datos_de_la_empresa = Datos_de_empresa;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Las_Empresas.actualizar_datos_de_la_empresa(Datos_de_la_empresa, datos_de_conexion);

            return (confimacion);

        }

        //Esctura dalete para traer datos de un solo usuario
        [HttpDelete("Delete_datos_de_la_empresa/{codigo}")]
        public ActionResult<Boolean> Delete_datos_de_la_empresa([FromRoute] string codigo)
        {

            Boolean confimacion;


            Empresa Datos_de_la_empresa = new Empresa();

            Datos_de_la_empresa.codigo = Convert.ToInt32(codigo);

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Las_Empresas.borrar_datos_de_la_empresa (Datos_de_la_empresa, datos_de_conexion);

            return (confimacion);

        }

    }
}
