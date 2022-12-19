using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Data
{
    public interface IPetShopRepository
    {
        IQueryable<Pet> Pet { get; }
        void SavePet(Pet b);
        void CreatePet(Pet b);
        void DeletePet(Pet b);
    }
}
