using PetShop.Models.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity.UI.Services
{
    public interface IContactSender
    {
        Task SendContactMailAsync(string email, string subject, string htmlMessage);
    }
}