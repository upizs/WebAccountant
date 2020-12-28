namespace WebAccountantASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateKeepertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DateKeepers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        LastStarted = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DateKeepers");
        }
    }
}
