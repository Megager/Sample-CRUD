namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSoundSystemType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SoundSystems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Loudspeakers", "SoundSystemId", c => c.Int(nullable: false));
            CreateIndex("dbo.Loudspeakers", "SoundSystemId");
            AddForeignKey("dbo.Loudspeakers", "SoundSystemId", "dbo.SoundSystems", "Id", cascadeDelete: true);
            DropColumn("dbo.Loudspeakers", "SoundSystem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Loudspeakers", "SoundSystem", c => c.String());
            DropForeignKey("dbo.Loudspeakers", "SoundSystemId", "dbo.SoundSystems");
            DropIndex("dbo.Loudspeakers", new[] { "SoundSystemId" });
            DropColumn("dbo.Loudspeakers", "SoundSystemId");
            DropTable("dbo.SoundSystems");
        }
    }
}
