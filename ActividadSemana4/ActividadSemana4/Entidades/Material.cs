using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadSemana4.Entidades
{
    public class Material
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public Material()
        {
            Codigo = 0;
            Nombre = string.Empty;
            Stock= 0;
        }
        public Material(int codigo, string nombre,int stock)
        {
            Codigo = codigo;
            Nombre = nombre;
            Stock = stock;
        }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
