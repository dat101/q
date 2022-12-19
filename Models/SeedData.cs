using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetShop.Data;
using System;
using System.Linq;

namespace PetShop.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            PetShopContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<PetShopContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            // Kiểm tra thông tin Pet đã tồn tại hay chưa
            if (context.Pet.Any())
            {
                return; // Không thêm nếu Pet đã tồn tại trong DB
            }
            context.Pet.AddRange(
            new Pet
            {
                PetName = "Mèo Xiêm",
                CreateAt = DateTime.Parse("2022-10-16"),
                Category = "Mèo",
                Price = 500000,
                Gender = "Đực",
                PictureURL = "https://sieupet.com/sites/default/files/meo_xiem_hoang_gia2.jpg"
            },
            new Pet
            {
                PetName = "Mèo Ragdoll",
                CreateAt = DateTime.Parse("2022-8-3"),
                Category = "Mèo",
                Price = 700000,
                Gender = "Cái",
                PictureURL = "https://sieupet.com/sites/default/files/giong_meo_ragdoll9.jpg"
            },
             new Pet
             {
                 PetName = "Chó Shiba",
                 CreateAt = DateTime.Parse("2022-5-29"),
                 Category = "Chó",
                 Price = 600000,
                 Gender = "Đực",
                 PictureURL = "https://i.pinimg.com/originals/e4/c8/cb/e4c8cb3ec889cce6c9d5411239bb40db.jpg"
             },
             new Pet
             {
                 PetName = "Chó Husky",
                 CreateAt = DateTime.Parse("2022-10-25"),
                 Category = "Chó",
                 Price = 1500000,
                 Gender = "Đực",
                 PictureURL = "https://azpet.com.vn/wp-content/uploads/2019/09/ly-do-husky-5.jpg"
             }
            );
            context.SaveChanges();//lưu dữ liệu
        
        }
    }
}
