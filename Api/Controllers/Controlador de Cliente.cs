using Microsoft.AspNetCore.Mvc;
using Modules;
using Newtonsoft.Json;
using Services;
using System.Data;

namespace Api.Controllers
{
    public class Controlador_de_Cliente : ControllerBase
    {
        //Instancia de la clase logica_de_los_usuarios globales
        logica_de_los_clientes logica_De_Los_Clientes = new logica_de_los_clientes();


        //Esctura post para registrar un cliente
        [HttpPost("Post_Registrar_Cliente/")]
        public ActionResult<Boolean> Post_Registrar_Cliente([FromBody] Cliente datos_del_cliente_nuevo)
        {
            Boolean confirmacion;

            Datos_login datos_de_conexion = new Datos_login();

            Cliente datos_usuario = new Cliente();

            datos_usuario = datos_del_cliente_nuevo;

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confirmacion = logica_De_Los_Clientes.registro_de_cliente(datos_usuario, datos_de_conexion);

            return confirmacion;

        }

        //Esctura get para traer todos los cliente
        [HttpGet("Get_Traer_Todos_los_Clientes/")]
        public ActionResult<string> Get_Traer_Todos_los_Clientes()
        {
            DataTable lista_de_clientes = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            lista_de_clientes = logica_De_Los_Clientes.consultar_todo_los_cliente(datos_de_conexion);


            string mensaje = JsonConvert.SerializeObject(lista_de_clientes);

            return (mensaje);

        }

        //Esctura get para traer datos de un solo cliente
        [HttpGet("Get_Datos_de_un_Cliente/{codigo}")]
        public ActionResult<string> Get_Datos_de_un_Usuario([FromRoute] string codigo)
        {

            Cliente datos_de_cliente = new Cliente();

            datos_de_cliente.codigo = Convert.ToInt32(codigo);

            DataTable Datos_de_cliente = new DataTable();

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            Datos_de_cliente = logica_De_Los_Clientes.consulta_De_datos_personales(datos_de_cliente, datos_de_conexion);

            string mensaje = JsonConvert.SerializeObject(Datos_de_cliente);

            return (mensaje);

        }

        //Esctura put para traer datos de un solo cliente
        [HttpPut("Put_Actualizar_Datos_Clientes/")]
        public ActionResult<Boolean> Put_Actualizar_Datos([FromRoute] Cliente datos_De_nuevo_cliente)
        {

            Boolean confimacion;

            Cliente datos_de_cliente = new Cliente();

            datos_de_cliente = datos_De_nuevo_cliente;

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Clientes.actualizar_datos_de_un_cliente(datos_de_cliente, datos_de_conexion);

            return (confimacion);

        }

        //Esctura dalete para traer datos de un solo usuario
        [HttpDelete("Delete_datos_del_cliente/{codigo}")]
        public ActionResult<Boolean> Delete_datos_del_cliente([FromRoute] string codigo)
        {

            Boolean confimacion;


            Cliente datos_de_cliente = new Cliente();

            datos_de_cliente.codigo = Convert.ToInt32(codigo);

            Datos_login datos_de_conexion = new Datos_login();

            datos_de_conexion.usuario = "admin";
            datos_de_conexion.constraseña = "admin";

            confimacion = logica_De_Los_Clientes.borrar_datos_de_un_cliente(datos_de_cliente, datos_de_conexion);

            return (confimacion);

        }
    }
}
