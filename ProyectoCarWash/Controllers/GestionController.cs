using ProyectoCarWash.Logica;
using ProyectoCarWash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoCarWash.Controllers
{
    public class GestionController : Controller
    {
        // GET: Gestion
        public ActionResult Recepcion()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Gestion
        public ActionResult RecepcionRegistro(int idplataforma)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            Plataforma objeto = HabitacionLogica.Instancia.Listar().Where(h => h.IdPlataforma == idplataforma).FirstOrDefault();

            return View(objeto);
        }

        public ActionResult DetalleRecepcion(int idPlataforma)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            RecepcionLavado oRecepcion = RecepcionLogica.Instancia.Listar().Where(h => h.oPlataforma.IdPlataforma == idPlataforma && h.Estado == true).FirstOrDefault();

            if (oRecepcion != null)
            {

                List<Venta> oVenta = (from vn in VentaLogica.Instancia.Listar()
                                      where vn.oRecepcion.IdRecepcionLavado == oRecepcion.IdRecepcionLavado
                                      select new Venta()
                                      {
                                          IdVenta = vn.IdVenta,
                                          oRecepcion = new RecepcionLavado() { IdRecepcionLavado = vn.oRecepcion.IdRecepcionLavado },
                                          Total = vn.Total,
                                          Estado = vn.Estado,
                                          oDetalleVenta = DetalleVentaLogica.Instancia.Listar().Where(dv => dv.IdVenta == vn.IdVenta).ToList()
                                      }).ToList();

                oRecepcion.oVenta = oVenta;
            }

            return View(oRecepcion);
        }

        // GET: Gestion
        public ActionResult Salida()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Gestion
        public ActionResult SalidaRegistro(int idPlataforma)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            RecepcionLavado oRecepcion = RecepcionLogica.Instancia.Listar().Where(h => h.oPlataforma.IdPlataforma == idPlataforma && h.Estado == true).FirstOrDefault();

            if (oRecepcion != null) {

                List<Venta> oVenta = (from vn in VentaLogica.Instancia.Listar()
                                      where vn.oRecepcion.IdRecepcionLavado == oRecepcion.IdRecepcionLavado
                                      select new Venta()
                                      {
                                          IdVenta = vn.IdVenta,
                                          oRecepcion = new RecepcionLavado() { IdRecepcionLavado = vn.oRecepcion.IdRecepcionLavado },
                                          Total = vn.Total,
                                          Estado = vn.Estado,
                                          oDetalleVenta = DetalleVentaLogica.Instancia.Listar().Where(dv => dv.IdVenta == vn.IdVenta).ToList()
                                      }).ToList();

                oRecepcion.oVenta = oVenta;
            }

            return View(oRecepcion);
        }

        // GET: Gestion
        public ActionResult Venta(int idPlataforma)
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            RecepcionLavado objeto = RecepcionLogica.Instancia.Listar().Where(h => h.oPlataforma.IdPlataforma == idPlataforma && h.Estado == true).FirstOrDefault();

            return View(objeto);
        }





        [HttpGet]
        public JsonResult ListarPlataforma(int idsucursal)
        {
            List<Plataforma> oLista = new List<Plataforma>();
            oLista = HabitacionLogica.Instancia.Listar().Where(x => x.oSucursal.IdSucursal == (idsucursal == 0 ? x.oSucursal.IdSucursal : idsucursal) ).OrderBy(o => o.Numero).ToList();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ActualizarEstadoHabitacion(int idhabitacion,int idestadohabitacion)
        {

            bool respuesta = false;
            respuesta = HabitacionLogica.Instancia.ActualizarEstado(idhabitacion, idestadohabitacion);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarPersona(RecepcionLavado objeto)
        {
            bool respuesta = false;
            respuesta = RecepcionLogica.Instancia.Registrar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistrarRecepcion(RecepcionLavado objeto)
        {
            bool respuesta = false;
            objeto.Observacion = objeto.Observacion == null ? "" : objeto.Observacion;
            respuesta = RecepcionLogica.Instancia.Registrar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CerrarRecepcion(RecepcionLavado objeto)
        {
            bool respuesta = false;
            respuesta = RecepcionLogica.Instancia.Cerrar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}