//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebTecnoPro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venta
    {
        public int idVenta { get; set; }
        public string nombreCliente { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
        public string descripcion { get; set; }
        public System.DateTime fecha { get; set; }
        public string estado { get; set; }
        public int idProducto { get; set; }
        public int idEmpleado { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
