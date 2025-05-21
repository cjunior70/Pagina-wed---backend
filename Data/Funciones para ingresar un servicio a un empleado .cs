using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modules;
using Oracle.ManagedDataAccess.Client;

namespace Data
{
    public class Funciones_para_ingresar_un_servicio_a_un_empleado
    {

        //Variables para poder uso globar
        private OracleConnection ora;

        //Funcion para la conexion con la base de datos 
        private void conexion(Datos_login datos_de_conexion)
        {
            //Cadena de conexion para ingresar el un servicio a un empleado
            string conexion = $"DATA SOURCE=localhost:1521/xepdb1;PASSWORD={datos_de_conexion.constraseña};USER ID={datos_de_conexion.usuario};";

            //Instancia de la clase de oracleconection para la conexion a la base de datos de oracle
            this.ora = new OracleConnection(conexion);
        }

        //Funcion para poder regirtar un cliente
        public Boolean Ingresar_Un_Servicio_a_un_empleado(Datos_login Conexion_del_cliente, Empleado datos_del_servicio) //, Servicio_de_un_empleado datos_del_servicio_del_empleado)
        {

            try
            {

                conexion(Conexion_del_cliente);

                //Abirir conexion
                ora.Open();


                Enviar_Datos(datos_del_servicio);//datos_del_servicio_del_empleado);


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

        //Funcion privada para registrar los datos de un servicio a un empleado
        private void Enviar_Datos(Empleado datos_del_servicio)//Servicio_de_un_empleado datos_del_servicio_del_empleado)
        {
            string sql = @"INSERT INTO EMPLEADOS_SERVICIOS (
                    CODIGO_EMPLEADO,
                    CODIGO_SERVICIO
                ) VALUES (
                    :CODIGO_EMPLEADO,
                    :CODIGO_SERVICIO
                )";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.BindByName = true;

                for (int i = 0; i < datos_del_servicio.lista_De_servicios.Count; i++)
                {
                    cmd.Parameters.Clear(); // Muy importante para evitar errores al reusar los parámetros

                    cmd.Parameters.Add(":CODIGO_EMPLEADO", OracleDbType.Int32).Value = datos_del_servicio.codigo;
                    cmd.Parameters.Add(":CODIGO_SERVICIO", OracleDbType.Int32).Value = datos_del_servicio.lista_De_servicios[i].codigo;

                    cmd.ExecuteNonQuery();
                }
            }


        }

        //Variable para poder guarda el listado de los servicios de los empleados
        DataTable Tabla_de_los_servicios_de_los_empleados = new DataTable();
        //Funcion para poder traer todos los usuarios existentes
        public DataTable Consultar_servicios_de_los_empleados(Datos_login Conexion_del_Cliente, Empleado datos_del_empleado)
        {

            try
            {
                conexion(Conexion_del_Cliente);

                ora.Open();

                Obtener_Servicios_De_Empleado(datos_del_empleado);

                ora.Close();

                return Tabla_de_los_servicios_de_los_empleados;

            }
            catch (Exception)
            {
                ora.Close();

                return null;
            }

        }
        //Funcion privada para buscar en la bases de datos todos los servicios y sus empleados
        private void Obtener_Servicios_De_Empleado(Empleado codigo_empleado)
        {

           string sql = @"
            SELECT 
                S.CODIGO,
                S.NOMBRE,
                SE.PRECIO,
                SE.TIEMPO_PROMEDIO,
                SE.CODIGO_EMPRESA
            FROM EMPLEADOS_SERVICIOS ES
            JOIN SERVICIOS S ON ES.CODIGO_SERVICIO = S.CODIGO
            JOIN EMPRESA_SERVICIOS SE ON SE.CODIGO_SERVICIO = S.CODIGO
            WHERE ES.CODIGO_EMPLEADO = :CODIGO_EMPLEADO";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.BindByName = true;
                cmd.Parameters.Add(":CODIGO_EMPLEADO", OracleDbType.Int32).Value = codigo_empleado.codigo;

                using (OracleDataReader lector = cmd.ExecuteReader())
                {
                    Tabla_de_los_servicios_de_los_empleados.Load(lector);
                }
            }
        }



        //Funcion para poder modificar los datos de un servicio de un empleado
        public Boolean Modificar_datos_de_un_servicio_De_un_Empleado(Datos_login Conexion_del_Cliente, Empleado datos_del_servicio_del_empleado)// Servicio_de_un_empleado datos_del_servicio_del_empleado)
        {
            try
            {
                //Funcion para hacer la conexion con la base de datos
                conexion(Conexion_del_Cliente);

                //Abrir la conexion con la base
                ora.Open();

                ////Funcion para enviar los datos nuevos a la base
                Actualizar_Servicios_De_Empleado(datos_del_servicio_del_empleado);

                //Cerrar la conexion con la base
                ora.Close();

                return true;
            }
            catch (Exception)
            {
                ora.Close();
                return false;
            }
        }

        //Funcion privada para buscar en la base de datos el servicio y el empleado
        private void Actualizar_Servicios_De_Empleado(Empleado empleado)
        {

            // Paso 1: Eliminar servicios actuales del empleado
            eliminar_lista_de_servicios_antiguos(empleado);

            // Paso 2: Insertar la nueva lista de servicios
            string insertSql = @"INSERT INTO EMPLEADOS_SERVICIOS (
                            CODIGO_EMPLEADO,
                            CODIGO_SERVICIO
                        ) VALUES (
                            :CODIGO_EMPLEADO,
                            :CODIGO_SERVICIO
                        )";

            using (OracleCommand insertCmd = new OracleCommand(insertSql, ora))
            {
                insertCmd.BindByName = true;

                foreach (var servicio in empleado.lista_De_servicios)
                {
                    insertCmd.Parameters.Clear();
                    insertCmd.Parameters.Add(":CODIGO_EMPLEADO", OracleDbType.Int32).Value = empleado.codigo;
                    insertCmd.Parameters.Add(":CODIGO_SERVICIO", OracleDbType.Int32).Value = servicio.codigo;

                    insertCmd.ExecuteNonQuery();
                }
            }
        }

        private void eliminar_lista_de_servicios_antiguos(Empleado empleado)
        {
            string deleteSql = @"DELETE FROM EMPLEADOS_SERVICIOS WHERE CODIGO_EMPLEADO = :CODIGO_EMPLEADO";

            using (OracleCommand deleteCmd = new OracleCommand(deleteSql, ora))
            {
                deleteCmd.BindByName = true;
                deleteCmd.Parameters.Add(":CODIGO_EMPLEADO", OracleDbType.Int32).Value = empleado.codigo;
                deleteCmd.ExecuteNonQuery();
            }
        }

        //Funcion para poder borrar un usuario
        public Boolean borrar_un_servicio_de_un_empleado(int codigo_del_empleado, int codigo_del_servicio, Datos_login datos_de_conexion)
        {
            try
            {

                conexion(datos_de_conexion);

                //Abirir conexion
                ora.Open();


                buscar_y_borrar_un_cliente(codigo_del_empleado, codigo_del_servicio);


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

        private void buscar_y_borrar_un_cliente(int codigo_del_empleado, int codigo_del_servicio)
        {
            string sql = @"DELETE FROM EMPLEADOS_SERVICIOS
                       WHERE CODIGO_EMPLEADO = :CODIGO_EMPLEADO AND CODIGO_SERVICIO = :CODIGO_SERVICIO";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":CODIGO_EMPLEADO", OracleDbType.Int32).Value = codigo_del_empleado;
                cmd.Parameters.Add(":CODIGO_SERVICIO", OracleDbType.Int32).Value = codigo_del_servicio;

                cmd.ExecuteNonQuery();
            }
        }

        ////Variable para traer los servicios de un empleado
        //DataTable servicios_de_un_empleado = new DataTable();
        ////Funcion para poder traer todos los usuario existentes
        //public DataTable Consultar_Un_Servicio_de_un_Empleado(Datos_login Conexion_del_Cliente, Servicio_de_un_empleado datos_del_servicio_del_empleado)
        //{

        //    try
        //    {
        //        conexion(Conexion_del_Cliente);

        //        //Abir conexion
        //        ora.Open();

        //        traer_datos_del_servicio(datos_del_servicio_del_empleado);

        //        //Cerrar conexion
        //        ora.Close();

        //        return servicios_de_un_empleado;

        //    }
        //    catch (Exception)
        //    {
        //        //Cerrar conexion
        //        ora.Close();

        //        return null;
        //    }

        //}

        ////Funcion privada para buscar en la base de dato al administrador
        //private void traer_datos_del_servicio(Servicio_de_un_empleado datos_del_servicio_del_empleado)
        //{
        //    OracleCommand comando = new OracleCommand("PK_BUSCAR_SERVICIOS_DE_UN_EMPLEADO", ora);
        //    comando.CommandType = System.Data.CommandType.StoredProcedure;


        //    comando.Parameters.Add("p_codigo", OracleDbType.Varchar2).Value = datos_del_servicio_del_empleado.codigo_del_empleado;

        //    Console.WriteLine("codigo del empleado : " + datos_del_servicio_del_empleado.codigo_del_empleado);

        //    comando.Parameters.Add("p_registro", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

        //    OracleDataAdapter adaptador = new OracleDataAdapter();
        //    adaptador.SelectCommand = comando;
        //    adaptador.Fill(servicios_de_un_empleado);
        //}

    }
}
