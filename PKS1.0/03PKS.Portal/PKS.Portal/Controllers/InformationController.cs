﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PKS.Portal.Controllers
{
    [AllowAnonymous]
    public class InformationController : Controller
    {
        // GET: Information
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NoAuthorize()
        {
            return View();
        }
    }
}