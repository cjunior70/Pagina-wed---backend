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
    public class Funciones_del_cliente
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

        //Funcion para poder regirtar un cliente
        public Boolean Ingresar_Un_Cliente(Datos_login Conexion_del_cliente, Cliente datos_del_cliente)
        {

            try
            {

                conexion(Conexion_del_cliente);

                //Abirir conexion
                ora.Open();

                //Funcion para enviar los datos a la base
                Enviar_Datos(datos_del_cliente);

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

        private void Enviar_Datos(Cliente datos_usuario)
        {
            //Escturcta para la sentencia sql
            string sql = @"INSERT INTO CLIENTES (cedula, primer_nombre, segundo_nombre, primer_apellido,segundo_apellido,
                        telefono,correo,foto,sexo
                    ) VALUES (:cedula,:primer_nombre,:segundo_nombre,:primer_apellido,:segundo_apellido,
                        :telefono,:correo,:foto,:sexo
                    )";

            //Intancia para poder entrar a la funcion
            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add("cedula", OracleDbType.Varchar2).Value = datos_usuario.cedula;
                cmd.Parameters.Add("primer_nombre", OracleDbType.Varchar2).Value = datos_usuario.primer_nombre;
                cmd.Parameters.Add("segundo_nombre", OracleDbType.Varchar2).Value = datos_usuario.segundo_nombre;
                cmd.Parameters.Add("primer_apellido", OracleDbType.Varchar2).Value = datos_usuario.primer_apellido;
                cmd.Parameters.Add("segundo_apellido", OracleDbType.Varchar2).Value = datos_usuario.segundo_apellido;
                cmd.Parameters.Add("telefono", OracleDbType.Varchar2).Value = datos_usuario.telefono;
                cmd.Parameters.Add("correo", OracleDbType.Varchar2).Value = datos_usuario.correo;
                cmd.Parameters.Add("foto", OracleDbType.Blob).Value = datos_usuario.foto; // Asegúrate que sea byte[]
                cmd.Parameters.Add("sexo", OracleDbType.Char).Value = datos_usuario.sexo;

                cmd.ExecuteNonQuery();
            }

        
        }

        //Variable para poder guarda el listado de los usuarios guardados
        DataTable Tabla_Clientes = new DataTable();
        //Funcion para poder traer todos los usuarios existentes
        public DataTable Consultar_Clientes(Datos_login Conexion_del_Cliente)
        {

            try
            {
                conexion(Conexion_del_Cliente);

                ora.Open();

                traer_datos();

                ora.Close();

                return Tabla_Clientes;

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
                 "telefono, correo, sexo, foto FROM CLIENTES";

            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;

                // Usar OracleDataReader para ejecutar la consulta y llenar el DataTable
                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    Tabla_Clientes.Load(lector); // Cargar los datos del lector directamente en el DataTable
                }
            }
        }


        //Funcion para poder modificar los datos de un cliente
        public Boolean Modificar_datos_del_cliente(Datos_login Conexion_del_Cliente, Cliente datos_nuevo_del_Cliente)
        {
            try
            {
                //Funcion para hacer la conexion con la base de datos
                conexion(Conexion_del_Cliente);

                //Abrir la conexion con la base
                ora.Open();

                //Funcion para enviar los datos nuevos a la base
                Enviar_actualizacion(datos_nuevo_del_Cliente);

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
        private void Enviar_actualizacion(Cliente datos_nuevos_del_Cliente)
        {

            string sql = "UPDATE CLIENTES SET " +
             "cedula = :cedula, " +
             "primer_nombre = :primer_nombre, " +
             "segundo_nombre = :segundo_nombre, " +
             "primer_apellido = :primer_apellido, " +
             "segundo_apellido = :segundo_apellido, " +
             "telefono = :telefono, " +
             "correo = :correo, " +
             "sexo = :sexo, " +
             "foto = :foto " +
             "WHERE codigo = :codigo";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":cedula", OracleDbType.Varchar2).Value = datos_nuevos_del_Cliente.cedula;
                cmd.Parameters.Add(":primer_nombre", OracleDbType.Varchar2).Value = datos_nuevos_del_Cliente.primer_nombre;
                cmd.Parameters.Add(":segundo_nombre", OracleDbType.Varchar2).Value = datos_nuevos_del_Cliente.segundo_nombre;
                cmd.Parameters.Add(":primer_apellido", OracleDbType.Varchar2).Value = datos_nuevos_del_Cliente.primer_apellido;
                cmd.Parameters.Add(":segundo_apellido", OracleDbType.Varchar2).Value = datos_nuevos_del_Cliente.segundo_apellido;
                cmd.Parameters.Add(":telefono", OracleDbType.Varchar2).Value = datos_nuevos_del_Cliente.telefono;
                cmd.Parameters.Add(":correo", OracleDbType.Varchar2).Value = datos_nuevos_del_Cliente.correo;
                cmd.Parameters.Add(":sexo", OracleDbType.Char).Value = datos_nuevos_del_Cliente.sexo;
                cmd.Parameters.Add(":foto", OracleDbType.Blob).Value = datos_nuevos_del_Cliente.foto;
                cmd.Parameters.Add(":codigo", OracleDbType.Int32).Value = datos_nuevos_del_Cliente.codigo;

                cmd.ExecuteNonQuery();
            }

        }

        //Funcion para poder borrar un usuario
        public Boolean borrar_un_cliente(Datos_login Conexion_del_Cliente, Cliente datos_del_cliente)
        {
            try
            {

                conexion(Conexion_del_Cliente);

                //Abirir conexion
                ora.Open();


                buscar_y_borrar_un_cliente(datos_del_cliente);


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

        private void buscar_y_borrar_un_cliente(Cliente datos_del_cliente_a_eliminar)
        {
            string sql = "DELETE FROM CLIENTES WHERE codigo = :codigo";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":codigo", OracleDbType.Int32).Value = datos_del_cliente_a_eliminar.codigo;
                cmd.ExecuteNonQuery();
            }
        }

        //Variable para traer los datos de un solo administrador
        DataTable Datos_Del_Cliente = new DataTable();
        //Funcion para poder traer todos los usuario existentes
        public DataTable Consultar_Un_Cliente(Datos_login Conexion_del_Cliente, Cliente datos_del_cliente)
        {

            try
            {
                conexion(Conexion_del_Cliente);

                //Abir conexion
                ora.Open();

                traer_datos_de_un_cliente(datos_del_cliente);

                //Cerrar conexion
                ora.Close();

                return Datos_Del_Cliente;

            }
            catch (Exception)
            {
                //Cerrar conexion
                ora.Close();

                return null;
            }

        }

        //Funcion privada para buscar en la base de dato al administrador
        private void traer_datos_de_un_cliente(Cliente datos_del_cliente)
        {
            string sql = "SELECT codigo,cedula, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, " +
                 "telefono, correo, sexo, foto FROM CLIENTES WHERE CODIGO = " + datos_del_cliente.codigo;


            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;

                // Usar OracleDataReader para ejecutar la consulta y llenar el DataTable
                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    Datos_Del_Cliente.Load(lector); // Cargar los datos del lector directamente en el DataTable
                }
            }
        }

    }
}
