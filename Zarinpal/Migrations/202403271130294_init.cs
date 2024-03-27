namespace Zarinpal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Amount = c.Long(nullable: false),
                        Authority = c.String(nullable: false),
                        RefrenceId = c.String(),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
