using AdminPanel.Data.Operations;
using AdminPanel.Services.Infrastructure;
using AdminPanel.Services.Mail;
using AdminPanelWeb.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace AdminPanelWeb
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

            services.AddSession();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISmtpBuilder, SmtpBuilder>();
            services.AddTransient<IFileUploadService, FileUploadService>();
            services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation(); 
            services.AddSingleton<IUserOperation, UserOperation>();
            services.AddSingleton<ICarOperation, CarOperation>();
            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));
            services.AddHttpContextAccessor();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AdminMapperConfiguration());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseCors(builder =>
             builder.AllowAnyOrigin()
             .AllowAnyHeader()
             .AllowAnyMethod());

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
