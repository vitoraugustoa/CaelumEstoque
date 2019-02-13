using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using CaelumEstoque.Filtros;

namespace CaelumEstoque.Controllers
{
    [AutorizacaoFilter]
    public class ProdutoController : Controller
    {

        // GET: Produto
        [Route("produtos", Name = "ListaProdutos" )] //Configurando urls  
        public ActionResult Index()
        {
                ProdutosDAO dao = new ProdutosDAO();

                IList<Produto> produtos = dao.Lista();
                // Criei um atributo na ViewBag Produtos e adicionei a lista de produtos
                return View(produtos);
        }

        public ActionResult Form()
        {
            CategoriasDAO categoriasDAO = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = categoriasDAO.Lista();
            ViewBag.Categoria = categorias;
            ViewBag.Produto = new Produto();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adiciona(Produto produto)
        {
            int idDaInformatica = 1;
            if(produto.CategoriaId.Equals(idDaInformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.Invalido","Informatica com preco abaixo de 100 reais !");
            }
            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto);

                return RedirectToAction("Index", "Produto");
            }
            else
            {
                ViewBag.Produto = produto;
                CategoriasDAO cat = new CategoriasDAO();
                ViewBag.Categoria = cat.Lista();
                return View("Form");
            }

        }

        [Route("produtos/{id}", Name ="VisualizaProduto")]
        public ActionResult Visualiza(int Id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(Id);
            ViewBag.Produto = produto;
            return View();
        }

        public ActionResult DecrementaQtd(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            produto.Quantidade--;
            dao.Atualiza(produto);
            return Json(produto);
        }

    }


}