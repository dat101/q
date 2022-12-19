using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace PetShop.Data
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        public string address { get; set; }
        [Required(ErrorMessage = "Thành phố không được bỏ trống")]
        public string City { get; set; }
        [Required(ErrorMessage = "Quận, huyện không được bỏ trống")]
        public string Town { get; set; }
        public string Zip { get; set; }
        [Required(ErrorMessage = "Quốc gia không được bỏ trống")]
        public string Country { get; set; }
        [BindNever]
        public bool Shipped { get; set; }
    }
}