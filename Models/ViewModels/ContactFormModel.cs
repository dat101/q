using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Models.ViewModels
{
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Không được để trống")]
        public string Name { set; get; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Subject { set; get; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Body { set; get; }
    }
}
