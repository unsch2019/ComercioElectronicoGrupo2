﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace Tienda2.Controllers
{
    public class ComunController : Controller
    {
        public static string ObtenerEmpresa()
        {
            return ConfigurationManager.AppSettings["Empresa"];
        }
    }
}