using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap.Web.Alunos.Models
{
    public class ProdutoModel
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        // Relacionamento com Fornecedor
        public int FornecedorId { get; set; }
        public FornecedorModel Fornecedor { get; set; }
        // Relacionamento com Pedido
        public List<PedidoProdutoModel> PedidoProdutos { get; set; }
    }
}