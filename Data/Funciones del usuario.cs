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
    public class Funciones_del_usuario
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
        public Boolean Ingresar_Un_Usuario(Datos_login Conexion_del_Usuario, Usuario datos_del_usuario)
        {

            try
            {

                conexion(Conexion_del_Usuario);

                //Abirir conexion
                ora.Open();


                Enviar_Datos(datos_del_usuario);


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
        private void Enviar_Datos(Usuario datos_usuario)
        {
            //Escturcta para la sentencia sql
            string sql= @"INSERT INTO USUARIOS (cedula, primer_nombre, segundo_nombre, primer_apellido,segundo_apellido,
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
                cmd.Parameters.Add("sexo", OracleDbType.Char).Value = datos_usuario.sexo;
                cmd.Parameters.Add("foto", OracleDbType.Blob).Value = datos_usuario.foto; // Asegúrate que sea byte[]

                cmd.ExecuteNonQuery();
            }

        }


        //Variable para traer los datos de un solo administrador
        DataTable Usuario_con_cedula = new DataTable();
        //Funcion para poder traer todos los usuario existentes
        public DataTable Consultar_Un_usuario_por_su_codigo(Datos_login Conexion_del_Usuario, Usuario datos_del_usuario)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                //Abir conexion
                ora.Open();

                traer_datos_de_un_administrador_del_usuario_con_codigo(datos_del_usuario);

                //Cerrar conexion
                ora.Close();

                return Usuario_con_cedula;

            }
            catch (Exception)
            {
                //Cerrar conexion
                ora.Close();

                return null;
            }

        }

        //Funcion privada para buscar en la base de dato al administrador
        private void traer_datos_de_un_administrador_del_usuario_con_codigo(Usuario datos_del_usuario)
        {
            string sql = "SELECT codigo,cedula, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, " +
                "telefono, correo, sexo, foto FROM USUARIOS WHERE CODIGO = " + datos_del_usuario.codigo;


            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;

                // Usar OracleDataReader para ejecutar la consulta y llenar el DataTable
                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    Usuario_con_cedula.Load(lector); // Cargar los datos del lector directamente en el DataTable
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
        public Boolean Modificar_datos_del_usuario(Datos_login Conexion_del_Usuario, Usuario datos_nuevo_del_usuario)
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
        private void Enviar_actualizacion(Usuario usuario)
        {

            string sql = "UPDATE USUARIOS SET " +
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
                cmd.Parameters.Add(":cedula", OracleDbType.Varchar2).Value = usuario.cedula;
                cmd.Parameters.Add(":primer_nombre", OracleDbType.Varchar2).Value = usuario.primer_nombre;
                cmd.Parameters.Add(":segundo_nombre", OracleDbType.Varchar2).Value = usuario.segundo_nombre;
                cmd.Parameters.Add(":primer_apellido", OracleDbType.Varchar2).Value = usuario.primer_apellido;
                cmd.Parameters.Add(":segundo_apellido", OracleDbType.Varchar2).Value = usuario.segundo_apellido;
                cmd.Parameters.Add(":telefono", OracleDbType.Varchar2).Value = usuario.telefono;
                cmd.Parameters.Add(":correo", OracleDbType.Varchar2).Value = usuario.correo;
                cmd.Parameters.Add(":sexo", OracleDbType.Char).Value = usuario.sexo;
                cmd.Parameters.Add(":foto", OracleDbType.Blob).Value = usuario.foto;
                cmd.Parameters.Add(":codigo", OracleDbType.Int32).Value = usuario.codigo;

                cmd.ExecuteNonQuery();
            }

        }

        //Funcion para poder borrar un usuario
        public Boolean borrar_un_usuario(Datos_login Conexion_del_Usuario, Usuario datos_del_usuario)
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

        private void buscar_y_borrar_un_usuario(Usuario datos_del_usuario_a_eliminar)
        {
            string sql = "DELETE FROM USUARIOS WHERE codigo = :codigo";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":codigo", OracleDbType.Int32).Value = datos_del_usuario_a_eliminar.codigo;
                cmd.ExecuteNonQuery();
            }
        }

        ////Variable para traer los datos de un solo administrador
        //DataTable Usuario = new DataTable();
        ////Funcion para poder traer todos los usuario existentes
        //public DataTable Consultar_Un_usuario(Datos_login Conexion_del_Usuario, Usuario datos_del_usuario)
        //{

        //    try
        //    {
        //        conexion(Conexion_del_Usuario);

        //        //Abir conexion
        //        ora.Open();

        //        traer_datos_de_un_administrador(datos_del_usuario);

        //        //Cerrar conexion
        //        ora.Close();

        //        return Usuario;

        //    }
        //    catch (Exception)
        //    {
        //        //Cerrar conexion
        //        ora.Close();

        //        return null;
        //    }

        //}

        ////Funcion privada para buscar en la base de dato al administrador
        //private void traer_datos_de_un_administrador(Usuario datos_del_usuario)
        //{
        //    string sql = "SELECT codigo,cedula, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, " +
        //        "telefono, correo, sexo, foto FROM USUARIOS WHERE CODIGO = :CODIGO";

        //    using (OracleCommand comando = new OracleCommand(sql, ora))
        //    {
        //        comando.CommandType = System.Data.CommandType.Text;
        //        comando.Parameters.Add("CODIGO", OracleDbType.Int64).Value = datos_del_usuario.codigo;

        //        // Usar OracleDataReader para ejecutar la consulta y llenar el DataTable
        //        using (OracleDataReader lector = comando.ExecuteReader())
        //        {
        //            Usuario.Load(lector); // Cargar los datos del lector directamente en el DataTable
        //        }
        //    }

        //}

        

    }
}
