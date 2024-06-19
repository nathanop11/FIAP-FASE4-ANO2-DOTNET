﻿using Fiap.Web.Alunos.Data.Contexts;
using Fiap.Web.Alunos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 

namespace Fiap.Web.Alunos.Controllers
{
    public class ClienteController : Controller
    {

        private readonly DatabaseContext _context;

        public ClienteController(DatabaseContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            // O método Include será explicado posteriomente
            var clientes = _context.Clientes.Include(c => c.Representante).ToList();
            return View(clientes);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Representantes = 
                new SelectList(_context.Representantes.ToList()
                                , "RepresentanteId"
                                , "NomeRepresentante");
            return View();
        }



        // Anotação de uso do Verb HTTP Post
        [HttpPost]
        public IActionResult Create(ClienteModel clienteModel)
        {
            _context.Clientes.Add(clienteModel);
            _context.SaveChanges();
            TempData["mensagemSucesso"] = $"O cliente {clienteModel.Nome} foi cadastrado com sucesso";
            return RedirectToAction(nameof(Index));
        }


        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        public IActionResult Detail(int id)
        {
            // Usando o método Include para carregar o representante associado
            var cliente = _context.Clientes
                            .Include(c => c.Representante) // Carrega o representante junto com o cliente
                            .FirstOrDefault(c => c.ClienteId == id); // Encontra o cliente pelo id

            if (cliente == null)
            {
                return NotFound(); // Retorna um erro 404 se o cliente não for encontrado
            }
            else
            {
                return View(cliente); // Retorna a view com os dados do cliente e seu representante
            }
        }




        // Anotação de uso do Verb HTTP Get
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            } else {  

                ViewBag.Representantes = 
                    new SelectList(_context.Representantes.ToList(), 
                                    "RepresentanteId", 
                                    "NomeRepresentante", 
                                    cliente.RepresentanteId);
                return View(cliente);
            }
        }


        [HttpPost]
        public IActionResult Edit(ClienteModel clienteModel)
        {
            _context.Update(clienteModel);
            _context.SaveChanges();
            TempData["mensagemSucesso"] = $"Os dados do cliente {clienteModel.Nome} foram alterados com sucesso";
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
                TempData["mensagemSucesso"] = $"Os dados do cliente {cliente.Nome} foram removidos com sucesso";
            }
            else
            {
                TempData["mensagemSucesso"] = "OPS !!! Cliente inexistente.";
            }
            return RedirectToAction(nameof(Index));
        }


    }
}