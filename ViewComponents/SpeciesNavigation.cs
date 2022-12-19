using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetShop.Data;
namespace PetShop.ViewComponents
{
    public class SpeciesNavigation : ViewComponent
    {
        private IPetShopRepository repository;
        public SpeciesNavigation(IPetShopRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedSpecies = RouteData?.Values["species"];
            return View(repository.Pet
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x));
        }
    }
}