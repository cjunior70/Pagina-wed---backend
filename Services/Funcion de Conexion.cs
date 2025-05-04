using Modules;
using Data;

namespace Services
{
    public class Funcion_de_Conexion
    {
        public Boolean conexion(Datos_login datos_de_conexion)
        {
            Conexion_General conexion = new Conexion_General();

            Boolean confirmacion;

            confirmacion = conexion.Conexion_con_la_base(datos_de_conexion);

            return confirmacion;

        }
    }
}
