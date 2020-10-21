namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAccountIDfomepot : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reports", name: "AccountId", newName: "Account_Id");
            RenameIndex(table: "dbo.Reports", name: "IX_AccountId", newName: "IX_Account_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Reports", name: "IX_Account_Id", newName: "IX_AccountId");
            RenameColumn(table: "dbo.Reports", name: "Account_Id", newName: "AccountId");
        }
    }
}
