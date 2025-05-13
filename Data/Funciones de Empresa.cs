using Modules;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Data
{
    public class Funciones_de_Empresa
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

        //Funcion para poder regirtar una datos_de_empresa_actualizados
        public Boolean Ingresar_Una_Empresa(Datos_login Conexion_del_usuario, Empresa datos_de_la_empresa)
        {

            try
            {

                conexion(Conexion_del_usuario);

                //Abirir conexion
                ora.Open();

                Console.WriteLine("ESTOY AQui");
                Enviar_Datos(datos_de_la_empresa);


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

        //Funcion privada para registrar los datos de una datos_de_empresa_actualizados
        private void Enviar_Datos(Empresa datos_de_la_empresa)
        {
            //Escturcta para la sentencia sql
            string sql = @"INSERT INTO EMPRESAS (nombre, EXTRELLAS, CORREO, DESCRIPCION_DE_LA_UBICACION,WHATSAPP,
                        FACEBOOK,INSTAGRAM,IMAGEN_EN_MINIATURA,IMAGEN_EN_GENERAL,COMIENZO_LABORAL,FINALIZACION_LABORAL,
                        CODIGO_UBICACION,CODIGO_USUARIO   
                    ) VALUES (:nombre, :EXTRELLAS,:CORREO, :DESCRIPCION_DE_LA_UBICACION,:WHATSAPP,
                        :FACEBOOK,:INSTAGRAM,:IMAGEN_EN_MINIATURA,:IMAGEN_EN_GENERAL,:COMIENZO_LABORAL,:FINALIZACION_LABORAL,
                        :CODIGO_UBICACION,:CODIGO_USUARIO 
                    )";

            //Intancia para poder entrar a la funcion
            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add("nombre", OracleDbType.Varchar2).Value = datos_de_la_empresa.nombre;
                cmd.Parameters.Add("EXTRELLAS", OracleDbType.Int64).Value = datos_de_la_empresa.extrellas;
                cmd.Parameters.Add("CORREO", OracleDbType.Varchar2).Value = datos_de_la_empresa.correo;
                cmd.Parameters.Add("DESCRIPCION_DE_LA_UBICACION", OracleDbType.Varchar2).Value = datos_de_la_empresa.Descripcion_de_la_Ubicacion;
                cmd.Parameters.Add("WHATSAPP", OracleDbType.Varchar2).Value = datos_de_la_empresa.whatsapp;
                cmd.Parameters.Add("FACEBOOK", OracleDbType.Varchar2).Value = datos_de_la_empresa.facebook;
                cmd.Parameters.Add("INSTAGRAM", OracleDbType.Varchar2).Value = datos_de_la_empresa.instagram;
                cmd.Parameters.Add("IMAGEN_EN_MINIATURA", OracleDbType.Blob).Value = datos_de_la_empresa.imagen_en_miniatura;
                cmd.Parameters.Add("IMAGEN_EN_GENERAL", OracleDbType.Blob).Value = datos_de_la_empresa.imagen_general;      // Asegúrate que sea byte[]
                cmd.Parameters.Add("COMIENZO_LABORAL", OracleDbType.Varchar2).Value = datos_de_la_empresa.comiezo_laboral;      // Asegúrate que sea byte[]
                cmd.Parameters.Add("FINALIZACION_LABORAL", OracleDbType.Varchar2).Value = datos_de_la_empresa.finalizacion_laboral; 
                cmd.Parameters.Add("CODIGO_UBICACION", OracleDbType.Int64).Value = datos_de_la_empresa.Ubicacion.codigo;
                cmd.Parameters.Add("CODIGO_USUARIO", OracleDbType.Int64).Value = datos_de_la_empresa.Usuario.codigo;

                cmd.ExecuteNonQuery();
            }

        }

        //Variable para poder guarda las empresas registradas
        DataTable Tabla_De_Empresas = new DataTable();
        //Funcion para poder traer todos las datos_de_empresa_actualizados existentes
        public DataTable Consultar_Todas_las_Empresas(Datos_login Conexion_del_Usuario)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                ora.Open();

                traer_datos();

                ora.Close();

                return Tabla_De_Empresas;

            }
            catch (Exception)
            {
                ora.Close();

                return null;
            }

        }

        //Funcion privada para buscar en la bases de datos todos las empresas registrados
        private void traer_datos()
        {
            string sql = "SELECT nombre, EXTRELLAS, CORREO, DESCRIPCION_DE_LA_UBICACION, WHATSAPP, FACEBOOK, INSTAGRAM, " +
              "IMAGEN_EN_MINIATURA, IMAGEN_EN_GENERAL, COMIENZO_LABORAL, FINALIZACION_LABORAL, " +
              "CODIGO_UBICACION, CODIGO_USUARIO FROM EMPRESAS";

            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;

                // Usar OracleDataReader para ejecutar la consulta y llenar el DataTable
                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    Tabla_De_Empresas.Load(lector); // Cargar los datos del lector directamente en el DataTable
                }
            }
        }



        //Funcion para poder modificar los datos de una datos_de_empresa_actualizados
        public Boolean Modificar_datos_una_empresa(Datos_login Conexion_del_Usuario, Empresa datos_nuevo_de_la_empresa)
        {
            try
            {
                //Funcion para hacer la conexion con la base de datos
                conexion(Conexion_del_Usuario);

                //Abrir la conexion con la base
                ora.Open();

                //Funcion para enviar los datos nuevos a la base
                Enviar_actualizacion(datos_nuevo_de_la_empresa);

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

        //Funcion privada para buscar en la base de datos una datos_de_empresa_actualizados y actualizar sus datos
        private void Enviar_actualizacion(Empresa datos_de_empresa_actualizados)
        {

            string sql = "UPDATE EMPRESAS SET " +
             "nombre = :nombre, " +
             "EXTRELLAS = :estrellas, " +
             "CORREO = :correo, " +
             "DESCRIPCION_DE_LA_UBICACION = :descripcion, " +
             "WHATSAPP = :whatsapp, " +
             "FACEBOOK = :facebook, " +
             "INSTAGRAM = :instagram, " +
             "IMAGEN_EN_MINIATURA = :imagenMiniatura, " +
             "IMAGEN_EN_GENERAL = :imagenGeneral, " +
             "COMIENZO_LABORAL = :comienzoLaboral, " +
             "FINALIZACION_LABORAL = :finalizacionLaboral, " +
             "CODIGO_UBICACION = :codigoUbicacion, " +
             "CODIGO_USUARIO = :codigoUsuario " +
             "WHERE CODIGO = :codigoEmpresa";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":nombre", OracleDbType.Varchar2).Value = datos_de_empresa_actualizados.nombre;
                cmd.Parameters.Add(":estrellas", OracleDbType.Int32).Value = datos_de_empresa_actualizados.extrellas;
                cmd.Parameters.Add(":correo", OracleDbType.Varchar2).Value = datos_de_empresa_actualizados.correo;
                cmd.Parameters.Add(":descripcion", OracleDbType.Varchar2).Value = datos_de_empresa_actualizados.Descripcion_de_la_Ubicacion;
                cmd.Parameters.Add(":whatsapp", OracleDbType.Varchar2).Value = datos_de_empresa_actualizados.whatsapp;
                cmd.Parameters.Add(":facebook", OracleDbType.Varchar2).Value = datos_de_empresa_actualizados.facebook;
                cmd.Parameters.Add(":instagram", OracleDbType.Varchar2).Value = datos_de_empresa_actualizados.instagram;
                cmd.Parameters.Add(":imagenMiniatura", OracleDbType.Blob).Value =datos_de_empresa_actualizados.imagen_en_miniatura;
                cmd.Parameters.Add(":imagenGeneral", OracleDbType.Blob).Value = datos_de_empresa_actualizados.imagen_general;
                cmd.Parameters.Add(":comienzoLaboral", OracleDbType.Varchar2).Value = datos_de_empresa_actualizados.comiezo_laboral;
                cmd.Parameters.Add(":finalizacionLaboral", OracleDbType.Varchar2).Value = datos_de_empresa_actualizados.finalizacion_laboral;
                cmd.Parameters.Add(":codigoUbicacion", OracleDbType.Int32).Value = datos_de_empresa_actualizados.Ubicacion.codigo;
                cmd.Parameters.Add(":codigoUsuario", OracleDbType.Int32).Value = 6;//datos_de_empresa_actualizados.Usuario.codigo;
                cmd.Parameters.Add(":codigoEmpresa", OracleDbType.Int32).Value = datos_de_empresa_actualizados.codigo;

                cmd.ExecuteNonQuery();
            }

        }

        //Funcion para poder borrar una datos_de_empresa_actualizados
        public Boolean borrar_una_empresa(Datos_login Conexion_del_Usuario, Empresa datos_de_una_empresa)
        {
            try
            {

                conexion(Conexion_del_Usuario);

                //Abirir conexion
                ora.Open();


                buscar_y_borrar_una_empresa(datos_de_una_empresa);


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

        private void buscar_y_borrar_una_empresa(Empresa datos_de_la_empresa_a_eliminar)
        {
            string sql = "DELETE FROM EMPRESAS WHERE codigo = :codigo";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":codigo", OracleDbType.Int32).Value = datos_de_la_empresa_a_eliminar.codigo;
                cmd.ExecuteNonQuery();
            }
        }

        //Variable para traer los datos de una sola datos_de_empresa_actualizados
        DataTable datos_de_empresa_actualizados = new DataTable();
        //Funcion para poder traer todos las empresas existentes
        public DataTable Consultar_Una_Empresa(Datos_login Conexion_del_Usuario, Empresa datos_de_la_empresa)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                //Abir conexion
                ora.Open();

                traer_datos_de_una_empresa(datos_de_la_empresa);

                //Cerrar conexion
                ora.Close();

                return datos_de_empresa_actualizados;

            }
            catch (Exception)
            {
                //Cerrar conexion
                ora.Close();

                return null;
            }

        }

        //Funcion privada para buscar en la base de dato a un empleado
        private void traer_datos_de_una_empresa(Empresa datos_de_la_empresa)
        {
            string sql = "SELECT nombre, EXTRELLAS, CORREO, DESCRIPCION_DE_LA_UBICACION, WHATSAPP, FACEBOOK, INSTAGRAM, " +
              "IMAGEN_EN_MINIATURA, IMAGEN_EN_GENERAL, COMIENZO_LABORAL, FINALIZACION_LABORAL, " +
              "CODIGO_UBICACION, CODIGO_USUARIO FROM EMPRESAS  WHERE CODIGO = " + datos_de_la_empresa.codigo;


            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;

                // Usar OracleDataReader para ejecutar la consulta y llenar el DataTable
                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    datos_de_empresa_actualizados.Load(lector); // Cargar los datos del lector directamente en el DataTable
                }
            }
        }


    }
}
