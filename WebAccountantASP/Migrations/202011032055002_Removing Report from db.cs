namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingReportfromdb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reports", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Reports", new[] { "Account_Id" });
            DropTable("dbo.Reports");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Reports", "Account_Id");
            AddForeignKey("dbo.Reports", "Account_Id", "dbo.Accounts", "Id");
        }
    }
}
