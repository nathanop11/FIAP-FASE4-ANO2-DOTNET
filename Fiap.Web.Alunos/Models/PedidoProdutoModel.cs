using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap.Web.Alunos.Models
{
    public class PedidoProdutoModel
    {
        public int PedidoId { get; set; }
        public PedidoModel Pedido { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }
    }
}