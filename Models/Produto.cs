using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Models
{
    public class Produto
    {

        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public int Preco { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public ICollection<Movimentacao> Movimentacoes { get; set; }

    }
}
