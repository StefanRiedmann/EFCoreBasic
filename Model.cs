using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFCoreBasic
{
    public class GardenContext : DbContext
    {
        public DbSet<Garden> Gardens { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<CropAssignment> CropAssignments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433; Database=GardenDb1;User=sa; Password=1StrongPassword!");
    }

    public class Garden
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GardenId { get; set; }
        public string Name { get; set; }

        public List<Bed> Beds { get; } = new List<Bed>();
    }

    public class Bed
    {
        public int BedId { get; set; }
        public int Number { get; set; }

        public int GardenId { get; set; }
        public Garden Garden { get; set; }
        public List<CropAssignment> CropAssignments { get; set; }
    }

    public class Crop
    {
        public int CropId { get; set; }
        public string Name  { get; set; }
        public List<CropAssignment> CropAssignments { get; set; }
    }

    public class CropAssignment
    {
        public int CropAssignmentId { get; set; }
        public int CropId { get; set; }
        public Crop Crop { get; set; }
        public int BedId { get; set; }
        public Bed Bed { get; set; }
        public int Year { get; set; }
    }
}