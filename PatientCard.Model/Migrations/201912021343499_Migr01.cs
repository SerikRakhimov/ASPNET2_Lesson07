namespace PatientCard.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migr01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Diagnosis = c.String(),
                        DateVisit = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Iin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journals", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.Journals", new[] { "Patient_Id" });
            DropTable("dbo.Patients");
            DropTable("dbo.Journals");
        }
    }
}
