using Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tienda2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Autenticar(UsuarioVm u)
        {
            var rm = new Comun.ResponseModel();
            //password = Comun.HashHelper.MD5(password);
            u.Clave = u.Clave.ToLower();
            Datos.Usuario usuario;
            using (var bd = new Datos.ComercioEntities())
            {
                usuario = bd.Usuario.FirstOrDefault(x => x.Correo == u.Usuario && x.Clave == u.Clave && x.Activo);
            }
            if (usuario != null)
            {
                //if (usuario.IndCambio)
                //{
                //    rm.SetResponse(true);
                //    //rm.function = "nuevaclave(" + usuario.UsuarioId + ",'" + username + "');";
                    
                //}
                //else
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
    }
}