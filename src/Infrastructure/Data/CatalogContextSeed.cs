using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogContext catalogContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!catalogContext.CatalogBrands.Any())
                {
                    catalogContext.CatalogBrands.AddRange(
                        GetPreconfiguredCatalogBrands());

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.CatalogTypes.Any())
                {
                    catalogContext.CatalogTypes.AddRange(
                        GetPreconfiguredCatalogTypes());

                    await catalogContext.SaveChangesAsync();
                }

                if (!catalogContext.CatalogItems.Any())
                {
                    catalogContext.CatalogItems.AddRange(
                        GetPreconfiguredItems());

                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<CatalogContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(catalogContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand("Honda"),
                new CatalogBrand("Skoda"),
                new CatalogBrand("BMW"),
                new CatalogBrand("Nissan"),
                new CatalogBrand("Other")
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType("2019"),
                new CatalogType("2018"),
                new CatalogType("2017"),
                new CatalogType("2016")
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {

                new CatalogItem(1,1, "Honda Insight HIBRID 2019", "Honda Insight HIBRID 2019", 27000,  "http://catalogbaseurltobereplaced/images/products/1.png"),
                new CatalogItem(1,1, "Honda HR-V Comfort 2019", "Honda HR-V Comfort 2019", 24800, "http://catalogbaseurltobereplaced/images/products/2.png"),
                new CatalogItem(1,2, "Skoda Octavia Ambition 2019", "Skoda Octavia  Ambition 2019", 24800, "http://catalogbaseurltobereplaced/images/products/4.png"),
                new CatalogItem(2,2, "Skoda Karoq Ambition 2018", "Skoda Karoq Ambition 2018", 28000, "http://catalogbaseurltobereplaced/images/products/5.png"),
                new CatalogItem(1,3, "BMW X7xDrive base 2019", "BMW X7xDrive base 2019", 13300, "http://catalogbaseurltobereplaced/images/products/6.png"),
                new CatalogItem(2,4, "Nissan Rogue SV 2016", "Nissan Rogue SV 2016",  15600, "http://catalogbaseurltobereplaced/images/products/7.png"),
                new CatalogItem(3,4, "Nissan Rogue 2017", "Nissan Rogue 2017", 16400, "http://catalogbaseurltobereplaced/images/products/8.png"),
                new CatalogItem(2,4, "Nissan Rogue 2018", "Nissan Rogue 2018", 18200, "http://catalogbaseurltobereplaced/images/products/9.png"),
                new CatalogItem(3,1, "Honda Civic 2.0 2017", "Honda Civic 2.0 2017", 13400, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new CatalogItem(2,1, "Honda Accord EX-L 2018", "Honda Accord EX-L 2018", 27000, "http://catalogbaseurltobereplaced/images/products/11.png"),
                new CatalogItem(1,5, "Volkswagen Jetta SEL 2019", "Volkswagen Jetta SEL 2019", 23500, "http://catalogbaseurltobereplaced/images/products/12.png"),
                new CatalogItem(1,1, "Honda Insight HIBRID 2018", "Honda Insight HIBRID 2018", 26000,  "http://catalogbaseurltobereplaced/images/products/1.png"),
                new CatalogItem(2,2, "Skoda Karoq Ambition 2017", "Skoda Karoq Ambition 2017", 18000, "http://catalogbaseurltobereplaced/images/products/5.png")
            };
        }
    }
}
