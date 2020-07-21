using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesteBinding.Models
{
    public class CategoriaMaragogi
    {
        public int CategoriaMaragogiId { get; set; }
        public string Nome { get; set; }
        public byte[] Icone { get; set; }
        public string Foto { get; set; }
        public string Localizacao { get; set; }
        public string UrlVideo { get; set; }
        public string Descricao { get; set; }
    }
}
