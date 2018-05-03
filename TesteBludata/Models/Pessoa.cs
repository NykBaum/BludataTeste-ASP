using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteBludata
{
    [MetadataType(typeof(PessoaHelper))]
    public partial class Pessoa { }

    public class PessoaHelper
    {
        [Display(Name = "Nome", Description = "Nome do Curso")]
        public string nome { get; set; }
        [Display(Name = "CPF", Description = "Código de Pessoa Física")]
        public string cpf { get; set; }
        [Display(Name = "Dt. Cadastro", Description = "Data de Cadastro")]
        public System.DateTime data_cad { get; set; }
        [Display(Name = "Dt. Nascimento", Description = "Data de Nascimento")]
        public System.DateTime data_nasc { get; set; }
        [Display(Name = "RG", Description = "Registro Geral")]
        public string rg { get; set; }
    }
}