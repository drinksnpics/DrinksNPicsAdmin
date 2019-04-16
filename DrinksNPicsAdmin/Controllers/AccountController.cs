using System;
using System.Threading.Tasks;
using DrinksNPicsAdmin.Models.Firebase;
using DrinksNPicsAdmin.Services;
using Firebase.Auth;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
            return aux != null ? Json(aux.password == _user.password ? new {status = true, message = "Login Successful!"} : new {status = false, message = "Invalid Password!"}) : Json(new { status = false, message = "Invalid Username!" });
        }
        
        /*public ActionResult Validate(Admins admin)
        {
            var _admin = db.Admins.Where(s => s.Email == admin.Email);
            if(_admin.Any()){
                if(_admin.Where(s => s.Password == admin.Password).Any()){
            
                    return Json(new { status = true, message = "Login Successful!"});
                }
                else
                {
                    return Json(new { status = false, message = "Invalid Password!"});
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid Email!"});
            }
        }*/
    }
}