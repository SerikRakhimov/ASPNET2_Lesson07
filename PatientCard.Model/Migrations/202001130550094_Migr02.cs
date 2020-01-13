namespace PatientCard.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migr02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Journals", "Doctor_Id", c => c.Int());
            CreateIndex("dbo.Journals", "Doctor_Id");
            AddForeignKey("dbo.Journals", "Doctor_Id", "dbo.Doctors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journals", "Doctor_Id", "dbo.Doctors");
            DropIndex("dbo.Journals", new[] { "Doctor_Id" });
            DropColumn("dbo.Journals", "Doctor_Id");
            DropTable("dbo.Doctors");
        }
    }
}
