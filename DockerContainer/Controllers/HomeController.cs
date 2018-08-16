using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DockerContainer.Models;
using DockerContainer.Data;

namespace DockerContainer.Controllers
{
    public class HomeController : Controller
    {
        dbUATContext _db = new dbUATContext();
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                var user = _db.TblUser.Any(x=>x.Email.Equals(model.Email));
                if (!user)
                {
                    TblUser tblUser = new TblUser()
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        Password = model.Password,
                        LastName = model.LastName
                    };
                    _db.TblUser.Add(tblUser);
                    _db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Class = "alert-success";
                    ViewBag.Message = "Registered successfully";
                }
                else
                {
                    ViewBag.Class = "alert-danger";
                    ViewBag.Message = "Email already exists";
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Class = "alert-danger";
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                var user = _db.TblUser.Any(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password));
                if (user)
                {
                    ModelState.Clear();
                    ViewBag.Class = "alert-success";
                    ViewBag.Message = "Logged in successfully";
                }
                else
                {
                    ViewBag.Class = "alert-danger";
                    ViewBag.Message = "Email id/Password wrong";
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Class = "alert-danger";
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
