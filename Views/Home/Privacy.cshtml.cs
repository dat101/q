using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PetShop.Views.Home
{
    public class PrivacyModel : PageModel
    {
        public IActionResult OnGet()
        {
            return ViewComponent("MessagePage", new PetShop.MessagePage.Message
            {
                title = "Thông báo chuyển hướng",
                htmlcontent = "Bạn sẽ được chuyển sang một <strong>trang khác</strong>",
                secondwait = 1,
                urlredirect = "/"
            });
        }
    }
}
