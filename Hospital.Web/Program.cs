using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Hospital.Application.Patients;
using Hospital.Application.Contracts.Patients;
using Hospital.In.Repositories;
using Hospital.Infrastructure.Repositories;
using Hospital.Application.Contracts.Pagination;

namespace Hospital.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<ApplicationDbContext>();

            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IPatientAppService<PatientDTO,Guid,CreateUpdatePatientDTO>, PatientAppService>();

            builder.Services.AddResponseCompression();

            //add swagger
            //builder.Services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v2", new OpenApiInfo { Title = "MVCCallWebAPI", Version = "v2" });
            //});
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //add swagger
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v2/swagger.json", "MVCCallWebAPI");
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
