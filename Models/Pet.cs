using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Data
{
    public class Pet
    {
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "Không dài quá {1} kí tự và không dưới {2} kí tự" ,MinimumLength = 3)]
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Thú cưng")]
        public string PetName { get; set; }

        [Display(Name = "Ngày tạo")]
        [Required(ErrorMessage = "Không được để trống")]
        [DataType(DataType.Date)]
        public DateTime CreateAt { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(30, ErrorMessage = "Không dài quá {1} kí tự và không dưới {2} kí tự", MinimumLength = 1)]
        [Display(Name = "Giống loài")]
        public string Category { get; set; }

        [Range(1, 1000000000)]
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Giá")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Giới tính")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        [Display(Name = "Hình ảnh")]
        public string PictureURL { get; set; }
    }
}
