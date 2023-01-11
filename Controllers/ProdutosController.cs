using Estoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estoque.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly Contexto _contexto;

        public ProdutosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Produtos.Include(p => p.Categoria).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> NovoProduto()
        {
            ViewData["CategoriaId"] = new SelectList(await _contexto.Categorias.ToListAsync(), "CategoriaId", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovoProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                await _contexto.Produtos.AddAsync(produto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriaId"] = new SelectList(await _contexto.Categorias.ToListAsync(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarProduto(int produtoId)
        {
            Produto produto = await _contexto.Produtos.FindAsync(produtoId);

            ViewData["CategoriaId"] = new SelectList(await _contexto.Categorias.ToListAsync(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Produtos.Update(produto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(await _contexto.Categorias.ToListAsync(), "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

    }
}
