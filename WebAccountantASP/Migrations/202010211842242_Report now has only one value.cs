namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reportnowhasonlyonevalue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "Value", c => c.Double(nullable: false));
            DropColumn("dbo.Reports", "DebitValue");
            DropColumn("dbo.Reports", "CreditValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "CreditValue", c => c.Double(nullable: false));
            AddColumn("dbo.Reports", "DebitValue", c => c.Double(nullable: false));
            DropColumn("dbo.Reports", "Value");
        }
    }
}
