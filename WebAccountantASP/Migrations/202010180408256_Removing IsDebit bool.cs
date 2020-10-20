namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingIsDebitbool : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE Accounts DROP COLUMN IsDebit");
        }
        
        public override void Down()
        {
        }
    }
}
