namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLoginFromView : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Views", name: "LoginId", newName: "Login_Id");
            RenameIndex(table: "dbo.Views", name: "IX_LoginId", newName: "IX_Login_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Views", name: "IX_Login_Id", newName: "IX_LoginId");
            RenameColumn(table: "dbo.Views", name: "Login_Id", newName: "LoginId");
        }
    }
}
