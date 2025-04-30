namespace OrdemAPI.Models
{
    public class Ordem
    {
        public string TipoOrdem { get; set; } 
        public string NomeAtivo { get; set; } 
        public decimal Preco { get; set; }     
        public int Quantidade { get; set; }     

    }
}
