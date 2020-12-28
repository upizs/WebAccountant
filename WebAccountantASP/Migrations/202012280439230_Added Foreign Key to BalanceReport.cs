namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeytoBalanceReport : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BalanceReports", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.BalanceReports", new[] { "Account_Id" });
            RenameColumn(table: "dbo.BalanceReports", name: "Account_Id", newName: "AccountId");
            AlterColumn("dbo.BalanceReports", "AccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.BalanceReports", "AccountId");
            AddForeignKey("dbo.BalanceReports", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BalanceReports", "AccountId", "dbo.Accounts");
            DropIndex("dbo.BalanceReports", new[] { "AccountId" });
            AlterColumn("dbo.BalanceReports", "AccountId", c => c.Int());
            RenameColumn(table: "dbo.BalanceReports", name: "AccountId", newName: "Account_Id");
            CreateIndex("dbo.BalanceReports", "Account_Id");
            AddForeignKey("dbo.BalanceReports", "Account_Id", "dbo.Accounts", "Id");
        }
    }
}
