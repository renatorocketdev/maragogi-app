using SQLite;

namespace AppTesteBinding.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string MainCategoria { get; set; }
        public string SubCategoria { get; set; }
        public string SubCategoriaTranslate { get; set; }
        public byte[] Icone { get; set; }

    }
}
