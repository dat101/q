using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using PetShop.Data;
using PetShop.Models;
using PetShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PetShop.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public int PageSize = 5;
        private IPetShopRepository repository;
        private readonly IContactSender _contactSender;

        public string CurrentSpecies { get; private set; }
        public IQueryable<Pet> Pet { get; private set; }

        public HomeController(IPetShopRepository repo, IContactSender contactSender)
        {
            repository = repo;
            _contactSender = contactSender;
        }

        public IActionResult Product(string species, int ProductPage = 1)
        {
            var PetListVM = new PetListViewModel
            {
                Pet = repository.Pet
                    .Where(p => species == null || p.Category == species)
                    .OrderBy(p => p.Id)
                    .Skip((ProductPage - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = ProductPage,
                    ItemsPerPage = PageSize,
                    TotalItems = species == null ?
                    repository.Pet.Count() :
                    repository.Pet.Where(e => e.Category == species).Count()
                },
                CurrentSpecies = species
            };
            
            return View(PetListVM);
        }

        public async Task<IActionResult> Index()
        {
            var pets = from b in repository.Pet
                        select b;
         
            return View(await pets.ToListAsync());          
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormModel model)
        {
            var msg = model.Name + Environment.NewLine + model.Body;

            try {
                await _contactSender.SendContactMailAsync(model.Email, model.Subject, msg);
                ViewBag.SendSuccess = "Gửi thành công";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ViewBag.SendFailed = "Gửi không thành công";
            }
            return View();
        }

        public async Task<IActionResult> SearchResult(string SearchString, int SortProduct)
        {
            var pets = from b in repository.Pet
                       select b;

            if (SortProduct == 1)
            {
                pets = pets.OrderBy(b => b.Price);
            }

            if (SortProduct == 2)
            {
                pets = pets.OrderByDescending(b => b.Price);
            }

            if (SortProduct == 3)
            {
                pets = pets.OrderBy(b => b.CreateAt);
            }
            if (SortProduct == 4)
            {
                pets = pets.OrderByDescending(b => b.CreateAt);
            }          

            if (!String.IsNullOrEmpty(SearchString))
            {
                pets = pets.Where(s => s.PetName!.Contains(SearchString));
            }
            return View(await pets.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await repository.Pet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
