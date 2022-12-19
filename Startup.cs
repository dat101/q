using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using Microsoft.AspNetCore.Http;
using PetShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using PetShop.Mail;

namespace PetShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<PetShopContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PetShopContext")));
            services.AddScoped<IPetShopRepository,EFPetShopRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();
            services.AddRazorPages();           
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<MyCart>(sp => MySessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddServerSideBlazor();

            // Thêm vào dịch vụ Identity với cấu hình mặc định cho AppUser (model user) vào IdentityRole (model Role - vai trò)
            // Thêm triển khai EF lưu trữ thông tin về Idetity (theo AppDbContext -> MS SQL Server).
            // Thêm Token Provider - nó sử dụng để phát sinh token (reset password, confirm email ...)
            // đổi email, số điện thoại ...
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<PetShopContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);  // Khóa 2 phút
                options.Lockout.MaxFailedAccessAttempts = 3;                        // Thất bại 3 lần thì khóa
            });

            services.AddOptions();                                        // Kích hoạt Options
            var mailsettings = Configuration.GetSection("MailSettings");  // đọc config
            services.Configure<MailSettings>(mailsettings);               // đăng ký để Inject

            services.AddTransient<IEmailSender, SendMailService>();
            services.AddTransient<IContactSender, ContactMailService>();// Đăng ký dịch vụ Mail

            services.AddAuthentication()
                    .AddGoogle(googleOptions =>
                    {
                        // Đọc thông tin Authentication:Google từ appsettings.json
                        IConfigurationSection googleAuthNSection = Configuration.GetSection("Authentication:Google");
                        // Thiết lập ClientID và ClientSecret để truy cập API google
                        googleOptions.ClientId = googleAuthNSection["ClientId"];
                        googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
                        // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
                        googleOptions.CallbackPath = "/dang-nhap-tu-google";
                    })

                    .AddFacebook(facebookOptions =>
                    {
                        // Đọc cấu hình
                        IConfigurationSection facebookAuthNSection = Configuration.GetSection("Authentication:Facebook");
                        facebookOptions.AppId = facebookAuthNSection["AppId"];
                        facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
                        // Thiết lập đường dẫn Facebook chuyển hướng đến
                        facebookOptions.CallbackPath = "/dang-nhap-tu-facebook";
                    });

            services.ConfigureApplicationCookie(options => {
                // options.Cookie.HttpOnly = true;
                // options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = $"/login/";
                options.LogoutPath = $"/logout/";
                options.AccessDeniedPath = $"/Home/";
            });

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                // Trên 30 giây truy cập lại sẽ nạp lại thông tin User (Role)
                // SecurityStamp trong bảng User đổi -> nạp lại thông tinn Security
                options.ValidationInterval = TimeSpan.FromSeconds(30);
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();   // Phục hồi thông tin đăng nhập (xác thực)
            app.UseAuthorization();   // Phục hồi thông tin về quyền của User

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("spepage", "{species}/{ProductPage:int}",
                new { Controller = "Home", action = "ThuCung" });
                endpoints.MapControllerRoute("default", "{controller}/{action}",
                new { controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("page", "{ProductPage:int}",
                new { Controller = "Home", action = "ThuCung", ProductPage = 1 });
                endpoints.MapControllerRoute("species", "{species}",
                new { Controller = "Home", action = "ThuCung", ProductPage = 1 });
                endpoints.MapControllerRoute("pagination", "Pet/{ProductPage}",
                new { Controller = "Home", action = "ThuCung", ProductPage = 1 });
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}",
               "/Admin/Index");
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
