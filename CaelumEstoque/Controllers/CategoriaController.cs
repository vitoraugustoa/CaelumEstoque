using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System.Collections;

namespace CaelumEstoque.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            CategoriasDAO dao = new CategoriasDAO();

            IList<CategoriaDoProduto> categorias = dao.Lista();
            ViewBag.CategoriasDeProdutos = categorias;
            return View();
        }

        public ActionResult Form()
        {

            return View();
        }

        public ActionResult AdicionarCategoria(CategoriaDoProduto categoria)
        {
            CategoriasDAO dao = new CategoriasDAO();
            dao.Adiciona(categoria);

            return RedirectToAction("Index", "Categoria");
        }
    }
}