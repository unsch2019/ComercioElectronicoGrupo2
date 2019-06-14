using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tienda2.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View(UsuarioBL.Listar());
        }
        public ActionResult Mantener(int id=0)
        {
           if (id==0)
                return View(new Datos.Usuario() { Activo = true, IndCambio = false });
            else            
                return View(UsuarioBL.Obtener(id));
        }
        [HttpPost]
        public ActionResult Guardar(Datos.Usuario usuario,string activo)
        {
            var rm = new Comun.ResponseModel();
            usuario.Activo = activo == "ON" ? true : false;
            try
            {
                if (usuario.Id == 0)
                {
                    usuario.Clave = usuario.Correo;
                    usuario.IndCambio = false;
                    UsuarioBL.Crear(usuario);
                }
                else
                {
                    UsuarioBL.ActualizarParcial(usuario, x => x.Nombre, x => x.Correo, x => x.Celular, x => x.Activo);
                }
                rm.SetResponse(true);
                rm.href = Url.Action("Index", "Usuario");
                
            }
            catch (Exception ex)
            {
                rm.SetResponse(false,ex.Message);                
            }
            return Json(rm, JsonRequestBehavior.AllowGet);
        }

       

        //public ActionResult Listar() {
        //    var user = UsuarioBL.Listar();

        //    return Json(user.Select(x => new { x.Id, x.Nombre }), JsonRequestBehavior.AllowGet);
        //}
    }
}