using Modules;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Funciones_del_empleado
    {

        //Variables para poder uso globar
        private OracleConnection ora;

        //Funcion para la conexion con la base de datos 
        private void conexion(Datos_login datos_de_conexion)
        {
            //Cadena de conexion para ingresar el nombre de empleado y su contraseña
            string conexion = $"DATA SOURCE=localhost:1521/xepdb1;PASSWORD={datos_de_conexion.constraseña};USER ID={datos_de_conexion.usuario};";

            //Instancia de la clase de oracleconection para la conexion a la base de datos de oracle
            this.ora = new OracleConnection(conexion);
        }

        //Funcion para poder regirtar un empleado
        public Boolean Ingresar_Un_Empleado(Datos_login Conexion_del_usuario, Empleado datos_del_empleado)
        {

            try
            {

                conexion(Conexion_del_usuario);

                //Abirir conexion
                ora.Open();

                Enviar_Datos(datos_del_empleado);

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

        //Funcion privada para registrar los datos del nuevo empleado
        private void Enviar_Datos(Empleado empleado)
        {
            //Escturcta para la sentencia sql
            string sql = @"INSERT INTO EMPLEADOS (
                        CEDULA, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, 
                        TELEFONO, CORREO, SEXO, FOTO, FECHA_DE_INICIO, FECHA_ACTUAL, 
                        CARGO, ESTADO, PORCENTAJE_DE_RESERVACIONES, CODIGO_EMPRESA, ESTACION
                    ) VALUES (
                        :CEDULA, :PRIMER_NOMBRE, :SEGUNDO_NOMBRE, :PRIMER_APELLIDO, :SEGUNDO_APELLIDO, 
                        :TELEFONO, :CORREO, :SEXO, :FOTO, :FECHA_DE_INICIO, :FECHA_ACTUAL, 
                        :CARGO, :ESTADO, :PORCENTAJE_DE_RESERVACIONES, :CODIGO_EMPRESA, :ESTACION
                    )";


            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":CEDULA", OracleDbType.Varchar2).Value = empleado.cedula;
                cmd.Parameters.Add(":PRIMER_NOMBRE", OracleDbType.Varchar2).Value = empleado.primer_nombre;
                cmd.Parameters.Add(":SEGUNDO_NOMBRE", OracleDbType.Varchar2).Value = empleado.segundo_nombre;
                cmd.Parameters.Add(":PRIMER_APELLIDO", OracleDbType.Varchar2).Value = empleado.primer_apellido;
                cmd.Parameters.Add(":SEGUNDO_APELLIDO", OracleDbType.Varchar2).Value = empleado.segundo_apellido;
                cmd.Parameters.Add(":TELEFONO", OracleDbType.Varchar2).Value = empleado.telefono;
                cmd.Parameters.Add(":CORREO", OracleDbType.Varchar2).Value = empleado.correo;
                cmd.Parameters.Add(":SEXO", OracleDbType.Char).Value = empleado.sexo;
                cmd.Parameters.Add(":FOTO", OracleDbType.Blob).Value = empleado.foto; // byte[]
                cmd.Parameters.Add(":FECHA_DE_INICIO", OracleDbType.Date).Value = empleado.fecha_de_inicio;
                cmd.Parameters.Add(":FECHA_ACTUAL", OracleDbType.Date).Value = empleado.fecha_de_actual;
                cmd.Parameters.Add(":CARGO", OracleDbType.Varchar2).Value = empleado.cargo;
                cmd.Parameters.Add(":ESTADO", OracleDbType.Varchar2).Value = empleado.estado;
                cmd.Parameters.Add(":PORCENTAJE_DE_RESERVACIONES", OracleDbType.Int32).Value = empleado.porcentaje_comision;
                cmd.Parameters.Add(":CODIGO_EMPRESA", OracleDbType.Int32).Value = empleado.Empresa.codigo;
                cmd.Parameters.Add(":ESTACION", OracleDbType.Varchar2).Value = empleado.estacion;

                cmd.ExecuteNonQuery();
            }

        }

        //Variable para poder guarda el listado de los empleados guardados
        DataTable Tabla_Empleados = new DataTable();
        //Funcion para poder traer todos los usuarios existentes
        public DataTable Consultar_Empleados_de_una_empresa(Datos_login Conexion_del_Usuario, Empresa datos_del_empleado)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                ora.Open();

                traer_datos(datos_del_empleado);

                ora.Close();

                return Tabla_Empleados;

            }
            catch (Exception)
            {
                ora.Close();

                return null;
            }

        }
        //Funcion privada para buscar en la bases de datos todos los usuario registrados

        private void traer_datos(Empresa datos_de_la_empresa)
        {
            string sql = "SELECT CEDULA, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, " +
                 "TELEFONO, CORREO, SEXO, FOTO, FECHA_DE_INICIO, FECHA_ACTUAL, " +
                 "CARGO, ESTADO, PORCENTAJE_DE_RESERVACIONES, CODIGO_EMPRESA, ESTACION " +
                 "FROM EMPLEADOS " +
                 "WHERE CODIGO_EMPRESA = :codigoEmpresa";

            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.CommandType = System.Data.CommandType.Text;

                // Agregar parámetro para filtrar por código de empresa
                comando.Parameters.Add(":codigoEmpresa", OracleDbType.Int32).Value = datos_de_la_empresa.codigo;

                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    Tabla_Empleados.Load(lector); // Cargar los datos directamente al DataTable
                }
            }

        }

        //Variable para traer los datos de un solo administrador
        DataTable Usuario = new DataTable();
        //Funcion para poder traer todos los usuario existentes
        public DataTable Consultar_Un_Empleado(Datos_login Conexion_del_Usuario, Empleado datos_del_empleado)
        {

            try
            {
                conexion(Conexion_del_Usuario);

                //Abir conexion
                ora.Open();

                traer_datos_de_un_empleado(datos_del_empleado);

                //Cerrar conexion
                ora.Close();

                return Usuario;

            }
            catch (Exception)
            {
                //Cerrar conexion
                ora.Close();

                return null;
            }

        }

        //Funcion privada para buscar en la base de dato a un empleado
        private void traer_datos_de_un_empleado(Empleado datos_del_empleado)
        {
            string sql = @"
                SELECT CEDULA, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO,
                   TELEFONO, CORREO, SEXO, FOTO, FECHA_DE_INICIO, FECHA_ACTUAL,
                   CARGO, ESTADO, PORCENTAJE_DE_RESERVACIONES, CODIGO_EMPRESA, ESTACION
            FROM EMPLEADOS
            WHERE CODIGO = :CODIGO_EMPLEADO";

            using (OracleCommand comando = new OracleCommand(sql, ora))
            {
                comando.Parameters.Add("CODIGO_EMPLEADO", OracleDbType.Int64).Value = datos_del_empleado.codigo;

                comando.CommandType = System.Data.CommandType.Text;

                // Usar OracleDataReader para ejecutar la consulta y llenar el DataTable
                using (OracleDataReader lector = comando.ExecuteReader())
                {
                    Usuario.Load(lector); // Cargar los datos del lector directamente en el DataTable
                }
            }
        }

        //Funcion para poder modificar los datos de un usuario
        public Boolean Modificar_datos_del_empleado(Datos_login Conexion_del_Usuario, Empleado datos_nuevo_del_empleado)
        {
            try
            {
                //Funcion para hacer la conexion con la base de datos
                conexion(Conexion_del_Usuario);

                //Abrir la conexion con la base
                ora.Open();

                //Funcion para enviar los datos nuevos a la base
                Enviar_actualizacion(datos_nuevo_del_empleado);

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
        private void Enviar_actualizacion(Empleado datos_nuevos_del_empleado)
        {

            string sql = @"
    UPDATE EMPLEADOS SET
        CEDULA = :CEDULA,
        PRIMER_NOMBRE = :PRIMER_NOMBRE,
        SEGUNDO_NOMBRE = :SEGUNDO_NOMBRE,
        PRIMER_APELLIDO = :PRIMER_APELLIDO,
        SEGUNDO_APELLIDO = :SEGUNDO_APELLIDO,
        TELEFONO = :TELEFONO,
        CORREO = :CORREO,
        SEXO = :SEXO,
        FOTO = :FOTO,
        FECHA_DE_INICIO = :FECHA_DE_INICIO,
        FECHA_ACTUAL = :FECHA_ACTUAL,
        CARGO = :CARGO,
        ESTADO = :ESTADO,
        PORCENTAJE_DE_RESERVACIONES = :PORCENTAJE_DE_RESERVACIONES,
        CODIGO_EMPRESA = :CODIGO_EMPRESA,
        ESTACION = :ESTACION
    WHERE CODIGO = :CODIGO_DEL_EMPLEADO ";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":cedula", OracleDbType.Varchar2).Value = "jose";
                cmd.Parameters.Add(":primer_nombre", OracleDbType.Varchar2).Value = "juan luis";
                cmd.Parameters.Add(":segundo_nombre", OracleDbType.Varchar2).Value = datos_nuevos_del_empleado.segundo_nombre;
                cmd.Parameters.Add(":primer_apellido", OracleDbType.Varchar2).Value = datos_nuevos_del_empleado.primer_apellido;
                cmd.Parameters.Add(":segundo_apellido", OracleDbType.Varchar2).Value = datos_nuevos_del_empleado.segundo_apellido;
                cmd.Parameters.Add(":telefono", OracleDbType.Varchar2).Value = datos_nuevos_del_empleado.telefono;
                cmd.Parameters.Add(":correo", OracleDbType.Varchar2).Value = datos_nuevos_del_empleado.correo;
                cmd.Parameters.Add(":sexo", OracleDbType.Char).Value = datos_nuevos_del_empleado.sexo;
                cmd.Parameters.Add(":foto", OracleDbType.Blob).Value = datos_nuevos_del_empleado.foto;
                cmd.Parameters.Add(":fecha_de_inicio", OracleDbType.Date).Value = datos_nuevos_del_empleado.fecha_de_inicio;
                cmd.Parameters.Add(":fecha_actual", OracleDbType.Date).Value = datos_nuevos_del_empleado.fecha_de_actual;
                cmd.Parameters.Add(":cargo", OracleDbType.Varchar2).Value = datos_nuevos_del_empleado.cargo;
                cmd.Parameters.Add(":estado", OracleDbType.Varchar2).Value = datos_nuevos_del_empleado.estado;
                cmd.Parameters.Add(":porcentaje_de_reservaciones", OracleDbType.Decimal).Value = datos_nuevos_del_empleado.porcentaje_comision;
                cmd.Parameters.Add(":codigo_empresa", OracleDbType.Int32).Value = 3;
                cmd.Parameters.Add(":estacion", OracleDbType.Varchar2).Value = datos_nuevos_del_empleado.estacion;
                cmd.Parameters.Add(":CODIGO_DEL_EMPLEADO", OracleDbType.Int32).Value = datos_nuevos_del_empleado.codigo;

                cmd.ExecuteNonQuery();
            }


        }

        //Funcion para poder borrar un empleado
        public Boolean borrar_un_empleado(Datos_login Conexion_del_Usuario, Empleado datos_del_empleado)
        {
            try
            {

                conexion(Conexion_del_Usuario);

                //Abirir conexion
                ora.Open();


                buscar_y_borrar_un_usuario(datos_del_empleado);


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

        private void buscar_y_borrar_un_usuario(Empleado datos_del_usuario_a_eliminar)
        {
            string sql = "DELETE FROM EMPLEADOS WHERE codigo = :codigo";

            using (OracleCommand cmd = new OracleCommand(sql, ora))
            {
                cmd.Parameters.Add(":codigo", OracleDbType.Int32).Value = datos_del_usuario_a_eliminar.codigo;
                cmd.ExecuteNonQuery();
            }
        }


        //Funcion para poder guarda la foto un empleado
        public Boolean actualizar_foto(Datos_login Conexion_del_usuario, Empleado datos_del_empleado)
        {

            try
            {

                conexion(Conexion_del_usuario);

                //Abirir conexion
                ora.Open();

                guardar_foto(datos_del_empleado);

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

        private void guardar_foto(Empleado datos_empleado)
        {
            //Intancia para poder entrar a la funcion
            using (OracleCommand cmd = new OracleCommand("PK_INGRESAR_FOTO_DEL_EMPLEADO", ora))
            {
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //cmd.Parameters.Add("p_cedula", OracleDbType.Varchar2).Value = datos_empleado.cedula;

                //cmd.Parameters.Add("p_foto", OracleDbType.LongRaw).Value = datos_empleado.Foto;

                cmd.ExecuteNonQuery();
            }
        }


    }
}
