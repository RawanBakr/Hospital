using Hospital.Domain.Repositories;
using Hospital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Hospital.Application.Patients;
using Hospital.Application.Contracts.Patients;
using Hospital.In.Repositories;
using Hospital.Application.Contracts.Interfaces;
using Hospital.Application.Contracts.Pagination;
using Hospital.Application.CustomExceptionMiddleware;
using Hospital.Infrastructure.UnitOfWork;
using Hospital.Application.Doctors;
using Hospital.Application.Contracts.Doctors;
using Hospital.Application.Notes;
using Hospital.Application.Contracts.Notes;

namespace Hospital.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddScoped<ApplicationDbContext>();

        //builder.Services.AddScoped<IPatientRepository, PatientRepository>();
        builder.Services.AddScoped<IPatientAppService<PatientDTO, Guid, CreateUpdatePatientDTO>, PatientAppService>();
        builder.Services.AddScoped<IExceptionMiddlewareService, ExceptionMiddlewareService>();
        builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

        builder.Services.AddScoped<IDoctorAppService<DoctorDTO, Guid, CreateUpdateDoctorDTO>, DoctorAppServie>();

        builder.Services.AddScoped<INoteAppService<NoteDTO, Guid, CreateUpdateNoteDTO,PatientDTO>, NoteAppService>();

        builder.Services.AddResponseCompression();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

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
