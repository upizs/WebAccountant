namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAccountTypeId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AccountTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "AccountTypeId");
        }
    }
}
