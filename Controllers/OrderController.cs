using DemoIndentityCore.Areas.Identity.Data;
using DemoIndentityCore.Data;
using DemoIndentityCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoIndentityCore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly DemoIndentityCoreContext _context;
        private IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<DemoIndentityCoreUser> _user;

        public OrderController(DemoIndentityCoreContext context, IWebHostEnvironment _webHostEnvironment, UserManager<DemoIndentityCoreUser> user)
        {
            _context = context;
            webHostEnvironment = _webHostEnvironment;
            _user = user;
        }

        // GET: OrderController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: OrderController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: OrderController/Create
        public IActionResult Create()
        {
            var users = _user.Users;
            ViewBag.listofUser = users;
            List<Car> list = new List<Car>();
            list = (from Car in _context.Cars select Car).ToList();
            ViewBag.listofCar = list;
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        public ActionResult Create(Order o)
        {
            o.CreateDate = DateTime.Now;
            Car c = _context.Cars.Find(o.CarId);
            o.CarName = c.CarName;
            o.Price = c.Price;
            o.Photo = c.Photo;
            o.Quantity = 1;
            _context.Orders.Add(o);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: OrderController/Edit/5
        public async Task<ActionResult> Edit(int? id, Order o)
        {
            var users = _user.Users;
            ViewBag.listofUser = users;
            List<Car> list = new List<Car>();
            list = (from Car in _context.Cars select Car).ToList();
            ViewBag.listofCar = list;

            if (id == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Order order)
        {

            Car c = _context.Cars.Find(order.CarId);
            order.CarName = c.CarName;
            order.Price = c.Price;
            order.Photo = c.Photo;
            order.Quantity = 1;
            order.CreateDate = DateTime.Now;
            if (id != order.OrderId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);

        }

        // GET: OrderController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    
}
}
