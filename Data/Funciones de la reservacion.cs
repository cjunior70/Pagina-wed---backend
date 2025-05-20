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
    public class Funciones_de_la_reservacion
    {

        //Variables para poder uso globar
        private OracleConnection ora;

        //Funcion para la conexion con la base de datos 
        private void conexion(Datos_login datos_de_conexion)
        {
            //Cadena de conexion para ingresar el nombre de empleado,cliente o usuario y su contraseña
            string conexion = $"DATA SOURCE=localhost:1521/xepdb1;PASSWORD={datos_de_conexion.constraseña};USER ID={datos_de_conexion.usuario};";

            //Instancia de la clase de oracleconection para la conexion a la base de datos de oracle
            this.ora = new OracleConnection(conexion);
        }

        //Funcion para poder regirtar una reservacion
        public Boolean Ingresar_Una_reservacion(Datos_login Conexion_del_usuario, Reservacion datos_de_una_reservacion)
        {

            try
            {

                conexion(Conexion_del_usuario);

                //Abirir conexion
                ora.Open();


                Enviar_Datos(datos_de_una_reservacion);


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

        //Funcion privada para registrar los datos de una reservacion
        private void Enviar_Datos(Reservacion datos_de_la_reservacion)
        {
            string sql = @"INSERT INTO RESERVACIONES (
                        CREACION,
                        FECHA,
                        VALOR,
                        ESTADO,
                        CODIGO_EMPRESA,
                        CODIGO_CLIENTE,
                        CODIGO_CONTABILIDAD
                   ) VALUES (
                        :CREACION,
                        :FECHA,
                        :VALOR,
                        :ESTADO,
                        :CODIGO_EMPRESA,
                        :CODIGO_CLIENTE,
                        :CODIGO_CONTABILIDAD
                   )";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":CREACION", OracleDbType.TimeStamp).Value = null;//datos_de_la_reservacion.creacion;
                cmd.Parameters.Add(":FECHA", OracleDbType.TimeStamp).Value = null;// datos_de_la_reservacion.fecha_reservacion;
                cmd.Parameters.Add(":VALOR", OracleDbType.Decimal).Value = null;//datos_de_la_reservacion.pago_total;
                cmd.Parameters.Add(":ESTADO", OracleDbType.Varchar2).Value = "d";//datos_de_la_reservacion.estado;
                cmd.Parameters.Add(":CODIGO_EMPRESA", OracleDbType.Int64).Value = null;//datos_de_la_reservacion.datos_empresa.codigo;
                cmd.Parameters.Add(":CODIGO_CLIENTE", OracleDbType.Int64).Value = null;//datos_de_la_reservacion.datos_cliente.codigo;
                cmd.Parameters.Add(":CODIGO_CONTABILIDAD", OracleDbType.Int64).Value = null;//datos_de_la_reservacion.datos_contabilidad.codigo;

                cmd.ExecuteNonQuery();
            }
        }

        //Variable para poder guarda el listado de los servicos
        DataTable Tabla_De_reservacion_De_un_cliente = new DataTable();
        //Funcion para poder traer todos los servicios existentes
        public DataTable Consultar_Todas_las_reservaciones(Datos_login Conexion_del_Usuario, Reservacion datos_de_una_reservacion)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                ora.Open();

                traer_datos(datos_de_una_reservacion);

                ora.Close();

                return Tabla_De_reservacion_De_un_cliente;

            }
            catch (Exception)
            {
                ora.Close();

                return null;
            }

        }
        //Funcion privada para buscar en la bases de datos todos las ubicaciones registrados

        private void traer_datos(Reservacion datos_de_una_reservacion)
        {
            // Supongamos que datos_de_la_reservacion.codigo contiene el ID de la reservación a buscar
            string sql = "SELECT CODIGO, CREACION, FECHA, VALOR, ESTADO, " +
                         "CODIGO_EMPRESA, CODIGO_CLIENTE, CODIGO_CONTABILIDAD " +
                         "FROM RESERVACIONES WHERE CODIGO_CLIENTE = :codigo_cliente";

            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.Add(":codigo_cliente", OracleDbType.Int32).Value = datos_de_una_reservacion.datos_cliente.codigo;

                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    Tabla_De_reservacion_De_un_cliente.Load(lector); // Cargar resultado en DataTable
                }
            }
        }

        //Variable para poder traer todas las reservaciones de un empresa
        DataTable Tabla_De_reservacion_De_la_empresa = new DataTable();
        //Funcion para poder traer todos los servicios existentes
        public DataTable reservaciones_De_la_empresa(Datos_login Conexion_del_Usuario, Reservacion datos_de_una_reservacion)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                ora.Open();

                traer_datos_de_la_empresa(datos_de_una_reservacion);

                ora.Close();

                return Tabla_De_reservacion_De_un_cliente;

            }
            catch (Exception)
            {
                ora.Close();

                return null;
            }

        }
        //Funcion privada para buscar en la bases de datos todos las ubicaciones registrados

        private void traer_datos_de_la_empresa(Reservacion datos_de_una_reservacion)
        {
            // Supongamos que datos_de_la_reservacion.codigo contiene el ID de la reservación a buscar
            string sql = "SELECT CODIGO, CREACION, FECHA, VALOR, ESTADO, " +
                         "CODIGO_EMPRESA, CODIGO_CLIENTE, CODIGO_CONTABILIDAD " +
                         "FROM RESERVACIONES WHERE CODIGO_EMPRESA = :codigo_empresa";

            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.Add(":codigo_empresa", OracleDbType.Int32).Value = datos_de_una_reservacion.datos_empresa.codigo;

                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    Tabla_De_reservacion_De_un_cliente.Load(lector); // Cargar resultado en DataTable
                }
            }
        }


        //Funcion para poder modificar los datos de una reservacion
        public Boolean Modificar_datos_una_reservacion(Datos_login Conexion_del_Usuario, Reservacion datos_nuevo_de_una_reservacion)
        {
            try
            {
                //Funcion para hacer la conexion con la base de datos
                conexion(Conexion_del_Usuario);

                //Abrir la conexion con la base
                ora.Open();

                //Funcion para enviar los datos nuevos a la base
                Enviar_actualizacion(datos_nuevo_de_una_reservacion);

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
        //Funcion privada para buscar en la base de datos un servicio y actualizar sus datos
        private void Enviar_actualizacion(Reservacion datos_reservacion)
        {

            string sql = @"UPDATE RESERVACIONES SET 
                CREACION = :CREACION,
                FECHA = :FECHA,
                VALOR = :VALOR,
                ESTADO = :ESTADO,
                CODIGO_EMPRESA = :CODIGO_EMPRESA,
                CODIGO_CLIENTE = :CODIGO_CLIENTE,
                CODIGO_CONTABILIDAD = :CODIGO_CONTABILIDAD
              WHERE CODIGO = :CODIGO";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":CREACION", OracleDbType.Date).Value = null;//datos_reservacion.creacion;
                cmd.Parameters.Add(":FECHA", OracleDbType.TimeStamp).Value = null; //datos_reservacion.fecha_reservacion;
                cmd.Parameters.Add(":VALOR", OracleDbType.Decimal).Value = null;//datos_reservacion.pago_total;
                cmd.Parameters.Add(":ESTADO", OracleDbType.Varchar2).Value = null;//datos_reservacion.estado;
                cmd.Parameters.Add(":CODIGO_EMPRESA", OracleDbType.Int32).Value = null; //datos_reservacion.datos_empresa.codigo;
                cmd.Parameters.Add(":CODIGO_CLIENTE", OracleDbType.Int32).Value = 14;//datos_reservacion.datos_cliente.codigo;
                cmd.Parameters.Add(":CODIGO_CONTABILIDAD", OracleDbType.Int32).Value = null;// datos_reservacion.datos_contabilidad.codigo;
                cmd.Parameters.Add(":CODIGO", OracleDbType.Int32).Value = 12;// datos_reservacion.codigo; // ID de la reservación que se actualizará

                cmd.ExecuteNonQuery();
            }


        }

        //Funcion para poder borrar una reservacion
        public Boolean borrar_una_reservacion(Datos_login Conexion_del_Usuario, Reservacion datos_de_la_reservacion)
        {
            try
            {

                conexion(Conexion_del_Usuario);

                //Abirir conexion
                ora.Open();


                buscar_y_borrar_una_reservacion(datos_de_la_reservacion);


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

        private void buscar_y_borrar_una_reservacion(Reservacion datos_de_la_ubicacion_a_eliminar)
        {
            string sql = "DELETE FROM RESERVACIONES WHERE CODIGO = :CODIGO";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":CODIGO", OracleDbType.Int32).Value = datos_de_la_ubicacion_a_eliminar.codigo;

                cmd.ExecuteNonQuery();
            }
        }

        //Variable para traer los datos des una reservacion
        DataTable Servicio = new DataTable();
        //Funcion para poder traer todos las reservaciones existentes
        public DataTable Consultar_Una_Reservacion(Datos_login Conexion_del_Usuario, Reservacion datos_del_servicio)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                //Abir conexion
                ora.Open();

                traer_datos_de_una_Reservacion(datos_del_servicio);

                //Cerrar conexion
                ora.Close();

                return Servicio;

            }
            catch (Exception)
            {
                //Cerrar conexion
                ora.Close();

                return null;
            }

        }

        //Funcion privada para buscar en la base de dato a una reservacion
        private void traer_datos_de_una_Reservacion(Reservacion datos_del_servicio)
        {
            OracleCommand comando = new OracleCommand("PK_BUSCAR_UNA_RESERVACION", ora);
            comando.CommandType = System.Data.CommandType.StoredProcedure;


            //comando.Parameters.Add("p_cliente_codigo", OracleDbType.Int16).Value = datos_del_servicio.Cliente.codigo;
            //comando.Parameters.Add("p_empresa_codigo", OracleDbType.Int16).Value = datos_del_servicio.datos_de_empresa_actualizados.codigo;
            //comando.Parameters.Add("p_fecha", OracleDbType.Date).Value = datos_del_servicio.fecha_de_la_reservacion;
            //comando.Parameters.Add("p_registro", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            adaptador.Fill(Servicio);
        }



    }
}
