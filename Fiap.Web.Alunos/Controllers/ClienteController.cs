using Fiap.Web.Alunos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Web.Alunos.Controllers
{
    public class ClienteController : Controller
    {
        //Lista para armazenar os clientes
        public IList<ClienteModel> clientes { get; set; }


        public ClienteController()
        {
            //Simula a busca de clientes no banco de dados
            clientes = GerarClientesMocados();
        }

        public IActionResult Index()
        {
            // Evitando valores null 
            if (clientes == null)
            {
                clientes = new List<ClienteModel>();
            }

            return View(clientes);
        }


        /**
         * Este método estático GerarClientesMocados 
         * cria uma lista de 5 clientes com dados fictícios
         */
        public static List<ClienteModel> GerarClientesMocados()
        {
            var clientes = new List<ClienteModel>();

            for (int i = 1; i <= 5; i++)
            {
                var cliente = new ClienteModel
                {
                    ClienteId = i,
                    Nome = "Cliente" + i,
                    Sobrenome = "Sobrenome" + i,
                    Email = "cliente" + i + "@example.com",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    Observacao = "Observação do cliente " + i,
                    RepresentanteId = i,
                    Representante = new RepresentanteModel
                    {
                        RepresentanteId = i,
                        NomeRepresentante = "Representante" + i,
                        Cpf = "00000000191"
                    }
                };

                clientes.Add(cliente);
            }

            return clientes;
        }

    }
}
