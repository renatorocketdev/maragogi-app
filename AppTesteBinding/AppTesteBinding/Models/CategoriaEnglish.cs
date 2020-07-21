using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesteBinding.Models
{
    public class CategoriaEnglish
    {
        public int CategoriaId { get; set; }
        public string MainCategoria { get; set; }
        public string SubCategoria { get; set; }
        public string SubCategoriaTranslate { get; set; }
        public byte[] Icone { get; set; }
    }
}
