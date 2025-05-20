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
    public class funciones_del_horario
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
        public Boolean Ingresar_Un_Horario(Datos_login Conexion_del_Usuario, Horario datos_del_usuario)
        {

            try
            {

                conexion(Conexion_del_Usuario);

                //Abirir conexion
                ora.Open();

                Enviar_Datos(datos_del_usuario);
                
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

        //Funcion privada para registrar los datos del nuevo usuario
        private void Enviar_Datos(Horario datos_del_horario)
        {
            //Escturcta para la sentencia sql
            string sql = @"
                            INSERT INTO HORARIOS (
                                DIA_DE_LA_SEMANA,
                                TURNO_DE_LA_INICIO_DE_DIA,
                                TURNO_DE_LA_TARDE,
                                TURNO_DE_LA_NOCHE,
                                CODIGO_EMPLEADO
                            ) VALUES (
                                :DIA_DE_LA_SEMANA,
                                :TURNO_DE_LA_INICIO_DE_DIA,
                                :TURNO_DE_LA_TARDE,
                                :TURNO_DE_LA_NOCHE,
                                :CODIGO_EMPLEADO
                            )";

            //Intancia para poder entrar a la funcion
            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {

                Console.WriteLine("Conexion abierta");
                cmd.Parameters.Add("DIA_DE_LA_SEMANA", OracleDbType.Varchar2).Value = datos_del_horario.dia_de_la_semana;
                cmd.Parameters.Add("TURNO_DE_LA_INICIO_DE_DIA", OracleDbType.Varchar2).Value = datos_del_horario.turno_de_la_mañana;
                cmd.Parameters.Add("TURNO_DE_LA_TARDE", OracleDbType.Varchar2).Value = datos_del_horario.turno_de_la_tarde;
                cmd.Parameters.Add("TURNO_DE_LA_NOCHE", OracleDbType.Varchar2).Value = datos_del_horario.turno_de_la_noche;  // Si no hay turno nocturno
                cmd.Parameters.Add("CODIGO_EMPLEADO", OracleDbType.Int32).Value = datos_del_horario.empleado.codigo;

                cmd.ExecuteNonQuery();
            }

        }


        
        //Variable para traer los datos de un solo administrador
        DataTable Usuario_con_cedula = new DataTable();
        //Funcion para poder traer todos los usuario existentes
        public DataTable Consultar_Un_horario_de_un_empleado(Datos_login Conexion_del_Usuario, Horario datos_del_usuario)
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
        private void traer_datos_de_un_administrador_del_usuario_con_codigo(Horario datos_del_usuario)
        {
            string sql = "SELECT DIA_DE_LA_SEMANA,TURNO_DE_LA_INICIO_DE_DIA,TURNO_DE_LA_TARDE,TURNO_DE_LA_NOCHE," +
                        "CODIGO_EMPLEADO FROM HORARIOS WHERE CODIGO_EMPLEADO = " + datos_del_usuario.codigo;


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

        //Funcion para poder modificar los datos de un usuario
        public Boolean Modificar_horario_de_un_empleado(Datos_login Conexion_del_Usuario, Horario datos_nuevo_del_usuario)
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
        private void Enviar_actualizacion(Horario datos_del_horario)
        {

            string sql = @"UPDATE HORARIOS SET 
                    DIA_DE_LA_SEMANA = :DIA_DE_LA_SEMANA,
                    TURNO_DE_LA_INICIO_DE_DIA = :TURNO_DE_LA_INICIO_DE_DIA,
                    TURNO_DE_LA_TARDE = :TURNO_DE_LA_TARDE,
                    TURNO_DE_LA_NOCHE = :TURNO_DE_LA_NOCHE
               WHERE CODIGO_EMPLEADO = :CODIGO";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":DIA_DE_LA_SEMANA", OracleDbType.Varchar2).Value = "jueves";//datos_del_horario.dia_de_la_semana;
                cmd.Parameters.Add(":TURNO_DE_LA_INICIO_DE_DIA", OracleDbType.Varchar2).Value = null;//datos_del_horario.turno_de_la_mañana;
                cmd.Parameters.Add(":TURNO_DE_LA_TARDE", OracleDbType.Varchar2).Value = null;// datos_del_horario.turno_de_la_tarde;
                cmd.Parameters.Add(":TURNO_DE_LA_NOCHE", OracleDbType.Varchar2).Value = null;//datos_del_horario.turno_de_la_noche;
                cmd.Parameters.Add(":CODIGO", OracleDbType.Int32).Value = 3;//datos_del_horario.codigo;

                cmd.ExecuteNonQuery();
            }
        }

        //Funcion para poder borrar un usuario
        public Boolean borrar_un_horario(Datos_login Conexion_del_Usuario, Horario datos_del_usuario)
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

        private void buscar_y_borrar_un_usuario(Horario datos_del_usuario_a_eliminar)
        {
            string sql = "DELETE FROM HORARIOS WHERE codigo = :codigo";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":codigo", OracleDbType.Int32).Value = datos_del_usuario_a_eliminar.codigo;
                cmd.ExecuteNonQuery();
            }
        }

        /*
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


        */


    }
    }
