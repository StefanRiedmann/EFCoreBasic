using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreBasic
{
    class Program
    {
        static void Main(string[] args)
        {
             using (var db = new GardenContext())
             {
                 if(args.Length > 0 && args[0] == "CreateSampleData")
                 {
                     CreateSampleData(db);
                 }
                 else
                 {
                     ReadThisYearsCrops(db);
                 }
             }
        }

        private static void ReadThisYearsCrops(GardenContext db)
        {
            Console.WriteLine("Getting all the crops for this year...");
            var assignments = db.CropAssignments.AsNoTracking()
                        .Where(ca => ca.Year == 2020)
                        .Include(ca => ca.Bed).ThenInclude(b => b.Garden)
                        .Include(ca => ca.Crop)
                        .OrderBy(ca => ca.Bed.Garden.Name).ThenBy(ca => ca.Crop.Name).ThenBy(ca => ca.Bed.Number)
                        .ToList();
            assignments.ForEach(a => Console.WriteLine($"Growing {a.Crop.Name} on bed {a.Bed.Number} in the garden {a.Bed.Garden.Name}"));
        }

        private static void CreateSampleData(GardenContext db)
        {
            var garden = (db.Gardens.Add(new Garden { Name = "My first garden" })).Entity;
            db.SaveChanges();

            var bed1 = (db.Beds.Add(new Bed{ GardenId = garden.GardenId, Number = 1 })).Entity;
            db.SaveChanges();
            var bed2 = (db.Beds.Add(new Bed{ GardenId = garden.GardenId, Number = 2 })).Entity;
            db.SaveChanges();
            var bed3 = (db.Beds.Add(new Bed{ GardenId = garden.GardenId, Number = 3 })).Entity;
            db.SaveChanges();

            var pumpkin = (db.Crops.Add(new Crop{Name = "Pumpkin"})).Entity;
            db.SaveChanges();
            var salad = (db.Crops.Add(new Crop{Name = "Salad"})).Entity;
            db.SaveChanges();
            var tomatoes = (db.Crops.Add(new Crop{Name = "Tomatoes"})).Entity;
            db.SaveChanges();
            var beans = (db.Crops.Add(new Crop{Name = "Beans"})).Entity;
            db.SaveChanges();

            var assignment1 = (db.CropAssignments.Add(
                new CropAssignment{
                    CropId = pumpkin.CropId,
                    BedId = bed1.BedId,
                    Year = 2020
                })).Entity;
            db.SaveChanges();
            var assignment2 = (db.CropAssignments.Add(
                new CropAssignment{
                    CropId = salad.CropId,
                    BedId = bed2.BedId,
                    Year = 2020
                })).Entity;
            db.SaveChanges();
            var assignment3 = (db.CropAssignments.Add(
                new CropAssignment{
                    CropId = beans.CropId,
                    BedId = bed3.BedId,
                    Year = 2020
                })).Entity;
            db.SaveChanges();
            var assignment4 = (db.CropAssignments.Add(
                new CropAssignment{
                    CropId = pumpkin.CropId,
                    BedId = bed3.BedId,
                    Year = 2021
                })).Entity;
            db.SaveChanges();
            var assignment5 = (db.CropAssignments.Add(
                new CropAssignment{
                    CropId = tomatoes.CropId,
                    BedId = bed2.BedId,
                    Year = 2021
                })).Entity;
            db.SaveChanges();
        }
    }
}
