using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

//GEMEINSAM MIT HUEMER PATRICK GECODED


var factory = new HotelContextFactory();
using var dbContext = factory.CreateDbContext();

if (args[0] == "add")
{
    await AddData();
}
else if (args[0] == "query")
{
    await QueryData();
}

async Task AddData()
{
    
}

async Task QueryData()
{
    
}

#region Model
enum Special        //The different hotel specials
{
    Spa,
    Sauna,
    DogFriendly,
    IndoorPool,
    OutdoorPool,
    BikeRental,
    ECarChargingStation,
    VegetarianCuisine,
    OrganicFood
}
class Hotel
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = "";
    [MaxLength(100)]
    public string Address { get; set; } = "";
    public List<HotelSpecial> Special { get; set; } = new();
    public List<RoomType> RoomTypes { get; set; } = new();
}
class HotelSpecial
{
    public int Id { get; set; }
    public Special Special { get; set; }
    public Hotel? Hotel { get; set; }
}
class RoomType
{
    public int Id { get; set; }
    [MaxLength(75)]
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Size { get; set; }
    public bool IsDisabilityAccessible { get; set; }
    public int NummberOfAvailableRooms { get; set; }
    public Hotel? Hotel { get; set; }
    public int HotelId { get; set; }
    public Price? Price { get; set; }
}
class Price
{
    public int Id { get; set; }
    public RoomType? RoomType { get; set; }
    public int RoomTypeId { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    [Column(TypeName = "decimal(8, 2)")]
    public decimal PriceEUR { get; set; }
}
#endregion

#region Context
class HotelContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<HotelSpecial> HotelSpecial { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Price> Prices { get; set; }
#pragma warning disable CS8618
    public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }
#pragma warning restore CS8618 

}
class HotelContextFactory : IDesignTimeDbContextFactory<HotelContext>
{
    public HotelContext CreateDbContext(string[]? args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<HotelContext>();
        optionsBuilder
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new HotelContext(optionsBuilder.Options);
    }
}
#endregion