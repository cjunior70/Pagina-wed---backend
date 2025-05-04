using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class Cliente : Datos_Personales
    {
        public Cliente()
        {
        }

        //Lista de reservaciones del cliente
        public List<Reservacion> lista_De_reservaciones = new List<Reservacion>();

        public Cliente(List<Reservacion> lista_De_reservaciones)
        {
            this.lista_De_reservaciones = lista_De_reservaciones;
        }

        public Cliente(int codigo, string cedula, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, string telefono, string correo, char sexo, byte[] foto) : base(codigo, cedula, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, telefono, correo, sexo, foto)
        {
        }
    }
}
