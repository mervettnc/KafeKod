namespace KafeKod.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ilk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Siparis Detaylar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunAd = c.String(nullable: false, maxLength: 50),
                        BirimFiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Adet = c.Int(nullable: false),
                        UrunId = c.Int(nullable: false),
                        SiparisId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Siparisler", t => t.SiparisId, cascadeDelete: true)
                .ForeignKey("dbo.Urunler", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.UrunId)
                .Index(t => t.SiparisId);
            
            CreateTable(
                "dbo.Siparisler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MasaNo = c.Int(nullable: false),
                        AcilisZamani = c.DateTime(),
                        KapanisZamani = c.DateTime(),
                        Durum = c.Int(nullable: false),
                        OdenenTutar = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Urunler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunAd = c.String(nullable: false, maxLength: 50),
                        BirimFiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Siparis Detaylar", "UrunId", "dbo.Urunler");
            DropForeignKey("dbo.Siparis Detaylar", "SiparisId", "dbo.Siparisler");
            DropIndex("dbo.Siparis Detaylar", new[] { "SiparisId" });
            DropIndex("dbo.Siparis Detaylar", new[] { "UrunId" });
            DropTable("dbo.Urunler");
            DropTable("dbo.Siparisler");
            DropTable("dbo.Siparis Detaylar");
        }
    }
}
