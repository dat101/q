using System.Linq;
namespace PetShop.Data
{
    public class EFPetShopRepository : IPetShopRepository
    {
        private PetShopContext context;
        public EFPetShopRepository(PetShopContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Pet> Pet => context.Pet;
        public void CreatePet(Pet b)
        {
            context.Add(b);
            context.SaveChanges();
        }
        public void DeletePet(Pet b)
        {
            context.Remove(b);
            context.SaveChanges();
        }
        public void SavePet(Pet b)
        {
            context.SaveChanges();
        }
    }
}
