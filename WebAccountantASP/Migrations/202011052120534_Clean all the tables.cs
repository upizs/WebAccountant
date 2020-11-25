namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cleanallthetables : DbMigration
    {
        public override void Up()
        {
            
            Sql("DELETE FROM Transactions");
            Sql("DELETE FROM Accounts");
        }
        
        public override void Down()
        {
        }
    }
}
