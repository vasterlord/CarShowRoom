namespace CarShowRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarBrand",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false, maxLength: 50),
                        CountryProducing = c.String(nullable: false, maxLength: 50),
                        FoundationYear = c.DateTime(nullable: false),
                        Logo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Brand, unique: true)
                .Index(t => t.CountryProducing, unique: true);
            
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarBrandId = c.Int(nullable: false),
                        Model = c.String(nullable: false, maxLength: 50),
                        Load = c.Double(nullable: false),
                        Axel = c.String(nullable: false, maxLength: 50),
                        Transmission = c.String(nullable: false, maxLength: 50),
                        EngineCapacity = c.Double(nullable: false),
                        FuelPerHunderdKm = c.Double(nullable: false),
                        ProductionYear = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarBrand", t => t.CarBrandId, cascadeDelete: true)
                .Index(t => t.CarBrandId);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 50),
                        HomeAddress = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 50),
                        DateBuy = c.DateTime(nullable: false),
                        BuyPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Car", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Client", "CarId", "dbo.Car");
            DropForeignKey("dbo.Car", "CarBrandId", "dbo.CarBrand");
            DropIndex("dbo.Client", new[] { "CarId" });
            DropIndex("dbo.Car", new[] { "CarBrandId" });
            DropIndex("dbo.CarBrand", new[] { "CountryProducing" });
            DropIndex("dbo.CarBrand", new[] { "Brand" });
            DropTable("dbo.Client");
            DropTable("dbo.Car");
            DropTable("dbo.CarBrand");
        }
    }
}
