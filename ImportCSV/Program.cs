using CODE4TECH.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.IO;

namespace ImportCSV
{
    class Program
    {
        const char Separator = ',';
        const string Path = @"C:\Users\bosak\Desktop\record_689.csv";
        const string ConnectionString = "Data Source=BOSAKPC;Database=Code4Tech;Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true";

        static void Main(string[] args)
        {
            IFormatProvider format = CultureInfo.InvariantCulture;
            var opt = new DbContextOptionsBuilder<Code4TechDbContext>().UseSqlServer(ConnectionString).Options;

            using (var ctx = new Code4TechDbContext(opt))
            using (var reader = new StreamReader(Path))
            {
                string line = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    Console.WriteLine(line);
                    string[] row = line.Split(Separator);

                    var reading = new Reading()
                    {
                        DeviceId = int.Parse(row[1]),
                        Longitude = double.Parse(row[2], format),
                        Latitude = double.Parse(row[3], format),
                        Time = DateTime.ParseExact(row[4], "yyyy-MM-dd HH:mm:ss", format),
                        Speed = double.Parse(row[6], format)
                    };

                    ctx.Readings.Add(reading);
                }

                ctx.SaveChanges();
            }
        }
    }
}
