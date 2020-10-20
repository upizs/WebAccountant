namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class experimentingwithAccountTypeID : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "AccountTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "AccountTypeId", c => c.Int(nullable: false));
        }
    }
}
