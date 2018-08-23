namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOperationSystemType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MediaPlayers", "OperationSystem", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MediaPlayers", "OperationSystem", c => c.String());
        }
    }
}
