using Modules;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    public class Funcion_de_los_servicios
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

        //Funcion para poder regirtar un servicio
        public Boolean Ingresar_Un_Servicio(Datos_login Conexion_del_usuario, Servicios datos_de_un_servicio)
        {

            try
            {

                conexion(Conexion_del_usuario);

                //Abirir conexion
                ora.Open();


                Enviar_Datos(datos_de_un_servicio);


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

        //Funcion privada para registrar los datos de un servicio
        private void Enviar_Datos(Servicios datos_de_servicio)
        {
            //Escturcta para la sentencia sql
            string sql = @"INSERT INTO SERVICIOS (nombre
                    ) VALUES (:nombre
                    )";

            //Intancia para poder entrar a la funcion
            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = "opcion1"; //datos_de_servicio.nombre;

                cmd.ExecuteNonQuery();
             
            }

        }

        //Variable para poder guarda el listado de los servicos
        DataTable Tabla_Servicios = new DataTable();
        //Funcion para poder traer todos los servicios existentes
        public DataTable Consultar_Todas_los_servicios(Datos_login Conexion_del_Usuario)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                ora.Open();

                traer_datos();

                ora.Close();

                return Tabla_Servicios;

            }
            catch (Exception)
            {
                ora.Close();

                return null;
            }

        }
        //Funcion privada para buscar en la bases de datos todos las ubicaciones registrados

        private void traer_datos()
        {
            string sql = "SELECT codigo,NOMBRE FROM SERVICIOS "; 


            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;

                // Usar OracleDataReader para ejecutar la consulta y llenar el DataTable
                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    Tabla_Servicios.Load(lector); // Cargar los datos del lector directamente en el DataTable
                }
            }
        }



        //Funcion para poder modificar los datos de un servicio
        public Boolean Modificar_datos_una_ubicacion(Datos_login Conexion_del_Usuario, Servicios datos_nuevo_de_un_servicio)
        {
            try
            {
                //Funcion para hacer la conexion con la base de datos
                conexion(Conexion_del_Usuario);

                //Abrir la conexion con la base
                ora.Open();

                //Funcion para enviar los datos nuevos a la base
                Enviar_actualizacion(datos_nuevo_de_un_servicio);

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
        private void Enviar_actualizacion(Servicios datos_del_servicio)
        {

            string sql = "UPDATE SERVICIOS SET " +
             "nombre = :nombre " +
             "WHERE codigo = :codigo";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":nombre", OracleDbType.Varchar2).Value = datos_del_servicio.nombre;
                cmd.Parameters.Add(":codigo", OracleDbType.Int64).Value = datos_del_servicio.codigo;

                cmd.ExecuteNonQuery();
            }

        }

        //Funcion para poder borrar un servicio
        public Boolean borrar_un_servicio(Datos_login Conexion_del_Usuario, Servicios datos_de_un_servicio)
        {
            try
            {

                conexion(Conexion_del_Usuario);

                //Abirir conexion
                ora.Open();


                buscar_y_borrar_un_servicio(datos_de_un_servicio);


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

        private void buscar_y_borrar_un_servicio(Servicios datos_del_servicio_a_eliminar)
        {
            string sql = "DELETE FROM SERVICIOS WHERE codigo = :codigo";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":codigo", OracleDbType.Int32).Value = datos_del_servicio_a_eliminar.codigo;
                cmd.ExecuteNonQuery();
            }
        }

        //Variable para traer los datos de un solo servicio
        DataTable Servicio = new DataTable();
        //Funcion para poder traer todos los servicios existentes
        public DataTable Consultar_Un_Servicio(Datos_login Conexion_del_Usuario, Servicios datos_del_servicio)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                //Abir conexion
                ora.Open();

                traer_datos_de_un_servicio(datos_del_servicio);

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

        //Funcion privada para buscar en la base de dato a un empleado
        private void traer_datos_de_un_servicio(Servicios datos_del_servicio)
        {
            OracleCommand comando = new OracleCommand("PK_BUSCAR_UN_SERVICIO", ora);
            comando.CommandType = System.Data.CommandType.StoredProcedure;


            comando.Parameters.Add("p_codigo", OracleDbType.Varchar2).Value = datos_del_servicio.codigo;
            comando.Parameters.Add("p_registro", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adaptador = new OracleDataAdapter();
            adaptador.SelectCommand = comando;
            adaptador.Fill(Servicio);
        }


    }
}
