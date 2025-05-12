using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Modules
{
    public class Usuario : Datos_Personales
    {
        public Usuario()
        {
        }

        [JsonIgnore]
        //Lista de empresa del empleado
        public List<Empresa> lista_de_empresas { get; set; }

        public Usuario(int codigo, string cedula, string primer_nombre, string segundo_nombre, string primer_apellido, string segundo_apellido, string telefono, string correo, char sexo, byte[] foto) : base(codigo, cedula, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, telefono, correo, sexo, foto)
        {
        }

        public Usuario(List<Empresa> lista_de_empresas)
        {
            this.lista_de_empresas = lista_de_empresas;
        }

    }
}
