namespace Trading.Reporsitory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exchanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Instruments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        SymbolShort = c.String(),
                        Name = c.String(),
                        LotSize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        RowId = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        Name = c.String(),
                        Time = c.DateTime(nullable: false),
                        Open = c.Decimal(precision: 18, scale: 2),
                        High = c.Decimal(precision: 18, scale: 2),
                        Close = c.Decimal(precision: 18, scale: 2),
                        Low = c.Decimal(precision: 18, scale: 2),
                        LastClose = c.Decimal(precision: 18, scale: 2),
                        Bid = c.Decimal(precision: 18, scale: 2),
                        Ask = c.Decimal(precision: 18, scale: 2),
                        Volumn = c.Decimal(precision: 18, scale: 2),
                        Amount = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RowId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Prices");
            DropTable("dbo.Instruments");
            DropTable("dbo.Exchanges");
        }
    }
}
