using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteBludata
{
    [MetadataType(typeof(TelefoneHelper))]
    public partial class Telefone { }

    public class TelefoneHelper
    {
        [Display(Name = "Telefone", Description = "Número do Telefone")]
        public string num_tel { get; set; }
    }
}