using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCommonModels.Models
{
    public class OrdemProcessada
    {
        public int Id { get; set; }
        public string TipoOrdem { get; set; } = string.Empty;
        public string NomeAtivo { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
