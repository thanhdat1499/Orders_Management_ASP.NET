using DemoIndentityCore.Areas.Identity.Data;
using DemoIndentityCore.Data;
using DemoIndentityCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoIndentityCore.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        private readonly DemoIndentityCoreContext _db;
        private readonly UserManager<DemoIndentityCoreUser> _userManager;
        //DI: Tiem constructor vao controller
        public AccountController(DemoIndentityCoreContext db, UserManager<DemoIndentityCoreUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        // GET: AccountController
        public ActionResult Index()
        {
            var lstUser = _db.Users.Select(x=> new UserViewModel { Id=x.Id,
                UserName=x.UserName,
                Email=x.Email,Firstname=x.Firstname,
                LastName=x.LastName,PhoneNumber=x.PhoneNumber}).ToList();
            return View(lstUser);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(string id)
        {
            var user_detail = _db.Users.Where(x => x.Id == id).Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Firstname = x.Firstname,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber
            }).FirstOrDefault();
            return View(user_detail);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                var user = new DemoIndentityCoreUser
                {
                    UserName = collection["UserName"],
                    Email = collection["Email"],
                    Firstname = collection["Firstname"],
                    LastName = collection["LastName"],
                    PhoneNumber = collection["PhoneNumber"]
                };
                var result = await _userManager.CreateAsync(user, "Abc**123");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(string id)
        {
            var user_detail = _db.Users.Where(x => x.Id == id).Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Firstname = x.Firstname,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber
            }).FirstOrDefault();
            return View(user_detail);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(string id, IFormCollection collection)
        {
            try
            {
                var user = _db.Users.Where(x => x.Id == id).FirstOrDefault();
                user.Firstname = collection["Firstname"];
                user.LastName = collection["LastName"];
                user.PhoneNumber = collection["PhoneNumber"];
                var result = await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public async Task<ActionResult> DeleteAsync(string id)
        {
            //lấy lại user có Id = id
            var user = _db.Users.Where(x => x.Id == id).FirstOrDefault();
            var result = await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
