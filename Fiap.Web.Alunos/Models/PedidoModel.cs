using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap.Web.Alunos.Models
{
    public class PedidoModel
    {
        public int PedidoId { get; set; }
        public DateTime DataPedido { get; set; }
        // Relacionamento com Cliente
        public int ClienteId { get; set; }
        public ClienteModel Cliente { get; set; }
        // Relacionamento com Loja
        public int LojaId { get; set; }
        public LojaModel Loja { get; set; }
        // Relacionamento com Produto
        public List<PedidoProdutoModel> PedidoProdutos { get; set; }
    }
}