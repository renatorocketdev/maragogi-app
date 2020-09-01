using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesteBinding.Models
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string NomeEmpresa { get; set; }
        public string Endereco { get; set; }
        public string Telefone1Empresa { get; set; }
        public string Telefone2Empresa { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Site { get; set; }
        public string SobreEmpresa { get; set; }
        public string Carac1 { get; set; }
        public string Carac2 { get; set; }
        public string Carac3 { get; set; }
        public string Carac4 { get; set; }
        public string Serv1 { get; set; }
        public string Serv2 { get; set; }
        public string Serv3 { get; set; }
        public string Serv4 { get; set; }
        public decimal Nota { get; set; }
        public string MainCategoria { get; set; }
        public string SubCategoria { get; set; }
        public byte[] Icone { get; set; }
        public string Video { get; set; }
        public int QtdVotos { get; set; }
        public string Rating { get; set; }
        public double Media { get; set; }
    }
}
