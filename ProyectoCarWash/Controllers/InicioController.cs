using ProyectoCarWash.Logica;
using ProyectoCarWash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoCarWash.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Inicio
        public ActionResult Usuario()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Inicio
        public ActionResult Cliente()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult ListarUsuario()
        {
            List<Usuario> oLista = new List<Usuario>();

            oLista = PersonaLogica.Instancia.ListarUsuarios().Where(u => u.oPersona.oTipoPersona.IdTipoPersona != 3 && u.Estado == true).ToList();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarTipoPersona()
        {
            List<TipoPersona> oLista = new List<TipoPersona>();
            oLista = PersonaLogica.Instancia.ListarTipoPersona();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarPersona()
        {
            List<Persona> oLista = new List<Persona>();

            oLista = PersonaLogica.Instancia.ListarPersonas().Where(p => p.oTipoPersona.IdTipoPersona == 3 && p.Estado == true).ToList();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarVehiculosXpersona(int idpersona)
        {
            List<Vehiculo> oLista = new List<Vehiculo>();

            oLista = PersonaLogica.Instancia.ListarVehiculos(idpersona);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarVehiculos()
        {
            List<Vehiculo> oLista = new List<Vehiculo>();

            oLista = PersonaLogica.Instancia.ListarVehiculos();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarServicios()
        {
            List<Servicio> oLista = new List<Servicio>();

            oLista = PersonaLogica.Instancia.ListarServicios();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarPersona(Persona objeto)
        {
            bool respuesta = false;
            //objeto.Clave = objeto.Clave == null ? "" : objeto.Clave;
            objeto.TipoDocumento = objeto.TipoDocumento == null ? "" : objeto.TipoDocumento;
            respuesta = (objeto.IdPersona == 0) ? PersonaLogica.Instancia.Registrar(objeto) : PersonaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminarPersona(int id)
        {
            bool respuesta = false;
            respuesta = PersonaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}