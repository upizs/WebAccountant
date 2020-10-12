namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransactions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DebitId = c.Int(),
                        CreditId = c.Int(),
                        Value = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.CreditId)
                .ForeignKey("dbo.Accounts", t => t.DebitId)
                .Index(t => t.DebitId)
                .Index(t => t.CreditId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "DebitId", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "CreditId", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "CreditId" });
            DropIndex("dbo.Transactions", new[] { "DebitId" });
            DropTable("dbo.Transactions");
        }
    }
}
