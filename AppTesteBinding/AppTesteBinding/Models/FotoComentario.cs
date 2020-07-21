using System;

namespace AppTesteBinding.Models
{
    public class FotoComentario
    {
        public string Comentario { get; set; }
        public string DataComentario { get; set; }
        public string Empresa { get; set; }
        public byte[] Foto { get; set; }
        public string Usuario { get; set; }
        public string Email { get; set; }
    }
}
