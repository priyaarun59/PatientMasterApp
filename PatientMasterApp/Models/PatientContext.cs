
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace PatientMasterApp.Model
{
    public class PatientContext : DbContext
    {

        public PatientContext(DbContextOptions<PatientContext> options)
           : base(options)
        {

        }

       
            //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            //{
            //    if (!optionsBuilder.IsConfigured)
            //    {
            //        IConfigurationRoot configuration = new ConfigurationBuilder()
            //           .SetBasePath(Directory.GetCurrentDirectory())
            //           .AddJsonFile("appsettings.json")
            //           .Build();
            //        var connectionString = configuration.GetConnectionString("ConStr");
            //        //optionsBuilder.UseMySQL(connectionString);
            //    }
            //}

            

            public DbSet<PatientInfo> DbPatientInfo { get; set; }

            public DbSet<Vaccinestatus> DbVaccinestatus { get; set; }

            

        }

    }




