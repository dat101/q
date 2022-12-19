using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetShop.MyTagHelper;
using PetShop.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace PetShop.Pages
{
    public class MyCartModel : PageModel
    {
        private IPetShopRepository repository;
        public MyCartModel(IPetShopRepository repo, MyCart myCartService)
        {
            repository = repo;
            myCart = myCartService;
        }
        public MyCart myCart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }
        public IActionResult OnPost(long Id, string returnUrl)
        {
            Pet pet = repository.Pet
            .FirstOrDefault(b => b.Id == Id);           
            myCart.AddItem(pet, 1);          
            return RedirectToPage(new { returnUrl = returnUrl });
        }
        public IActionResult OnPostRemove(long Id, string returnUrl)
        {
            myCart.RemoveLine(myCart.Lines.First(cl =>
            cl.Pet.Id == Id).Pet);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}