using System;
using System.Web;
using System.Threading.Tasks;
using DrinksNPicsAdmin.Models.Firebase;
using DrinksNPicsAdmin.Services;
using Firebase.Auth;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrinksNPicsAdmin.Controllers
{
    public class AccountController : Controller
    {                
        public IActionResult Login()
        {          
            return View();
        }

        public async Task<ActionResult> Validate(UserDP _user)
        {                                                   
            FirebaseService firebaseService = new FirebaseService();
            UserDP aux = await firebaseService.getUser(_user.username);
            if (aux == null) return Json(new {status = false, message = "Invalid Username!"});
            if (aux.password != _user.password) return Json(new {status = false, message = "Invalid Password!"});
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10),
                IsEssential = true
            };                        
            HttpContext.Response.Cookies.Append("User", _user.username, option);
            return Json(new { status = true, message = "Login Successful!"});
        }        
    }
}