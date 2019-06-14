using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tienda2.Controllers
{
    public class MarcaController : Controller
    {
        public ActionResult Index()
        {
            return View(MarcaBL.Listar());
        }
        public ActionResult Mantener(int id = 0)
        {
            var marca = new Datos.Marca();
            if (id == 0)
            {
                return View(marca);
            }
            else
            {
                marca = MarcaBL.Obtener(id);
                return View(marca);
            }
        }
        [HttpPost]
        public ActionResult Guardar(Datos.Marca marca)
        {
            var rm = new Comun.ResponseModel();
            try
            {
                if (marca.Id == 0)
                {
                    MarcaBL.Crear(marca);                    
                }
                else {
                    MarcaBL.ActualizarParcial(marca, x => x.Denominacion);                    
                }
                rm.SetResponse(true);
                rm.href = Url.Action("Index", "Marca");
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, ex.Message);
            }
            return Json(rm, JsonRequestBehavior.AllowGet);
        }
    }
}