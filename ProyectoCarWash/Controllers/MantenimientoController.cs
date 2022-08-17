using ProyectoCarWash.Logica;
using ProyectoCarWash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoCarWash.Controllers
{
    public class MantenimientoController : Controller
    {
        // GET: Mantenimiento
        public ActionResult Servicios()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }
        public ActionResult Plataformas()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }
        // GET: Mantenimiento
        public ActionResult Categoria()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Mantenimiento
        public ActionResult Sucursales()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }



        [HttpGet]
        public JsonResult ListarCategoria()
        {
            List<Categoria> oLista = new List<Categoria>();
            oLista = CategoriaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdCategoria == 0) ? CategoriaLogica.Instancia.Registrar(objeto) : CategoriaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            respuesta = CategoriaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarSucursal()
        {
            List<Sucursal> oLista = new List<Sucursal>();
            oLista = PisoLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarEstadoPlataforma()
        {
            List<EstadoPlataforma> oLista = new List<EstadoPlataforma>();
            oLista = PisoLogica.Instancia.ListarEstadoPlataforma();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarSucursal(Sucursal objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdSucursal == 0) ? PisoLogica.Instancia.Registrar(objeto) : PisoLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarSucursal(int id)
        {
            bool respuesta = false;
            respuesta = PisoLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarServicio()
        {
            List<Servicio> oLista = new List<Servicio>();
            oLista = HabitacionLogica.Instancia.ListarServicio();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListarPlataformas()
        {
            List<Plataforma> oLista = new List<Plataforma>();
            oLista = HabitacionLogica.Instancia.ListarPlataformas();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarServicio(Servicio objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdServicio == 0) ? HabitacionLogica.Instancia.RegistrarServicio(objeto) : HabitacionLogica.Instancia.ModificarServicio(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarServicio(int id)
        {
            bool respuesta = false;
            respuesta = HabitacionLogica.Instancia.EliminarServicio(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardarPlataforma(Plataforma objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdPlataforma == 0) ? HabitacionLogica.Instancia.RegistrarPlataforma(objeto) : HabitacionLogica.Instancia.ModificarPlataforma(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarPlataforma(int id)
        {
            bool respuesta = false;
            respuesta = HabitacionLogica.Instancia.EliminarPlataforma(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}