using System;
using System.Collections.Generic;
using System.Text;

namespace AppTesteBinding.Models
{
    public class ApiTranslate
    {
        public int code { get; set; }
        public string lang { get; set; }
        public List<string> text { get; set; }
    }
}
