﻿using Microsoft.AspNetCore.Mvc;

namespace MVC75NET.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Unauthorized")]
        public IActionResult Unauthorized()
        {
            return View();
        }

        [Route("Forbidden")]
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
