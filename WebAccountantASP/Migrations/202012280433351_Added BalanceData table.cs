namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBalanceDatatable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BalanceReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BalanceReports", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.BalanceReports", new[] { "Account_Id" });
            DropTable("dbo.BalanceReports");
        }
    }
}
