namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedaccounttype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AccountType", c => c.Int(nullable: false));
            
        }
        
        public override void Down()
        {
            
            DropColumn("dbo.Accounts", "AccountType");
        }
    }
}
