namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedSomeTransactions : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Transactions ( DebitId, CreditId, Value, Date) VALUES ( 1, 3, 50.43, 20/12/2008)");

        }
        
        public override void Down()
        {
        }
    }
}
