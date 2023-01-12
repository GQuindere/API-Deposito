using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Estoque.Models;

namespace Estoque.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly Contexto _contexto;

        public MovimentacoesController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult NovaMovimentacao(int produtoId)
        {
            Movimentacao movimentacao = new Movimentacao
            { ProdutoId = produtoId };

            return View(movimentacao);
        }

        [HttpPost]
        public async Task<IActionResult> NovaMovimentacao(Movimentacao movimentacao)
        {
            movimentacao.DataMovimentacao = DateTime.Now.ToString();

            if (ModelState.IsValid)
            {
                await _contexto.Movimentacoes.AddAsync(movimentacao);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("DetalhesProduto", "Produtos", new { produtoId = movimentacao.ProdutoId });
            }

            return View(movimentacao);
        }
    }
}