using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio;
namespace Tienda2.Controllers
{
    public class LoginController : Controller
    {
        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Autenticar(UsuarioVm u)
        {
            var rm = new Comun.ResponseModel();
            //password = Comun.HashHelper.MD5(password);
            u.Clave = u.Clave.ToLower();

            var usuario= UsuarioBL.Obtener(x => x.Correo == u.Usuario && x.Clave == u.Clave && x.Activo);
            if (usuario != null)
            {
                if (!usuario.IndCambio)
                {
                    rm.SetResponse(true);
                    rm.function = "nuevaclave(" + usuario.Id + ",'" + usuario.Correo + "');";
                }
                else
                {
                    AddSesion(usuario.Id, ref rm);
                    rm.SetResponse(true);
                    rm.href = Url.Action("Index", "Home");
                }
            }
            else
            {
                rm.SetResponse(false, "Usuario o Clave Incorrecta");
            }
            return Json(rm);
        }
        private void AddSesion(int usuarioId, ref Comun.ResponseModel rm)
        {
            Comun.SessionHelper.AddUserToSession(usuarioId.ToString());
            rm.SetResponse(true);
            rm.href = Url.Action("Index", "Home");
            //Session["menu"] = MenuBL.ListarMenuPermiso(usuarioId);
            //rm.function = "localStorage.setItem('mnuclick', 'mnuhome'); $.ajax({url:'Login/_CargarMenu',dataType:'html',cache: false,success: function(d) {localStorage.setItem('mnu', d)} });";
        }
        public ActionResult Logout()
        {
            Comun.SessionHelper.DestroyUserSession();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public JsonResult CambiarClave(int id, string usuario, string clave)
        {
            var rm = new Comun.ResponseModel();
            try
            {
                //var enc = Comun.HashHelper.MD5(clave);
                UsuarioBL.ActualizarParcial(new Datos.Usuario { Id = id, Clave = clave, IndCambio = true }, x => x.Clave, x => x.IndCambio);
                rm.SetResponse(true);
                Autenticar(new UsuarioVm { Usuario = usuario, Clave = clave });
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, ex.Message);
            }
            return Json(rm);
        }
    }
}