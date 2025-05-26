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
    public class Funciones_de_las_fechas
    {

        //Variables para poder uso globar
        private OracleConnection ora;

        //Funcion para la conexion con la base de datos 
        private void conexion(Datos_login datos_de_conexion)
        {
            //Cadena de conexion para ingresar el nombre de usuario y su contraseña
            string conexion = $"DATA SOURCE=localhost:1521/xepdb1;PASSWORD={datos_de_conexion.constraseña};USER ID={datos_de_conexion.usuario};";

            //Instancia de la clase de oracleconection para la conexion a la base de datos de oracle
            this.ora = new OracleConnection(conexion);
        }

        //Funcion para poder regirtar un usuario
        public Boolean Ingresar_Una_fecha(Datos_login Conexion_del_Usuario, Fechas datos_fechas)
        {

            try
            {

                conexion(Conexion_del_Usuario);

                //Abirir conexion
                ora.Open();


                Enviar_Datos(datos_fechas);


                //Cerrar conexion
                //ora.Close();
                return true;

            }
            catch (Exception)
            {
                //Cerrar conexion
                // ora.Close();

                return false;
            }

        }

        //Funcion privada para registrar los datos del nuevo usuario
        private void Enviar_Datos(Fechas datos_fechas)
        {
            string sql = @"INSERT INTO FECHAS (FECHA, ESTADO, CODIGO_EMPRESA)
                   VALUES (:FECHA, :ESTADO, :CODIGO_EMPRESA)";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add("FECHA", OracleDbType.Date).Value = datos_fechas.fecha;
                cmd.Parameters.Add("ESTADO", OracleDbType.Varchar2).Value =  datos_fechas.estado;
                cmd.Parameters.Add("CODIGO_EMPRESA", OracleDbType.Int32).Value = 3;//datos_fechas.Empresa.codigo;

                cmd.ExecuteNonQuery();
            }

        }


        //Variable para traer los datos de un solo administrador
        DataTable tabla_de_fechas = new DataTable();
        //Funcion para poder traer todos los usuario existentes
        public DataTable Consultar_Un_usuario_por_su_codigo(Datos_login Conexion_del_Usuario, Empresa datos_de_la_empresa)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                //Abir conexion
                ora.Open();

                traer_datos_de_un_administrador_del_usuario_con_codigo(datos_de_la_empresa);

                //Cerrar conexion
                ora.Close();

                return tabla_de_fechas;

            }
            catch (Exception)
            {
                //Cerrar conexion
                ora.Close();

                return null;
            }

        }

        //Funcion privada para buscar en la base de dato al administrador
        private void traer_datos_de_un_administrador_del_usuario_con_codigo(Empresa datos_de_la_empresa)
        {
            string sql = @"SELECT CODIGO, FECHA, ESTADO, CODIGO_EMPRESA 
                   FROM FECHAS 
                   WHERE CODIGO_EMPRESA = :codigo_empresa";

            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.Parameters.Add("codigo_empresa", OracleDbType.Int32).Value = datos_de_la_empresa.codigo;

                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    tabla_de_fechas.Load(lector); // Asegúrate de tener un DataTable llamado tabla_de_fechas
                }
            }
        }

        //Guardar la lista de los usuarios registados en la base de datos
        private DataTable lista_De_Usuarios_registrados = new DataTable();

        //Funcion para poder traer todos los usuarios existentes
        public DataTable Consultar_Usuarios(Datos_login Conexion_del_Usuario)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                ora.Open();

                traer_datos();

                ora.Close();

                return lista_De_Usuarios_registrados;


            }
            catch (Exception)
            {
                ora.Close();

                return null;
            }

        }

        //Funcion privada para buscar en la bases de datos todos los usuario registrados
        private void traer_datos()
        {
            string sql = "SELECT codigo,cedula, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, " +
                 "telefono, correo, sexo, foto FROM USUARIOS";

            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;

                // Usar OracleDataReader para ejecutar la consulta y llenar el DataTable
                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    lista_De_Usuarios_registrados.Load(lector); // Cargar los datos del lector directamente en el DataTable
                }
            }
        }

        //Funcion para poder modificar los datos de un usuario
        public Boolean Modificar_datos_del_usuario(Datos_login Conexion_del_Usuario, Fechas datos_nuevo_del_usuario)
        {
            try
            {
                //Funcion para hacer la conexion con la base de datos
                conexion(Conexion_del_Usuario);

                //Abrir la conexion con la base
                ora.Open();

                //Funcion para enviar los datos nuevos a la base
                Enviar_actualizacion(datos_nuevo_del_usuario);

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
        //Funcion privada para buscar en la base de datos al usuario y actualizar sus datos
        private void Enviar_actualizacion(Fechas datos_fecha)
        {

            string sql = @"UPDATE FECHAS SET 
                    FECHA = :FECHA,
                    ESTADO = :ESTADO,
                    CODIGO_EMPRESA = :CODIGO_EMPRESA
                   WHERE CODIGO = :CODIGO";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":FECHA", OracleDbType.Date).Value = datos_fecha.fecha;
                cmd.Parameters.Add(":ESTADO", OracleDbType.Varchar2).Value = datos_fecha.estado;
                cmd.Parameters.Add(":CODIGO_EMPRESA", OracleDbType.Int32).Value = datos_fecha.Empresa.codigo;
                cmd.Parameters.Add(":CODIGO", OracleDbType.Int32).Value = datos_fecha.codigo;

                cmd.ExecuteNonQuery();
            }

        }

        //Funcion para poder borrar un usuario
        public Boolean borrar_una_fechas(Datos_login Conexion_del_Usuario, Fechas datos_del_usuario)
        {
            try
            {

                conexion(Conexion_del_Usuario);

                //Abirir conexion
                ora.Open();


                buscar_y_borrar_un_usuario(datos_del_usuario);


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

        private void buscar_y_borrar_un_usuario(Fechas datos_De_la_fecha_a_eliminar)
        {
            string sql = "DELETE FROM FECHAS WHERE codigo = :codigo";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":codigo", OracleDbType.Int32).Value = datos_De_la_fecha_a_eliminar.codigo;
                cmd.ExecuteNonQuery();
            }
        }

    }
}
