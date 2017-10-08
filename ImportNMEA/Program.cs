using System;
using System.IO;
using NmeaParser;
using NmeaParser.Nmea;
using NmeaParser.Nmea.Gps;
using CODE4TECH.Database;
using Microsoft.EntityFrameworkCore;

namespace ImportNMEA
{
    class Program
    {
        const string Path = @"C:\Users\bosak\Desktop\3333.txt";
        const string ConnectionString = "Data Source=BOSAKPC;Database=Code4Tech;Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true";
        const double KnotsToKmh = 1.8520;

        static void Main(string[] args)
        {
            var opt = new DbContextOptionsBuilder<Code4TechDbContext>().UseSqlServer(ConnectionString).Options;

            var id = int.Parse(Console.ReadLine());

            using (var reader = new StreamReader(Path))
            using (var ctx = new Code4TechDbContext(opt))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.StartsWith("$"))
                    {
                        try
                        {
                            var nmea = NmeaMessage.Parse(line);
                            if (nmea is Gprmc msg)
                            {
                                var reading = new Reading()
                                {
                                    DeviceId = id,
                                    Latitude = msg.Latitude,
                                    Longitude = msg.Longitude,
                                    Speed = msg.Speed * KnotsToKmh,
                                    Time = msg.FixTime
                                };

                                ctx.Readings.Add(reading);
                                ctx.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }
}
