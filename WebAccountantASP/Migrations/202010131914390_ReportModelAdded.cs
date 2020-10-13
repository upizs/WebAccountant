namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(),
                        DebitValue = c.Double(nullable: false),
                        CreditValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Reports", new[] { "AccountId" });
            DropTable("dbo.Reports");
        }
    }
}
