using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Data
{
    public class MyCart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public virtual void AddItem(Pet pet, int quantity)
        {
            CartLine line = Lines
            .Where(b => b.Pet.Id == pet.Id)
            .FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Pet = pet,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Pet pet) =>
        Lines.RemoveAll(l => l.Pet.Id == pet.Id);
        public decimal ComputeTotalValue() =>
        Lines.Sum(e => e.Pet.Price * e.Quantity);
        public virtual void Clear() => Lines.Clear();
    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Pet Pet { get; set; }
        public int Quantity { get; set; }
    }
}