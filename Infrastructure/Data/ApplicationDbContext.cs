using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Receptionist> Receptionists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Doctor>(x => {
            x.ToTable("Doctors");
            x.Property(e => e.Id).HasDefaultValueSql("NEWID()");
            x.Property(e=>e.Name).HasMaxLength(100);
            x.Property(m => m.Phone).HasMaxLength(100);
            });

        modelBuilder.Entity<Note>(x => {
            x.ToTable("Notes");
            x.Property(e => e.Id).HasDefaultValueSql("NEWID()");
            x.Property(m => m.Mediciness).HasMaxLength(500);
            x.HasOne<Patient>(p=>p.Patient).WithMany(n=>n.Notes).HasForeignKey(e=>e.PatientId).OnDelete(DeleteBehavior.NoAction);
            
        });

        modelBuilder.Entity<Patient>(x => {
            x.ToTable("Patients");
            x.Property(e => e.Id).HasDefaultValueSql("NEWID()");
            x.Property(m => m.Name).HasMaxLength(100);
            x.Property(m=>m.UserName).HasMaxLength(100);
            x.Property(m=>m.Password).HasMaxLength(100);
            x.HasMany<Note>(n=>n.Notes).WithOne(p=>p.Patient).HasForeignKey(m => m.PatientId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Receptionist>().HasData(
            new Receptionist() { Id=1, Emaill="reception11@gmail.com" });
    }

    public async Task<int> GetTotalPatientCountAsync()
    {
        return await Patients.CountAsync();
    }

    public async Task<int> GetTotalDoctorCountAsync()
    {
        return await Doctors.CountAsync();
    }
    public async Task<int> GetTotalNotesCountAsync()
    {
        return await Notes.CountAsync();
    }
}
