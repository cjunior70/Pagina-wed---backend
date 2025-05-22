using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modules;
using Oracle.ManagedDataAccess.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data
{
    public class Funciones_para_agregar_un_servicio_a_una_empresa
    {

        //Variables para poder uso globar
        private OracleConnection ora;

        //Funcion para la conexion con la base de datos 
        private void conexion(Datos_login datos_de_conexion)
        {
            //Cadena de conexion para ingresar el un servicio a una datos_de_empresa_actualizados
            string conexion = $"DATA SOURCE=localhost:1521/xepdb1;PASSWORD={datos_de_conexion.constraseña};USER ID={datos_de_conexion.usuario};";

            //Instancia de la clase de oracleconection para la conexion a la base de datos de oracle
            this.ora = new OracleConnection(conexion);
        }

        //Funcion para poder regirtar un cliente
        public bool Ingresar_Un_Servicio_a_una_Empresa(Empresa datos_del_servicio_de_la_empresa_y_su_servicio, Datos_login Conexion_del_cliente)
        {

            try
            {

                conexion(Conexion_del_cliente);

                //Abirir conexion
                ora.Open();


                Enviar_Datos_EmpresaServicios(datos_del_servicio_de_la_empresa_y_su_servicio);


                //Cerrar conexion
                ora.Close();
                return true;

            }
            catch (Exception)
            {
                //Cerrar conexion
                ora.Close();

                return false;
            }

        }

        private void Enviar_Datos_EmpresaServicios(Empresa datos_de_la_empresa)
        {
            // Sentencia SQL de inserción sin el campo CODIGO, que es generado por el trigger
            string sql = @"INSERT INTO EMPRESA_SERVICIOS (
                       CODIGO_SERVICIO,
                       CODIGO_EMPRESA,
                       PRECIO,
                       TIEMPO_PROMEDIO
                   ) VALUES (
                       :CODIGO_SERVICIO,
                       :CODIGO_EMPRESA,
                       :PRECIO,
                       :TIEMPO_PROMEDIO
                   )";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.BindByName = true;

                for (int i = 0; i < datos_de_la_empresa.lista_de_servicios.Count; i++)
                {
                    cmd.Parameters.Clear(); // Limpiar parámetros en cada iteración

                    cmd.Parameters.Add(":CODIGO_SERVICIO", OracleDbType.Int32).Value = datos_de_la_empresa.lista_de_servicios[i].codigo;
                    cmd.Parameters.Add(":CODIGO_EMPRESA", OracleDbType.Int32).Value = datos_de_la_empresa.codigo;
                    cmd.Parameters.Add(":PRECIO", OracleDbType.Decimal).Value = datos_de_la_empresa.lista_de_servicios[i].precio;
                    cmd.Parameters.Add(":TIEMPO_PROMEDIO", OracleDbType.IntervalDS).Value = datos_de_la_empresa.lista_de_servicios[i].tiempo_promedio;

                    cmd.ExecuteNonQuery();
                }
            }
        }


        //Variable para poder guarda el listado de los servicios de la datos_de_empresa_actualizados
        DataTable Tabla_de_los_servicios_de_la_empresa = new DataTable();
        //Funcion para poder traer todos los usuarios existentes
        public DataTable Consultar_servicios_y_empresas_relacionados(Datos_login datos_de_conexion, Empresa datos_de_la_empresa)
        {

            try
            {
                conexion(datos_de_conexion);

                ora.Open();

                traer_datos(datos_de_la_empresa);

                ora.Close();

                return Tabla_de_los_servicios_de_la_empresa;

            }
            catch (Exception)
            {
                ora.Close();

                return null;
            }

        }
        //Funcion privada para buscar en la bases de datos todos los servicios y sus empresas
        private void traer_datos(Empresa datos_de_la_empresa)
        {
            string sql = @"SELECT 
                        es.CODIGO,
                        es.CODIGO_SERVICIO,
                        es.CODIGO_EMPRESA,
                        es.PRECIO,
                        es.TIEMPO_PROMEDIO,
                        s.NOMBRE AS NOMBRE_SERVICIO
                   FROM 
                        EMPRESA_SERVICIOS es
                   JOIN 
                        SERVICIOS s ON es.CODIGO_SERVICIO = s.CODIGO
                   WHERE 
                        es.CODIGO_EMPRESA = :codigo_empresa";

            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = CommandType.Text;
                comando.Parameters.Add(":codigo_empresa", OracleDbType.Int32).Value = datos_de_la_empresa.codigo;

                OracleDataAdapter adaptador = new OracleDataAdapter(comando);
                Tabla_de_los_servicios_de_la_empresa.Clear(); // Limpiar antes de llenar (opcional pero recomendado)
                adaptador.Fill(Tabla_de_los_servicios_de_la_empresa);
            }
        }

        //funcion para poder modificar los datos de un servicio de una datos_de_empresa_actualizados
        public bool modificar_datos_de_un_servicio_de_una_empresa(Datos_login conexion_del_cliente, Empresa datos_del_serivcio_de_la_empresa_a_actualizar, string codigo_del_servicio)
        {
            try
            {
                //funcion para hacer la conexion con la base de datos
                conexion(conexion_del_cliente);

                //abrir la conexion con la base
                ora.Open();

                //funcion para enviar los datos nuevos a la base
                enviar_actualizacion(datos_del_serivcio_de_la_empresa_a_actualizar, codigo_del_servicio);

                //cerrar la conexion con la base
                ora.Close();

                return true;
            }
            catch (Exception)
            {
                ora.Close();
                return false;
            }
        }

        //funcion privada para buscar en la base de datos el servicio y la datos_de_empresa_actualizados
        private void enviar_actualizacion(Empresa datos_del_servicio_de_la_empresa_y_su_servicio, string codigo_del_servicio)
        {

            string sql = @"UPDATE EMPRESA_SERVICIOS
                           SET 
                               CODIGO_SERVICIO = :NUEVO_CODIGO_SERVICIO,
                               PRECIO = :PRECIO,
                               TIEMPO_PROMEDIO = :TIEMPO_PROMEDIO
                           WHERE 
                               CODIGO = :CODIGO";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":NUEVO_CODIGO_SERVICIO", OracleDbType.Int32).Value = 2;//datos_del_servicio_de_la_empresa_y_su_servicio.lista_de_servicios[1].codigo;
                cmd.Parameters.Add(":PRECIO", OracleDbType.Decimal).Value = 18;//datos_del_servicio_de_la_empresa_y_su_servicio.lista_de_servicios[1].precio;
                cmd.Parameters.Add(":TIEMPO_PROMEDIO", OracleDbType.IntervalDS).Value = null;//datos_del_servicio_de_la_empresa_y_su_servicio.lista_de_servicios[1].tiempo_promedio;

                cmd.Parameters.Add(":CODIGO", OracleDbType.Int32).Value = codigo_del_servicio;

                cmd.ExecuteNonQuery();
            }

        }


        //Funcion para poder borrar una datos_de_empresa_actualizados
        public bool borrar_un_servicio_de_una_empresa(Datos_login Conexion_del_Cliente, string codigo)
        {
            try
            {

                conexion(Conexion_del_Cliente);

                //Abirir conexion
                ora.Open();


                buscar_y_borrar_un_cliente(codigo);


                //Cerrar conexion
                ora.Close();
                return true;

            }
            catch (Exception)
            {
                //Cerrar conexion
                ora.Close();

                return false;
            }
        }

        private void buscar_y_borrar_un_cliente(string codigo)
        {
            string sql = @"DELETE FROM EMPRESA_SERVICIOS
                   WHERE CODIGO = :CODIGO";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":CODIGO", OracleDbType.Int32).Value = codigo;
                cmd.ExecuteNonQuery();
            }
        }

        //Variable para traer los servicios de una datos_de_empresa_actualizados
        // DataTable servicio_de_un_empleado = new DataTable();
        //Funcion para poder traer todos los usuario existentes
        //public DataTable Consultar_Un_Servicio_de_una_empresa(Datos_login Conexion_del_Cliente, Servicio_de_una_Empresa datos_del_servicio_de_una_empresa)
        //{

        //    try
        //    {
        //        conexion(Conexion_del_Cliente);

        //        //Abir conexion
        //        ora.Open();

        //        traer_datos_del_servicio(datos_del_servicio_de_una_empresa);

        //        //Cerrar conexion
        //        ora.Close();

        //        return servicio_de_un_empleado;

        //    }
        //    catch (Exception)
        //    {
        //        //Cerrar conexion
        //        ora.Close();

        //        return null;
        //    }

        //}

        ////Funcion privada para buscar en la base de dato al administrador
        //private void traer_datos_del_servicio(Servicio_de_una_Empresa datos_del_servicio_de_la_empresa)
        //{
        //    OracleCommand comando = new OracleCommand("PK_BUSCAR_SERVICIOS_DE_UNA_EMPRESA", ora);
        //    comando.CommandType = System.Data.CommandType.StoredProcedure;


        //    comando.Parameters.Add("p_codigo", OracleDbType.Varchar2).Value = datos_del_servicio_de_la_empresa.codigo_de_la_empresa;
        //    comando.Parameters.Add("p_registro", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

        //    OracleDataAdapter adaptador = new OracleDataAdapter();
        //    adaptador.SelectCommand = comando;
        //    adaptador.Fill(servicio_de_un_empleado);
        //}


    }
}
