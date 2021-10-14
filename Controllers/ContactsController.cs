using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_crud.Data;
using project_crud.Models;

namespace project_crud.Controllers
{
    public class ContactsController : Controller
    {
        private ApplicationDbContext _context;
        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        } 

        // Index action này sẽ hiện ra toàn bộ danh sách thông tin người dùng
        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return View(contacts);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            // validate that our model meets the requirement
            if (ModelState.IsValid)
            {
                try
                {
                    // update the ef core context in memory 
                    _context.Add(contact);

                    // sync the changes of ef code in memory with the database
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            // We return the object back to view
            return View(contact);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var exist = await _context.Contacts.Where(x => x.Id == id).FirstOrDefaultAsync();

            return View(exist);
        }
           [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            // validate that our model meets the requirement
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if the contact exist based on the id
                    var exist = _context.Contacts.Where(x => x.Id == contact.Id).FirstOrDefault();

                    // if the contact is not null we update the information
                    if(exist != null)
                    {
                        exist.FirstName = contact.FirstName;
                        exist.LastName = contact.LastName;
                        exist.Mobile = contact.Mobile;
                        exist.Email = contact.Email;

                        // we save the changes into the db
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            return View(contact);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _context.Contacts.Where(x => x.Id == id).FirstOrDefaultAsync();

            return View(exist);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _context.Contacts.Where(x => x.Id == contact.Id).FirstOrDefault();

                    if(exist != null)
                    {
                        _context.Remove(exist);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index");
                    } 
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Something went wrong {ex.Message}");
                }
            }

            ModelState.AddModelError(string.Empty, $"Something went wrong, invalid model");

            return View();
        }
    }
}