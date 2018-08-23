namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProtableField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MediaPlayers", "IsPortable", c => c.Boolean(nullable: false));
            DropColumn("dbo.MediaPlayers", "OperationSystem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MediaPlayers", "OperationSystem", c => c.Int(nullable: false));
            DropColumn("dbo.MediaPlayers", "IsPortable");
        }
    }
}
